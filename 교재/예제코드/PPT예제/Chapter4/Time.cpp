#include<iostream>
#include<time.h>
#include<Windows.h>
using namespace std;

//void gotoxy(int x, int y)
//{
//	COORD Pos = { x, y };
//	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), Pos);
//}
//
//
//void main()
//{
//	int OldClock, CurClock, i = 0;
//	OldClock = clock();
//
//	while (1)
//	{
//		CurClock = clock();
//		if (CurClock - OldClock >= 1000)
//		{
//			i++;
//			gotoxy(10, 10);
//			cout << "Count :" << left << i;
//			OldClock = CurClock;
//		}
//	}
//}

//void gotoxy(int x, int y)
//{
//	COORD Pos = { x, y };
//	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), Pos);
//}
//
//
//void main()
//{
//	int CountClock, SayClock, CurClock, i = 0, j = 0;
//	CountClock = clock();
//	SayClock = clock();
//	while (1)
//	{
//		CurClock = clock();
//		if (CurClock - CountClock >= 1000)
//		{
//			i++;
//			gotoxy(10, 10);
//			cout << "Count :" << i;
//			CountClock = CurClock;
//		}
//		if (CurClock - SayClock >= 5000)
//		{
//			j++;
//			gotoxy(10, 11 + j);
//			cout << j << "번째 인사 : Hello~";
//			SayClock = CurClock;
//		}
//	}
//}

