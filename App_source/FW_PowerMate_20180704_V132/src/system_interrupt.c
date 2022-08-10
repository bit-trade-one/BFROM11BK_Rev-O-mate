/*******************************************************************************
 System Interrupt Source File

  Company:
    Microchip Technology Inc. 
	
  File Name:
    system_interrupt.c

  Summary:
    Raw ISR definitions.

  Description:
    This file contains a definitions of the raw ISRs required to support the 
    interrupt sub-system.
*******************************************************************************/

// DOM-IGNORE-BEGIN
/*******************************************************************************
Copyright (c) 2011-2012 released Microchip Technology Inc.  All rights reserved.

Microchip licenses to you the right to use, modify, copy and distribute
Software only when embedded on a Microchip microcontroller or digital signal
controller that is integrated into your product or third party product
(pursuant to the sublicense terms in the accompanying license agreement).

You should refer to the license agreement accompanying this Software for
additional information regarding your rights and obligations.

SOFTWARE AND DOCUMENTATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY KIND,
EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION, ANY WARRANTY OF
MERCHANTABILITY, TITLE, NON-INFRINGEMENT AND FITNESS FOR A PARTICULAR PURPOSE.
IN NO EVENT SHALL MICROCHIP OR ITS LICENSORS BE LIABLE OR OBLIGATED UNDER
CONTRACT, NEGLIGENCE, STRICT LIABILITY, CONTRIBUTION, BREACH OF WARRANTY, OR
OTHER LEGAL EQUITABLE THEORY ANY DIRECT OR INDIRECT DAMAGES OR EXPENSES
INCLUDING BUT NOT LIMITED TO ANY INCIDENTAL, SPECIAL, INDIRECT, PUNITIVE OR
CONSEQUENTIAL DAMAGES, LOST PROFITS OR LOST DATA, COST OF PROCUREMENT OF
SUBSTITUTE GOODS, TECHNOLOGY, SERVICES, OR ANY CLAIMS BY THIRD PARTIES
(INCLUDING BUT NOT LIMITED TO ANY DEFENSE THEREOF), OR OTHER SIMILAR COSTS.
 *******************************************************************************/
// DOM-IGNORE-END

#include <xc.h>
#include <sys/attribs.h>
#include "app.h"
#include "l_timer.h"
#include "main_sub.h"

//void __ISR ( _TIMER_1_VECTOR, ipl3) _InterruptHandler_Timer1 ( void )
void __ISR ( _TIMER_1_VECTOR, IPL3SOFT) _InterruptHandler_Timer1 ( void )
{
    Timer1_Task();
}

//void __ISR ( _TIMER_2_VECTOR, ipl3) _InterruptHandler_Timer2 ( void )
void __ISR ( _TIMER_2_VECTOR, IPL3SOFT) _InterruptHandler_Timer2 ( void )
{
    Timer2_Task();
}

//void __ISR ( _EXTERNAL_2_VECTOR, ipl4) _InterruptHandler_IRIN1 ( void )
void __ISR ( _EXTERNAL_2_VECTOR, IPL4SOFT) _InterruptHandler_IRIN2 ( void )
{
    Int2_Task_ISR();
}
//void __ISR ( _EXTERNAL_4_VECTOR, ipl4) _InterruptHandler_IRIN2 ( void )
void __ISR ( _EXTERNAL_4_VECTOR, IPL4SOFT) _InterruptHandler_IRIN4 ( void )
{
    Int4_Task_ISR();
}
