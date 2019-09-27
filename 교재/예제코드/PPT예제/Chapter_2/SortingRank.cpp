#include<iostream>
using namespace std;

#define SIZE 5

void Ascending(int arr[], int Rank[])
{
	int Count;

	for (int i = 0; i < SIZE; i++)
	{
		Count = 1;
		
		for (int j = 0; j < SIZE; j++)
		{
			if (arr[i] < arr[j])
			{
				Count++;
			}
		}

		Rank[i] = Count;
	}
}
void main()
{
	int arr[SIZE] = { 82,85,76,79,96 };
	int Rank[SIZE];

	cout << "======================" << endl;
	
	for (int i = 0; i < SIZE; i++)
	{
		cout << arr[i] << "점 : ??등" << endl;
	}

	cout << "======================" << endl;
	
	Ascending(arr, Rank);
	
	cout << "======================" << endl;
	
	for (int i = 0; i < SIZE; i++)
	{
		cout << arr[i] << "점 : " << Rank[i] << "등" << endl;
	}
	
	cout << "======================" << endl;
}
