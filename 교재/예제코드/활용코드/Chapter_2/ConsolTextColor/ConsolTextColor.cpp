#include<iostream>
#include<Windows.h>
using namespace std;

//////////////////////////////////////////////////////
#define col GetStdHandle(STD_OUTPUT_HANDLE) 
#define BLACK SetConsoleTextAttribute( col,0x0000 ); // ����
#define DARK_BLUE SetConsoleTextAttribute( col,0x0001 ); // ���Ķ�
#define GREEN SetConsoleTextAttribute( col,0x0002 ); // �ʷ�
#define BLUE_GREEN SetConsoleTextAttribute( col,0x0003 ); // û��
#define BLOOD SetConsoleTextAttribute( col,0x0004 ); // �˺���
#define PUPPLE SetConsoleTextAttribute( col,0x0005 ); // ����
#define GOLD SetConsoleTextAttribute( col,0x0006 ); // �ݻ�		
#define ORIGINAL SetConsoleTextAttribute( col,0x0007 ); // ���� ȸ��(���� ���ڻ�)	
#define GRAY SetConsoleTextAttribute( col,0x0008 ); // ȸ��
#define BLUE SetConsoleTextAttribute( col,0x0009 ); // �Ķ�
#define HIGH_GREEN SetConsoleTextAttribute( col,0x000a ); // ����
#define SKY_BLUE SetConsoleTextAttribute( col,0x000b ); // �ϴ�
#define RED SetConsoleTextAttribute( col,0x000c ); // ����
#define PLUM SetConsoleTextAttribute( col,0x000d ); // ����
#define YELLOW SetConsoleTextAttribute( col,0x000e ); // ���
#define WHITE SetConsoleTextAttribute(col, 0x000f); // �Ͼ�
//////////////////////////////////////////////////////

void main()
{
	RED
		cout << "Hellow" << endl;
	ORIGINAL
}