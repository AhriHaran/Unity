#include <iostream>
using namespace std;

//Exam_1
class A
{
public:
	void func1()
	{
		cout << "A�Լ� �Դϴ�." << endl;
	}
};
class B
{
public:
	void func2()
	{
		cout << "B�Լ� �Դϴ�." << endl;
	}
};

class C : public A, public B
{
public:
	void func3()
	{
		func1();
		func2();
	}
};

void main()
{
	C c;
	c.func3();
}

//Exam_2
//class A
//{
//public:
//	void func()
//	{
//		cout << "A�Լ� �Դϴ�." << endl;
//	}
//};
//class B
//{
//public:
//	void func()
//	{
//		cout << "B�Լ� �Դϴ�." << endl;
//	}
//};
//
//class C : public A, public B
//{
//public:
//	void func3()
//	{
//		func();
//		func();
//	}//����
//};
//void main()
//{
//	C c;
//	c.func3();
//}

//Exam_3
//class A
//{
//public:
//	A()
//	{
//		cout << "A�Լ� ������." << endl;
//	}
//};
//
//class B : public A
//{
//public:
//	B()
//	{
//		cout << "B�Լ� ������." << endl;
//	}
//};
//
//class C : public A
//{
//public:
//	C()
//	{
//		cout << "C�Լ� ������." << endl;
//	}
//};
//
//class D : public B, public C
//{
//public:
//	D()
//	{
//		cout << "D�Լ� ������." << endl;
//	}
//};
//
//void main()
//{
//	D d;
//}
