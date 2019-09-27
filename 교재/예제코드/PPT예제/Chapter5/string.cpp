#include<stdio.h>
#include<string.h>
#include <iostream>
#include <string>
using namespace std;

//void main()
//{
//	char str[10] = "Hello";
//	printf("%s문자열의 길이 : %d\n", str, strlen(str));
//}

//void main()
//{
//	char Name[10];
//	char My_Name[10] = "SoulSeek";
//
//	strcpy(Name, My_Name);
//	printf("Name : %s\n", Name);
//	printf("My_Name : %s\n", My_Name);
//}

//void main()
//{
//	char str[10] = "Hello";
//	printf("%s\n", str);
//	strcat(str, "^^");
//	printf("%s\n", str);
//}

//void main()
//{
//	char str1[10] = "string!!";
//	char str2[10] = "string";
//
//	printf("%s == %s : %d\n", str1, str2, strcmp(str1, str2));
//	printf("%s == %s : %d\n", "abc", "abc", strcmp("abc", "abc"));
//	printf("%s == %s : %d\n", "abc", "def", strcmp("abc", "def"));
//}

//void main()
//{
//	string str = "Hello";
//	cout << "str = " << str << endl << endl;
//	cout << "새로운 문자열 입력 : ";
//	cin >> str;
//	cout << "새로운 str = " << str << endl;
//	return;
//}

//void main()
//{
//	char str[6] = "Hello";
//	char* tmp = str;
//	cout << "str = " << str << endl;
//	cout << "tmp = " << tmp << endl;
//	strcpy(str, "Bye");
//	cout << "str = " << str << endl;
//	cout << "tmp = " << tmp << endl;
//
//	return;
//}

//void main()
//{
//	char str[6] = "Hello";
//	char tmp[6];
//
//	for (int i = 0; i < 6; i++)
//		tmp[i] = str[i];
//
//	cout << "str = " << str << endl;
//	cout << "tmp = " << tmp << endl;
//
//	strcpy(str, "Bye");
//
//	cout << "str = " << str << endl;
//	cout << "tmp = " << tmp << endl;
//
//	return;
//}

//void main()
//{
//	string str = "Hello";
//
//	char* arr = (char*)str.c_str();
//
//	cout << "str = " << str << endl;
//	cout << "arr = " << arr << endl;
//
//	str = "Bye";
//
//	cout << "str = " << str << endl;
//	cout << "arr = " << arr << endl;
//
//	return;
//}

//void main()
//{
//	string str1 = "Hello";
//	string str2;
//
//	str2 = str1;
//
//	cout << "str1 = " << str1 << endl;
//	cout << "str2 = " << str2 << endl;
//
//	str1 = "Bye";
//
//	cout << "str1 = " << str1 << endl;
//	cout << "str2 = " << str2 << endl;
//
//	return;
//}

//void main()
//{
//	string s1;
//	string s2 = "123";
//	string s3 = "Hello";
//	string s4 = "안녕하세요";
//	cout << "s1 = " << s1.length() << endl;
//	cout << "s2 = " << s2.length() << endl;
//	cout << "s3 = " << s3.length() << endl;
//	cout << "s4 = " << s4.length() << endl;
//
//	//cout << "s4 = " << s4.size() << endl;
//	return;
//}

//void main()
//{
//	string str;
//	while (1)
//	{
//		system("cls");
//		cout << "Very를 입력하시오 : ";
//		cin >> str;
//		if (str == "Very")
//		{
//			str += "Good";
//			cout << str << endl;
//			break;
//		}
//		cout << "잘못 입력 다시" << endl;
//		system("pause");
//	}
//	return;
//}

void main()
{
	string str = "Education is the best provision for old age.\n - Aristotle";
	int index = str.find("provision");
	cout << "Find Provision = " << index << endl;
	cout << str[index] << endl;
	cout << str.substr(index, sizeof("provision"));
	return;
}
