#include "Skill.h"



Skill::Skill()
{
	m_iSkillIndex = 0;
	m_eSkillType = SKILL_END;
	m_fMaxSkillGage = 0.0f;
	m_fCurSkillGage = 0.0f;
	m_bSkillUse = false;
	memset(m_iarrSkill, NULL, SKILL_BUF_END);
}

void Skill::Init(string strText, string strType, int iIndex, float fMax)	//열 파일 이름, 캐릭터 타입, 스킬 인덱스
{
	string strTmp;
	ifstream load;
	load.open(strText);
	if (load.is_open())
	{
		while (!load.eof())
		{
			getline(load, strTmp, ',');	//타입
			if (strTmp == strType)
			{
				strTmp.clear();
				getline(load, strTmp, ',');	//스킬 인덱스가 맞는가?
				if (iIndex == stoi(strTmp))
				{
					m_iSkillIndex = iIndex;
					//스킬 타입
					strTmp.clear();
					getline(load, strTmp, ',');	//스킬 인덱스가 맞는가?
					m_eSkillType = (SKILL_TYPE)stoi(strTmp);
					//스킬이 무슨 타입인가?
					for (int i = 0; i < SKILL_BUF_END; i++)
					{
						strTmp.clear();
						if (i == SKILL_BUF_END - 1)
							getline(load, strTmp, '\n');
						else
							getline(load, strTmp, ',');
						m_iarrSkill[i] = stoi(strTmp);
					}
					m_fMaxSkillGage = fMax;
					m_cSKillBit[0] = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//power_up.bmp");
					m_cSKillBit[1] = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//power_down.bmp");
					break;
				}
			}
			strTmp.clear();
			getline(load, strTmp, '\n');
		}
	}
	load.close();
	//셋팅 종료
}

void Skill::Update(float fCurSkill)
{
	m_fCurSkillGage = fCurSkill;
	if (m_fMaxSkillGage <= m_fCurSkillGage)
	{
		m_bSkillUse = true;
		//스킬 사용 가능
		m_fCurSkillGage = 0.0f;
		//다시 초기화
	}
}

void Skill::Draw(JEngine::POINT pt, int iType)
{
	m_cSKillBit[iType]->Draw(pt);
}

Skill::~Skill()
{
}
