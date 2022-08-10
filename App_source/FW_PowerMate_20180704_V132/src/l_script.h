#ifndef L_SCRIPT_H
#define L_SCRIPT_H

#include "app.h"
#include "l_flash.h"



// SCRIPT���s���p�@�e��DEFINE
#define SCRIPT_EXE_DATA_BUFF_BANK_NUM	2		// �X�N���v�g�f�[�^�ǂݍ��݃o�b�t�@�o���N��
#define SCRIPT_EXE_DATA_BUFF_SIZE		12		// �X�N���v�g�f�[�^�ǂݍ��݃o�b�t�@�T�C�Y
#define SCRIPT_EXE_FLAG_NOEXE			0		// ���s�t���O�@�����s
#define SCRIPT_EXE_FLAG_EXE				1		// ���s�t���O�@���s
#define SCRIPT_EXE_CTRL_ONE_MODE		0		// �X�N���v�g���� 1����s
#define SCRIPT_EXE_CTRL_LOOP_MODE		1		// �X�N���v�g���� ���[�v���[�h
#define SCRIPT_EXE_CTRL_FIRE_MODE		2		// �X�N���v�g���� �t�@�C���[���[�h
#define SCRIPT_EXE_CTRL_HOLD_MODE		3		// �X�N���v�g���� �z�[���h���[�h
#define SCRIPT_EXE_BUFF_BANK0_EXE		0		// ���R�}���h���s�o�b�t�@ �o���N0
#define SCRIPT_EXE_BUFF_BANK1_EXE		1		// ���R�}���h���s�o�b�t�@ �o���N1
#define SCRIPT_EXE_READ_NOREQ			0		// �o�b�t�@�ǂݍ��ݗv�� ���v��
#define SCRIPT_EXE_READ_REQ_BANK0		1		// �o�b�t�@�ǂݍ��ݗv�� �o���N0�ǂݍ��ݗv��
#define SCRIPT_EXE_READ_COMP_BANK0		2		// �o�b�t�@�ǂݍ��ݗv�� �o���N0�ǂݍ��݊���
#define SCRIPT_EXE_READ_REQ_BANK1		3		// �o�b�t�@�ǂݍ��ݗv�� �o���N1�ǂݍ��ݗv��
#define SCRIPT_EXE_READ_COMP_BANK1		4		// �o�b�t�@�ǂݍ��ݗv�� �o���N1�ǂݍ��݊���
#define SCRIPT_COMMAND_ID_WAIT			0x70	// �X�N���v�g�R�}���hID WAIT
#define SCRIPT_COMMAND_ID_KEY_PRESS		0x41	// �X�N���v�g�R�}���hID �L�[�v���X
#define SCRIPT_COMMAND_ID_KEY_RELESE	0x40	// �X�N���v�g�R�}���hID �L�[�����[�X
#define SCRIPT_COMMAND_ID_MULTI_PRESS       0x43	// �X�N���v�g�R�}���hID �}���`�v���X
#define SCRIPT_COMMAND_ID_MULTI_RELESE      0x42	// �X�N���v�g�R�}���hID �}���`�����[�X
#define SCRIPT_COMMAND_ID_MOUSE_PRESS_L		0x29	// �X�N���v�g�R�}���hID �}�E�X�v���X���N���b�N
#define SCRIPT_COMMAND_ID_MOUSE_RELESE_L	0x21	// �X�N���v�g�R�}���hID �}�E�X�����[�X���N���b�N
#define SCRIPT_COMMAND_ID_MOUSE_PRESS_R		0x2A	// �X�N���v�g�R�}���hID �}�E�X�v���X�E�N���b�N
#define SCRIPT_COMMAND_ID_MOUSE_RELESE_R	0x22	// �X�N���v�g�R�}���hID �}�E�X�����[�X�E�N���b�N
#define SCRIPT_COMMAND_ID_MOUSE_PRESS_W		0x2C	// �X�N���v�g�R�}���hID �}�E�X�v���X�z�C�[���N���b�N
#define SCRIPT_COMMAND_ID_MOUSE_RELESE_W	0x24	// �X�N���v�g�R�}���hID �}�E�X�����[�X�z�C�[���N���b�N
#define SCRIPT_COMMAND_ID_MOUSE_PRESS_B4	0x2D	// �X�N���v�g�R�}���hID �}�E�X�v���X�{�^��4�N���b�N
#define SCRIPT_COMMAND_ID_MOUSE_RELESE_B4	0x25	// �X�N���v�g�R�}���hID �}�E�X�����[�X�{�^��4�N���b�N
#define SCRIPT_COMMAND_ID_MOUSE_PRESS_B5	0x2E	// �X�N���v�g�R�}���hID �}�E�X�v���X�{�^��5�N���b�N
#define SCRIPT_COMMAND_ID_MOUSE_RELESE_B5	0x26	// �X�N���v�g�R�}���hID �}�E�X�����[�X�{�^��5�N���b�N
#define SCRIPT_COMMAND_ID_MOUSE_SCROLL_UP	0x31	// �X�N���v�g�R�}���hID �}�E�X�X�N���[���A�b�v
#define SCRIPT_COMMAND_ID_MOUSE_SCROLL_DOWN	0x32	// �X�N���v�g�R�}���hID �}�E�X�X�N���[���_�E��
#define SCRIPT_COMMAND_ID_MOUSE_MOVE        0x33	// �X�N���v�g�R�}���hID �}�E�X���[�u
#define SCRIPT_COMMAND_ID_JOY_BTN_PRESS 	0x69	// �X�N���v�g�R�}���hID �W���C�X�e�B�b�N�{�^���v���X
#define SCRIPT_COMMAND_ID_JOY_BTN_RELESE 	0x61	// �X�N���v�g�R�}���hID �W���C�X�e�B�b�N�{�^�������[�X
#define SCRIPT_COMMAND_ID_JOY_HATSW_PRESS 	0x6A	// �X�N���v�g�R�}���hID �W���C�X�e�B�b�NHATSW�v���X
#define SCRIPT_COMMAND_ID_JOY_HATSW_RELESE 	0x62	// �X�N���v�g�R�}���hID �W���C�X�e�B�b�NHATSW�����[�X
#define SCRIPT_COMMAND_ID_JOY_L_LEVER           0x6B	// �X�N���v�g�R�}���hID �W���C�X�e�B�b�N�����o�[�o��
#define SCRIPT_COMMAND_ID_JOY_L_LEVER_CENTER 	0x63	// �X�N���v�g�R�}���hID �W���C�X�e�B�b�N�����o�[����
#define SCRIPT_COMMAND_ID_JOY_R_LEVER           0x6C	// �X�N���v�g�R�}���hID �W���C�X�e�B�b�N�E���o�[�o��
#define SCRIPT_COMMAND_ID_JOY_R_LEVER_CENTER 	0x64	// �X�N���v�g�R�}���hID �W���C�X�e�B�b�N�E���o�[����
// SCRIPT���s���@�\����
#if 0
typedef struct
{
	BYTE	Script_Data[SCRIPT_EXE_DATA_BUFF_BANK_NUM][SCRIPT_EXE_DATA_BUFF_SIZE];
} ST_SCRIPT_EXE_SCRIPT_DATA;
typedef struct
{
	ST_SCRIPT_EXE_SCRIPT_DATA Script_Data[SCRIPT_EXE_INFO_NUM];
} ST_SCRIPT_EXE_SCRIPT_DATAS;
#endif
#if 0
typedef struct
{
	BYTE	Exe_Flag_Integration;						// �X�N���v�g���s�t���O�@����
	BYTE	Exe_Flag[SCRIPT_EXE_INFO_NUM];				// �X�N���v�g���s�t���O 0=�����s 1=���s��
	BYTE	Script_No[SCRIPT_EXE_INFO_NUM];				// �X�N���v�g���sNo.
	WORD	Pause_Flag[SCRIPT_EXE_INFO_NUM];				// �X�N���v�g�ꎞ��~�t���O�@0!=�ꎞ��~
	BYTE	Control_Flag0[SCRIPT_EXE_INFO_NUM];			// �R���g���[���t���O�@0=1����s 1=���[�v���[�h 2=�t�@�C���[���[�h
	BYTE	Control_Flag1[SCRIPT_EXE_INFO_NUM];			// �R���g���[���t���O�@���R�}���h���s�o�b�t�@ �o���N
	BYTE	Control_Flag2[SCRIPT_EXE_INFO_NUM];			// �R���g���[���t���O ���R�}���h���s�o�b�t�@�ʒu
	BYTE	Control_Flag3[SCRIPT_EXE_INFO_NUM];			// �R���g���[���t���O�@�o�b�t�@�ǂݍ��ݗv�� 0=���v�� 1=�o�b�t�@A�ǂݍ��ݗv�� 2=�o�b�t�@A�ǂݍ��݊��� 3=�o�b�t�@B�ǂݍ��ݗv�� 4=�o�b�t�@B�ǂݍ��݊���
//	BYTE	Script_Data[SCRIPT_EXE_DATA_BUFF_BANK_NUM][SCRIPT_EXE_DATA_BUFF_SIZE];	// �X�N���v�g�f�[�^�ǂݍ��݃o�b�t�@
	BYTE	reserve1[SCRIPT_EXE_INFO_NUM];				// 
	BYTE	Command_ID[SCRIPT_EXE_INFO_NUM];				// �����s�X�N���v�g�R�}���hID
	WORD_VAL	Command_DATA[SCRIPT_EXE_INFO_NUM];		// �����s�X�N���v�g�R�}���hDATA
	WORD_VAL	Interval[SCRIPT_EXE_INFO_NUM];			// ���s�Ԋu
	DWORD_VAL	Address[SCRIPT_EXE_INFO_NUM];			// �X�N���v�g�f�[�^�ۑ��A�h���X
	DWORD_VAL	Script_Size[SCRIPT_EXE_INFO_NUM];		// �X�N���v�g�T�C�Y
	DWORD_VAL	Script_Exe_Pos[SCRIPT_EXE_INFO_NUM];		// �X�N���v�g���s���ʒu[byte]
	DWORD_VAL	Next_Read_Address[SCRIPT_EXE_INFO_NUM];	// �X�N���v�g�f�[�^���ǂݍ��݃A�h���X
	BYTE	reserve2[SCRIPT_EXE_INFO_NUM];				// 
	BYTE	USB_Send_Code_Mouse[SCRIPT_EXE_INFO_NUM];						// USB���M�R�[�h�@�}�E�X
	WORD	USB_Send_Wheel_Scroll[SCRIPT_EXE_INFO_NUM];						// USB���M�R�[�h�@�}�E�X �z�C�[���X�N���[��
	BYTE	USB_Send_Code_Keyboard[SCRIPT_EXE_INFO_NUM][HID_INT_IN_EP_SIZE];	// USB���M�R�[�h�@�L�[�{�[�h
	BYTE	USB_Send_Code_Mouse_Fix;										// USB���M�R�[�h�@�}�E�X �m��
	WORD	USB_Send_Wheel_Scroll_Fix;										// USB���M�R�[�h�@�}�E�X �z�C�[���X�N���[�� �m��
	BYTE	USB_Send_Code_Keyboard_Fix[HID_INT_IN_EP_SIZE];					// USB���M�R�[�h�@�L�[�{�[�h �m��
} ST_SCRIPT_EXE_INFO;
#endif
#if 0
typedef struct
{
	BYTE	Exe_Flag_Integration;						// �X�N���v�g���s�t���O�@����
	BYTE	Exe_Flag[SCRIPT_EXE_INFO_NUM];				// �X�N���v�g���s�t���O 0=�����s 1=���s��
	BYTE	Script_No[SCRIPT_EXE_INFO_NUM];				// �X�N���v�g���sNo.
	WORD	Pause_Flag[SCRIPT_EXE_INFO_NUM];				// �X�N���v�g�ꎞ��~�t���O�@0!=�ꎞ��~
	BYTE	Control_Flag0[SCRIPT_EXE_INFO_NUM];			// �R���g���[���t���O�@0=1����s 1=���[�v���[�h 2=�t�@�C���[���[�h
	BYTE	Control_Flag1[SCRIPT_EXE_INFO_NUM];			// �R���g���[���t���O�@���R�}���h���s�o�b�t�@ �o���N
	BYTE	Control_Flag2[SCRIPT_EXE_INFO_NUM];			// �R���g���[���t���O ���R�}���h���s�o�b�t�@�ʒu
	BYTE	Control_Flag3[SCRIPT_EXE_INFO_NUM];			// �R���g���[���t���O�@�o�b�t�@�ǂݍ��ݗv�� 0=���v�� 1=�o�b�t�@A�ǂݍ��ݗv�� 2=�o�b�t�@A�ǂݍ��݊��� 3=�o�b�t�@B�ǂݍ��ݗv�� 4=�o�b�t�@B�ǂݍ��݊���
//	BYTE	Script_Data[SCRIPT_EXE_DATA_BUFF_BANK_NUM][SCRIPT_EXE_DATA_BUFF_SIZE];	// �X�N���v�g�f�[�^�ǂݍ��݃o�b�t�@
	BYTE	reserve1[SCRIPT_EXE_INFO_NUM];				// 
//	BYTE	Command_ID[SCRIPT_EXE_INFO_NUM];				// �����s�X�N���v�g�R�}���hID
//	WORD_VAL	Command_DATA[SCRIPT_EXE_INFO_NUM];		// �����s�X�N���v�g�R�}���hDATA
//	WORD_VAL	Interval[SCRIPT_EXE_INFO_NUM];			// ���s�Ԋu
//	DWORD_VAL	Address[SCRIPT_EXE_INFO_NUM];			// �X�N���v�g�f�[�^�ۑ��A�h���X
//	DWORD_VAL	Script_Size[SCRIPT_EXE_INFO_NUM];		// �X�N���v�g�T�C�Y
//	DWORD_VAL	Script_Exe_Pos[SCRIPT_EXE_INFO_NUM];		// �X�N���v�g���s���ʒu[byte]
//	DWORD_VAL	Next_Read_Address[SCRIPT_EXE_INFO_NUM];	// �X�N���v�g�f�[�^���ǂݍ��݃A�h���X
//	BYTE	reserve2[SCRIPT_EXE_INFO_NUM];				// 
	BYTE	USB_Send_Code_Mouse[SCRIPT_EXE_INFO_NUM];						// USB���M�R�[�h�@�}�E�X
	WORD	USB_Send_Wheel_Scroll[SCRIPT_EXE_INFO_NUM];						// USB���M�R�[�h�@�}�E�X �z�C�[���X�N���[��
	BYTE	USB_Send_Code_Keyboard[SCRIPT_EXE_INFO_NUM][HID_INT_IN_EP_SIZE];	// USB���M�R�[�h�@�L�[�{�[�h
	BYTE	USB_Send_Code_Mouse_Fix;										// USB���M�R�[�h�@�}�E�X �m��
	WORD	USB_Send_Wheel_Scroll_Fix;										// USB���M�R�[�h�@�}�E�X �z�C�[���X�N���[�� �m��
	BYTE	USB_Send_Code_Keyboard_Fix[HID_INT_IN_EP_SIZE];					// USB���M�R�[�h�@�L�[�{�[�h �m��
} ST_SCRIPT_EXE_INFO;
#endif
#if 1
typedef struct
{
	BYTE	Exe_Flag;				// 0 �X�N���v�g���s�t���O 0=�����s 1=���s��
	BYTE	Script_No;				// 1 �X�N���v�g���sNo.
	WORD	Pause_Flag;				// 2 �X�N���v�g�ꎞ��~�t���O�@0!=�ꎞ��~
	BYTE	Control_Flag0;			// 4 �R���g���[���t���O�@0=1����s 1=���[�v���[�h 2=�t�@�C���[���[�h
	BYTE	Control_Flag1;			// 5 �R���g���[���t���O�@���R�}���h���s�o�b�t�@ �o���N
	BYTE	Control_Flag2;			// 6 �R���g���[���t���O ���R�}���h���s�o�b�t�@�ʒu
	BYTE	Control_Flag3;			// 7 �R���g���[���t���O�@�o�b�t�@�ǂݍ��ݗv�� 0=���v�� 1=�o�b�t�@A�ǂݍ��ݗv�� 2=�o�b�t�@A�ǂݍ��݊��� 3=�o�b�t�@B�ǂݍ��ݗv�� 4=�o�b�t�@B�ǂݍ��݊���
	BYTE	Script_Data[SCRIPT_EXE_DATA_BUFF_BANK_NUM][SCRIPT_EXE_DATA_BUFF_SIZE];	// 8 �X�N���v�g�f�[�^�ǂݍ��݃o�b�t�@
	BYTE	reserve1;				// 32
	BYTE	Command_ID;				// 33 �����s�X�N���v�g�R�}���hID
	WORD_VAL	Command_DATA;		// 34 �����s�X�N���v�g�R�}���hDATA
	WORD_VAL	Interval;			// 36 ���s�Ԋu
	DWORD_VAL	Address;			// 38 �X�N���v�g�f�[�^�ۑ��A�h���X
	DWORD_VAL	Script_Size;		// 42 �X�N���v�g�T�C�Y
	DWORD_VAL	Script_Exe_Pos;		// 46 �X�N���v�g���s���ʒu[byte]
	DWORD_VAL	Next_Read_Address;	// 50 �X�N���v�g�f�[�^���ǂݍ��݃A�h���X
	BYTE	USB_Send_Code_Mouse[MOUSE_BUFF_SIZE];       // 54 USB���M�R�[�h�@�}�E�X
	WORD	USB_Send_Wheel_Scroll;						// 58 USB���M�R�[�h�@�}�E�X �z�C�[���X�N���[��
	BYTE	USB_Send_Code_Keyboard[KEYBOARD_BUFF_SIZE];	// 60 USB���M�R�[�h�@�L�[�{�[�h
	BYTE	reserve2;				// 69
	BYTE	USB_Send_Code_Joystick[JOYSTICK_BUFF_SIZE];	// 70 USB���M�R�[�h�@�W���C�X�e�B�b�N
	BYTE	reserve3;				// 77
	BYTE	USB_Send_Code_MultiMedia[MULTIMEDIA_BUFF_SIZE];	// 78 USB���M�R�[�h�@�}���`���f�B�A
	BYTE	reserve4;				// 81
} ST_SCRIPT_EXE_INFO;
#endif

extern BYTE Set_Script_Exe_Info(BYTE p_idx, BYTE p_ScriptNo);
extern BYTE Script_Exe_Info_Clr(void);
extern BYTE Script_Reset(BYTE p_idx);
extern BYTE Get_Next_Script_Data(void);
extern BYTE Get_Next_Script_Command(void);
extern BYTE Set_USB_Send_Code(void);
extern BYTE Get_Script_Output_Status( void );
//extern BYTE Get_Script_Output_Status_Multi(BYTE p_idx);
//extern BYTE Script_Interval_Update(void);
extern BYTE Script_Run(BYTE p_idx);
extern BYTE Script_Stop(BYTE p_idx);
extern BYTE Script_Fire_Mode_Stop(BYTE p_idx, BYTE p_ScriptNo);
extern BYTE Script_Hold_Mode_Stop(BYTE p_idx, BYTE p_ScriptNo);
extern BYTE Get_Exe_Script_No(BYTE p_idx);
extern BYTE Get_Script_Attribute(BYTE p_idx);


extern ST_SCRIPT_EXE_INFO script_exe_info;
//extern ST_SCRIPT_EXE_SCRIPT_DATAS script_exe_script_datas;
//extern ST_SCRIPT_EXE_BUFF script_exe_buff;
//extern ST_SCRIPT_EXE_COMMAND script_exe_command;
//extern ST_SCRIPT_EXE_SEND_CODE script_exe_send_code;

extern BYTE next_script_exe_req_scriptNo;


#endif//L_SCRIPT_H
