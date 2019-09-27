#include<iostream>
using namespace std;

void main()
{
	int nNo;
	int* pNo = NULL;

	nNo = 10;
	pNo = &nNo;

	cout << "nNo : " << nNo << endl;
	cout << "pNo : " << pNo << endl;
	cout << "nNo : " << *pNo << endl;

	*pNo = 100;

	cout << "nNo : " << *pNo << endl;
}

//void main()
//{
//	int nNo = 10, nNo2 = 20;
//	int* pNo = NULL;
//
//	pNo = &nNo2;
//
//	*pNo += 100;
//	*pNo -= *pNo - nNo++;
//
//	pNo = &nNo2;
//	nNo *= *pNo;
//
//	(*pNo)--;
//
//	cout << "nNo : " << nNo << "nNo2 : " << nNo2 << "*pNo : " << *pNo << endl;
//}

//void main()
//{
//	int nNum = 10;
//	char cCh = 'a';
//
//	int* pNum = NULL;
//	char* pCh = NULL;
//
//	pNum = &nNum;
//	pCh = &cCh;
//
//	cout << "char 자료형 크기 : " << sizeof(cCh) << endl;
//	cout << "int 자료형 크기 : " << sizeof(nNum) << endl;
//	cout << "char* 자료형 크기 : " << sizeof(pCh) << endl;
//	cout << "int* 자료형 크기 : " << sizeof(pNum) << endl;
//}

//void main()
//{
//	int nArray[10];
//
//	int* pArray = nArray;
//	//int* pArray = &nArray[0];
//	
//	for (int i = 0; i < 10; ++i)
//	{
//		*(pArray + i) = i;
//
//		cout << "Array[" << i << "] : " << nArray[i] << endl;
//	}
//}

//void main()
//{
//	long lArray[20];
//
//	long(*p)[20] = &lArray;
//
//	(*p)[3] = 300;
//
//	cout << "lArray[3] : " << lArray[3] << endl;
//}

//void main()
//{
//	int Num = 10;
//	int *pNum = &Num;
//	int* *ppNum = &pNum;
//	int** *pppNum = &ppNum;
//
//	cout << "Num : " << Num << ", &Num : " << &Num << endl;
//	cout << "*pNum : " << *pNum << ", pNum : " << pNum << ", &pNum : " << &pNum << endl;
//
//	cout << "**ppNum : " << **ppNum << ", *ppNum : " << *ppNum;
//	cout <<", ppNum : " << ppNum << "&ppNum : " << &ppNum << endl;
//
//	cout << "***pppNum : " << ***pppNum << ", **pppNum : " << **pppNum;
//	cout << ", *pppNum : " << *pppNum << ", pppNum : " << pppNum; 
//	cout << ", &pppNum : " << &pppNum << endl;
//}

//최대공약수, 최소공배수
//void GCD_LCM(int a, int b, int* pgcd, int* plcm)
//{
//	int z;
//	int x = a;
//	int y = b;
//
//	while (true)
//	{
//		z = x % y;
//
//		if (0 == z)
//			break;
//
//		x = y;
//		y = z;
//	}
//
//	*pgcd = y;
//	*plcm = a * b / *pgcd;
//}
//
//int main()
//{
//	int gcd = 0;
//	int lcm = 0;
//
//	GCD_LCM(28, 35, &gcd, &lcm);
//
//	cout << "GCD = " << gcd << endl;
//	cout << "LCM = " << lcm << endl;
//
//	return 0;
//}