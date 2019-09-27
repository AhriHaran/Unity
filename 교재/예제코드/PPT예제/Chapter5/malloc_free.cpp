
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
	cout << "======" << St->Name << "학생(" << St->Number << "%d번)======" << endl;
	cout << "나이 : " << St->Age << ",  성별 : " << St->Gender << ",  학년 : " << St->Class << endl;
	cout << "======================" << endl;
}

void SetStudent(Student* St, int* Number)
{
	St->Number = ++(*Number);
	cout << "======" << St->Number << "번 학생======" << endl;
	cout << "이름 입력 : ";
	cin >> St->Name;
	cout << "나이 입력 : ";
	cin >> St->Age;
	cout << "성별 입력 : ";
	cin >> St->Gender;
	cout << "학년 입력 : ";
	cin >> St->Class;
}

void main()
{
	Student* Student_List[MAX];
	int Select;
	while (1)
	{
		system("cls");
		cout << "=====학생관리프로그램=====(총 인원 : " << StudentCount << ")" << endl;
		cout << "1.학생 등록" << endl;
		cout << "2.학생 목록" << endl;
		cout << "3.종료" << endl;
		cout << "입력 : " << endl;
		cin >> Select;
		switch (Select)
		{
		case 1:
			if (StudentCount + 1 >= 50)
			{
				cout << "학생정원(" << MAX << "명)이 모두 찼습니다." << endl;
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
				cout << Student_List[i]->Name << "학생 동적할당 해제 완료" << endl;
				delete Student_List[i];
			}
			return;
		}
	}
}