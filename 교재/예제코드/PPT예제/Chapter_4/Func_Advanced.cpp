#include <iostream>
using namespace std;
//====================����Ʈ �Ű� ����===================//
//void SetFramesPerSec(int fps = 60)
//{
//	cout << "FPS = " << fps << endl;
//}
//
//void main()
//{
//	SetFramesPerSec();
//	//SetFramesPerSec(10);
//}


//void func(int n = 10)
//{
//	int Sum = 0;
//
//	for (int i = 1; i <= n; i++)
//		Sum += i;
//
//	cout << "1 ~ " << n << "������ �� �� : " << Sum << endl;
//}
//
//void main()
//{
//	func(100);
//	func();
//}



//void Addfunc(int& x, int y = 2)
//{
//	int count = 0;
//
//	while (count < y)
//	{
//		x *= x;
//
//		count++;
//	}
//}
//
//void main()
//{
//	int a;
//
//	cout << "�Է°� : ";
//	cin >> a;
//
//	Addfunc(a);
//	//Addfunc(a, 10);
//
//	cout << "��°� : " << a << endl;
//}


//struct Item
//{
//	int dmg;
//	int str;
//	int min;
//};
//
//struct Info
//{
//	int level;
//	int hp;
//	int mp;
//	Item item;
//};
//
//Info userInfo = { 0 };
//
//void UserInfo(int level_, int hp_, int mp_, Item item_ = { 0 })
//{
//	userInfo.level = level_;
//	userInfo.hp = hp_;
//	userInfo.mp = mp_;
//	userInfo.item = item_;
//
//	cout << "=======User Info========" << endl;
//	cout << "level : " << userInfo.level << endl;
//	cout << "hp : " << userInfo.hp << endl;
//	cout << "mp : " << userInfo.mp << endl;
//}
//
//void main()
//{
//	UserInfo(11, 1000, 1000);
//	//UserInfo(11, 1000, 1000, item);
//}

//====================�����ε�===================//

//void func()
//{
//	cout << "func1() �Լ��� ȣ���Ͽ����ϴ�." << endl;
//}
//
//void func(int a)
//{
//	cout << "func1(int a) �Լ��� ȣ���Ͽ����ϴ�." << endl;
//}
//
//void main()
//{
//	func();
//	func(10);
//}

//struct Info
//{
//	int level;
//	int hp;
//	int mp;
//};
//
//Info userInfo = { 0 };
//
//void UserInfo(int level_)
//{
//	userInfo.level = level_;
//
//	cout << "=======User Info========" << endl;
//	cout << "level : " << userInfo.level << endl;
//	cout << "hp : " << userInfo.hp << endl;
//	cout << "mp : " << userInfo.mp << endl;
//}
//
//void UserInfo(int hp_, int mp_)
//{
//	userInfo.hp = hp_;
//	userInfo.mp = mp_;
//
//	cout << "=======User Info========" << endl;
//	cout << "level : " << userInfo.level << endl;
//	cout << "hp : " << userInfo.hp << endl;
//	cout << "mp : " << userInfo.mp << endl;
//}
//
//void main()
//{
//	UserInfo(11);
//	UserInfo(1000, 1000);
//}

//====================�Լ�������===================//

//void func1()
//{
//	cout << "�Լ� ������1 ȣ��" << endl;
//}
//
//void func2()
//{
//	cout << "�Լ� ������2 ȣ��" << endl;
//}
//
//void main()
//{
//	void(*p) ();
//
//	p = &func1;
//	p();
//
//	p = &func2;
//	p();
//}

//void func1(int x, int y)
//{
//	cout << x << " + " << y << " = " << x + y << endl;
//}
//
//void func2(void(*p)(int x, int y))
//{
//	(*p)(10, 15);
//}
//
//void main()
//{
//	void(*p) (int x, int y);
//
//	p = &func1;
//	func2(p);
//}

//typedef void(*FUNC)(int, int);
//
//void func1(int x, int y)
//{
//	cout << x << " + " << y << " = " << x + y << endl;
//}
//
//void func2(FUNC p)
//{
//	p(10, 15);
//}
//void main()
//{
//	FUNC p;
//
//	p = &func1;
//	func2(p);
//}

//====================���ȣ��===================//
//void WhilePositive(int n)
//{
//	//���ڸ� ����Ѵ�.
//	cout << "n = " << n << "\n";
//
//	if (n < 0)
//		return;
//
//	WhilePositive(n - 1);
//
//	//���ڸ� �ٽ� ����Ѵ�.
//	//cout << "n = " << n << "\n";
//}
//
//void main()
//{
//	WhilePositive(10);
//}