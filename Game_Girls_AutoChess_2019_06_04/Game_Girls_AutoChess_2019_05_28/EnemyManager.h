#ifndef _H_ENEMY_MANAGER_H_
#define _H_ENEMY_MANAGER_H_
#include "JEngine.h"
#include "Character.h"
#include "GlobalDefine.h"
#include "Mecro.h"
#include "List.h"

typedef struct _enemy_
{
	Character		st_cChar;				//에너미 캐릭터 클래스
	bool			st_bDeploy;
}st_Enemy;

typedef List<st_Enemy> ENEMY_LIST;

class EnemyManager
{
private:
	ENEMY_LIST  * m_EneList;
	int			  m_iMaxEnemy;
public:
	EnemyManager();
	void EnemySet(int iStage);
	void Update(float fDelta);
	void Draw(int iIndex);
	void Release();
	bool LoseCheck();
	JEngine::POINT ReturnPt(int iIndex, int iPoint);
	Character* ReturnNode(int iIndex);
	bool ReturnDeploy(int iIndex);
	inline int ReturnCurEnemy()
	{
		return m_iMaxEnemy;
	}
	~EnemyManager();
};

#endif