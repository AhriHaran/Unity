#include "Circle.h"

Circle::Circle()
{
}

void Circle::Draw()
{
	cout << fixed << setprecision(4) << "반지름 : " << pi << " 인 원형을 그렸습니다." << endl;
}

void Circle::SetSize()
{
	cout << "반지름 : ";
	cin >> pi;
}

Circle::~Circle()
{
}
