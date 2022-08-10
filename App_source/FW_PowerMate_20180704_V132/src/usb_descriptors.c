/********************************************************************
 FileName:     	usb_descriptors.c
 Dependencies:	See INCLUDES section
 Processor:		PIC18 or PIC24 USB Microcontrollers
 Hardware:		The code is natively intended to be used on the following
 				hardware platforms: PICDEM?FS USB Demo Board, 
 				PIC18F87J50 FS USB Plug-In Module, or
 				Explorer 16 + PIC24 USB PIM.  The firmware may be
 				modified for use on other USB platforms by editing the
 				HardwareProfile.h file.
 Complier:  	Microchip C18 (for PIC18) or C30 (for PIC24)
 Company:		Microchip Technology, Inc.

 Software License Agreement:

 The software supplied herewith by Microchip Technology Incorporated
 (the “Company? for its PIC® Microcontroller is intended and
 supplied to you, the Company’s customer, for use solely and
 exclusively on Microchip PIC Microcontroller products. The
 software is owned by the Company and/or its supplier, and is
 protected under applicable copyright laws. All rights are reserved.
 Any use in violation of the foregoing restrictions may subject the
 user to criminal sanctions under applicable laws, as well as to
 civil liability for the breach of the terms and conditions of this
 license.

 THIS SOFTWARE IS PROVIDED IN AN “AS IS?CONDITION. NO WARRANTIES,
 WHETHER EXPRESS, IMPLIED OR STATUTORY, INCLUDING, BUT NOT LIMITED
 TO, IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
 PARTICULAR PURPOSE APPLY TO THIS SOFTWARE. THE COMPANY SHALL NOT,
 IN ANY CIRCUMSTANCES, BE LIABLE FOR SPECIAL, INCIDENTAL OR
 CONSEQUENTIAL DAMAGES, FOR ANY REASON WHATSOEVER.

*********************************************************************
-usb_descriptors.c-
-------------------------------------------------------------------
Filling in the descriptor values in the usb_descriptors.c file:
-------------------------------------------------------------------

[Device Descriptors]
The device descriptor is defined as a USB_DEVICE_DESCRIPTOR type.  
This type is defined in usb_ch9.h  Each entry into this structure
needs to be the correct length for the data type of the entry.

[Configuration Descriptors]
The configuration descriptor was changed in v2.x from a structure
to a BYTE array.  Given that the configuration is now a byte array
each byte of multi-byte fields must be listed individually.  This
means that for fields like the total size of the configuration where
the field is a 16-bit value "64,0," is the correct entry for a
configuration that is only 64 bytes long and not "64," which is one
too few bytes.

The configuration attribute must always have the _DEFAULT
definition at the minimum. Additional options can be ORed
to the _DEFAULT attribute. Available options are _SELF and _RWU.
These definitions are defined in the usb_device.h file. The
_SELF tells the USB host that this device is self-powered. The
_RWU tells the USB host that this device supports Remote Wakeup.

[Endpoint Descriptors]
Like the configuration descriptor, the endpoint descriptors were 
changed in v2.x of the stack from a structure to a BYTE array.  As
endpoint descriptors also has a field that are multi-byte entities,
please be sure to specify both bytes of the field.  For example, for
the endpoint size an endpoint that is 64 bytes needs to have the size
defined as "64,0," instead of "64,"

Take the following example:
    // Endpoint Descriptor //
    0x07,                       //the size of this descriptor //
    USB_DESCRIPTOR_ENDPOINT,    //Endpoint Descriptor
    _EP02_IN,                   //EndpointAddress
    _INT,                       //Attributes
    0x08,0x00,                  //size (note: 2 bytes)
    0x02,                       //Interval

The first two parameters are self-explanatory. They specify the
length of this endpoint descriptor (7) and the descriptor type.
The next parameter identifies the endpoint, the definitions are
defined in usb_device.h and has the following naming
convention:
_EP<##>_<dir>
where ## is the endpoint number and dir is the direction of
transfer. The dir has the value of either 'OUT' or 'IN'.
The next parameter identifies the type of the endpoint. Available
options are _BULK, _INT, _ISO, and _CTRL. The _CTRL is not
typically used because the default control transfer endpoint is
not defined in the USB descriptors. When _ISO option is used,
addition options can be ORed to _ISO. Example:
_ISO|_AD|_FE
This describes the endpoint as an isochronous pipe with adaptive
and feedback attributes. See usb_device.h and the USB
specification for details. The next parameter defines the size of
the endpoint. The last parameter in the polling interval.

-------------------------------------------------------------------
Adding a USB String
-------------------------------------------------------------------
A string descriptor array should have the following format:

rom struct{byte bLength;byte bDscType;word string[size];}sdxxx={
sizeof(sdxxx),DSC_STR,<text>};

The above structure provides a means for the C compiler to
calculate the length of string descriptor sdxxx, where xxx is the
index number. The first two bytes of the descriptor are descriptor
length and type. The rest <text> are string texts which must be
in the unicode format. The unicode format is achieved by declaring
each character as a word type. The whole text string is declared
as a word array with the number of characters equals to <size>.
<size> has to be manually counted and entered into the array
declaration. Let's study this through an example:
if the string is "USB" , then the string descriptor should be:
(Using index 02)
rom struct{byte bLength;byte bDscType;word string[3];}sd002={
sizeof(sd002),DSC_STR,'U','S','B'};

A USB project may have multiple strings and the firmware supports
the management of multiple strings through a look-up table.
The look-up table is defined as:
rom const unsigned char *rom USB_SD_Ptr[]={&sd000,&sd001,&sd002};

The above declaration has 3 strings, sd000, sd001, and sd002.
Strings can be removed or added. sd000 is a specialized string
descriptor. It defines the language code, usually this is
US English (0x0409). The index of the string must match the index
position of the USB_SD_Ptr array, &sd000 must be in position
USB_SD_Ptr[0], &sd001 must be in position USB_SD_Ptr[1] and so on.
The look-up table USB_SD_Ptr is used by the get string handler
function.

-------------------------------------------------------------------

The look-up table scheme also applies to the configuration
descriptor. A USB device may have multiple configuration
descriptors, i.e. CFG01, CFG02, etc. To add a configuration
descriptor, user must implement a structure similar to CFG01.
The next step is to add the configuration descriptor name, i.e.
cfg01, cfg02,.., to the look-up table USB_CD_Ptr. USB_CD_Ptr[0]
is a dummy place holder since configuration 0 is the un-configured
state according to the definition in the USB specification.

********************************************************************/
 
/*********************************************************************
 * Descriptor specific type definitions are defined in:
 * usb_device.h
 *
 * Configuration options are defined in:
 * usb_config.h
 ********************************************************************/

#ifndef __USB_DESCRIPTORS_C
#define __USB_DESCRIPTORS_C

/** INCLUDES *******************************************************/
#include "./USB/usb.h"
#include "./USB/usb_function_hid.h"

/** CONSTANTS ******************************************************/


/* Device Descriptor */
ROM USB_DEVICE_DESCRIPTOR device_dsc=
{
    0x12,    // Size of this descriptor in bytes
    USB_DESCRIPTOR_DEVICE,                // DEVICE descriptor type
    0x0200,                 // USB Spec Release Number in BCD format
    0x00,                   // Class Code
    0x00,                   // Subclass code
    0x00,                   // Protocol code
    USB_EP0_BUFF_SIZE,          // Max packet size for EP0, see usb_config.h
    MY_VID,                 // Vendor ID
    MY_PID,                 // Product ID: Mouse in a circle fw demo
    0x0001,                 // Device release number in BCD format
    0x01,                   // Manufacturer string index
    0x02,                   // Product string index
    0x00,                   // Device serial number string index
    0x01                    // Number of possible configurations
};

/* Configuration 1 Descriptor */
ROM BYTE configDescriptor1[]={
    /* Configuration Descriptor */
    0x09,//sizeof(USB_CFG_DSC),    // Size of this descriptor in bytes
    USB_DESCRIPTOR_CONFIGURATION,                // CONFIGURATION descriptor type
    DESC_CONFIG_WORD(0x0082),   // Total length of data for this cfg
    4,                      // Number of interfaces in this cfg
    1,                      // Index value of this configuration
    0,                      // Configuration string index
    _DEFAULT | _SELF | _RWU,               // Attributes, see usb_device.h
    50,                     // Max power consumption (2X mA)

    /* Keyboard */
    /* Interface Descriptor */
    0x09,//sizeof(USB_INTF_DSC),   // Size of this descriptor in bytes
    USB_DESCRIPTOR_INTERFACE,               // INTERFACE descriptor type
    0,                      // Interface Number
    0,                      // Alternate Setting Number
    2,                      // Number of endpoints in this intf
    HID_INTF,               // Class code
    BOOT_INTF_SUBCLASS,     // Subclass code
    HID_PROTOCOL_KEYBOARD,     // Protocol code '0x01'
    0,                      // Interface string index

    /* HID Class-Specific Descriptor */
    0x09,//sizeof(USB_HID_DSC)+3,    // Size of this descriptor in bytes RRoj hack
    DSC_HID,                // HID descriptor type
    DESC_CONFIG_WORD(0x0111),                 // HID Spec Release Number in BCD format (1.11)
    0x00,                   // Country Code (0x00 for Not supported)
    HID_NUM_OF_DSC,         // Number of class descriptors, see usbcfg.h
    DSC_RPT,                // Report descriptor type
    DESC_CONFIG_WORD(HID_RPT01_SIZE),   //sizeof(hid_rpt03),      // Size of the report descriptor

    /* Endpoint Descriptor */
    0x07,/*sizeof(USB_EP_DSC)*/
    USB_DESCRIPTOR_ENDPOINT,    //Endpoint Descriptor
    HID_EP1 | _EP_IN,            //EndpointAddress
    _INTERRUPT,                       //Attributes
    DESC_CONFIG_WORD(9),        //size
    0x01,                        //Interval

    /* Endpoint Descriptor */
    0x07,/*sizeof(USB_EP_DSC)*/
    USB_DESCRIPTOR_ENDPOINT,    //Endpoint Descriptor
    HID_EP1 | _EP_OUT,            //EndpointAddress
    _INTERRUPT,                       //Attributes
    DESC_CONFIG_WORD(8),        //size
    0x01,                        //Interval

    /* Mouse */
    /* Interface Descriptor */
    0x09,//sizeof(USB_INTF_DSC),   // Size of this descriptor in bytes
    USB_DESCRIPTOR_INTERFACE,               // INTERFACE descriptor type '0x04'
    1,                      // Interface Number
    0,                      // Alternate Setting Number
    1,                      // Number of endpoints in this intf
    HID_INTF,               // Class code	'0x03'
    BOOT_INTF_SUBCLASS,     // Subclass code	'0x01'
    HID_PROTOCOL_MOUSE,     // Protocol code	'0x02'
    0,                      // Interface string index

    /* HID Class-Specific Descriptor */
    0x09,//sizeof(USB_HID_DSC)+3,    // Size of this descriptor in bytes RRoj hack
    DSC_HID,                // HID descriptor type	'0x21'
    DESC_CONFIG_WORD(0x0111),                 // HID Spec Release Number in BCD format (1.11)
    0x00,                   // Country Code (0x00 for Not supported)
    HID_NUM_OF_DSC,         // Number of class descriptors, see usbcfg.h
    DSC_RPT,                // Report descriptor type		'0x22'
    DESC_CONFIG_WORD(HID_RPT02_SIZE),   //sizeof(hid_rpt01),      // Size of the report descriptor

    /* Endpoint Descriptor */
    0x07,/*sizeof(USB_EP_DSC)*/
    USB_DESCRIPTOR_ENDPOINT,    //Endpoint Descriptor	'0x05'
    HID_EP2 | _EP_IN,            //EndpointAddress
    _INTERRUPT,                       //Attributes		'0x03'
    DESC_CONFIG_WORD(0x0004),        //size
    0x01,                        //Interval

    /* Joystick */
    /* Interface Descriptor */
    0x09,//sizeof(USB_INTF_DSC),   // Size of this descriptor in bytes
    USB_DESCRIPTOR_INTERFACE,               // INTERFACE descriptor type
    2,                      // Interface Number
    0,                      // Alternate Setting Number
    2,                      // Number of endpoints in this intf
    HID_INTF,               // Class code
    BOOT_INTF_SUBCLASS,     // Subclass code
    HID_PROTOCOL_NONE,     // Protocol code
    0,                      // Interface string index

    /* HID Class-Specific Descriptor */
    0x09,//sizeof(USB_HID_DSC)+3,    // Size of this descriptor in bytes RRoj hack
    DSC_HID,                // HID descriptor type
    DESC_CONFIG_WORD(0x0111),                 // HID Spec Release Number in BCD format (1.11)
    0x00,                   // Country Code (0x00 for Not supported)
    HID_NUM_OF_DSC,         // Number of class descriptors, see usbcfg.h
    DSC_RPT,                // Report descriptor type
    DESC_CONFIG_WORD(HID_RPT03_SIZE),   //sizeof(hid_rpt02),      // Size of the report descriptor

    /* Endpoint Descriptor */
    0x07,/*sizeof(USB_EP_DSC)*/
    USB_DESCRIPTOR_ENDPOINT,    //Endpoint Descriptor
    HID_EP3 | _EP_IN,            //EndpointAddress
    _INTERRUPT,                       //Attributes
    DESC_CONFIG_WORD(64),        //size
    0x01,                        //Interval

    /* Endpoint Descriptor */
    0x07,/*sizeof(USB_EP_DSC)*/
    USB_DESCRIPTOR_ENDPOINT,    //Endpoint Descriptor
    HID_EP3 | _EP_OUT,            //EndpointAddress
    _INTERRUPT,                       //Attributes
    DESC_CONFIG_WORD(64),        //size
    0x01,                        //Interval

    /* USB Data */
    /* Interface Descriptor */
    0x09,//sizeof(USB_INTF_DSC),   // Size of this descriptor in bytes
    USB_DESCRIPTOR_INTERFACE,               // INTERFACE descriptor type
    3,                      // Interface Number
    0,                      // Alternate Setting Number
    2,                      // Number of endpoints in this intf
    HID_INTF,               // Class code
    0,                      // Subclass code
    0,                      // Protocol code
    0,                      // Interface string index

    /* HID Class-Specific Descriptor */
    0x09,//sizeof(USB_HID_DSC)+3,    // Size of this descriptor in bytes RRoj hack
    DSC_HID,                // HID descriptor type
    DESC_CONFIG_WORD(0x0111),                 // HID Spec Release Number in BCD format (1.11)
    0x00,                   // Country Code (0x00 for Not supported)
    HID_NUM_OF_DSC,         // Number of class descriptors, see usbcfg.h
    DSC_RPT,                // Report descriptor type
    HID_RPT04_SIZE,0x00,   //sizeof(hid_rpt01),      // Size of the report descriptor
    
    /* Endpoint Descriptor */
    0x07,/*sizeof(USB_EP_DSC)*/
    USB_DESCRIPTOR_ENDPOINT,    //Endpoint Descriptor
    HID_EP4 | _EP_IN,            //EndpointAddress
    _INTERRUPT,                       //Attributes
    DESC_CONFIG_WORD(0x40),                  //size
    0x01,                        //Interval

    0x07,/*sizeof(USB_EP_DSC)*/
    USB_DESCRIPTOR_ENDPOINT,    //Endpoint Descriptor
    HID_EP4 | _EP_OUT,           //EndpointAddress
    _INTERRUPT,                       //Attributes
    DESC_CONFIG_WORD(0x40),                  //size
    0x01                        //Interval
};


//Language code string descriptor
ROM struct{BYTE bLength;BYTE bDscType;WORD string[1];}sd000={
sizeof(sd000),USB_DESCRIPTOR_STRING,{0x0409
}};

//Manufacturer string descriptor
ROM struct{BYTE bLength;BYTE bDscType;WORD string[20];}sd001={
sizeof(sd001),USB_DESCRIPTOR_STRING,
{'B','i','t',' ','T','r','a','d','e',' ','O','n','e',',',' ','L','T','D','.',' '}
};

//Product string descriptor
ROM struct{BYTE bLength;BYTE bDscType;WORD string[10];}sd002={
sizeof(sd002),USB_DESCRIPTOR_STRING,
{'R','e','v','-','O','-','m','a','t','e'}
};

//Class specific descriptor - HID keyboard
#if 1
ROM struct{BYTE report[HID_RPT01_SIZE];}hid_rpt01={{
    0x05, 0x01,                    // USAGE_PAGE (Generic Desktop)
    0x09, 0x06,                    // USAGE (Keyboard)
    0xa1, 0x01,                    // COLLECTION (Application)
    0x85, 0x01,  /*   Report ID  */
    0x05, 0x07,                    //   USAGE_PAGE (Keyboard)
    0x19, 0xe0,                    //   USAGE_MINIMUM (Keyboard LeftControl)
    0x29, 0xe7,                    //   USAGE_MAXIMUM (Keyboard Right GUI)
    0x15, 0x00,                    //   LOGICAL_MINIMUM (0)
    0x25, 0x01,                    //   LOGICAL_MAXIMUM (1)
    0x75, 0x01,                    //   REPORT_SIZE (1)
    0x95, 0x08,                    //   REPORT_COUNT (8)
    0x81, 0x02,                    //   INPUT (Data,Var,Abs)
    0x95, 0x01,                    //   REPORT_COUNT (1)
    0x75, 0x08,                    //   REPORT_SIZE (8)
    0x81, 0x03,                    //   INPUT (Cnst,Var,Abs)
    0x95, 0x05,                    //   REPORT_COUNT (5)
    0x75, 0x01,                    //   REPORT_SIZE (1)
    0x05, 0x08,                    //   USAGE_PAGE (LEDs)
    0x19, 0x01,                    //   USAGE_MINIMUM (Num Lock)
    0x29, 0x05,                    //   USAGE_MAXIMUM (Kana)
    0x91, 0x02,                    //   OUTPUT (Data,Var,Abs)
    0x95, 0x01,                    //   REPORT_COUNT (1)
    0x75, 0x03,                    //   REPORT_SIZE (3)
    0x91, 0x03,                    //   OUTPUT (Cnst,Var,Abs)
    0x95, 0x06,                    //   REPORT_COUNT (6)
    0x75, 0x08,                    //   REPORT_SIZE (8)
    0x15, 0x00,                    //   LOGICAL_MINIMUM (0)
    0x25, 0xE7,                    //   LOGICAL_MAXIMUM (231)
    0x05, 0x07,                    //   USAGE_PAGE (Keyboard)
    0x19, 0x00,                    //   USAGE_MINIMUM (Reserved (no event indicated))
    0x29, 0xE7,                    //   USAGE_MAXIMUM (Keyboard Application)
    0x81, 0x00,                    //   INPUT (Data,Ary,Abs)
    0xc0,                          // End Collection
//
	0x05, 0x0C, /*		Usage Page (Consumer Devices)		*/
	0x09, 0x01, /*		Usage (Consumer Control)			*/
	0xA1, 0x01, /*		Collection (Application)			*/
	0x85, 0x02,	/*		Report ID=2							*/
	0x05, 0x0C, /*		Usage Page (Consumer Devices)		*/
	0x15, 0x00, /*		Logical Minimum (0)					*/
	0x25, 0x01, /*		Logical Maximum (1)					*/
	0x75, 0x01, /*		Report Size (1)						*/
	0x95, 0x0C, /*		Report Count (12)					*/
	0x09, 0xB0, /*		Usage (Play)                        */
	0x09, 0xB1, /*		Usage (Pause)				        */
	0x09, 0xB2, /*		Usage (Record)				        */
	0x09, 0xB3, /*		Usage (FAST FORWARD)				*/
	0x09, 0xB4, /*		Usage (REWIND)				        */
	0x09, 0xB5, /*		Usage (Scan Next Track)				*/
	0x09, 0xB6, /*		Usage (Scan Previous Track)			*/
	0x09, 0xB7, /*		Usage (Stop)						*/
	0x09, 0xCD, /*		Usage (Play / Pause)				*/
	0x09, 0xE2, /*		Usage (Mute)						*/
	0x09, 0xE9, /*		Usage (Volume Up)					*/
	0x09, 0xEA, /*		Usage (Volume Down)					*/
	0x81, 0x02, /*		Input (Data, Variable, Absolute)	*/
	0x95, 0x04, /*		Report Count (4)					*/
	0x81, 0x01, /*		Input (Constant)					*/
	0xC0		/*		End Collection						*/
    }
};
#endif
#if 0
ROM struct{BYTE report[HID_RPT01_SIZE];}hid_rpt01={{
    0x05, 0x01,                    // USAGE_PAGE (Generic Desktop)
    0x09, 0x06,                    // USAGE (Keyboard)
    0xa1, 0x01,                    // COLLECTION (Application)
    0x05, 0x07,                    //   USAGE_PAGE (Keyboard)
    0x19, 0xe0,                    //   USAGE_MINIMUM (Keyboard LeftControl)
    0x29, 0xe7,                    //   USAGE_MAXIMUM (Keyboard Right GUI)
    0x15, 0x00,                    //   LOGICAL_MINIMUM (0)
    0x25, 0x01,                    //   LOGICAL_MAXIMUM (1)
    0x75, 0x01,                    //   REPORT_SIZE (1)
    0x95, 0x08,                    //   REPORT_COUNT (8)
    0x81, 0x02,                    //   INPUT (Data,Var,Abs)
    0x95, 0x01,                    //   REPORT_COUNT (1)
    0x75, 0x08,                    //   REPORT_SIZE (8)
    0x81, 0x03,                    //   INPUT (Cnst,Var,Abs)
    0x95, 0x05,                    //   REPORT_COUNT (5)
    0x75, 0x01,                    //   REPORT_SIZE (1)
    0x05, 0x08,                    //   USAGE_PAGE (LEDs)
    0x19, 0x01,                    //   USAGE_MINIMUM (Num Lock)
    0x29, 0x05,                    //   USAGE_MAXIMUM (Kana)
    0x91, 0x02,                    //   OUTPUT (Data,Var,Abs)
    0x95, 0x01,                    //   REPORT_COUNT (1)
    0x75, 0x03,                    //   REPORT_SIZE (3)
    0x91, 0x03,                    //   OUTPUT (Cnst,Var,Abs)
    0x95, 0x06,                    //   REPORT_COUNT (6)
    0x75, 0x08,                    //   REPORT_SIZE (8)
    0x15, 0x00,                    //   LOGICAL_MINIMUM (0)
    0x25, 0xE7,                    //   LOGICAL_MAXIMUM (231)
    0x05, 0x07,                    //   USAGE_PAGE (Keyboard)
    0x19, 0x00,                    //   USAGE_MINIMUM (Reserved (no event indicated))
    0x29, 0xE7,                    //   USAGE_MAXIMUM (Keyboard Application)
    0x81, 0x00,                    //   INPUT (Data,Ary,Abs)
    0xc0                          // End Collection
    }
};
#endif
//Class specific descriptor - HID mouse
ROM struct{BYTE report[HID_RPT02_SIZE];}hid_rpt02={{
  0x05, 0x01,        //USAGE_PAGE (Generic Desktop)
  0x09, 0x02,        //Usage (Mouse)
  0xA1, 0x01, /* Collection (Application)                 */
  0x09, 0x01, /*  Usage (Pointer)                         */
  0xA1, 0x00, /*  Collection (Physical)                   */
  0x05, 0x09, /*      Usage Page (Buttons)                */
  0x19, 0x01, /*      Usage Minimum (01)                  */
  0x29, 0x05, /*      Usage Maximum (05)                  */
  0x15, 0x00, /*      Logical Minimum (0)                 */
  0x25, 0x01, /*      Logical Maximum (1)                 */
  0x95, 0x05, /*      Report Count (5)                    */
  0x75, 0x01, /*      Report Size (1)                     */
  0x81, 0x02, /*      Input (Data, Variable, Absolute)    */
  0x95, 0x01, /*      Report Count (1)                    */
  0x75, 0x03, /*      Report Size (3)                     */
  0x81, 0x01, /*      Input (Constant)    ;5 bit padding  */
  0x05, 0x01, /*      Usage Page (Generic Desktop)        */
  0x09, 0x30, /*      Usage (X)                           */
  0x09, 0x31, /*      Usage (Y)                           */
  0x09, 0x38, /*      Usage (Wheel)                       */
  0x15, 0x81, /*      Logical Minimum (-127)              */
  0x25, 0x7F, /*      Logical Maximum (127)               */
  0x75, 0x08, /*      Report Size (8)                     */
  0x95, 0x03, /*      Report Count (3)                    */
  0x81, 0x06, /*      Input (Data, Variable, Relative)    */
  0xC0, 0xC0,  // End Collection , End Collection
}
};
//Class specific descriptor - HID joystick
ROM struct{BYTE report[HID_RPT03_SIZE];}hid_rpt03={{
  0x05,0x01,        //USAGE_PAGE (Generic Desktop)
  0x09,0x05,        //USAGE (Game Pad)
  0xA1,0x01,        //COLLECTION (Application)
  0x15,0x00,        //  LOGICAL_MINIMUM(0)
  0x25,0x01,        //  LOGICAL_MAXIMUM(1)
  0x35,0x00,        //  PHYSICAL_MINIMUM(0)
  0x45,0x01,        //  PHYSICAL_MAXIMUM(1)
  0x75,0x01,        //  REPORT_SIZE(1)
  0x95,0x0D,        //  REPORT_COUNT(13)
  0x05,0x09,        //  USAGE_PAGE(Button)
  0x19,0x01,        //  USAGE_MINIMUM(Button 1)
  0x29,0x0D,        //  USAGE_MAXIMUM(Button 13)
  0x81,0x02,        //  INPUT(Data,Var,Abs)
  0x95,0x03,        //  REPORT_COUNT(3)
  0x81,0x01,        //  INPUT(Cnst,Ary,Abs)
  0x05,0x01,        //  USAGE_PAGE(Generic Desktop)
  0x25,0x07,        //  LOGICAL_MAXIMUM(7)
  0x46,0x3B,0x01,   //  PHYSICAL_MAXIMUM(315)
  0x75,0x04,        //  REPORT_SIZE(4)
  0x95,0x01,        //  REPORT_COUNT(1)
  0x65,0x14,        //  UNIT(Eng Rot:Angular Pos)
  0x09,0x39,        //  USAGE(Hat Switch)
  0x81,0x42,        //  INPUT(Data,Var,Abs,Null)
  0x65,0x00,        //  UNIT(None)
  0x95,0x01,        //  REPORT_COUNT(1)
  0x81,0x01,        //  INPUT(Cnst,Ary,Abs)
  0x26,0xFF,0x00,   //  LOGICAL_MAXIMUM(255)
  0x46,0xFF,0x00,   //  PHYSICAL_MAXIMUM(255)
  0x09,0x30,        //  USAGE(X)
  0x09,0x31,        //  USAGE(Y)
  0x09,0x32,        //  USAGE(Z)
  0x09,0x35,        //  USAGE(Rz)
  0x75,0x08,        //  REPORT_SIZE(8)
  0x95,0x04,        //  REPORT_COUNT(4)
  0x81,0x02,        //  INPUT(Data,Var,Abs)
  0xC0              //END_COLLECTION
}
};
//Class specific descriptor - HID data
ROM struct{BYTE report[HID_RPT04_SIZE];}hid_rpt04={
{
    0x06, 0x00, 0xFF,       // Usage Page = 0xFF00 (Vendor Defined Page 1)
    0x09, 0x01,             // Usage (Vendor Usage 1)
    0xA1, 0x01,             // Collection (Application)
    0x19, 0x01,             //      Usage Minimum
    0x29, 0x40,             //      Usage Maximum 	//64 input usages total (0x01 to 0x40)
    0x15, 0x00,             //      Logical Minimum (data bytes in the report may have minimum value = 0x00)
    0x26, 0xFF, 0x00, 	  	//      Logical Maximum (data bytes in the report may have maximum value = 0x00FF = unsigned 255)
    0x75, 0x08,             //      Report Size: 8-bit field size
    0x95, 0x40,             //      Report Count: Make sixty-four 8-bit fields (the next time the parser hits an "Input", "Output", or "Feature" item)
    0x81, 0x00,             //      Input (Data, Array, Abs): Instantiates input packet fields based on the above report size, count, logical min/max, and usage.
    0x19, 0x01,             //      Usage Minimum
    0x29, 0x40,             //      Usage Maximum 	//64 output usages total (0x01 to 0x40)
    0x91, 0x00,             //      Output (Data, Array, Abs): Instantiates output packet fields.  Uses same report size and count as "Input" fields, since nothing new/different was specified to the parser since the "Input" item.
    0xC0}                   // End Collection
};

//Array of configuration descriptors
ROM BYTE *ROM USB_CD_Ptr[]=
{
    (ROM BYTE *ROM)&configDescriptor1
};

//Array of string descriptors
ROM BYTE *ROM USB_SD_Ptr[]=
{
    (ROM BYTE *ROM)&sd000,
    (ROM BYTE *ROM)&sd001,
    (ROM BYTE *ROM)&sd002
};

/** EOF usb_descriptors.c ***************************************************/

#endif
