#include "EnemyManager.h"
#include "MapManager.h"
#include "InputManager.h"
#include "ResoucesManager.h"
#include "Player.h"

EnemyManager::EnemyManager()
{
	memset(&m_EneList, NULL, sizeof(m_EneList));
	m_iMaxEnemy = 0;
}

void EnemyManager::EnemySet(int iStage)
{
	int iBoss = 0, iRnum = 0, iRX = 8, iRY = 0;
	char buf[256];
	JEngine::POINT 	sizept, pt, Mpt;
	m_EneList = new ENEMY_LIST;	//에너미 노드 리스트 생성
	//스테이지 별로 적을 셋팅

	if (iStage < 10)
		m_iMaxEnemy = 3;
	else if(iStage < 20)
	{
		iBoss = 1;
		m_iMaxEnemy = 3;
	}
	else if (iStage < 30)
	{
		iBoss = 2;
		m_iMaxEnemy = 4;
	}
	else
	{
		iBoss = 3;
		m_iMaxEnemy = 5;
	}

	for (int i = 0; i < m_iMaxEnemy; i++)
	{
		st_Enemy * Node = m_EneList->CreateNode();
		if (i < iBoss)
		{
			while (true)
			{
				iRnum = rand() % INDEX_BOSS_CHAR_END;
				if (iRnum >= INDEX_BOSS_CHAR_START && iRnum < INDEX_BOSS_CHAR_END)
				{
					iRnum += BOSS_NUM;
					break;
				}
			}
			sprintf(buf, "FILE//Boss.csv");
		}
		else
		{
			while (true)
			{
				iRnum = rand() % INDEX_ENEMY_CHAR_END;
				if (iRnum >= INDEX_ENEMY_CHAR_START && iRnum < INDEX_ENEMY_CHAR_END)
				{
					iRnum += ENEMY_NUM;
					break;
				}
			}
			sprintf(buf, "FILE//Enemy.csv");
		}

		Node->st_cChar.Init(buf, to_string(iRnum), ANIME_TIME);	//에너미 캐릭터 셋팅
		while (true)
		{
			iRY = rand() % TILE_MAP_SIZE + 1;
			if (iRY >= 1 && iRY < TILE_MAP_SIZE)
			{
				if (MapManager::GetInstance()->KeyInput(MapManager::GetInstance()->XYPoint(iRX, iRY), pt))
				{
					Mpt = Node->st_cChar.ReturnAPT(POINT_CHAR_MIDDLE);	//캐릭터 그림 중심 좌표
					//pt는 맵의 타일의 시작 위치
					pt.x += MAP_MIDDLE_X;
					pt.y += MAP_MIDDLE_Y;
					//내가 놓고자하는 맵의 중심 좌표
					Node->st_cChar.PointSet(CHAR_XY, (pt.x - Mpt.x), (pt.y - Mpt.y));
					break;
				}
			}
		}	//캐릭터 위치 셋팅

		Node->st_bDeploy = true;
		m_EneList->PushNode(Node);
	}
}


void EnemyManager::Update(float fDelta)
{
	for (int i = 0; i < m_iMaxEnemy; i++)
	{
		st_Enemy * Node = m_EneList->FindNode(i);
		if(Node->st_bDeploy)
			Node->st_cChar.Update(fDelta);
	}
}

void EnemyManager::Draw(int iIndex)
{
	st_Enemy * Node = m_EneList->FindNode(iIndex);
	if (Node->st_bDeploy)
		Node->st_cChar.Draw();
}

void EnemyManager::Release()
{
	if (m_EneList != NULL)
	{
		m_EneList->Release();
		delete m_EneList;
		memset(&m_EneList, NULL, sizeof(m_EneList));
		m_iMaxEnemy = 0;
	}
}

JEngine::POINT EnemyManager::ReturnPt(int iIndex, int iPoint)
{
	st_Enemy * Node = m_EneList->FindNode(iIndex);
	return Node->st_cChar.ReturnPT(iPoint);
}

Character* EnemyManager::ReturnNode(int iIndex)
{
	st_Enemy* Node = m_EneList->FindNode(iIndex);
	return &Node->st_cChar;
}

bool EnemyManager::ReturnDeploy(int iIndex)
{
	st_Enemy * Node = m_EneList->FindNode(iIndex);
	return Node->st_bDeploy;
}

bool EnemyManager::LoseCheck()
{
	int iCount = 0;
	CHARACTER_STATE* eState;
	for (int i = 0; i < m_iMaxEnemy; i++)
	{
		st_Enemy * Node = m_EneList->FindNode(i);
		eState = Node->st_cChar.ReturnState();

		if (*eState == STATE_DIE)
		{
			Node->st_bDeploy = false;
			iCount++;
		}
	}
	if (iCount >= m_iMaxEnemy)
		return true;
	return false;
}

EnemyManager::~EnemyManager()
{
}
