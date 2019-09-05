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
	int					m_iSkillIndex;				//스킬 인덱스
	SKILL_TYPE			m_eSkillType;				//스킬 타입
	float				m_fMaxSkillGage;			//스킬 발동 조건 게이지
	float				m_fCurSkillGage;			//현재 스킬 게이지
	bool				m_bSkillUse;				//스킬 사용 가능
	int					m_iarrSkill[SKILL_BUF_END];	//내가 가진 스킬들의 버프 인덱스
	JEngine::BitMap	*	m_cSKillBit[SKILL_END];	//스킬 버프 그림
public:
	Skill();
	void Init(string strText, string strType, int iIndex, float fMax);
	void Update(float fCurSkill);
	void Draw(JEngine::POINT pt, int iType);
	SKILL_TYPE ReturnType()
	{
		return m_eSkillType;
	}
	bool * ReturnUse()	//스킬 사용 여부 확인
	{
		return &m_bSkillUse;
	}
	int * ReturnBuf()		//스킬 여부
	{
		return m_iarrSkill;
	}
	~Skill();
};

#endif