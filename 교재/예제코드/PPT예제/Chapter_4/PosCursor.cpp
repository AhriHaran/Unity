#include <iostream>
#include <Windows.h>
using namespace std;

void gotoxy(int x, int y);

void main()
{
	int cnt = -1;
	char *str = "2018/03/07!";
	cout << "HelloWorld!";

	while (cnt++ != 12)
	{
		gotoxy(cnt, 0);
		cout << str[cnt];
		Sleep(500);
	}
}

void gotoxy(int x, int y)
{
	COORD Pos;
	Pos.X = x;
	Pos.Y = y;

	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), Pos);
}
