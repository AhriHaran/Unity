#include "Character.h"
#include "MapManager.h"
#include "TextManager.h"


Character::Character()
{
	memset(&m_cAnime, NULL, sizeof(m_strName));
	memset(&m_iarrStats, NULL, sizeof(m_iarrStats));
	memset(&m_carrPort, NULL, sizeof(m_carrPort));
	memset(&m_cArrGage, NULL, sizeof(m_cArrGage));
	memset(&m_iarrSkillUP, NULL, sizeof(m_iarrSkillUP));

	m_iIndex = 0;
	m_iRare = 0;
	m_iGold = 0;
	m_iSkillType = -1;

	m_eState = STATE_WAIT;
	m_eType = SKILL_END;

	m_fHp = 0.0f;
	m_fCurHp = 0.0f;
	m_fCurSpeed = 0.0f;
	m_fAtkSpeed = 0.0f;
	m_fCurSkillGage = 0.0f;
	m_fSkillGage = 0.0f;
}

void Character::Init(string FileName, string strIndex, float fDelta)
{
	//�ڷḦ �ҷ��ͼ� �д´�.
	string strTmp;
	ifstream load;
	load.open(FileName);
	char buf[128];
	float fStats = 0.0f;
	int iCount = 0;
	if (load.is_open())
	{
		while (!load.eof())
		{
			getline(load, strTmp, ',');
			if (strTmp == strIndex)	//�ش� �ε���
			{
				m_iIndex = stoi(strTmp);				//�ε��� ��ȣ
				strTmp.clear();

				getline(load, strTmp, ',');				//�̸�
				m_strName = strTmp;
				strTmp.clear();

				getline(load, strTmp, ',');				//Ÿ��
				m_strType = strTmp;
				strTmp.clear();

				getline(load, strTmp, ',');				//���
				m_iRare = stoi(strTmp);
				strTmp.clear();

				getline(load, strTmp, ',');				//����
				m_iGold = stoi(strTmp);
				strTmp.clear();

				AnimeSet((&load), m_cAnime, STATE_END, 5);			//������Ʈ �ִϸ� ����

				for (int i = PORTRAITS_START; i < PORTRAITS_END; i++)
				{
					getline(load, strTmp, ',');
					sprintf(buf, strTmp.c_str(), m_strName.c_str());
					if (strTmp != "NULL")
						m_carrPort[i] = JEngine::ResoucesManager::GetInstance()->GetBitmap(buf);
					strTmp.clear();
				}//�ʻ�ȭ ������Ʈ

				getline(load, strTmp, '\n');
				m_iSkillType = stoi(strTmp);	//��ų Ÿ�� ����
				strTmp.clear();

				m_bAnime = true;
				m_bAttack = false;
				m_bHit = false;
				m_ePort = PORTRAITS_NORMAL;	//���� �ʻ�ȭ ����
				m_cRangeBit = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//range.bmp");	//�����Ÿ��� �����̹Ƿ�
				break;
			}
			else
			{
				strTmp.clear();
				getline(load, strTmp, '\n');
			}
		}
	}
	load.close();

	strTmp.clear();
	sprintf(buf, "FILE//Effect.csv");
	load.open(buf);
	if (load.is_open())
	{
		if (m_iIndex > INDEX_PLAYER_CHAR_END)
		{
			strTmp.clear();
			getline(load, strTmp, '\n');
		}
		AnimeSet((&load), m_cHitAttack, CHAR_H_A_END, 5);			//������Ʈ �ִϸ� ����
	}
	load.close();

	sprintf(buf, "FILE//Type.csv");

	load.open(buf);

	if (load.is_open())
	{
		fStats = (float)m_iRare / 2;	//2���̸� 1��, 3���̸� 1.5�� 4���̸� 2��, 5���̸� 2.5��

		while (!load.eof())
		{
			getline(load, strTmp, ',');	//Ÿ��
			if (strTmp == m_strType)	//�ش� Ÿ��
			{
				strTmp.clear();
				getline(load, strTmp, ',');
				m_fHp = stof(strTmp);
				strTmp.clear();	//ĳ���� ��ü HP
				m_fHp *= fStats;
				//��� ���� �߰� ����

				for (int i = STATS_START; i < STATS_END; i++)
				{
					getline(load, strTmp, ',');
					m_iarrStats[i] = stoi(strTmp);
					if (i < STATS_RANGE)
						m_iarrStats[i] *= fStats;
					else if (i == STATS_RANGE)
					{
						if (m_iRare > 3)
							m_iarrStats[i] *= 1.5;
						//��Ÿ��� 3�� �̻� �� ���� ���
					}
					strTmp.clear();
				}
				getline(load, strTmp, ',');
				m_fSkillGage = stof(strTmp);
				strTmp.clear();
				//�ƽ� ��ų ������ ������Ʈ

				getline(load, strTmp, '\n');
				m_fAtkSpeed = stof(strTmp);
				strTmp.clear();
				//���� �ӵ� ������Ʈ
				break;
				//����
			}
			else
			{
				strTmp.clear();
				getline(load, strTmp, '\n');
			}
		}
	}
	//Ÿ�Ժ��� �������ͽ��� ����
	load.close();
	//�� ������ ��ų ���������� ����

	if (m_iSkillType != -1)
	{
		//��ų�� �����Ѵ�.
		m_cSkill = new Skill;
		m_cSkill->Init("FILE//Skill.csv", m_strType, m_iSkillType, m_fSkillGage);
		//��ų ����
	}
	m_fCurHp = m_fHp;
	m_fCurSkillGage = 0.0f;
	m_bSkill = false;
}

void Character::AnimeSet(ifstream * Load, Anime * pAni, int iSize, int iAnimeSize)
{
	int iarr[5];
	string strTmp;
	char buf[128];
	for (int i = 0; i < iSize; i++)
	{
		getline((*Load), strTmp, ',');

		sprintf(buf, strTmp.c_str(), m_strName.c_str());	//���� ��Ʈ
		strTmp.clear();
		for (int j = 0; j < iAnimeSize; j++)
		{
			getline((*Load), strTmp, ',');
			iarr[j] = stoi(strTmp);
			strTmp.clear();
		}
		pAni[i].Init(buf, iarr, ANIME_TIME);
	}
	//���ϸ��̼� ����
}

void Character::GageSet(int iWidth, JEngine::POINT pt)
{
	int	iX = (ReturnPortX() + ((iWidth - (ReturnPortX() * 2)) / 2));
	for (int i = GAGE_START; i < GAGE_END; i++)
	{
		if (i == GAGE_HP)
		{
			m_cArrGage[i].Set("RES//hp.bmp", (iX + ((iWidth - (iX * 2)) / 2)), (pt.y + 20), m_fHp);
			m_cArrGage[i].Update(m_fCurHp);
		}
		else
		{
			m_cArrGage[i].Set("RES//skill.bmp", (iX + ((iWidth - (iX * 2)) / 2)), (pt.y + 45), m_fSkillGage);
			m_cArrGage[i].Update(m_fCurSkillGage);
		}
	}
}

void Character::Update(float fDelta)
{
	float fX = 0.0f;
	bool bRoop = false;
	int iMaxX = 0;

	if (m_eType != SKILL_END && !m_bSkill)
	{
		m_fCurHp += (float)m_iarrSkillUP[0];
		m_iarrStats[STATS_ATK] += m_iarrSkillUP[1];
		m_iarrStats[STATS_DEF] += m_iarrSkillUP[2];
		m_fCurSkillGage += (float)m_iarrSkillUP[3];
		m_fAtkSpeed -= (float)m_iarrSkillUP[4];
		m_bSkill = true;
		m_iarrSkillUP[0] = 0;
		m_iarrSkillUP[3] = 0;
		//HP�� ��ų �������� �߰����ִ� ����̸� �������� �ð��� ������ ������.
		//��ų�� ���� �������� HP, ATK, DEF, SkillGage, ����
	}

	if (m_bAnime)
		m_cAnime[m_eState].Update(fDelta, bRoop);

	if (m_eState == STATE_DIE)
	{
		m_ePort = PORTRAITS_DEMAGE;
		m_bAttack = false;
		if (bRoop)
			m_bAnime = false;	//��� ����� �� �� ���� ���
	}
	else if (m_eState == STATE_ATTACK)
	{
		m_fCurSpeed += (fDelta * 3);		//������Ʈ�� ���� ������Ʈ�� ������ ������ Ȯ�� �Ѵ�.
		if (m_fCurSpeed >= m_fAtkSpeed)
		{
			if (m_iSkillType != -1)			//��ų�� �����Ѵٸ� ���� ����� �����ϸ� �÷��ش�.
				m_fCurSkillGage += (m_cArrGage[GAGE_SKILL].ReturnCurUP() * 10);	
			m_fCurSpeed = 0.0f;
			m_bAttack = true;	//���� ���� ����
		}
		m_cHitAttack[CHAR_ATTACK].Update(fDelta, bRoop);	//��Ʈ �ִϸ��̼�
	}
	else if (m_eState == STATE_VICTORY)
	{
		m_bAttack = false;		//���尡 ������ 
		m_bHit = false;
		m_iarrStats[STATS_ATK] -= m_iarrSkillUP[1];
		m_iarrStats[STATS_DEF] -= m_iarrSkillUP[2];
		m_fAtkSpeed += (float)m_iarrSkillUP[4];
		m_eType = SKILL_END;
		m_bSkill = false;
		for (int i = 0; i < SKILL_BUF_END; i++)
			m_iarrSkillUP[i] = 0;
	}
	else	//���丮 ���´� ��� �ִϸ��̼Ǹ��� ���� ��Ų��.
	{
		iMaxX = MapManager::GetInstance()->ReturnTmpX();
		if (m_iIndex > INDEX_PLAYER_CHAR_END && iMaxX < m_carrPoint[CHAR_XY].x) //�÷��̾ �ƴ϶�� ������ �� �ִ�.
		{
			m_eState = STATE_MOVE;
			fX = m_carrPoint[CHAR_XY].x - (m_iarrStats[STATS_SPEED] * fDelta);
			PointSet(CHAR_XY, (int)fX, m_carrPoint[CHAR_XY].y);
		}
	}	//Ư�� ������Ʈ�� �ƴϴ�.
	if (m_bHit)
	{
		m_cHitAttack[CHAR_HIT].Update(fDelta, bRoop);
		if (bRoop)
			m_bHit = false;
	}
}

void Character::Update()	//������ ������Ʈ
{
	m_cArrGage[GAGE_HP].Update(m_fCurHp);
	m_cArrGage[GAGE_SKILL].Update(m_fCurSkillGage);
	m_cSkill->Update(m_fCurSkillGage);
}

bool Character::RangeCheck(JEngine::POINT Ept)
{
	int iTmpX = 0, iTmpX2 = 0, iMinX = 0, iMaxX = 0;
	int iMinY = 0, iMaxY = 0;
	int iCharX = 0, iCharY = 0;

	MapManager::GetInstance()->RangeCheck(Ept.x, Ept.y);	//���� ���� �����ϴ� ��ǥ ��ȣ�� ���
	iCharX = m_carrPoint[CHAR_MIDDLE_XY].x;
	iCharY = m_carrPoint[CHAR_MIDDLE_XY].y;
	MapManager::GetInstance()->RangeCheck(iCharX, iCharY);	//�� ĳ������ ��ǥ ��ȣ�� ���

	if (m_eState < STATE_VICTORY)		//��� ����, �����̴� ����, ���� ���¿����� ��� ������ üũ
	{
		for (int y = -m_iarrStats[STATS_RANGE]; y < m_iarrStats[STATS_RANGE] + 1; y++)
		{
			iMinY = (iCharY + y);
			iMaxY = (iCharY - y);

			iMinX = (iCharX + iTmpX);
			iMaxX = (iCharX + iTmpX2);	//����� �������� ��Ÿ� ���� ĳ���͵��� ��� üũ

			if ((iMinX <= Ept.x && Ept.x <= iMaxX) && (iMinY <= Ept.y && Ept.y <= iMaxY))
			{
				m_eState = STATE_ATTACK;	//���� �ִϸ��̼� ��� ����
				return true;
			}
			if (y > -1)
			{
				iTmpX += 1;
				iTmpX2 -= 1;
			}
			else
			{
				iTmpX -= 1;
				iTmpX2 += 1;
			}
			//iTmpX�� ����, iTmpX2�� ������
		}
	}
	return false;
}

void Character::HitCheck(int iATK)
{
	if (m_eState < STATE_VICTORY)
	{
		if (m_iarrStats[STATS_DEF] > iATK)
			iATK = 1;	//������ ������ ��� 1�� ������
		else
			iATK -= m_iarrStats[STATS_DEF];
		//������ ������ �켱 �����ϰ�

		m_fCurHp -= iATK;

		JEngine::TextManager::GetInstance()->SetFont("HY�߰��", 25, 0, FW_BOLD);
		JEngine::TextManager::GetInstance()->SetColor(255, 0, 0);	//������ �÷��� ���
		if (m_iIndex < INDEX_PLAYER_CHAR_END)
			JEngine::TextManager::GetInstance()->Draw(m_carrPoint[CHAR_XY].x, m_carrPoint[CHAR_XY].y - 10, to_string(iATK));
		else
			JEngine::TextManager::GetInstance()->Draw(m_carrPoint[CHAR_XY].x + m_cAnime[m_eState].GetSizeX(), m_carrPoint[CHAR_XY].y - 10, to_string(iATK));
		//�������� �׷��ֱ� ���ؼ�
		JEngine::TextManager::GetInstance()->SetColor(255, 255, 255);	//���������� ����
		JEngine::TextManager::GetInstance()->ReleaseFont();
		m_bHit = true;

		if (m_fCurHp <= 0)	//�׾���
		{
			m_fCurHp = 0.0f;
			m_bAttack = false;
			m_bHit = false;
			m_ePort = PORTRAITS_DEMAGE;
			m_eState = STATE_DIE;	//�׾���.
			JEngine::POINT Mappt, Mpt;
			MapManager::GetInstance()->KeyInput(m_carrPoint[CHAR_MIDDLE_XY], Mappt);
			Mappt.x += MAP_MIDDLE_X;
			Mappt.y += MAP_MIDDLE_Y;
			Mpt = m_cAnime[m_eState].ReturnPT(POINT_CHAR_MIDDLE);
			PointSet(CHAR_XY, (Mappt.x - Mpt.x), (Mappt.y - Mpt.y));
		}
	}
}

void Character::Draw()
{
	JEngine::POINT pt;
	if (m_bAnime)
		m_cAnime[m_eState].Draw(m_carrPoint[CHAR_XY]);

	//ĳ������ �ִϸ��̼� ��ο�
	if (m_eState == STATE_ATTACK)	//���� ���� ��ǥ�� ���� ������ �� ������ 
	{
		JEngine::POINT mpt = m_cHitAttack[CHAR_ATTACK].ReturnPT(POINT_CHAR_MIDDLE);
		if (m_iIndex < INDEX_PLAYER_CHAR_END)
			pt.x = (m_carrPoint[CHAR_XY].x + m_cAnime[m_eState].GetSizeX()) - mpt.x;
		else
			pt.x = (m_carrPoint[CHAR_XY].x - m_cHitAttack[CHAR_ATTACK].GetSizeX()) + mpt.x;
		pt.y = m_carrPoint[CHAR_MIDDLE_XY].y - 40 - mpt.y;
		m_cHitAttack[CHAR_ATTACK].Draw(pt);
	}	//������ ���� ��� ���� �ִϸ��̼��� ��� ������ �������� ���ӿ� ���� ����.

	if (m_bHit)
	{
		JEngine::POINT mpt = m_cHitAttack[CHAR_HIT].ReturnPT(POINT_CHAR_MIDDLE);
		pt.x = m_carrPoint[CHAR_MIDDLE_XY].x - mpt.x;
		pt.y = m_carrPoint[CHAR_MIDDLE_XY].y - mpt.y;
		m_cHitAttack[CHAR_HIT].Draw(pt);
	}	//��Ʈ ����Ʈ

	if (m_eType == SKILL_BUF)
	{
		//������ ĳ������ ������
		pt.x = m_carrPoint[CHAR_XY].x + 100;
		pt.y = m_carrPoint[CHAR_XY].y - 20;
		m_cSkill->Draw(pt, m_eType);
	}
	else if (m_eType == SKILL_DEBUF)
	{
		//������� ĳ������ ����
		pt.x = m_carrPoint[CHAR_XY].x - 20;
		pt.y = m_carrPoint[CHAR_XY].y - 20;
		m_cSkill->Draw(pt, m_eType);
	}
}

void Character::StatsDraw()
{
	int iSizeX = 55, iSizeY = 0, iSum = 15, iHp = 0;
	JEngine::POINT pt = m_carrPoint[CHAR_STATS_XY];
	//���� ��ο�
	//���� ����Ʈ�� �������� ������ �׷��ش�
	JEngine::TextManager::GetInstance()->SetColor(255, 255, 255);	//�÷�
	JEngine::TextManager::GetInstance()->Draw(pt.x, pt.y + (iSizeY * iSum), "HP:");
	iHp = m_fHp;
	JEngine::TextManager::GetInstance()->Draw(pt.x + iSizeX, pt.y + (iSizeY * iSum), to_string(iHp));
	//HP
	iSizeY++;
	JEngine::TextManager::GetInstance()->Draw(pt.x, pt.y + (iSizeY * iSum), "ATK:");
	JEngine::TextManager::GetInstance()->Draw(pt.x + iSizeX, pt.y + (iSizeY * iSum), to_string(m_iarrStats[0]));
	//���ݷ�
	iSizeY++;
	JEngine::TextManager::GetInstance()->Draw(pt.x, pt.y + (iSizeY * iSum), "DEF:");
	JEngine::TextManager::GetInstance()->Draw(pt.x + iSizeX, pt.y + (iSizeY * iSum), to_string(m_iarrStats[1]));
	//����
	iSizeY++;
	JEngine::TextManager::GetInstance()->Draw(pt.x, pt.y + (iSizeY * iSum), "RANGE:");
	JEngine::TextManager::GetInstance()->Draw(pt.x + iSizeX, pt.y + (iSizeY * iSum), to_string(m_iarrStats[2]));
	//����
	iSizeY++;
	JEngine::TextManager::GetInstance()->Draw(pt.x, pt.y + (iSizeY * iSum), "GOLD:");
	JEngine::TextManager::GetInstance()->Draw(pt.x + iSizeX, pt.y + (iSizeY * iSum), to_string(m_iGold));
	//����
	iSizeY++;
	JEngine::TextManager::GetInstance()->Draw(pt.x, pt.y + (iSizeY * iSum), "RARE:");
	JEngine::TextManager::GetInstance()->Draw(pt.x + iSizeX, pt.y + (iSizeY * iSum), to_string(m_iRare));
	//���
}

void Character::NameDraw()
{
	JEngine::TextManager::GetInstance()->SetColor(255, 255, 255);	//������ �÷��� ���
	JEngine::TextManager::GetInstance()->Draw(m_carrPoint[CHAR_NAME_XY], m_strName);
}

void Character::PortDraw()
{
	m_carrPort[m_ePort]->Draw(m_carrPoint[CHAR_PORT_XY]);
}

void Character::RangeDraw()
{
	//�� ���� �����
	int iSizeX = 0, iSizeY = 0, iRange = m_iarrStats[STATS_RANGE], iMinX = 0, iMaxX = 0;
	iSizeX = MapManager::GetInstance()->ReturnSizeX();
	iSizeY = MapManager::GetInstance()->ReturnSizeY();
	//�� ���� ��ġ�� �������� 
	JEngine::POINT CurPT, DrawPT;
	for (int y = -iRange; y < iRange + 1; y++)
	{
		CurPT.y = m_carrPoint[CHAR_MIDDLE_XY].y + (y * iSizeY);
		CurPT.x = m_carrPoint[CHAR_MIDDLE_XY].x + (iMinX * iSizeX);
		if (MapManager::GetInstance()->KeyInput(CurPT, DrawPT))
			m_cRangeBit->Draw(DrawPT);

		CurPT.y = m_carrPoint[CHAR_MIDDLE_XY].y + (y * iSizeY);
		CurPT.x = m_carrPoint[CHAR_MIDDLE_XY].x + (iMaxX * iSizeX);
		if (MapManager::GetInstance()->KeyInput(CurPT, DrawPT))
			m_cRangeBit->Draw(DrawPT);

		if (y > -1)
		{
			iMinX += 1;
			iMaxX -= 1;
		}
		else
		{
			iMinX -= 1;
			iMaxX += 1;
		}
		//iTmpX�� ����, iTmpX2�� ������
	}
}

void Character::GageDraw()
{
	for (int i = GAGE_START; i < GAGE_END; i++)
		m_cArrGage[i].Draw();
}
void Character::PointSet(int iIndex, int iX, int iY)
{
	m_carrPoint[iIndex].x = iX;
	m_carrPoint[iIndex].y = iY;
	//�Ϲ� ����Ʈ ����
	if (iIndex == CHAR_XY)
	{
		JEngine::POINT pt = m_cAnime[m_eState].ReturnPT(POINT_CHAR_MIDDLE); //�ִϸ��̼� �߽� ��ǥ
		m_carrPoint[CHAR_MIDDLE_XY].x = iX + pt.x;
		m_carrPoint[CHAR_MIDDLE_XY].y = iY + pt.y;
		m_cRect.Set(m_carrPoint[iIndex].x, m_carrPoint[iIndex].y,
			m_carrPoint[iIndex].x + m_cAnime[m_eState].GetSizeX(), m_carrPoint[iIndex].y + m_cAnime[m_eState].GetSizeY());
	}
	//�߾� ����Ʈ ����
}

void Character::SkillUP(int * iarr, SKILL_TYPE eType)
{
	for (int i = 0; i < SKILL_BUF_END; i++)
		m_iarrSkillUP[i] = iarr[i];
	m_eType = eType;
	//���� �޴� ������ ����� ����� �ӽ÷� ����
}

void Character::SkillReset()
{
	m_fCurSkillGage = 0.0f;
}

bool Character::RectCheck(JEngine::POINT pt)
{
	return m_cRect.isPtin(pt);
}

Character::~Character()
{
}
