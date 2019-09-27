#include<iostream>
using namespace std;

//void main()
//{
//	int arr[5][5] = { 0 };
//	arr[2][2] = 1;
//	for (int y = 0; y < 5; y++)
//	{
//		for (int x = 0; x < 5; x++)
//		{
//			cout << arr[y][x] << " ";
//		}
//
//		cout << endl;
//	}
//}

#define STAR 1
#define NULL 0

void main()
{
	int arr[5][5] = 
	{
		{ 1, 1, 1, 1, 1 },
		{ 1, 0, 0, 0, 1 },
		{ 1, 0, 1, 0, 1 },
		{ 1, 0, 0, 0, 1 },
		{ 1, 1, 1, 1, 1 },
	};

	for (int y = 0; y < 5; y++)
	{
		for (int x = 0; x < 5; x++)
		{
			if (arr[y][x] == STAR)
			{
				cout << "¡Ú";
			}
			else if (arr[y][x] == NULL)
			{
				cout << "¡Ù";
			}
		}

		cout << endl;
	}
}
