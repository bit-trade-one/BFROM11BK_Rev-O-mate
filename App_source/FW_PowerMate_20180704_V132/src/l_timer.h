#ifndef L_TIMER_H
#define L_TIMER_H

//#include <timers.h>
#include "app.h"


//--------------------------------------------------
//Timer
// FREQ / Prescale / X
// 1 / X second(1ms‚È‚ç1/1000sec‚È‚Ì‚ÅX=1000)
//--------------------------------------------------
/***** Timer1 *****/
/* Timer1 0.1ms */
/* FREQ=50MHz, Prescale=1, X=1/10000sec */
/* 50MHz / 1 / 10000 = 5,000(0x1388) */
//#define TIMER1_PERIOD        0x1388        //Timer1‚ÌŽžŠÔ
/* Timer1 0.2ms */
/* FREQ=50MHz, Prescale=1, X=1/5000sec */
/* 50MHz / 1 / 5000 = 10,000(0x2710) */
//#define TIMER1_PERIOD        0x2710        //Timer1‚ÌŽžŠÔ
/* Timer1 1ms */
/* FREQ=50MHz, Prescale=1, X=1/1000sec */
/* 50MHz / 1 / 1000 = 50,000(0xC350) */
#define TIMER1_PERIOD        0xC350        //Timer1‚ÌŽžŠÔ
/***** Timer2 *****/
/* Timer2 0.05ms */
/* FREQ=50MHz, Prescale=1, X=1/20000sec */
/* 50MHz / 1 / 20000 = 2,500(0x09C4) */
//#define TIMER2_PERIOD        0x09C4        //Timer2‚ÌŽžŠÔ
/* Timer2 0.06ms */
/* FREQ=50MHz, Prescale=1, X=1/18000sec */
/* 50MHz / 1 / 18000 = 2,778(0x0ADA) */
//#define TIMER2_PERIOD        0x0ADA        //Timer2‚ÌŽžŠÔ
/* Timer2 0.07ms */
/* FREQ=50MHz, Prescale=1, X=1/16000sec */
/* 50MHz / 1 / 16000 = 3,125(0x0C35) */
#define TIMER2_PERIOD        0x0C35        //Timer2‚ÌŽžŠÔ
/* Timer2 0.1ms */
/* FREQ=50MHz, Prescale=1, X=1/10000sec */
/* 50MHz / 1 / 10000 = 5,000(0x1388) */
//#define TIMER2_PERIOD        0x1388        //Timer2‚ÌŽžŠÔ
/* Timer2 0.2ms */
/* FREQ=50MHz, Prescale=1, X=1/5000sec */
/* 50MHz / 1 / 5000 = 10,000(0x2710) */
//#define TIMER2_PERIOD        0x2710        //Timer2‚ÌŽžŠÔ
/* Timer2 1ms */
/* FREQ=50MHz, Prescale=1, X=1/1000sec */
/* 50MHz / 1 / 1000 = 50,000(0xC350) */
//#define TIMER2_PERIOD        0xC350        //Timer1‚ÌŽžŠÔ

#define SW_INPUT_TIME   1       // SW“ü—ÍŽüŠú[ms]

extern BYTE sw_input_counter;
extern BYTE sw_input_flag;


extern void timer1_init(void);
extern void timer2_init(void);
extern void timer4_init(void);
extern void Timer1_Task(void);
extern void Timer2_Task(void);



#endif //L_TIMER_H
