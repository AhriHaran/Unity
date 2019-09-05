#ifndef _H_GAME_SCENE_H_
#define _H_GAME_SCENE_H_
#include "JEngine.h"
#include "POINT.h"
#include "Mecro.h"
#include "StoreManager.h"
#include "EnemyManager.h"

typedef struct _cursor_
{
	JEngine::POINT		st_Cursorpt;		//Ŀ�� ����Ʈ
	JEngine::POINT		st_CursorDrawpt;	//Ŀ�� ��ο� ����Ʈ
	JEngine::BitMap	*	st_CursorBit;		//Ŀ�� �׸�
}st_Cursor;


class GameScene :public JEngine::Scene
{
private:
	JEngine::BitMap		*	m_cWin;				//���� �¸� �׸�
	JEngine::BitMap		*	m_cLose;			//���� ���� �׸�
	JEngine::BitMap		*	m_pTitleScene;		//Ÿ��Ʋ ��Ʈ��
	JEngine::BitMap		*	m_cTips;			//�� ��Ʈ��
	JEngine::boolButton *	m_cTipButton;		//�� ��ư
	JEngine::POINT			m_pTitlePoint;		//Ÿ��Ʋ ����Ʈ
	int						m_iStage;			//��������
	bool					m_bGameStart;		//���� ��ŸƮ Ȯ��
	bool					m_bTip;				//�� ��ư
	StoreManager			m_cStore;			//�����
	EnemyManager			m_cEManger;			//��
	st_Cursor				m_stCursor;			//Ŀ��
	float					m_fSetTime;			//�ʹ� ���� �ð�
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