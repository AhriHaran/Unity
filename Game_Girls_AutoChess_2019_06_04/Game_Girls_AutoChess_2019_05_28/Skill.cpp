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

void Skill::Init(string strText, string strType, int iIndex, float fMax)	//�� ���� �̸�, ĳ���� Ÿ��, ��ų �ε���
{
	string strTmp;
	ifstream load;
	load.open(strText);
	if (load.is_open())
	{
		while (!load.eof())
		{
			getline(load, strTmp, ',');	//Ÿ��
			if (strTmp == strType)
			{
				strTmp.clear();
				getline(load, strTmp, ',');	//��ų �ε����� �´°�?
				if (iIndex == stoi(strTmp))
				{
					m_iSkillIndex = iIndex;
					//��ų Ÿ��
					strTmp.clear();
					getline(load, strTmp, ',');	//��ų �ε����� �´°�?
					m_eSkillType = (SKILL_TYPE)stoi(strTmp);
					//��ų�� ���� Ÿ���ΰ�?
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
	//���� ����
}

void Skill::Update(float fCurSkill)
{
	m_fCurSkillGage = fCurSkill;
	if (m_fMaxSkillGage <= m_fCurSkillGage)
	{
		m_bSkillUse = true;
		//��ų ��� ����
		m_fCurSkillGage = 0.0f;
		//�ٽ� �ʱ�ȭ
	}
}

void Skill::Draw(JEngine::POINT pt, int iType)
{
	m_cSKillBit[iType]->Draw(pt);
}

Skill::~Skill()
{
}
