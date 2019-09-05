#ifndef _H_CHARACTER_H_
#define _H_CHARACTER_H_
#include "JEngine.h"
#include "Anime.h"
#include "Skill.h"
#include "Gage.h"
enum CHAR_POINT
{
	CHAR_START,
	CHAR_XY = CHAR_START,		//ĳ���� �׸� ��ǥ
	CHAR_MIDDLE_XY,				//ĳ���� �߽� ��ǥ
	CHAR_PORT_XY,				//ĳ���� �ʻ�ȭ xy
	CHAR_NAME_XY,				//ĳ���� ���� xy
	CHAR_STATS_XY,				//ĳ���� ���� xy
	CHAR_END
};

enum CHAR_PORTRAITS
{
	PORTRAITS_START,
	PORTRAITS_NORMAL = PORTRAITS_START,
	PORTRAITS_DEMAGE,
	PORTRAITS_END
};
//ĳ���� �ʻ�ȭ

enum CHAR_HIT_ATTACK
{
	CHAR_H_A_START,
	CHAR_ATTACK = CHAR_H_A_START,
	CHAR_HIT,
	CHAR_H_A_END
};

enum PLAYER_GAGE
{
	GAGE_START,
	GAGE_HP = GAGE_START,
	GAGE_SKILL,
	GAGE_END
};

//���� ���� ���� ������ ������ ĳ���͵��� ����

//��ų�� ���� �������� HP, ATK, DEF, SkillGage, ����

class Character
{
private:
	string				m_strName;					//ĳ���� �̸�
	string				m_strType;					//�ѱ�Ÿ��

	Anime				m_cAnime[STATE_END];		//�ִϸ��̼�
	Anime				m_cHitAttack[CHAR_H_A_END];	//���� �ǰ� �ִϸ��̼�
	Gage				m_cArrGage[GAGE_END];		//������
	Skill		*		m_cSkill;					//��ų

	int					m_iarrStats[STATS_END];		//����
	int					m_iarrSkillUP[SKILL_BUF_END];//��� ��ų ������
	int					m_iIndex;					//���� �ε���
	int					m_iRare;					//���
	int					m_iGold;					//����
	int					m_iSkillType;				//��ų�� ���� ���θ� Ȯ��

	float				m_fHp;						//ĳ������ MAX HP
	float				m_fCurHp;					//ĳ������ ���� HP
	float				m_fSkillGage;				//ĳ���� ��ų ������
	float				m_fCurSkillGage;			//ĳ������ ���� ��ų ������
	float				m_fAtkSpeed;				//ĳ���� ���� ���ǵ�
	float				m_fCurSpeed;				//ĳ������ ���� ���

	CHARACTER_STATE		m_eState;					//ĳ���� ����
	CHAR_PORTRAITS		m_ePort;					//�ʻ�ȭ ����
	SKILL_TYPE			m_eType;					//�ش� ĳ���Ͱ� ���� ��ų ����, �����

	JEngine::BitMap	*	m_carrPort[PORTRAITS_END];	//ĳ���� �ʻ�ȭ
	JEngine::BitMap *	m_cRangeBit;				//�����Ÿ� ��Ʈ��
	JEngine::POINT		m_carrPoint[CHAR_END];		//ĳ���� �� ��ο�
	JEngine::RECT		m_cRect;					//ĳ���� ��Ʈ

	bool				m_bAttack;					//���� ����
	bool				m_bHit;						//�´� ��
	bool				m_bAnime;					//�ִϸ��̼� ��� ����
	bool				m_bSkill;					//��ų �ߵ�
public:
	Character();
	void Init(string FileName, string strIndex, float fDelta);	//�� ���� �̸��� �����ش�. �ε��� ���� �ѹ�
	void AnimeSet(ifstream * Load, Anime * pAni, int iSize, int iAnimeSize);
	void GageSet(int iWidth, JEngine::POINT pt);
	void Update(float fDelta);					//������Ʈ
	void Update();								//������ ������Ʈ
	void Draw();								//ĳ���� ��ο�
	void StatsDraw();							//���� ��ο�
	void NameDraw();							//�̸� ��ο�
	void PortDraw();							//�ʻ�ȭ ��ο�
	void RangeDraw();							//��Ÿ����� ��ο�
	void GageDraw();
	void PointSet(int iIndex, int iX, int iY);	//ĳ���� ��ġ ����
	void SkillUP(int * iarr, SKILL_TYPE eType);
	void SkillReset();
	void HitCheck(int iATK);
	bool RangeCheck(JEngine::POINT Ept);
	bool RectCheck(JEngine::POINT pt);
	void AnimeReset()
	{
		m_cAnime[m_eState].Reset();
	}
	JEngine::POINT ReturnPT(int iIndex)
	{
		return m_carrPoint[iIndex];				//����Ʈ ��ȯ
	}
	JEngine::POINT ReturnAPT(int iIndex)
	{
		return m_cAnime[m_eState].ReturnPT(iIndex);//�ִϸ��̼� ����Ʈ ��ȯ
	}
	Skill * ReturnSkill()
	{
		return m_cSkill;
	}
	string ReturnType()
	{
		return m_strType;
	}
	bool* ReturnAttack()
	{
		return &m_bAttack;						//���� ���� ���¸� ��ȯ �޴´�. ����Ʈ ����
	}
	CHARACTER_STATE* ReturnState()				//ĳ���� ���� ��ȭ
	{
		return &m_eState;
	}
	inline int ReturnStats(int iIndex)
	{
		return m_iarrStats[iIndex];
	}
	inline int ReturnIndex()
	{
		return m_iIndex;
	}
	inline int ReturnGold()
	{
		return m_iGold;
	}
	int ReturnPortX()
	{
		return m_carrPort[m_ePort]->GetWidth();
	}
	int ReturnPortY()
	{
		return m_carrPort[m_ePort]->GetHeight();
	}
	int ReturnAnimeX()
	{
		return m_cAnime[m_eState].GetSizeX();
	}
	int ReturnAnimeY()
	{
		return m_cAnime[m_eState].GetSizeY();
	}
	~Character();
};

#endif