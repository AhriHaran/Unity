#pragma once
#include "Figure.h"
#include <iomanip>

class Circle : public Figure
{
private:
	float pi;

public:
	virtual void Draw();
	virtual void SetSize();

	Circle();
	~Circle();
};

