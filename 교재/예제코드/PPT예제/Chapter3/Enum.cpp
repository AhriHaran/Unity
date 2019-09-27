#include <iostream>
using namespace std;

//enum JOB
//{
//	JOB_WARRIOR,
//	JOB_SORCERER,
//	JOB_ARCHER,
//};
//
//enum GAME_STATE
//{
//	GAME_START = 10,
//	GAME_PLAY,
//	GAME_STOP,
//
//	GAME_END = 99,
//};
//
//void main()
//{
//	JOB jobKind = JOB_WARRIOR;
//	GAME_STATE gameState = GAME_START;
//
//	cout << "JOBÀÇ Å©±â : " << sizeof(jobKind) << endl;
//}

enum GAME_STATE
{
	GAME_START = 10,
	GAME_PLAY,
	GAME_STOP,

	GAME_END = 99,
};

GAME_STATE gameState = GAME_START;

bool bGameStart = true;
bool bGamePlay = false;
bool bGameStop = false;
bool bGameEnd = false;

void main()
{
	if (bGameStart)
	{

	}
	else
	{

	}

	if (bGamePlay)
	{

	}
	else
	{

	}

	if (bGameStop)
	{

	}
	else
	{

	}

	if (bGameEnd)
	{

	}
	else
	{

	}

	switch (gameState)
	{
		case GAME_START:
			break;
		case GAME_PLAY:
			break;
		case GAME_STOP:
			break;
		case GAME_END:
			break;
	}
}