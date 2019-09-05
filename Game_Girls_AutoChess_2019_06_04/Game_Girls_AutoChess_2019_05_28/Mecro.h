#ifndef _H_MECRO_H_
#define _H_MECRO_H_
#include <time.h>
#include <fstream>
#include <iostream>
#include <string.h>
#include <algorithm>
#define ANIME_TIME  0.02f	//�ִϸ��̼� Ÿ��
#define MAX_CHAR_SIZE 5		//��ü ĳ���� ������

enum ALL_SCENCE
{
	TITLE_SCENE,	//���� Ÿ��Ʋ ��
	GAME_SCENE		//���� ��
};

enum PLAY_STATE
{
	PLAY_NONE,
	PLAY_WIN,
	PLAY_LOSE
};

enum ALL_SIZE
{
	WINDOW_SIZE_X = 1280,
	WINDOW_SIZE_Y = 800,
	TILE_MAP_SIZE = 10,

	MAP_MIDDLE_X = 40,
	MAP_MIDDLE_Y = 70,

	INDEX_EXCEL_SIZE = 49,
	INDEX_PLAYER_CHAR_START = 0,
	INDEX_PLAYER_CHAR_SIZE = 9,
	INDEX_PLAYER_CHAR_END = INDEX_PLAYER_CHAR_START + INDEX_PLAYER_CHAR_SIZE,
	//�÷��̾�� ĳ���� ����

	ENEMY_NUM = 1000,

	INDEX_ENEMY_CHAR_START = 0,
	INDEX_ENEMY_CHAR_SIZE = 4,
	INDEX_ENEMY_CHAR_END = INDEX_ENEMY_CHAR_START + INDEX_ENEMY_CHAR_SIZE,
	//���� ����

	BOSS_NUM = 10000,

	INDEX_BOSS_CHAR_START = 0,
	INDEX_BOSS_CHAR_SIZE = 6,
	INDEX_BOSS_CHAR_END = INDEX_BOSS_CHAR_START + INDEX_BOSS_CHAR_SIZE,
	//������ ����
};

enum POINT_SIZE
{
	POINT_START,
	POINT_DC_START = POINT_START,	//DC ���� ����Ʈ
	POINT_BIT_SIZE,					//�׷��� ��Ʈ�� ������
	POINT_BIT_NUM,					//��Ʈ���� ����
	POINT_CHAR_MIDDLE,				//�߰���
	POINT_END
};

enum CHARACTER_STATE
{
	STATE_START,
	STATE_WAIT = STATE_START,	//��� ����
	STATE_ATTACK,				//���� ����
	STATE_MOVE,					//���� ����
	STATE_VICTORY,				//�¸� ����
	STATE_DIE,					//���� ����
	STATE_END,					//�Ϲ� ĳ���� ����
	STATE_RELOAD = STATE_END,	//���ε�
	STATE_SET,					//����
	STATE_ALL_END
};

enum CHARACTER_STATS
{
	STATS_START,
	STATS_ATK = STATS_START,	//���ݷ�
	STATS_DEF,					//����
	STATS_RANGE,				//����
	STATS_SPEED,				//��ü �ӷ�
	STATS_END
};

enum SKILL_TYPE
{
	SKILL_TYPE_START,
	SKILL_BUF = SKILL_TYPE_START,
	SKILL_DEBUF,
	SKILL_END
};

#endif // !_H_MECRO_H_

