#include<iostream>
#include<stdio.h>
using namespace std;

void main()
{
	char buf[40];
	int age = 20;
	char Name[10] = "SoulSeek";
	
	sprintf(buf, "%s���� ���̴� %d�� �Դϴ�.", Name, age);
	cout << buf << endl;
}
