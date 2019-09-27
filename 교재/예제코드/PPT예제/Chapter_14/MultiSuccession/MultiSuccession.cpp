#include <iostream>
using namespace std;

//Exam_1
class A
{
public:
	void func1()
	{
		cout << "A함수 입니다." << endl;
	}
};
class B
{
public:
	void func2()
	{
		cout << "B함수 입니다." << endl;
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
//		cout << "A함수 입니다." << endl;
//	}
//};
//class B
//{
//public:
//	void func()
//	{
//		cout << "B함수 입니다." << endl;
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
//	}//에러
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
//		cout << "A함수 생성자." << endl;
//	}
//};
//
//class B : public A
//{
//public:
//	B()
//	{
//		cout << "B함수 생성자." << endl;
//	}
//};
//
//class C : public A
//{
//public:
//	C()
//	{
//		cout << "C함수 생성자." << endl;
//	}
//};
//
//class D : public B, public C
//{
//public:
//	D()
//	{
//		cout << "D함수 생성자." << endl;
//	}
//};
//
//void main()
//{
//	D d;
//}
