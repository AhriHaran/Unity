#include <iostream>
using namespace std;

class Point
{
private:
	int m_ipx, m_ipy;
public:
	//인자가 없는 경우
	Point()
	{
		m_ipx = 5;
		m_ipy = 5;
	}

	//인자로 전달 받는경우
	Point(int x, int y)
	{
		m_ipx = x;
		m_ipy = y;
	}

	int getPx() { return m_ipx; }
	int getPy() { return m_ipy; }
};

void main()
{
	//인자로 전달 하는게 없는 경우
	Point pt1;
	//인자로 전달 하는게 있는 경우
	Point pt2(10, 20);

	//배열로 생성해서 인자로 전달 하는 경우
	Point pt3[3] = { Point(3,5),Point(20,40),Point(50,80) };

	cout << "=== 인자로 전달 하는게 없는 경우 ===" << endl;
	cout << "pt -> x : " << pt1.getPx() << ", y : " << pt1.getPy() << endl << endl;

	cout << "=== 인자로 전달 하는게 있는 경우 ===" << endl;
	cout << "pt -> x : " << pt2.getPx() << ", y : " << pt2.getPy() << endl << endl;

	cout << "=== 배열로 생성해서 인자로 전달 하는 경우 ===" << endl;
	for (int i = 0; i < 3; i++)
		cout << "pt[" << i << "]->x : " << pt3[i].getPx() << ", y : " << pt3[i].getPy() << endl;

	cout << endl;
}
