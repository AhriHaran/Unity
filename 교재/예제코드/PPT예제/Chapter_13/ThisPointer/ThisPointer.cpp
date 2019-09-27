#include <iostream>
using namespace std;

class calc
{
private:
	int num1;
	int num2;
public:
	void Setdata(int num1, int num2)
	{
		//모호함의 해결
		this->num1 = num1;
		this->num2 = num2;
	}
	int GetSum()
	{
		return num1 + num2;
	}
};

void main()
{
	int su1, su2;
	calc ca;
	cin >> su1 >> su2;
	ca.Setdata(su1, su2);
	cout << su1 << " + " << su2 << " = " << ca.GetSum();
}
