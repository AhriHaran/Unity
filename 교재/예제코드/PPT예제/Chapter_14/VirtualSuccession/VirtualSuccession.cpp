#include <iostream>
using namespace std;

class A
{
public:
	A()
	{
		cout << "A함수 생성자." << endl;
	}
};
class B : virtual public A
{
public:
	B()
	{
		cout << "B함수 생성자." << endl;
	}
};
class C : virtual public A
{
public:
	C()
	{
		cout << "C함수 생성자." << endl;
	}
};
class D : public B, public C
{
public:
	D()
	{
		cout << "D함수 생성자." << endl;
	}
};

void main()
{
	D d;
}
