#ifndef _H_GAME_SCENE_H_
#define _H_GAME_SCENE_H_
#include "JEngine.h"
#include "POINT.h"
#include "Mecro.h"
#include "StoreManager.h"
#include "EnemyManager.h"

typedef struct _cursor_
{
	JEngine::POINT		st_Cursorpt;		//커서 포인트
	JEngine::POINT		st_CursorDrawpt;	//커서 드로우 포인트
	JEngine::BitMap	*	st_CursorBit;		//커서 그림
}st_Cursor;


class GameScene :public JEngine::Scene
{
private:
	JEngine::BitMap		*	m_cWin;				//전역 승리 그림
	JEngine::BitMap		*	m_cLose;			//전역 실패 그림
	JEngine::BitMap		*	m_pTitleScene;		//타이틀 비트맵
	JEngine::BitMap		*	m_cTips;			//팁 비트맵
	JEngine::boolButton *	m_cTipButton;		//팁 버튼
	JEngine::POINT			m_pTitlePoint;		//타이틀 포인트
	int						m_iStage;			//스테이지
	bool					m_bGameStart;		//게임 스타트 확인
	bool					m_bTip;				//팁 버튼
	StoreManager			m_cStore;			//스토어
	EnemyManager			m_cEManger;			//적
	st_Cursor				m_stCursor;			//커서
	float					m_fSetTime;			//초반 세팅 시간
	PLAY_STATE				m_eState;
public:
	GameScene();
	virtual void Init(HWND hWnd);
	virtual bool Input(float fETime);
	virtual void Update(float fETime);
	virtual void Draw(HDC hdc);
	virtual void Release();
	void GameSet();
	void CursorUpdate();
	void HitCheck(int iAttack, int iHit, bool bPlayer);
	//bool HitCheck(Character* Attack, Character * Hit);
	virtual ~GameScene();
};

#endif