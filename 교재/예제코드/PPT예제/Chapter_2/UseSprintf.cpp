#include<iostream>
#include<stdio.h>
using namespace std;

void main()
{
	char buf[40];
	int age = 20;
	char Name[10] = "SoulSeek";
	
	sprintf(buf, "%s님의 나이는 %d살 입니다.", Name, age);
	cout << buf << endl;
}
