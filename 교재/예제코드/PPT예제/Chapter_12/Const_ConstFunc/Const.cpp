#include<iostream>
using namespace std;

class Point
{
private:
	int m_ix;
	//const int m_ix; 
	int m_iy;
public:
	Point() {}
	void SetNum(int x, int y)
	{
		m_ix = x;
		m_iy = y;
	}
	void ShowNum() //const
	{
		cout << "x = " << m_ix << endl;
		cout << "y = " << m_iy << endl;
		m_ix = 30;
		m_iy = 40;
		cout << "x = " << m_ix << endl;
	}
	/*const*/ int* GetNum() //const
	{
		m_ix = 50;
		return &m_ix;
	}
};

void main()
{
	Point p;
	p.SetNum(10, 20);
	p.ShowNum();
	*(p.GetNum()) = 100;

	//cout << *(p.GetNum()) << endl;

	p.ShowNum();
}