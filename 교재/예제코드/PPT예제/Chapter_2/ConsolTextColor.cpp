#include<iostream>
#include<Windows.h>
using namespace std;

//////////////////////////////////////////////////////
#define col GetStdHandle(STD_OUTPUT_HANDLE) 
#define BLACK SetConsoleTextAttribute( col,0x0000 ); // 검정
#define DARK_BLUE SetConsoleTextAttribute( col,0x0001 ); // 검파랑
#define GREEN SetConsoleTextAttribute( col,0x0002 ); // 초록
#define BLUE_GREEN SetConsoleTextAttribute( col,0x0003 ); // 청녹
#define BLOOD SetConsoleTextAttribute( col,0x0004 ); // 검붉은
#define PUPPLE SetConsoleTextAttribute( col,0x0005 ); // 보라
#define GOLD SetConsoleTextAttribute( col,0x0006 ); // 금색		
#define ORIGINAL SetConsoleTextAttribute( col,0x0007 ); // 밝은 회색(원래 글자색)	
#define GRAY SetConsoleTextAttribute( col,0x0008 ); // 회색
#define BLUE SetConsoleTextAttribute( col,0x0009 ); // 파랑
#define HIGH_GREEN SetConsoleTextAttribute( col,0x000a ); // 연두
#define SKY_BLUE SetConsoleTextAttribute( col,0x000b ); // 하늘
#define RED SetConsoleTextAttribute( col,0x000c ); // 빨강
#define PLUM SetConsoleTextAttribute( col,0x000d ); // 자주
#define YELLOW SetConsoleTextAttribute( col,0x000e ); // 노랑
#define WHITE SetConsoleTextAttribute(col, 0x000f); // 하양
//////////////////////////////////////////////////////

void main()
{
	RED
		cout << "Hellow" << endl;
	ORIGINAL
}