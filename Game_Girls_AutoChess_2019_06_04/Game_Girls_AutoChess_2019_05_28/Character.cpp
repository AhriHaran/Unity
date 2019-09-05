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
	//자료를 불러와서 읽는다.
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
			if (strTmp == strIndex)	//해당 인덱스
			{
				m_iIndex = stoi(strTmp);				//인덱스 번호
				strTmp.clear();

				getline(load, strTmp, ',');				//이름
				m_strName = strTmp;
				strTmp.clear();

				getline(load, strTmp, ',');				//타입
				m_strType = strTmp;
				strTmp.clear();

				getline(load, strTmp, ',');				//레어도
				m_iRare = stoi(strTmp);
				strTmp.clear();

				getline(load, strTmp, ',');				//가격
				m_iGold = stoi(strTmp);
				strTmp.clear();

				AnimeSet((&load), m_cAnime, STATE_END, 5);			//스테이트 애니메 셋팅

				for (int i = PORTRAITS_START; i < PORTRAITS_END; i++)
				{
					getline(load, strTmp, ',');
					sprintf(buf, strTmp.c_str(), m_strName.c_str());
					if (strTmp != "NULL")
						m_carrPort[i] = JEngine::ResoucesManager::GetInstance()->GetBitmap(buf);
					strTmp.clear();
				}//초상화 업데이트

				getline(load, strTmp, '\n');
				m_iSkillType = stoi(strTmp);	//스킬 타입 설정
				strTmp.clear();

				m_bAnime = true;
				m_bAttack = false;
				m_bHit = false;
				m_ePort = PORTRAITS_NORMAL;	//현재 초상화 상태
				m_cRangeBit = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//range.bmp");	//사정거리는 공용이므로
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
		AnimeSet((&load), m_cHitAttack, CHAR_H_A_END, 5);			//스테이트 애니메 셋팅
	}
	load.close();

	sprintf(buf, "FILE//Type.csv");

	load.open(buf);

	if (load.is_open())
	{
		fStats = (float)m_iRare / 2;	//2성이면 1배, 3성이면 1.5배 4성이면 2배, 5성이면 2.5배

		while (!load.eof())
		{
			getline(load, strTmp, ',');	//타입
			if (strTmp == m_strType)	//해당 타입
			{
				strTmp.clear();
				getline(load, strTmp, ',');
				m_fHp = stof(strTmp);
				strTmp.clear();	//캐릭터 자체 HP
				m_fHp *= fStats;
				//레어도 별로 추가 스탯

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
						//사거리는 3성 이상 일 때만 상승
					}
					strTmp.clear();
				}
				getline(load, strTmp, ',');
				m_fSkillGage = stof(strTmp);
				strTmp.clear();
				//맥스 스킬 게이지 업데이트

				getline(load, strTmp, '\n');
				m_fAtkSpeed = stof(strTmp);
				strTmp.clear();
				//공격 속도 업데이트
				break;
				//종료
			}
			else
			{
				strTmp.clear();
				getline(load, strTmp, '\n');
			}
		}
	}
	//타입별로 스테이터스를 셋팅
	load.close();
	//다 끝나면 스킬 게이지들을 셋팅

	if (m_iSkillType != -1)
	{
		//스킬이 존재한다.
		m_cSkill = new Skill;
		m_cSkill->Init("FILE//Skill.csv", m_strType, m_iSkillType, m_fSkillGage);
		//스킬 셋팅
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

		sprintf(buf, strTmp.c_str(), m_strName.c_str());	//저장 루트
		strTmp.clear();
		for (int j = 0; j < iAnimeSize; j++)
		{
			getline((*Load), strTmp, ',');
			iarr[j] = stoi(strTmp);
			strTmp.clear();
		}
		pAni[i].Init(buf, iarr, ANIME_TIME);
	}
	//에니메이션 관리
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
		//HP와 스킬 게이지는 추가해주는 방식이며 나머지는 시간이 지나면 빠진다.
		//스킬은 버프 형식으로 HP, ATK, DEF, SkillGage, 공속
	}

	if (m_bAnime)
		m_cAnime[m_eState].Update(fDelta, bRoop);

	if (m_eState == STATE_DIE)
	{
		m_ePort = PORTRAITS_DEMAGE;
		m_bAttack = false;
		if (bRoop)
			m_bAnime = false;	//사망 모션은 단 한 번만 출력
	}
	else if (m_eState == STATE_ATTACK)
	{
		m_fCurSpeed += (fDelta * 3);		//스테이트가 공격 스테이트면 공속을 돌려서 확인 한다.
		if (m_fCurSpeed >= m_fAtkSpeed)
		{
			if (m_iSkillType != -1)			//스킬이 존재한다면 공속 사용이 가능하면 올려준다.
				m_fCurSkillGage += (m_cArrGage[GAGE_SKILL].ReturnCurUP() * 10);	
			m_fCurSpeed = 0.0f;
			m_bAttack = true;	//공격 가능 상태
		}
		m_cHitAttack[CHAR_ATTACK].Update(fDelta, bRoop);	//히트 애니메이션
	}
	else if (m_eState == STATE_VICTORY)
	{
		m_bAttack = false;		//라운드가 끝나면 
		m_bHit = false;
		m_iarrStats[STATS_ATK] -= m_iarrSkillUP[1];
		m_iarrStats[STATS_DEF] -= m_iarrSkillUP[2];
		m_fAtkSpeed += (float)m_iarrSkillUP[4];
		m_eType = SKILL_END;
		m_bSkill = false;
		for (int i = 0; i < SKILL_BUF_END; i++)
			m_iarrSkillUP[i] = 0;
	}
	else	//빅토리 상태는 계속 애니메이션만을 루프 시킨다.
	{
		iMaxX = MapManager::GetInstance()->ReturnTmpX();
		if (m_iIndex > INDEX_PLAYER_CHAR_END && iMaxX < m_carrPoint[CHAR_XY].x) //플레이어가 아니라면 움직일 수 있다.
		{
			m_eState = STATE_MOVE;
			fX = m_carrPoint[CHAR_XY].x - (m_iarrStats[STATS_SPEED] * fDelta);
			PointSet(CHAR_XY, (int)fX, m_carrPoint[CHAR_XY].y);
		}
	}	//특정 스테이트가 아니다.
	if (m_bHit)
	{
		m_cHitAttack[CHAR_HIT].Update(fDelta, bRoop);
		if (bRoop)
			m_bHit = false;
	}
}

void Character::Update()	//게이지 업데이트
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

	MapManager::GetInstance()->RangeCheck(Ept.x, Ept.y);	//적이 현재 존재하는 좌표 번호를 계산
	iCharX = m_carrPoint[CHAR_MIDDLE_XY].x;
	iCharY = m_carrPoint[CHAR_MIDDLE_XY].y;
	MapManager::GetInstance()->RangeCheck(iCharX, iCharY);	//내 캐릭터의 좌표 번호를 계산

	if (m_eState < STATE_VICTORY)		//대기 상태, 움직이는 상태, 공격 상태에서는 계속 레인지 체크
	{
		for (int y = -m_iarrStats[STATS_RANGE]; y < m_iarrStats[STATS_RANGE] + 1; y++)
		{
			iMinY = (iCharY + y);
			iMaxY = (iCharY - y);

			iMinX = (iCharX + iTmpX);
			iMaxX = (iCharX + iTmpX2);	//블록을 기준으로 사거리 내의 캐릭터들을 모두 체크

			if ((iMinX <= Ept.x && Ept.x <= iMaxX) && (iMinY <= Ept.y && Ept.y <= iMaxY))
			{
				m_eState = STATE_ATTACK;	//공격 애니메이션 출력 상태
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
			//iTmpX는 왼쪽, iTmpX2는 오른쪽
		}
	}
	return false;
}

void Character::HitCheck(int iATK)
{
	if (m_eState < STATE_VICTORY)
	{
		if (m_iarrStats[STATS_DEF] > iATK)
			iATK = 1;	//방어력이 높으면 계속 1의 데미지
		else
			iATK -= m_iarrStats[STATS_DEF];
		//데미지 공식은 우선 간단하게

		m_fCurHp -= iATK;

		JEngine::TextManager::GetInstance()->SetFont("HY견고딕", 25, 0, FW_BOLD);
		JEngine::TextManager::GetInstance()->SetColor(255, 0, 0);	//붉은색 컬러로 출력
		if (m_iIndex < INDEX_PLAYER_CHAR_END)
			JEngine::TextManager::GetInstance()->Draw(m_carrPoint[CHAR_XY].x, m_carrPoint[CHAR_XY].y - 10, to_string(iATK));
		else
			JEngine::TextManager::GetInstance()->Draw(m_carrPoint[CHAR_XY].x + m_cAnime[m_eState].GetSizeX(), m_carrPoint[CHAR_XY].y - 10, to_string(iATK));
		//데미지를 그려주기 위해서
		JEngine::TextManager::GetInstance()->SetColor(255, 255, 255);	//검은색으로 리턴
		JEngine::TextManager::GetInstance()->ReleaseFont();
		m_bHit = true;

		if (m_fCurHp <= 0)	//죽었다
		{
			m_fCurHp = 0.0f;
			m_bAttack = false;
			m_bHit = false;
			m_ePort = PORTRAITS_DEMAGE;
			m_eState = STATE_DIE;	//죽었다.
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

	//캐릭터의 애니메이션 드로우
	if (m_eState == STATE_ATTACK)	//내가 받은 좌표를 토대로 어택은 좀 앞으로 
	{
		JEngine::POINT mpt = m_cHitAttack[CHAR_ATTACK].ReturnPT(POINT_CHAR_MIDDLE);
		if (m_iIndex < INDEX_PLAYER_CHAR_END)
			pt.x = (m_carrPoint[CHAR_XY].x + m_cAnime[m_eState].GetSizeX()) - mpt.x;
		else
			pt.x = (m_carrPoint[CHAR_XY].x - m_cHitAttack[CHAR_ATTACK].GetSizeX()) + mpt.x;
		pt.y = m_carrPoint[CHAR_MIDDLE_XY].y - 40 - mpt.y;
		m_cHitAttack[CHAR_ATTACK].Draw(pt);
	}	//범위에 들어가면 계속 공격 애니메이션이 출력 되지만 데미지는 공속에 따라서 들어간다.

	if (m_bHit)
	{
		JEngine::POINT mpt = m_cHitAttack[CHAR_HIT].ReturnPT(POINT_CHAR_MIDDLE);
		pt.x = m_carrPoint[CHAR_MIDDLE_XY].x - mpt.x;
		pt.y = m_carrPoint[CHAR_MIDDLE_XY].y - mpt.y;
		m_cHitAttack[CHAR_HIT].Draw(pt);
	}	//히트 임팩트

	if (m_eType == SKILL_BUF)
	{
		//버프는 캐릭터의 오른쪽
		pt.x = m_carrPoint[CHAR_XY].x + 100;
		pt.y = m_carrPoint[CHAR_XY].y - 20;
		m_cSkill->Draw(pt, m_eType);
	}
	else if (m_eType == SKILL_DEBUF)
	{
		//디버프는 캐릭터의 왼쪽
		pt.x = m_carrPoint[CHAR_XY].x - 20;
		pt.y = m_carrPoint[CHAR_XY].y - 20;
		m_cSkill->Draw(pt, m_eType);
	}
}

void Character::StatsDraw()
{
	int iSizeX = 55, iSizeY = 0, iSum = 15, iHp = 0;
	JEngine::POINT pt = m_carrPoint[CHAR_STATS_XY];
	//스탯 드로우
	//받은 포인트를 기준으로 밑으로 그려준다
	JEngine::TextManager::GetInstance()->SetColor(255, 255, 255);	//컬러
	JEngine::TextManager::GetInstance()->Draw(pt.x, pt.y + (iSizeY * iSum), "HP:");
	iHp = m_fHp;
	JEngine::TextManager::GetInstance()->Draw(pt.x + iSizeX, pt.y + (iSizeY * iSum), to_string(iHp));
	//HP
	iSizeY++;
	JEngine::TextManager::GetInstance()->Draw(pt.x, pt.y + (iSizeY * iSum), "ATK:");
	JEngine::TextManager::GetInstance()->Draw(pt.x + iSizeX, pt.y + (iSizeY * iSum), to_string(m_iarrStats[0]));
	//공격력
	iSizeY++;
	JEngine::TextManager::GetInstance()->Draw(pt.x, pt.y + (iSizeY * iSum), "DEF:");
	JEngine::TextManager::GetInstance()->Draw(pt.x + iSizeX, pt.y + (iSizeY * iSum), to_string(m_iarrStats[1]));
	//방어력
	iSizeY++;
	JEngine::TextManager::GetInstance()->Draw(pt.x, pt.y + (iSizeY * iSum), "RANGE:");
	JEngine::TextManager::GetInstance()->Draw(pt.x + iSizeX, pt.y + (iSizeY * iSum), to_string(m_iarrStats[2]));
	//범위
	iSizeY++;
	JEngine::TextManager::GetInstance()->Draw(pt.x, pt.y + (iSizeY * iSum), "GOLD:");
	JEngine::TextManager::GetInstance()->Draw(pt.x + iSizeX, pt.y + (iSizeY * iSum), to_string(m_iGold));
	//가격
	iSizeY++;
	JEngine::TextManager::GetInstance()->Draw(pt.x, pt.y + (iSizeY * iSum), "RARE:");
	JEngine::TextManager::GetInstance()->Draw(pt.x + iSizeX, pt.y + (iSizeY * iSum), to_string(m_iRare));
	//레어도
}

void Character::NameDraw()
{
	JEngine::TextManager::GetInstance()->SetColor(255, 255, 255);	//붉은색 컬러로 출력
	JEngine::TextManager::GetInstance()->Draw(m_carrPoint[CHAR_NAME_XY], m_strName);
}

void Character::PortDraw()
{
	m_carrPort[m_ePort]->Draw(m_carrPoint[CHAR_PORT_XY]);
}

void Character::RangeDraw()
{
	//맵 범위 내라면
	int iSizeX = 0, iSizeY = 0, iRange = m_iarrStats[STATS_RANGE], iMinX = 0, iMaxX = 0;
	iSizeX = MapManager::GetInstance()->ReturnSizeX();
	iSizeY = MapManager::GetInstance()->ReturnSizeY();
	//내 현재 위치를 기준으로 
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
		//iTmpX는 왼쪽, iTmpX2는 오른쪽
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
	//일반 포인트 셋팅
	if (iIndex == CHAR_XY)
	{
		JEngine::POINT pt = m_cAnime[m_eState].ReturnPT(POINT_CHAR_MIDDLE); //애니메이션 중심 좌표
		m_carrPoint[CHAR_MIDDLE_XY].x = iX + pt.x;
		m_carrPoint[CHAR_MIDDLE_XY].y = iY + pt.y;
		m_cRect.Set(m_carrPoint[iIndex].x, m_carrPoint[iIndex].y,
			m_carrPoint[iIndex].x + m_cAnime[m_eState].GetSizeX(), m_carrPoint[iIndex].y + m_cAnime[m_eState].GetSizeY());
	}
	//중앙 포인트 설정
}

void Character::SkillUP(int * iarr, SKILL_TYPE eType)
{
	for (int i = 0; i < SKILL_BUF_END; i++)
		m_iarrSkillUP[i] = iarr[i];
	m_eType = eType;
	//내가 받는 버프나 디버프 목록을 임시로 저장
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
