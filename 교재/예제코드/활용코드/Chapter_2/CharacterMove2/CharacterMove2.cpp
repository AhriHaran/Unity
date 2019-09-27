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
#define POTAL_MAX 2//포탈 갯수
#define ENTRY_START 10
#define EXIT_START 20
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
	{ 1,	0,	10,	0,	0,	0,	0,	11,	0,	1 },
	{ 1,	0,	0,	0,	0,	0,	0,	0,	0,	1 },
	{ 1,	0,	0,	0,	0,	2,	0,	0,	0,	1 },
	{ 1,	0,	0,	0,	0,	0,	0,	0,	0,	1 },
	{ 1,	0,	0,	0,	0,	0,	0,	0,	0,	1 },
	{ 1,	0,	21,	0,	0,	0,	0,	20,	0,	1 },
	{ 1,	0,	0,	0,	0,	0,	0,	0,	0,	1 },
	{ 1,	1,	1,	1,	1,	1,	1,	1,	1,	1 } 
};

int m_iarrCharacter[2];
int Entry_Potal[POTAL_MAX][2];
int Exit_Potal[POTAL_MAX][2];

int Find_Entry(int x, int y)
{
	for (int i = 0; i < POTAL_MAX; i++)
	{
		if (Entry_Potal[i][Y] == y &&Entry_Potal[i][X] == x)
			return i;
	}
	return -1;
}

int Find_Exit(int x, int y)
{
	for (int i = 0; i < POTAL_MAX; i++)
	{
		if (Exit_Potal[i][Y] == y &&Exit_Potal[i][X] == x)
			return i;
	}
	return -1;
}

void Init()
{
	int Width = (WIDTH * 2) + 1;
	int Height = HEIGHT + 1;
	char buf[256];

	//system 함수의 명령어로 사용할 문자열을 만든다.
	sprintf(buf, "mode con: lines=%d cols=%d", Height, Width);
	
	//mode con: lines= cols= 라는 명령문자열을 입력받아서 문자단위의 행과 열 크기만큼의 콘솔창을 만든다.
	system(buf);

	for (int y = 0; y < HEIGHT; y++)
	{
		for (int x = 0; x < WIDTH; x++)
		{
			if (map[y][x] == CHARACTER)
			{
				character[X] = x;
				character[Y] = y;
			}
			else if (map[y][x] >= ENTRY_START && map[y][x] < ENTRY_START + POTAL_MAX)
			{
				Entry_Potal[map[y][x] - ENTRY_START][X] = x;
				Entry_Potal[map[y][x] - ENTRY_START][Y] = y;
			}
			else if (map[y][x] >= EXIT_START && map[y][x] < EXIT_START + POTAL_MAX)
			{
				Exit_Potal[map[y][x] - EXIT_START][X] = x;
				Exit_Potal[map[y][x] - EXIT_START][Y] = y;
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
			{
				cout << "▩";
			}
			else if (map[y][x] == CHARACTER)
			{
				RED
					cout << "♧";
				ORIGINAL
			}
			else if (Find_Entry(x, y) != -1)
			{
				BLUE
					cout << "◎";
				ORIGINAL
			}
			else if (Find_Exit(x, y) != -1)
			{
				YELLOW
					cout << "●";
				ORIGINAL
			}
			else if (map[y][x] == NULL)
			{
				cout << "  ";
			}
		}
		cout << endl;
	}
}

void MoveCheck()
{
	int index = Find_Entry(character[X], character[Y]);
	if (index != -1)
	{
		character[X] = Exit_Potal[index][X];
		character[Y] = Exit_Potal[index][Y];
	}
}

void Move()
{
	char ch;
	ch = getchar();
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