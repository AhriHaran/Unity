#include <iostream>
using namespace std;

//Exam_1
class A
{
public:
	A()
	{
		cout << "A Class ����" << endl;
	}
};
class B : public A
{
public:
	B()
	{
		cout << "B Class ����" << endl;
	}
};

void main()
{
	B b;
}

//Exam_2
//class A
//{
//public:
//	A()
//	{
//		cout << "A�� ������ ȣ��" << endl;
//	}
//	~A()
//	{
//		cout << "A�� �Ҹ��� ȣ��" << endl;
//	}
//};
//
//class B : public A
//{
//public:
//	B()
//	{
//		cout << "B�� ������ ȣ��" << endl;
//	}
//	~B()
//	{
//		cout << "B�� �Ҹ��� ȣ��" << endl;
//	}
//};
//
//void main()
//{
//	B b;
//}

//Exam_3
//class A
//{
//private:
//	int m_ia;
//
//protected:
//	int m_ib;
//
//public:
//	int m_ic;
//	void Test()
//	{
//		m_ia = 10;
//		m_ib = 10;
//		m_ic = 10;
//	}
//};
//
//class B : public A
//{
//	void Test()
//	{
//		m_ia = 10;
//		m_ib = 10;
//		m_ic = 10;
//	}
//};
//
//class C : public B
//{
//	void Test()
//	{
//		m_ia = 10;
//		m_ib = 10;
//		m_ic = 10;
//	}
//};
//
//void main()
//{
//	B b;
//	b.m_ia = 10;
//	b.m_ib = 10;
//	b.m_ic = 10;
//}

//Exam_4 - Overriding
//class Mammal
//{
//public:
//	void speak(int cnt)
//	{
//		cout << cnt << "�� ¢��" << endl;
//	}
//	void speak()
//	{
//		cout << "¢��" << endl;
//	}
//};
//class Dog : public Mammal
//{
//public:
//	void speak()
//	{
//		cout << "�۸�" << endl;
//	}
//};
//void main()
//{
//	Mammal dongmul;
//	Dog jindo;
//	dongmul.speak();
//	dongmul.speak(3);
//	jindo.speak();
//	//jindo.speak(5);
//}

//Exam_5 - UpCasting
//class Mammal
//{
//public:
//	void speak(int cnt)
//	{
//		cout << cnt << "�� ¢��" << endl;
//	}
//	void speak()
//	{
//		cout << " ¢��" << endl;
//	}
//};
//class Dog : public Mammal
//{
//public:
//	void speak()
//	{
//		cout << "�۸�" << endl;
//	}
//};
//
//void main()
//{
//	Mammal* ptr;
//	Dog jindo;
//	ptr = &jindo;
//	ptr->speak();
//	ptr->speak(5);
//}

