#include "Circle.h"

Circle::Circle()
{
}

void Circle::Draw()
{
	cout << fixed << setprecision(4) << "������ : " << pi << " �� ������ �׷Ƚ��ϴ�." << endl;
}

void Circle::SetSize()
{
	cout << "������ : ";
	cin >> pi;
}

Circle::~Circle()
{
}
