#include <iostream>
using namespace std;

class Bumo
{
public:
	virtual void func()
	{
		cout << "Bumo함수 입니다." << endl;
	}
};
class Jasic : public Bumo
{
public:
	void func()
	{
		cout << "Jasic함수 입니다." << endl;
	}
};
void main()
{
	Jasic ob;
	Bumo* mom = &ob;
	mom->func();
}
