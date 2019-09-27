#include<iostream>
#include<Windows.h>
using namespace std;

void main()
{
	while(true)
	{
		system("cls");
		cout << "전화 거는중 ☏" << endl;

		for (int i = 0; i < 3; i++)
		{
			Sleep(1000);
			cout << ".";
		}

		Sleep(1000);
	}
}

//void Draw(int a[][10])
//{
//	for (int y = 0; y < 10; y++)
//	{
//		for (int x = 0; x < 10; x++)
//		{
//			if (a[y][x] == 1)
//				cout << "☆";
//			else
//				cout << "  ";
//		}
//		cout << endl;
//	}
//}
//
//void Update(int a[][10])
//{
//	for (int y = 0; y < 10; y++)
//	{
//		for (int x = 0; x < 10; x++)
//		{
//			if (a[y][x] == 1)
//			{
//				if (x - 1 < 0)
//				{
//					a[y][x] = 0;
//					a[y][9] = 1;
//				}
//				else
//				{
//					a[y][x] = 0;
//					a[y][x - 1] = 1;
//				}
//			}
//		}
//	}
//}
//
//
//void main()
//{
//	int a[10][10] = { 0 };
//	for (int y = 0; y < 10; y++)
//	{
//		for (int x = 0; x < 10; x++)
//		{
//			if (x == 9)
//				a[y][x] = 1;
//		}
//	}
//	while (1)
//	{
//		Draw(a);
//		Sleep(500);
//		system("cls");
//		Update(a);
//	}
//}