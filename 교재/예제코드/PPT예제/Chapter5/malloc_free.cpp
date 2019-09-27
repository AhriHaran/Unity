
#include<iostream>
#include<stdlib.h>
#include<Windows.h>
using namespace std;
#define MAX 50

struct Student
{
	char Name[10];
	int Age;
	int Class;
	int Number;
	char Gender[10];
}

int StudentCount = 0;

void ShowStudent(Student* St)
{
	cout << "======" << St->Name << "�л�(" << St->Number << "%d��)======" << endl;
	cout << "���� : " << St->Age << ",  ���� : " << St->Gender << ",  �г� : " << St->Class << endl;
	cout << "======================" << endl;
}

void SetStudent(Student* St, int* Number)
{
	St->Number = ++(*Number);
	cout << "======" << St->Number << "�� �л�======" << endl;
	cout << "�̸� �Է� : ";
	cin >> St->Name;
	cout << "���� �Է� : ";
	cin >> St->Age;
	cout << "���� �Է� : ";
	cin >> St->Gender;
	cout << "�г� �Է� : ";
	cin >> St->Class;
}

void main()
{
	Student* Student_List[MAX];
	int Select;
	while (1)
	{
		system("cls");
		cout << "=====�л��������α׷�=====(�� �ο� : " << StudentCount << ")" << endl;
		cout << "1.�л� ���" << endl;
		cout << "2.�л� ���" << endl;
		cout << "3.����" << endl;
		cout << "�Է� : " << endl;
		cin >> Select;
		switch (Select)
		{
		case 1:
			if (StudentCount + 1 >= 50)
			{
				cout << "�л�����(" << MAX << "��)�� ��� á���ϴ�." << endl;
				system("pause");
				break;
			}
			Student_List[StudentCount] = new Student;
			SetStudent(Student_List[StudentCount], &StudentCount);
			break;
		case 2:
			for (int i = 0; i < StudentCount; i++)
				ShowStudent(Student_List[i]);
			system("pause");
			break;
		case 3:
			for (int i = 0; i < StudentCount; i++)
			{
				cout << Student_List[i]->Name << "�л� �����Ҵ� ���� �Ϸ�" << endl;
				delete Student_List[i];
			}
			return;
		}
	}
}