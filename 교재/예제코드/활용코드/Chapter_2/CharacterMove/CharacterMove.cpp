#include<iostream>
#include<conio.h>
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

#define WALL 1
#define NULL 0
#define Y 0
#define X 1
#define CHARACTER 2
#define ENTRY_POTAL 10
#define EXIT_POTAL 20

#define LEFT 97 // a 아스키 코드
#define RIGHT 100 // d 아스키 코드
#define UP 119 // w 아스키 코드
#define DOWN 115 // s 아스키 코드

#define WIDTH 10
#define HEIGHT 10

int map[HEIGHT][WIDTH] = 
{
	{ 1,	1,	1,	1,	1,	1,	1,	1,	1,	1 },
	{ 1,	0,	0,	0,	0,	0,	0,	0,	0,	1 },
	{ 1,	0,	10,	0,	0,	0,	0,	0,	0,	1 },
	{ 1,	0,	0,	0,	0,	0,	0,	0,	0,	1 },
	{ 1,	0,	0,	0,	0,	2,	0,	0,	0,	1 },
	{ 1,	0,	0,	0,	0,	0,	0,	0,	0,	1 },
	{ 1,	0,	0,	0,	0,	0,	0,	0,	0,	1 },
	{ 1,	0,	0,	0,	0,	0,	0,	20,	0,	1 },
	{ 1,	0,	0,	0,	0,	0,	0,	0,	0,	1 },
	{ 1,	1,	1,	1,	1,	1,	1,	1,	1,	1 } 
};

int character[2];
int Entry_Potal[2];
int Exit_Potal[2];

void Init()
{
	for (int y = 0; y < HEIGHT; y++)
	{
		for (int x = 0; x < WIDTH; x++)
		{
			if (map[y][x] == CHARACTER)
			{
				character[X] = x;
				character[Y] = y;
			}
			else if (map[y][x] == ENTRY_POTAL)
			{
				Entry_Potal[X] = x;
				Entry_Potal[Y] = y;
			}
			else if (map[y][x] == EXIT_POTAL)
			{
				Exit_Potal[X] = x;
				Exit_Potal[Y] = y;
			}
		}
	}
}
void MapDraw()
{
	for (int y = 0; y < HEIGHT; y++)
	{
		for (int x = 0; x < WIDTH; x++)
		{
			if (map[y][x] == WALL)
				cout << "▩";
			else if (map[y][x] == CHARACTER)
			{
				RED
					cout << "♧";
				ORIGINAL
			}
			else if (Entry_Potal[X] == x && Entry_Potal[Y] == y)
			{
				BLUE
					cout << "◎";
				ORIGINAL
			}
			else if (Exit_Potal[X] == x && Exit_Potal[Y] == y)
			{
				YELLOW
					cout << "●";
				ORIGINAL
			}
			else if (map[y][x] == NULL)
				cout << "  ";
		}
		
		cout << endl;
	}
}

void MoveCheck()
{
	if (character[X] == Entry_Potal[X] && character[Y] == Entry_Potal[Y])
	{
		character[X] = Exit_Potal[X];
		character[Y] = Exit_Potal[Y];
	}
}

void Move()
{
	char ch;
	//읽어들인 문자를 10진 아스키코드값으로 리턴한다.
	ch = getchar();
	//콘솔의 화면을 지워준다.(반복해서 그릴때 변경된 것을 출력해 줄 수 있다.(whil문으로 변경된 것을 다시 그릴 필요가 있을때)
	system("cls");
	map[character[Y]][character[X]] = NULL;
	switch (ch)
	{
	case LEFT:
		if (map[character[Y]][character[X] - 1] != WALL)
			character[X]--;
		break;
	case RIGHT:
		if (map[character[Y]][character[X] + 1] != WALL)
			character[X]++;
		break;
	case UP:
		if (map[character[Y] - 1][character[X]] != WALL)
			character[Y]--;
		break;
	case DOWN:
		if (map[character[Y] + 1][character[X]] != WALL)
			character[Y]++;
		break;
	}
	MoveCheck();
	map[character[Y]][character[X]] = CHARACTER;
}
void main()
{
	Init();
	while (1)
	{
		MapDraw();
		Move();
	}
}