#include <iostream>
using namespace std;

void WhilePositive(int n)
{
	//인자를 출력한다.
	cout << "n = " << n << "\n";

	if (n < 0)
		return;

	WhilePositive(n - 1);

	//인자를 다시 출력한다.
	//cout << "n = " << n << "\n";
}

int main()
{
	WhilePositive(10);

	return 0;
}