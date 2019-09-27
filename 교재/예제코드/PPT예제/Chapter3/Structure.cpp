#include <iostream>
using namespace std;

struct Character
{
	char NickName[12];
	int Level;
	int Hp;
};

void main()
{
	struct Character player = { "SoulSeek", 99, 10000 };

	cout << "닉네임 : " << player.NickName << ", 레벨 : " 
		 << player.Level << ", 체력 : " << player.Hp << endl;
}

//struct Character
//{
//	char NickName[12];
//	int Level;
//	int Hp;
//};
//
//void ShowPlayerInfo(Character p)
//{
//	cout << "★☆★☆★☆★☆★☆★☆★☆" << endl;
//	cout << "닉네임 : " << p.NickName << endl;
//	cout << "레벨 : " << p.Level << endl;
//	cout << "체력 : " << p.Hp << endl;
//	cout << "★☆★☆★☆★☆★☆★☆★☆" << endl;
//}
//
//void main()
//{
//	Character P_List[3];
//
//	for (int i = 0; i < 3; i++)
//	{
//		cout << "★☆★☆" << i << "번째 ★☆★☆" << endl;
//		cout << "닉네임 입력 : ";
//		cin >> P_List[i].NickName;
//		cout << "레벨 입력 : ";
//		cin >> P_List[i].Level;
//		cout << "체력 입력 : ";
//		cin >> P_List[i].Hp;
//	}
//
//	for (int i = 0; i < 3; i++)
//	{
//		ShowPlayerInfo(P_List[i]);
//	}
//}

//struct Rectangle
//{
//	int x, y;
//	int width, height;
//};
//
//void main()
//{
//	Rectangle rc = { 100, 100, 50, 50 };
//
//	Rectangle* p = &rc;
//
//	p->y = 250; //(*p).x와 같은 의미 -  이미 배열과 포인터의 관계에서 배웠다.
//
//	cout << "rc = (" << rc.x << ", " << rc.y << ", ";
//	cout << rc.width << ", " << rc.height << ")" << endl;
//}

//struct Dizzy
//{
//	int id;
//	Dizzy* p;
//};
//
//void main()
//{
//	Dizzy a, b, c;
//
//	a.id = 1;
//	a.p = &b;
//	b.id = 2;
//	b.p = &c;
//	c.id = 3;
//	c.p = &a;
//
//	cout << "a.id : " << a.id << endl;
//	cout << "b.id : " << a.p->id << endl;
//	cout << "c.id : " << a.p->p->id << endl;
//	cout << "a.id : " << a.p->p->p->id << endl;
//}

//struct Point
//{
//	int x, y;
//};
//
//double Distance(Point* p1, Point* p2);
//
//void main()
//{
//	Point a = { 0, 0 };
//	Point b = { 3, 4 };
//
//	double dist_a_b = Distance(&a, &b);
//}
//
//double Distance(Point* p1, Point* p2)
//{
//	cout << "p1 = " << p1->x << ", " << p1->y << endl;
//
//	cout << "p2 = " << p2->x << ", " << p2->y << endl;
//
//	return 0;
//}
