#pragma once
#include"Figure.h"

class Triangle : public Figure
{
public:
	Triangle();

	void Draw();
	void SetSize();

	~Triangle();
};

