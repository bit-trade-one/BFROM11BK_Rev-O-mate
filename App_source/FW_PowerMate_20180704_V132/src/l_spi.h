#ifndef L_SPI_H
#define L_SPI_H

//#include <spi.h>
#include "app.h"

//SPI module
//extern void spi1_init(void);
extern void l_spi1_init(void);
extern BYTE l_WriteSPI1(BYTE data);
extern BYTE l_ReadSPI1(void);


#define SCK1_Lo()	LATBbits.LATB15 = 0
#define SCK1_Hi()	LATBbits.LATB15 = 1
#define SDI1_VAL()	PORTAbits.RA4
#define SDO1_Lo()	LATCbits.LATC3 = 0
#define SDO1_Hi()	LATCbits.LATC3 = 1


#define SS1_ON()  LATAbits.LATA9 = 0	// SS1 RA9 Lo
#define SS1_OFF() LATAbits.LATA9 = 1	// SS1 RA9 Hi


#endif //L_SPI_H
