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
		cout << "이름 입력 : ";
		cin >> str;
		name = new char[str.length() + 1];
		strcpy(name, str.c_str());
	}

	// 시스템이 종료하거나 클래스가 해제될 때, 실행되기 때문에 
	// 해당 클래스 안에서 할당받아서 사용하고 있는 것들은 모두 해제 해줘야 한다.
	// 메모리 누수를 방지하는 방법으로 소멸자에서 할당된 리소스들을 해제해준다.
	~str()
	{
		cout << name << " 의 동적 할당을 해제합니다.\n";
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
