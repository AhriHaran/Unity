#include <iostream>
using namespace std;

void WhilePositive(int n)
{
	//���ڸ� ����Ѵ�.
	cout << "n = " << n << "\n";

	if (n < 0)
		return;

	WhilePositive(n - 1);

	//���ڸ� �ٽ� ����Ѵ�.
	//cout << "n = " << n << "\n";
}

int main()
{
	WhilePositive(10);

	return 0;
}