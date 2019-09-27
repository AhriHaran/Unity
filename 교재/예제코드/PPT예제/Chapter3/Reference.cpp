#include <iostream>
using namespace std;

int main()
{
	int target = 20;

	int& ref = target;

	cout << "ref	= " << ref << "\n";
	cout << "target = " << target << "\n";
	cout << "&ref	= " << &ref << "\n";
	cout << "&target	= " << &target << "\n";

	ref = 100;

	cout << "ref	= " << ref << "\n";
	cout << "target = " << target << "\n";

	return 0;
}
