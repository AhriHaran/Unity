#pragma once
#include"Figure.h"
#include<iomanip>

class Circle : public Figure
{
private:
	float pi;

public:
	Circle();

	void Draw();
	void SetSize();

	~Circle();
};

