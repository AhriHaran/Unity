#include<stdio.h>

//void main()
//{
//	FILE* f = fopen("Test.txt", "w");
//	int Num = 123;
//	fprintf(f, "덮어 쓰기 모드  %d", Num);
//	fclose(f);
//}

//void main()
//{
//	FILE* f = fopen("Test.txt", "a");
//	int Num = 123;
//	fprintf(f, "추가 모드  %d", Num);
//	fclose(f);
//}

//void main()
//{
//	FILE* f = fopen("Test.txt", "w");
//		int Num;
//	fprintf(f, "1 2 3 4");
//	fclose(f);
//	f = fopen("Test.txt", "r");
//
//	if (f == NULL)
//		printf("해당 이름의 파일이 없습니다.");
//	else
//	{
//		while (!feof(f))
//		{
//			fscanf(f, "%d", &Num);
//			printf("%d", Num);
//		}
//		fclose(f);
//	}
//
//}

//typedef struct people
//{
//	char Name[10];
//	int Age;
//	char PhonNumber[20];
//}People;
//
//void main()
//{
//	People P1 = { "A",20,"010-1234-5678" };
//	FILE* f = fopen("People.txt", "w");
//	fprintf(f, "%s %d %s", P1.Name, P1.Age, P1.PhonNumber);
//	fclose(f);
//	f = fopen("People.txt", "r");
//	if (f == NULL)
//		printf("해당 이름의 파일이 없습니다.");
//	else
//	{
//		fscanf(f, "%s", P1.Name);
//		fscanf(f, "%d", &P1.Age);
//		fscanf(f, "%s", P1.PhonNumber);
//		printf("이름 : %s \n나이 : %d \n", P1.Name, P1.Age);
//		printf("휴대폰번호 : %s\n", P1.PhonNumber);
//	}
//	fclose(f);
//}

//typedef struct people
//{
//	char Name[10];
//	int Age;
//	char PhonNumber[20];
//}People;
//
//void main()
//{
//	People P1 = { "A",20,"010-1234-5678" };
//	FILE* f = fopen("People.txt", "w");
//	char buf[256];
//	fprintf(f, "이름 : %s나이 : %d\n", P1.Name, P1.Age);
//	fprintf(f, "휴대폰 번호 : %s", P1.PhonNumber);
//	fclose(f);
//	f = fopen("People.txt", "r");
//	if (f == NULL)
//		printf("해당 이름의 파일이 없습니다.");
//	else
//	{
//		while (!feof(f))
//		{
//			fgets(buf, sizeof(buf), f);
//			printf("%s\n", buf);
//		}
//		fclose(f);
//	}
//}

#include<Windows.h>
#include<string.h>

//typedef struct people
//{
//	char Name[10];
//	int Age;
//	char PhonNumber[20];
//}People;
//
//void main()
//{
//	People P1 = { "A",20,"010-1234-5678" };
//	FILE* f = fopen("People.txt", "w");
//	char buf[256];
//
//	memset(buf, 0, sizeof(256)); // string.h 필요
//	ZeroMemory(buf, 256); // Windows.h 필요
//
//	fprintf(f, "이름 : %s나이 : %d\n", P1.Name, P1.Age);
//	fprintf(f, "휴대폰 번호 : %s\n", P1.PhonNumber);
//	fclose(f);
//	f = fopen("People.txt", "r");
//	if (f == NULL)
//		printf("해당 이름의 파일이 없습니다.");
//	else
//	{
//		fread(buf, sizeof(buf), 1, f);
//		printf("%s\n", buf);
//	}
//	fclose(f);
//}

#include<iostream>
#include<fstream>
using namespace std;

//void main()
//{
//	ofstream save;
//	save.open("test.txt");
//	if (save.is_open())
//	{
//		save << "이건 파일 입출력" << endl << "입니다.";
//		save.close();
//	}
//
//}

#include<string>

//void main()
//{
//	ofstream save;
//	save.open("test.txt", ios::app);
//	if (save.is_open())
//	{
//		save << "\n이건 파일 입출력추가모드";
//		save.close();
//	}
//}

//void main()
//{
//	ofstream save;
//	save.open("test.txt");
//	if (save.is_open())
//	{
//		save << "이건 파일 입출력";
//		save.close();
//	}
//
//	ifstream load;
//	string str;
//	string tmp;
//	load.open("test.txt");
//	while (!load.eof())
//	{
//		load >> tmp;
//		str += tmp;
//		str += " ";
//	}
//	cout << str;
//}

void main()
{
	ofstream save;
	save.open("test.txt");
	if (save.is_open())
	{
		save << "이건 파일 입출력";
		save.close();
	}

	ifstream load;
	string str;
	string tmp;
	load.open("test.txt");

	while (!load.eof())
	{
		getline(load, str);
		cout << str << endl;
	}
}


