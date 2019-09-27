#include <iostream>
using namespace std;

//typedef int INT;
//typedef int* PINT;
//
//void main()
//{
//	INT a = 10;
//	cout << "a = " << a << endl;
//	PINT pA = &a;
//	cout << "a 주소 : " << pA << "a 값 : " << *pA << endl;
//}

//#define PI 3.14
////const float PI = 3.14;
//
//void main()
//{
//	double tmp;
//
//	cout << "PI = " << PI << endl;
//	//cout << "PI = " << PI << endl;
//
//	tmp = PI + 1;
//	//tmp = 3.14 + 1;
//
//	cout << "tmp = " << tmp << endl;
//}

#define DebugLog(x) {cout << "들어온 값은 = " << x << endl;}

void main()
{
	int x = 5;

	DebugLog(x);
}