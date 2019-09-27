#include <iostream>
using namespace std;

class Point
{
private:
	int m_ipx, m_ipy;
public:
	//���ڰ� ���� ���
	Point()
	{
		m_ipx = 5;
		m_ipy = 5;
	}

	//���ڷ� ���� �޴°��
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
	//���ڷ� ���� �ϴ°� ���� ���
	Point pt1;
	//���ڷ� ���� �ϴ°� �ִ� ���
	Point pt2(10, 20);

	//�迭�� �����ؼ� ���ڷ� ���� �ϴ� ���
	Point pt3[3] = { Point(3,5),Point(20,40),Point(50,80) };

	cout << "=== ���ڷ� ���� �ϴ°� ���� ��� ===" << endl;
	cout << "pt -> x : " << pt1.getPx() << ", y : " << pt1.getPy() << endl << endl;

	cout << "=== ���ڷ� ���� �ϴ°� �ִ� ��� ===" << endl;
	cout << "pt -> x : " << pt2.getPx() << ", y : " << pt2.getPy() << endl << endl;

	cout << "=== �迭�� �����ؼ� ���ڷ� ���� �ϴ� ��� ===" << endl;
	for (int i = 0; i < 3; i++)
		cout << "pt[" << i << "]->x : " << pt3[i].getPx() << ", y : " << pt3[i].getPy() << endl;

	cout << endl;
}
