#include <iostream>
#include<string>
using namespace std;

class str
{
private:
	char* name;

public:
	str()
	{
		string str;
		cout << "�̸� �Է� : ";
		cin >> str;
		name = new char[str.length() + 1];
		strcpy(name, str.c_str());
	}

	// �ý����� �����ϰų� Ŭ������ ������ ��, ����Ǳ� ������ 
	// �ش� Ŭ���� �ȿ��� �Ҵ�޾Ƽ� ����ϰ� �ִ� �͵��� ��� ���� ����� �Ѵ�.
	// �޸� ������ �����ϴ� ������� �Ҹ��ڿ��� �Ҵ�� ���ҽ����� �������ش�.
	~str()
	{
		cout << name << " �� ���� �Ҵ��� �����մϴ�.\n";
		delete[] name;
	}

	void Disp()
	{
		cout << "name = " << name << endl;
	}
};

void main()
{
	str st1, st2;
	st1.Disp();
	st2.Disp();
}
