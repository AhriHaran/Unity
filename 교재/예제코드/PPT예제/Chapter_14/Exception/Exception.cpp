#include <iostream>
using namespace std;

//Exam_1
void main()
{
	int i = 5, j = 0;
	try
	{
		if (j == 0)
			throw j;
		cout << i / j << endl;
	}
	catch (int k)
	{
		cout << "0���� ���� �� �����ϴ�." << endl;
	}
}

//Exam_2
//void main()
//{
//	int i = 5, j = 0;
//
//	try
//	{
//		if (j == 0)
//			throw "j�� 0\n";
//
//		cout << i / j << endl;
//	}
//	catch (int k)
//	{
//		cout << " 0���� ���� �� �����ϴ�." << endl;
//	}
//	catch (const char* k)
//	{
//		cout << k << endl;
//	}
//}

//Exam_3
//void main()
//{
//	try
//	{
//		throw 'a';
//	}
//	catch (int)
//	{
//		cout << "Exception int" << endl;
//	}
//	catch (char)
//	{
//		cout << "Exception unsigned char" << endl;
//	}
//}

