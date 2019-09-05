#ifndef _H_SKILL_H_
#define _H_SKILL_H_
#include "JEngine.h"
#include "ResoucesManager.h"
#include "GlobalDefine.h"
#include "Mecro.h"
#define SKILL_BUF_END 5
class Skill
{
private:
	int					m_iSkillIndex;				//��ų �ε���
	SKILL_TYPE			m_eSkillType;				//��ų Ÿ��
	float				m_fMaxSkillGage;			//��ų �ߵ� ���� ������
	float				m_fCurSkillGage;			//���� ��ų ������
	bool				m_bSkillUse;				//��ų ��� ����
	int					m_iarrSkill[SKILL_BUF_END];	//���� ���� ��ų���� ���� �ε���
	JEngine::BitMap	*	m_cSKillBit[SKILL_END];	//��ų ���� �׸�
public:
	Skill();
	void Init(string strText, string strType, int iIndex, float fMax);
	void Update(float fCurSkill);
	void Draw(JEngine::POINT pt, int iType);
	SKILL_TYPE ReturnType()
	{
		return m_eSkillType;
	}
	bool * ReturnUse()	//��ų ��� ���� Ȯ��
	{
		return &m_bSkillUse;
	}
	int * ReturnBuf()		//��ų ����
	{
		return m_iarrSkill;
	}
	~Skill();
};

#endif