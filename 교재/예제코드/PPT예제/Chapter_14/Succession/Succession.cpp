#include <iostream>
using namespace std;

//Exam_1
class A
{
public:
	A()
	{
		cout << "A Class »ý¼º" << endl;
	}
};
class B : public A
{
public:
	B()
	{
		cout << "B Class »ý¼º" << endl;
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
//		cout << "AÀÇ »ý¼ºÀÚ È£Ãâ" << endl;
//	}
//	~A()
//	{
//		cout << "AÀÇ ¼Ò¸êÀÚ È£Ãâ" << endl;
//	}
//};
//
//class B : public A
//{
//public:
//	B()
//	{
//		cout << "BÀÇ »ý¼ºÀÚ È£Ãâ" << endl;
//	}
//	~B()
//	{
//		cout << "BÀÇ ¼Ò¸êÀÚ È£Ãâ" << endl;
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
//		cout << cnt << "¹ø Â¢´Ù" << endl;
//	}
//	void speak()
//	{
//		cout << "Â¢´Ù" << endl;
//	}
//};
//class Dog : public Mammal
//{
//public:
//	void speak()
//	{
//		cout << "¸Û¸Û" << endl;
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
//		cout << cnt << "¹ø Â¢´Ù" << endl;
//	}
//	void speak()
//	{
//		cout << " Â¢´Ù" << endl;
//	}
//};
//class Dog : public Mammal
//{
//public:
//	void speak()
//	{
//		cout << "¸Û¸Û" << endl;
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

