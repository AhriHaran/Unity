#include<iostream>
using namespace std;

#define SIZE 5

void Ascending(int arr[])
{
	int tmp;

	for (int i = 0; i < SIZE - 1; i++)
	{
		for (int j = i + 1; j < SIZE; j++)
		{
			if (arr[i] > arr[j])
			{
				tmp = arr[i];
				arr[i] = arr[j];
				arr[j] = tmp;
			}
		}
	}
}

void main()
{
	int arr[SIZE] = { 4, 8, 2, 7, 6 };

	cout << "Input : ";

	for (int i = 0; i < SIZE; i++)
	{
		cout << arr[i];
	}

	cout << endl;

	Ascending(arr);

	cout << "Order By : ";

	for (int i = 0; i < SIZE; i++)
	{	
		cout << arr[i];
	}

	cout << endl;
}