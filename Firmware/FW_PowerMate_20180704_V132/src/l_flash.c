#include "app.h"
#include "l_spi.h"
#include "l_flash.h"
#include "main_sub.h"


UN_BASE_HEAD p_base_head;               // [8byte] 基本設定情報読み込みバッファ
UN_BASE_INFO p_base_infos[MODE_NUM];    // [32byte]*3=96 基本設定モード情報読み込みバッファ
UN_FUNC_INFO p_func_infos[MODE_NUM][MODE_FUNCTION_NUM];  // [24byte]*4*3=288 機能設定情報読み込みバッファ
UN_ENCODER_SCRIPT_INFO p_encoder_script_infos[ENCODER_SCRIPT_NUM]; // [48byte]*3=144 エンコーダースクリプト設定情報読み込みバッファ
UN_SW_FUNC_INFO p_sw_func_infos[MODE_NUM][SW_NUM];         // [8byte]*3*11=264 SW機能設定情報読み込みバッファ
UN_SCRIPT_HEAD p_script_head;			// [8byte] スクリプトヘッダ情報読み込みバッファ
UN_SCRIPT_INFO p_script_info;			// [10byte] スクリプト情報読み込みバッファ



//=========================================================
// low level access
//=========================================================
void flash_send_adr(FLASH_ADR adr);
void flash_wrsr(char val);
void flash_write_byte(FLASH_ADR adr,BYTE data);
void flash_write_word(FLASH_ADR adr,WORD data);
BYTE flash_rdsr(void);
BYTE flash_busy(void);
void flash_wait_wip(void);
BYTE flash_read_byte(FLASH_ADR adr);
WORD flash_read_word(FLASH_ADR adr);
void flash_read_adr(FLASH_ADR adr);
void flash_wren(void);
void flash_wrdi(void);
void flash_sector_erase_low(FLASH_ADR adr);

void flash_continuous_start(void);
void flash_continuous_end(void);
void flash_continuous_write(FLASH_ADR adr);
void flash_continuous_read(FLASH_ADR adr);
BYTE flash_continuous_access_w(BYTE data);
BYTE flash_continuous_access_r();

//=========================================================
// hi level access
//=========================================================
void flash_unprotected(void);
void flash_protect(void);
void flash_sector_erase(FLASH_ADR adr);
void flash_n_write(FLASH_ADR adr, BYTE *buf, int len);
void flash_n_read(FLASH_ADR adr, BYTE *buf, int len);

void u_BaseHead_buffClr(UN_BASE_HEAD* p_base_head);
void u_BaseInfo_buffClr(UN_BASE_INFO* p_base_info);
void u_FuncInfo_buffClr(UN_FUNC_INFO* p_func_info);
void u_EncoderScriptInfo_buffClr(UN_ENCODER_SCRIPT_INFO* p_encoder_script_info);
void u_ScriptHead_buffClr(UN_SCRIPT_HEAD* p_script_head);
void u_ScriptInfo_buffClr(UN_SCRIPT_INFO* p_script_info);

int u_GetBaseHead(UN_BASE_HEAD* p_base_head);
int u_GetBaseInfo(BYTE p_mode_no, UN_BASE_INFO* p_base_info);
int u_GetFuncInfo(BYTE p_mode_no, BYTE p_func_no, UN_FUNC_INFO* p_func_info);
int u_GetEncoderScriptInfo(BYTE p_no, UN_ENCODER_SCRIPT_INFO* p_encoder_script_info);
int u_GetScriptHead(UN_SCRIPT_HEAD* p_script_head);
int u_GetScriptInfo(BYTE p_script_no, UN_SCRIPT_INFO* p_script_info);


//********************************************************
// flash_send_adr
//********************************************************
void flash_send_adr(FLASH_ADR adr)
{
#if FLASH_ADR_BITS == 24
    l_WriteSPI1(adr >> 16);
#endif
    l_WriteSPI1(adr >> 8);
    l_WriteSPI1(adr);
}

//========================================================
// flash_wrsr 少し時間がかかる　RDSRで書き込み完了をチェック
//========================================================
void flash_wrsr(char val)
{
    FLASH_CS_ON();
    l_WriteSPI1(FLASH_INST_WRSR);
    l_WriteSPI1(val);
    FLASH_CS_OFF();
}

//========================================================
// flash_write_byte
//========================================================
void flash_write_byte(FLASH_ADR adr,BYTE data)
{
    FLASH_CS_ON();

    l_WriteSPI1(FLASH_INST_WRITE);

    flash_send_adr(adr);

    l_WriteSPI1(data);

    FLASH_CS_OFF();
}

//========================================================
// flash_write_word
//========================================================
void flash_write_word(FLASH_ADR adr,WORD data)
{
    flash_write_byte(adr  , (BYTE)((data >> 8) & 0xFF));
    flash_write_byte(adr+1, (BYTE)(data & 0xFF));
}

//========================================================
// flash_rdsr
//========================================================
BYTE flash_rdsr(void)
{
    volatile BYTE result;

    FLASH_CS_ON();
    l_WriteSPI1(FLASH_INST_RDSR);
    result = l_ReadSPI1();
    FLASH_CS_OFF();

    return result;
}

//========================================================
// flash_busy
//========================================================
BYTE flash_busy(void)
{
    volatile BYTE result;

    FLASH_CS_ON();
    l_WriteSPI1(FLASH_INST_RDSR);
    result = l_ReadSPI1();;
    FLASH_CS_OFF();

    result &= 0x01;

    return result;
}

//========================================================
// flash_wait_wip
//========================================================
void flash_wait_wip(void)
{
    BYTE result;

    FLASH_CS_ON();
    l_WriteSPI1(FLASH_INST_RDSR);
    do{
        result = l_ReadSPI1();
    }while(result & 0x01);

    FLASH_CS_OFF();
}

//========================================================
// flash_read_byte
//========================================================
BYTE flash_read_byte(FLASH_ADR adr)
{
    BYTE data;

    FLASH_CS_OFF();
    FLASH_CS_ON();

    l_WriteSPI1(FLASH_INST_READ);
    flash_send_adr(adr);
    data = l_ReadSPI1();

    FLASH_CS_OFF();

    return data;
}

//========================================================
// flash_read_word
//========================================================
WORD flash_read_word(FLASH_ADR adr)
{
    WORD val;
    val  = ((WORD)flash_read_byte(adr)) << 8;
    val |= flash_read_byte(adr+1);
    return val;
}

//========================================================
// flash_read_adr
//========================================================
void flash_read_adr(FLASH_ADR adr)
{
    FLASH_CS_OFF();
    FLASH_CS_ON();

    l_WriteSPI1(FLASH_INST_READ);
    flash_send_adr(adr);

//  FLASH_CS_OFF();
}

//========================================================
// flash_wren
//========================================================
void flash_wren(void)
{
    FLASH_CS_ON();
    l_WriteSPI1(FLASH_INST_WREN);
    FLASH_CS_OFF();
}

//========================================================
// flash_wrdi
//========================================================
void flash_wrdi(void)
{
    FLASH_CS_ON();
    l_WriteSPI1(FLASH_INST_WRDI);
    FLASH_CS_OFF();
}

//========================================================
// flash_sector_erase_low
//========================================================
void flash_sector_erase_low(FLASH_ADR adr)
{
    FLASH_CS_ON();

    l_WriteSPI1(FLASH_INST_SECTOR_ERASE);
    flash_send_adr(adr);

    FLASH_CS_OFF();
}

//========================================================
// flash_continuous_start
//========================================================
void flash_continuous_start(void)
{
    FLASH_CS_ON();
}

//========================================================
// flash_continuous_end
//========================================================
void flash_continuous_end(void)
{
    FLASH_CS_OFF();
}

//========================================================
// flash_continuous_write
//========================================================
void flash_continuous_write(FLASH_ADR adr)
{
    l_WriteSPI1(FLASH_INST_WRITE);
    flash_send_adr(adr);
}

//========================================================
// flash_continuous_read
//========================================================
void flash_continuous_read(FLASH_ADR adr)
{
    l_WriteSPI1(FLASH_INST_READ);
    flash_send_adr(adr);
}

//========================================================
// flash_continuous_access_w (Write)
//========================================================
BYTE flash_continuous_access_w(BYTE data)
{
    return l_WriteSPI1(data);
}

//========================================================
// flash_continuous_access_r (Read)
//========================================================
BYTE flash_continuous_access_r()
{
    return l_ReadSPI1();
}

//::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// flash_unprotected
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::
void flash_unprotected(void)
{
    flash_wren();
    flash_wrsr(0x00);
    flash_wait_wip();
}

//::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// flash_protect
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::
void flash_protect(void)
{
    flash_wren();
    flash_wrsr(0x1C);
    flash_wait_wip();
}

//::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// flash_sector_erase
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::
void flash_sector_erase(FLASH_ADR adr)
{
#ifdef USE_SECTOR_ERASE
    flash_wren();
    flash_sector_erase_low(adr);
    flash_wait_wip();
#endif
}

//::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// flash_n_write
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::
void flash_n_write(FLASH_ADR adr, BYTE *buf, int len)
{
    int i;
    flash_wren();
    flash_continuous_start();
    flash_continuous_write(adr);
    for(i=0;i<len;i++){
        flash_continuous_access_w(*buf++);
    }
    flash_continuous_end();
    flash_wait_wip();
}

//::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// flash_n_read
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::
void flash_n_read(FLASH_ADR adr, BYTE *buf, int len)
{
    int i;
    flash_continuous_start();
    flash_continuous_read(adr);
    for(i=0;i<len;i++){
        *buf++ = flash_continuous_access_r();
    }
    flash_continuous_end();
}






void u_BaseHead_buffClr(UN_BASE_HEAD* p_base_head)
{
	int fi;
	int bufsize = sizeof(UN_BASE_HEAD);
//	int bufsize = sizeof(p_base_head->byte);
	for( fi = 0; fi < bufsize; fi++ )
	{
		p_base_head->byte[fi] = 0x00;
	}
}
void u_BaseInfo_buffClr(UN_BASE_INFO* p_base_info)
{
	int fi;
	int bufsize = sizeof(UN_BASE_INFO);
//	int bufsize = sizeof(p_base_info->byte);
	for( fi = 0; fi < bufsize; fi++ )
	{
		p_base_info->byte[fi] = 0x00;
	}
}
void u_FuncInfo_buffClr(UN_FUNC_INFO* p_func_info)
{
	int fi;
	int bufsize = sizeof(UN_FUNC_INFO);
//	int bufsize = sizeof(p_func_info->byte);
	for( fi = 0; fi < bufsize; fi++ )
	{
		p_func_info->byte[fi] = 0x00;
	}
}
void u_EncoderScriptInfo_buffClr(UN_ENCODER_SCRIPT_INFO* p_encoder_script_info)
{
	int fi;
	int bufsize = sizeof(UN_ENCODER_SCRIPT_INFO);
//	int bufsize = sizeof(p_encoder_script_info->byte);
	for( fi = 0; fi < bufsize; fi++ )
	{
		p_encoder_script_info->byte[fi] = 0x00;
	}
}
void u_ScriptHead_buffClr(UN_SCRIPT_HEAD* p_script_head)
{
	int fi;
	int bufsize = sizeof(UN_SCRIPT_HEAD);
//	int bufsize = sizeof(p_script_head->byte);
	for( fi = 0; fi < bufsize; fi++ )
	{
		p_script_head->byte[fi] = 0x00;
	}
}
void u_ScriptInfo_buffClr(UN_SCRIPT_INFO* p_script_info)
{
	int fi;
	int bufsize = sizeof(UN_SCRIPT_INFO);
//	int bufsize = sizeof(p_script_info->byte);
	for( fi = 0; fi < bufsize; fi++ )
	{
		p_script_info->byte[fi] = 0x00;
	}
}	

// 基本設定情報を、FlashMemoryから読み込む
int u_GetBaseHead(UN_BASE_HEAD* p_base_head)
{
	flash_n_read(FLASH_BASE_SETTING_ADR, &p_base_head->byte[0], sizeof(p_base_head->byte));
	return 0;
}
int u_GetBaseInfo(BYTE p_mode_no, UN_BASE_INFO* p_base_info)
{
	FLASH_ADR adr;
	if(MODE_NUM <= p_mode_no )
	{
		return -1;
	}
    
	adr = (FLASH_ADR)(FLASH_BASE_SETTING_MODE_ADR + (FLASH_BASE_SETTING_MODE_SIZE * p_mode_no));
	flash_n_read(adr, &p_base_info->byte[0], sizeof(p_base_info->byte));
	return 0;
}
// 機能設定情報を、FlashMemoryから読み込む
int u_GetFuncInfo(BYTE p_mode_no, BYTE p_func_no, UN_FUNC_INFO* p_func_info)
{
	FLASH_ADR adr;
	if(MODE_NUM <= p_mode_no )
	{
		return -1;
	}
	if(MODE_FUNCTION_NUM <= p_func_no )
	{
		return -2;
	}
		
	adr = (FLASH_ADR)(FLASH_FUNC_INFO_TOP_ADR + (FLASH_FUNC_INFO_SIZE * ((MODE_FUNCTION_NUM * p_mode_no) + p_func_no)));
	flash_n_read(adr, &p_func_info->byte[0], sizeof(p_func_info->byte));
	return 0;
}
// エンコーダースクリプト設定情報を、FlashMemoryから読み込む
int u_GetEncoderScriptInfo(BYTE p_no, UN_ENCODER_SCRIPT_INFO* p_encoder_script_info)
{
	FLASH_ADR adr;
	if(ENCODER_SCRIPT_NUM <= p_no )
	{
		return -1;
	}
    
	adr = (FLASH_ADR)(FLASH_ENCODER_SCRIPT_ADR + (FLASH_ENCODER_SCRIPT_SIZE * p_no));
	flash_n_read(adr, &p_encoder_script_info->byte[0], sizeof(p_encoder_script_info->byte));
	return 0;
}
// SW機能設定情報を、FlashMemoryから読み込む
int u_GetSWFuncInfo(BYTE p_mode_no, BYTE p_sw_no, UN_SW_FUNC_INFO* p_sw_func_info)
{
	FLASH_ADR adr;
	if(MODE_NUM <= p_mode_no )
	{
		return -1;
	}
	if(SW_NUM <= p_sw_no )
	{
		return -2;
	}
		
	adr = (FLASH_ADR)(FLASH_SW_FUNC_INFO_TOP_ADR + (FLASH_SW_FUNC_INFO_SIZE * ((SW_NUM * p_mode_no) + p_sw_no)));
	flash_n_read(adr, &p_sw_func_info->byte[0], sizeof(p_sw_func_info->byte));
	return 0;
}
// スクリプトヘッダ情報を、FlashMemoryから読み込む
int u_GetScriptHead(UN_SCRIPT_HEAD* p_script_head)
{
	flash_n_read(FLASH_SCRIPT_HEAD_ADR, &p_script_head->byte[0], sizeof(p_script_head->byte));
	return 0;
}
// スクリプト情報を、FlashMemoryから読み込む
int u_GetScriptInfo(BYTE p_script_no, UN_SCRIPT_INFO* p_script_info)
{
	FLASH_ADR adr;
	if(p_script_no < 1 || SCRIPT_MAX_NUM < p_script_no )
	{
		return -1;
	}
		
	adr = (FLASH_ADR)(FLASH_SCRIPT_INFO_ADR + (FLASH_SCRIPT_INFO_SIZE * (p_script_no - 1)));
	flash_n_read(adr, &p_script_info->byte[0], sizeof(p_script_info->byte));
	return 0;
}

