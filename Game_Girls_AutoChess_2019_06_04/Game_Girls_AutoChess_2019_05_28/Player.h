#ifndef _H_PLAYER_H_
#define _H_PLAYER_H_
#include "JEngine.h"
#include "GlobalDefine.h"
#include "Mecro.h"
#include "Character.h"
#include "List.h"
#include "SingleTon.h"
#include "Gage.h"
#include "boolButton.h"
typedef struct _profile_char_
{
	Character				st_cChar;				//ĳ����
	JEngine::POINT			st_cBackPoint;			//������ �� �׶��� ����Ʈ
	JEngine::RECT			st_cRect;				//��Ʈ üũ
	bool					st_bDrawSD;				//SD ĳ���͸� �׷����
	bool					st_bDeploy;				//�� ��ġ �Ϸ�
	bool					st_bSkill;				//��ų ���
}st_ProChar;										//ĳ���� ������ â

typedef List<st_ProChar> MY_LIST;

typedef struct _commander_
{
	Anime					st_carrAinme[STATE_ALL_END];	//���ְ� ĳ������ �ִϸ��̼�
	Anime					st_carrEffect[CHAR_H_A_END];	//��ų ����Ʈ �ִϸ��̼�
	CHARACTER_STATE			st_eState;						//���ְ� ĳ������ ����
	JEngine::POINT			st_cCharPoint[CHAR_NAME_XY];	//ĳ���� ����Ʈ
	JEngine::POINT			st_cEffectPT[3];				//����Ʈ ����Ʈ
	JEngine::POINT			st_cFirePT;						//�߻� ����Ʈ
	JEngine::RECT			st_cRect;						//ĳ���� ��Ʈ
	JEngine::BitMap		*	st_cComProfile;					//ĳ���� ������ �̹���
	JEngine::boolButton	*	st_cSkillButton;				//��ų ��ư
	bool					st_bDeploy;						//�� ��ġ �Ϸ�
	bool					st_bSet;						//��ġ �Ϸ�
	bool					st_bShot;						//��� ����
	bool					st_barrEffect[3];				//����Ʈ Ȯ��
	bool					st_bFire;						//�߻�
	Gage				*	st_cGage;						//��ų ������
	float					st_cCurSkill;					//���� ��ų ������
	float					st_cMaxSkill;					//�ƽ� ��ų ������
	int						st_iCurEffect;					//����Ʈ
	int						st_iAtk;						//��ų ���ݷ�
}st_Commander;
/*
���ְ� ĳ���ͷ�, ĳ���Ͱ� ����ϸ� ������ ������ ���� ����.
���ְ� ĳ���ʹ� ��ų ó�� ���� ������ �����ϴ�
*/


class Player : public SingleTon <Player>
{
private:
	JEngine::BitMap		*	m_cSkillBit;		//��ų ��� ���� ���� ǥ��
	JEngine::BitMap		*	m_cAutoON;			//���� ��ư ON
	JEngine::BitMap		*	m_cAutoOFF;			//���� ��ư OFF
	JEngine::BitMap		*	m_cBackBit;			//�� ������ ��Ʈ��
	JEngine::boolButton	*	m_cAutoButton;		//���� ��ư
	JEngine::POINT			m_cAutoPT;
	int						m_iGold;			//���� ���� ������ ���
	int						m_iCharNum;			//���� ���� ���� ĳ����
	int						m_iSkillNum;		//��ų �ߵ� ĳ����
	bool					m_bAuto;			//���� üũ
	MY_LIST				*	m_MyList;			//�� ĳ���� ����Ʈ
	st_Commander		*	m_stCom;			//���ְ� ĳ����
public:
	Player();
	void Init();
	void BuyChar(int iIndex, int iGold);		//ĳ���� ����
	void Update(float fDelta, bool bGame);
	void CommanderSet(int iX, int iY);
	void Draw();
	void Draw(int iIndex);
	void KeyInput(JEngine::POINT Cursorpt);
	void Release();
	void ComRelease();
	JEngine::POINT ReturnPt(int iIndex, int iPoint);
	bool LoseCheck();
	Character* ReturnNode(int iIndex);
	bool ReturnDeploy(int iIndex);
	st_Commander * ReturnCom()
	{
		return m_stCom;
	}
	void Reset(int iStage);
	inline int ReturnCurChar()
	{
		return m_iCharNum;
	}
	int * ReturnSkillNum()
	{
		return &m_iSkillNum;
	}
	~Player();
};


#endif // !_H_PLAYER_H_