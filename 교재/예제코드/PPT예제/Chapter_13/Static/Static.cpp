#include <iostream>
using namespace std;

//void counter()
//{
//	static int count = 0; //�ڵ�� �ȿ��� �����ϸ� �� �ڵ� �� �ȿ����� ��밡���ϴ�. => ������ġ�� �߿��ϴ�.
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
//	static int count; //�ɹ� ������ ����Ǹ� ����� ����.
//
//	A()
//	{
//		cout << ++count << endl;
//	}
//};
//
//// Ŭ���� ���ο��� �ʱ�ȭ�� �Ұ����ϴ� �������� ����� �ϱ� ������
//// Ŭ���� �ܺο��� �����ؾ��Ѵ�.
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

	//���� �Լ� �ȿ����� ���� ������ ��� �����ϴ�.
	//���� �Լ� �ȿ����� ���� �Լ��� ��� �����ϴ�.
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
