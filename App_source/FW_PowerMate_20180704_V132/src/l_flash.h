#ifndef L_FLASH_H
#define L_FLASH_H

#include "app.h"

//#define FLASH_PART_AT25128
//#define FLASH_PART_25AA1024
#define FLASH_PART_M25P16
//#define FLASH_PART_M25P32
//#define FLASH_PART_AT24C1024B


#define FLASH_INST_WREN             0x06		// Write Enable
#define FLASH_INST_WRDI             0x04		// Write Disable
#define FLASH_INST_RDSR             0x05		// Read Status Register
#define FLASH_INST_WRSR             0x01		// Write Status Register
#define FLASH_INST_READ             0x03		// Read Data Bytes
#define FLASH_INST_WRITE            0x02		// Page Program
#define FLASH_INST_PAGE_PROGRAM     0x02		// Page Program
#define FLASH_INST_SECTOR_ERASE     0xD8		// Sector Erase
#define FLASH_INST_BULK_ERASE       0xC7		// Bulk Erase

#ifdef FLASH_PART_AT25128
#define FLASH_ADR_BITS              16
#define FLASH_SIZE                  (128*1024/8)
#define FLASH_SECTOR_SIZE           32768	//(byte) Sector Erase dummy
#define FLASH_PAGE_SIZE             256		//(byte) Page Program
#endif

#ifdef FLASH_PART_25AA1024
#define FLASH_ADR_BITS              24
#define FLASH_SIZE                  0x20000	//1MBits
#define FLASH_SECTOR_SIZE           32768	//(byte) Sector Erase
#define FLASH_PAGE_SIZE             256		//(byte) Page Program
#endif

#ifdef FLASH_PART_M25P16
#define FLASH_ADR_BITS              24
#define FLASH_SIZE                  0x200000    //16MBits
#define FLASH_SECTOR_SIZE           65536	//(byte) Sector Erase
#define FLASH_PAGE_SIZE             256		//(byte) Page Program
#define USE_SECTOR_ERASE			//Sector erase命令使用
#endif

#ifdef FLASH_PART_M25P32
#define FLASH_ADR_BITS              24
#define FLASH_SIZE                  0x400000        //32MBits
#define FLASH_SECTOR_SIZE           65536           //(byte) Sector Erase
#define FLASH_PAGE_SIZE             256		//(byte) Page Program
#define USE_SECTOR_ERASE			//Sector erase命令使用
#endif

#ifdef FLASH_PART_AT24C1024B
#define FLASH_ADR_BITS              16
#define FLASH_SIZE                  (1024*1024/8)
#define FLASH_SECTOR_SIZE           32768	//(byte) Sector Erase dummy
#define FLASH_PAGE_SIZE             256		//(byte) Page Program
#endif

#if FLASH_ADR_BITS == 16
typedef WORD FLASH_ADR;
#endif

#if FLASH_ADR_BITS == 24
typedef DWORD FLASH_ADR;
#endif

// Flash Memory
// Address
#define FLASH_BASE_SETTING_ADR          0x000000
#define FLASH_BASE_SETTING_MODE_ADR     0x000010
#define FLASH_BASE_SETTING_MODE_SIZE    0x0020
#define FLASH_FUNC_HEAD_ADR             0x000100
#define FLASH_FUNC_INFO_TOP_ADR         0x000100
#define FLASH_FUNC_INFO_SIZE            0x0018
#define FLASH_ENCODER_SCRIPT_ADR        0x000A00
#define FLASH_ENCODER_SCRIPT_SIZE       0x0030
#define FLASH_SW_FUNC_INFO_TOP_ADR      0x000B00
#define FLASH_SW_FUNC_INFO_SIZE         0x0008
#define FLASH_SCRIPT_HEAD_ADR           0x010000
#define FLASH_SCRIPT_INFO_ADR           0x010010
#define FLASH_SCRIPT_INFO_SIZE          0x0110
#define FLASH_SCRIPT_DATA_ADR           0x020000


#define FLASH_CS_ON()  LATAbits.LATA9 = 0	// SS RA9 Lo
#define FLASH_CS_OFF() LATAbits.LATA9 = 1	// SS RA9 Hi





// 外部宣言 変数　
extern UN_BASE_HEAD p_base_head;                // 基本設定情報読み込みバッファ
extern UN_BASE_INFO p_base_infos[MODE_NUM];     // 基本設定モード情報読み込みバッファ
extern UN_FUNC_INFO p_func_infos[MODE_NUM][MODE_FUNCTION_NUM];          // 機能設定情報読み込みバッファ
extern UN_ENCODER_SCRIPT_INFO p_encoder_script_infos[ENCODER_SCRIPT_NUM];
extern UN_SW_FUNC_INFO p_sw_func_infos[MODE_NUM][SW_NUM];         // SW機能設定情報読み込みバッファ
extern UN_SCRIPT_HEAD p_script_head;			// スクリプトヘッダ情報読み込みバッファ
extern UN_SCRIPT_INFO p_script_info;			// スクリプト情報読み込みバッファ



//=========================================================
// low level access
//=========================================================
extern void flash_write_byte(FLASH_ADR adr,BYTE data);
extern void flash_write_word(FLASH_ADR adr,WORD data);
extern BYTE flash_rdsr(void);
extern void flash_wrsr(char val);
extern BYTE flash_busy(void);
extern void flash_wait_wip(void);
extern BYTE flash_read_byte(FLASH_ADR adr);
extern WORD flash_read_word(FLASH_ADR adr);
extern void flash_read_adr(FLASH_ADR adr);
extern void flash_wren(void);
extern void flash_wrdi(void);
extern void flash_sector_erase_low(FLASH_ADR adr);

extern void flash_continuous_start(void);
extern void flash_continuous_end(void);
extern void flash_continuous_write(FLASH_ADR adr);
extern void flash_continuous_read(FLASH_ADR adr);
extern BYTE flash_continuous_access_w(BYTE data);
extern BYTE flash_continuous_access_r();

//=========================================================
// hi level access
//=========================================================
extern void flash_unprotected(void);
extern void flash_protect(void);
extern void flash_sector_erase(FLASH_ADR adr);
extern void flash_n_write(FLASH_ADR adr, BYTE *buf, int len);
extern void flash_n_read(FLASH_ADR adr, BYTE *buf, int len);

extern void u_BaseHead_buffClr(UN_BASE_HEAD* p_base_head);
extern void u_BaseInfo_buffClr(UN_BASE_INFO* p_base_info);
extern void u_FuncInfo_buffClr(UN_FUNC_INFO* p_func_info);
extern void u_EncoderScriptInfo_buffClr(UN_ENCODER_SCRIPT_INFO* p_encoder_script_info);
extern void u_ScriptHead_buffClr(UN_SCRIPT_HEAD* p_script_head);
extern void u_ScriptInfo_buffClr(UN_SCRIPT_INFO* p_script_info);

extern int u_GetBaseHead(UN_BASE_HEAD* p_base_head);
extern int u_GetBaseInfo(BYTE p_mode_no, UN_BASE_INFO* p_base_info);
extern int u_GetFuncInfo(BYTE p_mode_no, BYTE p_func_no, UN_FUNC_INFO* p_func_info);
extern int u_GetEncoderScriptInfo(BYTE p_no, UN_ENCODER_SCRIPT_INFO* p_encoder_script_info);
extern int u_GetScriptHead(UN_SCRIPT_HEAD* p_script_head);
extern int u_GetScriptInfo(BYTE p_script_no, UN_SCRIPT_INFO* p_script_info);




#endif//L_FLASH_H
