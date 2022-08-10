//#include <spi.h>
#include "app.h"
#include "l_spi.h"
#include "main_sub.h"

//SPI module
void l_spi1_init(void);


//========================================================
// SPI1 マスタ
//========================================================
/*
	50MHz / 5 = 10MHz
*/
void l_spi1_init(void)
{
    SPI1CONbits.ON = 0;

    TRISBbits.TRISB15 = 0;    //RB15 = SCK1 SPI1クロック出力
    TRISAbits.TRISA4 = 1;     //RA4 = SDI1 SPI1データ入力
    TRISCbits.TRISC3 = 0;     //RC3 = SDO1 SPI1データ出力
    TRISAbits.TRISA9 = 0;     //RA9 = CS FLASH 出力

    SS1_OFF();
    SCK1_Lo();
}

// 想定クロック　50MHz/5 = 10MHz
BYTE l_WriteSPI1(BYTE data)
{
    BYTE rData = 0;
    BYTE fi = 0;
    BYTE fj = 1;
    BYTE fk = 1;
    BYTE mask = 0x80;

    SCK1_Lo();
    fj += fj;   // 時間稼ぎ
    fk += fk;   // 時間稼ぎ
    for(fi=0; fi<8; fi++)
    {
        fj += fj;   // 時間稼ぎ
        fk += fk;   // 時間稼ぎ
        if((data & mask) != 0)
        {
            SDO1_Hi();
        }
        else
        {
            SDO1_Lo();
        }
        fj += fj;   // 時間稼ぎ
        fk += fk;   // 時間稼ぎ
        SCK1_Hi();
        mask = mask >> 1;
        fj += fj;   // 時間稼ぎ
        fk += fk;   // 時間稼ぎ
        fj += fj;   // 時間稼ぎ
        fk += fk;   // 時間稼ぎ
        SCK1_Lo();
    }
    rData = data;
    return rData;
}
// 想定クロック　50MHz/5 = 10MHz
BYTE l_ReadSPI1(void)
{
    BYTE rData = 0;
    BYTE fi = 0;
    BYTE fj = 1;
    BYTE fk = 1;
    
    SCK1_Lo();
    fj += fj;   // 時間稼ぎ
    fk += fk;   // 時間稼ぎ
    for(fi=0; fi < 8; fi++)
    {
        fj += fj;   // 時間稼ぎ
        fk += fk;   // 時間稼ぎ
        SCK1_Hi();
        fj += fj;   // 時間稼ぎ
        fk += fk;   // 時間稼ぎ
        rData = rData << 1;
        if(SDI1_VAL() == 1)
        {
            rData |= 0x01;
        }
        fj += fj;   // 時間稼ぎ
        fk += fk;   // 時間稼ぎ
        SCK1_Lo();
        fj += fj;   // 時間稼ぎ
        fk += fk;   // 時間稼ぎ
    }
    
    return rData;
}




