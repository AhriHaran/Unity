#include<iostream>
using namespace std;

class A
{
private:
	const int m_iNum1;
	int m_iNum2;

public:
	A(int Num1, int Num2) : m_iNum1(Num1)
	{
		//m_iNum2 = Num1;
		m_iNum2 = Num2;
	}
	void print()
	{
		cout << "const int m_iNum1 = " << m_iNum1 << endl;
		cout << "int m_iNum2 = " << m_iNum2 << endl;
	}
};

void  main()
{
	A a(10, 20);
	a.print();
}
