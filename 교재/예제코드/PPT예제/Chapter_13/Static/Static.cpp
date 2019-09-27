#include <iostream>
using namespace std;

//void counter()
//{
//	static int count = 0; //코드블럭 안에서 선언하면 그 코드 블럭 안에서만 사용가능하다. => 선언위치가 중요하다.
//	cout << ++count << endl;
//}
//
//void main()
//{
//	for (int i = 0; i < 10; i++)
//		counter();
//	
//	//count = 20;
//}

//class A
//{
//public:
//	static int count; //맴버 변수로 선언되면 사용이 쉽다.
//
//	A()
//	{
//		cout << ++count << endl;
//	}
//};
//
//// 클래스 내부에서 초기화가 불가능하다 전역변수 취급을 하기 때문에
//// 클래스 외부에서 선언해야한다.
//int A::count = 0; 
//			
//void main()
//{
//	A a1;
//	A a2;
//	A a3;
//	A::count = 10;
//	A a4;
//	A a5;
//}

class A
{
private:
	int Num;

public:
	static int count;

	A()
	{
		Num = 10;
		cout << ++count << endl;
	}

	void StringPrint()
	{
		cout << "Num : " << Num << endl;
	}

	//정적 함수 안에서는 정적 변수만 사용 가능하다.
	//정적 함수 안에서는 정적 함수만 사용 가능하다.
	static void Print()
	{
		cout << "count : " << count << endl;
		//cout << "Num : " << Num << endl;
		//StringPrint();
	}
};

int A::count = 0;

void main()
{
	A a1;
	A a2;
	A::count = 10;
	A a3;
	A a4;
	A a5;

	a5.Print();
}
