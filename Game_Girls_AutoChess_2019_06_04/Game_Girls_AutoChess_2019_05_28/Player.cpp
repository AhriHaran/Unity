#include "Player.h"
#include "InputManager.h"
#include "MapManager.h"
#include "TextManager.h"

#define PLAYER_COM_DEFAULT_X 1
#define PLAYER_COM_DEFAULT_Y 5
#define COMMANDER_NUM 1000


Player::Player()
{
	m_iGold = 0;
	m_iCharNum = 0;
	m_iSkillNum = -1;
	memset(&m_MyList, NULL, sizeof(m_MyList));
	memset(&m_stCom, NULL, sizeof(m_stCom));
}

void Player::Init()
{
	ifstream load;
	int iarr[5];
	string strRES, strTmp;
	m_iGold = 5000;
	m_MyList = new MY_LIST;	//마이리스트 제작
	m_cBackBit = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//char_profile.bmp");
	m_cSkillBit = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//char_skill.bmp");
	m_cAutoOFF = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//autobutton00.bmp");
	m_cAutoON = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//autobutton01.bmp");
	m_cAutoPT.x = 1100;
	m_cAutoPT.y = 100;
	m_cAutoButton = new JEngine::boolButton;
	m_cAutoButton->Init(1100, 100, "RES//autobutton00.bmp", true);
	m_bAuto = false;
	m_stCom = new st_Commander;

	load.open("FILE//Commander.csv");
	for (int i = STATE_START; i < STATE_ALL_END; i++)
	{
		getline(load, strRES, ',');
		for (int j = 0; j < 5; j++)
		{
			getline(load, strTmp, ',');
			iarr[j] = stoi(strTmp);
		}
		m_stCom->st_carrAinme[i].Init(strRES, iarr, ANIME_TIME);
	}
	//캐릭터 애니메이션 설정
	for (int i = CHAR_H_A_START; i < CHAR_H_A_END; i++)
	{
		getline(load, strRES, ',');
		for (int j = 0; j < 5; j++)
		{
			if(i == CHAR_HIT && j == 4)
				getline(load, strTmp, '\n');
			else
				getline(load, strTmp, ',');
			iarr[j] = stoi(strTmp);
		}
		m_stCom->st_carrEffect[i].Init(strRES, iarr, ANIME_TIME + 0.02f);
	}
	//발사 임팩트
	load.close();

	m_stCom->st_cComProfile = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//tow.bmp");
	m_stCom->st_cCharPoint[CHAR_PORT_XY].x = 0;
	m_stCom->st_cCharPoint[CHAR_PORT_XY].y = 650;
	m_stCom->st_cGage = new Gage;
	m_stCom->st_cMaxSkill = 500.0f;
	m_stCom->st_cGage->Set("RES//skill.bmp", 130, 720, m_stCom->st_cMaxSkill);
	m_stCom->st_cSkillButton = new JEngine::boolButton;
	m_stCom->st_cSkillButton->Init(m_stCom->st_cCharPoint[CHAR_PORT_XY].x, m_stCom->st_cCharPoint[CHAR_PORT_XY].y, "RES//tow_skill.bmp", true);
	int iX = 500, iCurX = 100, iY = 50, iCurY = 200;
	for (int i = 0; i < 3; i++)
	{
		m_stCom->st_barrEffect[i] = false;

		m_stCom->st_cEffectPT[i].x = iX;
		m_stCom->st_cEffectPT[i].y = iY;

		if (i < 1)
			iX += iCurX;
		else
			iX -= 80;
		iY += iCurY;
	}	
	m_stCom->st_iAtk = 1000;
	ComRelease();
	m_stCom->st_cCurSkill = 200.0f;
}

void Player::BuyChar(int iIndex, int iGold)
{
	//구매
	JEngine::POINT pt;
	if (iGold <= m_iGold && m_iCharNum < MAX_CHAR_SIZE)	//돈과 여유 공간이 충분하다.
	{
		m_iGold -= iGold;
		m_iCharNum++;
		st_ProChar * Node = m_MyList->CreateNode();

		char buf[126] = "FILE//MyDoll.csv";
		Node->st_cChar.Init(buf, to_string(iIndex), ANIME_TIME);
		Node->st_cChar.PointSet(CHAR_XY, -1, -1);
		//캐릭터 애니메이션 위치 임시 위치
		Node->st_cChar.PointSet(CHAR_PORT_XY, 0, 50 + (m_iCharNum * (Node->st_cChar.ReturnPortY() + 30)));
		//초상화 위치
		Node->st_cChar.PointSet(CHAR_NAME_XY, Node->st_cChar.ReturnPortX() + 5, (55 + (m_iCharNum * (Node->st_cChar.ReturnPortY() + 30))));
		//이름 위치
		Node->st_bDrawSD = false;
		Node->st_bDeploy = false;
		Node->st_bSkill = false;

		pt = Node->st_cChar.ReturnPT(CHAR_PORT_XY);
		//캐릭터 프로필 백그라운드
		Node->st_cBackPoint.x = pt.x;
		Node->st_cBackPoint.y = pt.y;
		//캐릭터 프로필 백그라운드 포인트
		Node->st_cChar.GageSet(m_cBackBit->GetWidth() , pt);
		Node->st_cRect.Set(Node->st_cBackPoint.x, Node->st_cBackPoint.y,
			Node->st_cBackPoint.x + m_cBackBit->GetWidth(), Node->st_cBackPoint.y + m_cBackBit->GetHeight());
		m_MyList->PushNode(Node);
	}
}

void Player::Update(float fDelta, bool bGame)
{
	static float fDeltaFire = 0.0f;

	st_ProChar * Node;
	JEngine::POINT Mpt;
	bool bRoop = false;

	if (m_cAutoButton->Update())
	{
		if (m_bAuto)
			m_bAuto = false;
		else
			m_bAuto = true;
	}

	for (int i = 0; i < m_iCharNum; i++)
	{
		Node = m_MyList->FindNode(i);
		if (Node->st_bDrawSD)
		{
			Node->st_cChar.Update(fDelta);	//캐릭터 자체 업데이트
			if ((Node->st_bDeploy && Node->st_bSkill) || m_bAuto)
			{
				Skill * buf;
				bool *bSkill;
				buf = Node->st_cChar.ReturnSkill();
				bSkill = buf->ReturnUse();
				if ((*bSkill))	//스킬 사용 가능 여부를 확인
					m_iSkillNum = i;	//해당 노드가 스킬을 사용하였다는 것을 알려줌
				Node->st_bSkill = false;
			}
		}
		Node->st_cChar.Update();	//게이지 업데이트
	}	//업데이트에서 계속해서 플레이어의 상태를 업데이트 한다. 공격 가능한가와 기타 등등

	m_stCom->st_carrAinme[m_stCom->st_eState].Update(fDelta, bRoop);
	m_stCom->st_cGage->Update(m_stCom->st_cCurSkill);

	if (m_stCom->st_eState == STATE_ATTACK)
	{
		fDeltaFire += fDelta;
		if (fDeltaFire >= 1.5f)
		{
			fDeltaFire = 0.0f;
			m_stCom->st_bFire = true;
		}
	}	//발사 애니메이션을 맞춰 주기 위해서

	if (bRoop)	//캐릭터 자체 애니메이션 루프 끝남
	{
		m_stCom->st_carrAinme[m_stCom->st_eState].Reset();
		if (m_stCom->st_eState == STATE_SET)
		{
			m_stCom->st_bSet = true;
			m_stCom->st_eState = STATE_WAIT;		//셋팅 모션을 마치면 발사가 가능하다.
		}
		else if (m_stCom->st_eState == STATE_ATTACK)
		{
			m_stCom->st_eState = STATE_RELOAD;		//리로드 모션이 나오면 필드에 포격을 그려준다.
			m_stCom->st_iCurEffect = 0;				//포격 범위를 그려준 뒤 게임 신에서 모든 적을 제거한다.
		}
		else if (m_stCom->st_eState == STATE_RELOAD)
			m_stCom->st_eState = STATE_WAIT;
		else if (m_stCom->st_eState == STATE_DIE)
			m_stCom->st_bDeploy = false;
		MapManager::GetInstance()->KeyInput(m_stCom->st_cCharPoint[CHAR_MIDDLE_XY], Mpt, true);
		CommanderSet(Mpt.x + MAP_MIDDLE_X, Mpt.y + MAP_MIDDLE_Y);
	}

	bRoop = false;
	if (m_stCom->st_bFire)
	{
		m_stCom->st_carrEffect[CHAR_ATTACK].Update(fDelta, bRoop);
		if (bRoop)
			m_stCom->st_bFire = false;
	}

	if (m_stCom->st_cCurSkill >= m_stCom->st_cMaxSkill)
		m_stCom->st_bShot = true;	//스킬 사용 가능

	if (m_stCom->st_bShot && m_stCom->st_bSet)	//스킬 발동
	{
		if (bGame && (m_stCom->st_cSkillButton->Update() || m_bAuto))
		{
			m_stCom->st_carrAinme[m_stCom->st_eState].Reset();
			m_stCom->st_eState = STATE_ATTACK;
			m_stCom->st_bShot = false;
			m_stCom->st_cCurSkill = 0.0f;
		}
	}

	bRoop = false;
	if (m_stCom->st_iCurEffect > -1)
	{
		m_stCom->st_carrEffect[CHAR_HIT].Update(fDelta, m_stCom->st_barrEffect[m_stCom->st_iCurEffect]);
		if (m_stCom->st_barrEffect[m_stCom->st_iCurEffect])
		{
			m_stCom->st_iCurEffect++;
			if (m_stCom->st_iCurEffect >= 3)
			{
				m_stCom->st_iCurEffect = -1;
				memset(&m_stCom->st_barrEffect, false, sizeof(bool));
			}
		}
	}	//공격 임팩트는 3군데 정도 드로우하며 순차적으로 재생 한다.
}

void Player::KeyInput(JEngine::POINT Cursorpt)
{
	JEngine::POINT pt;
	CHARACTER_STATE * eState;
	st_ProChar * Node;
	static bool sbClick = false;
	static int iClickNum = -1;
	if (JEngine::InputManager::GetInstance()->isKeyPress(VK_LBUTTON))
	{
		if (!sbClick)	//첫 클릭이다.
		{
			for (int i = 0; i < m_iCharNum; i++)
			{
				Node = m_MyList->FindNode(i);
				eState = Node->st_cChar.ReturnState();
				pt = Node->st_cChar.ReturnPT(CHAR_XY);	//캐릭터의 자체 xy
				if ((*eState) < STATE_VICTORY)	//캐릭터가 기본(wait, move, attack) 상태이다.
				{
					if (Node->st_cRect.isPtin(Cursorpt))	//내가 초상화 범위를 클릭하였다.
					{
						if (!Node->st_bDeploy)			//배치를 위한 클릭
						{
							Node->st_bDrawSD = true;
							sbClick = true;
							iClickNum = i;
						}
						else//스킬 사용을 위한 클릭
							Node->st_bSkill = true;
					}
					else if (Node->st_cChar.RectCheck(Cursorpt) && Node->st_bDeploy)	//맵에 존재하는 캐릭터를 옮기려고 할 때
					{
						MapManager::GetInstance()->MoveChar(Cursorpt);
						sbClick = true;
						Node->st_bDeploy = false;
						iClickNum = i;
					}	
				}
			}
			if (m_stCom->st_eState < STATE_VICTORY || m_stCom->st_eState == STATE_SET)	//커맨더에 대한 이동
			{
				if (m_stCom->st_cRect.isPtin(Cursorpt))
				{
					MapManager::GetInstance()->MoveChar(Cursorpt);
					sbClick = true;
					m_stCom->st_bDeploy = false;
					iClickNum = COMMANDER_NUM;
				}
			}
			//커맨더에 대한 맵 배치
		}
		if (sbClick)	//키 입력이 계속 유지 되는 경우
		{
			if (iClickNum == COMMANDER_NUM)	//커맨더에 대한 키 입력
			{
				if(m_stCom->st_eState != STATE_MOVE)
					m_stCom->st_carrAinme[m_stCom->st_eState].Reset();
				m_stCom->st_eState = STATE_MOVE;	//무브
				CommanderSet(Cursorpt.x, Cursorpt.y);
			}
			else if (iClickNum > -1)	//캐릭터에 대한 키 입력 유지
			{
				Node = m_MyList->FindNode(iClickNum);
				eState = Node->st_cChar.ReturnState();
				if ((*eState) < STATE_VICTORY)
				{
					(*eState) = STATE_MOVE;
					JEngine::POINT Mpt = Node->st_cChar.ReturnAPT(POINT_CHAR_MIDDLE);
					int iX = Cursorpt.x - Mpt.x;
					int iY = Cursorpt.y - Mpt.y;

					Node->st_cChar.PointSet(CHAR_XY, iX, iY);
					Node->st_cChar.RangeDraw();
				}
			}
		}
	}

	if (JEngine::InputManager::GetInstance()->isKeyUp(VK_LBUTTON))		//버튼을 땠다면?
	{
		if (sbClick)
		{
			if (MapManager::GetInstance()->KeyInput(Cursorpt, pt, true))
			{
				if (iClickNum == COMMANDER_NUM)
				{
					m_stCom->st_carrAinme[m_stCom->st_eState].Reset();
					m_stCom->st_eState = STATE_SET;
					m_stCom->st_bDeploy = true;
					pt.x += MAP_MIDDLE_X;
					pt.y += MAP_MIDDLE_Y;
					CommanderSet(pt.x, pt.y);
				}
				else if (iClickNum > -1)
				{
					Node = m_MyList->FindNode(iClickNum);
					eState = Node->st_cChar.ReturnState();
					if ((*eState) < STATE_VICTORY)
					{
						(*eState) = STATE_WAIT;
						Node->st_bDeploy = true;
						JEngine::POINT Mpt = Node->st_cChar.ReturnAPT(POINT_CHAR_MIDDLE);	//캐릭터 그림 중심 좌표
						pt.x += MAP_MIDDLE_X;		//pt는 맵의 타일의 시작 위치
						pt.y += MAP_MIDDLE_Y;		//내가 놓고자하는 맵의 중심 좌표
						Node->st_cChar.PointSet(CHAR_XY, (pt.x - Mpt.x), (pt.y - Mpt.y));	//맵 위치로 고정
					}
				}
			}
			else		//맵 위치가 아니다.
			{
				for (int i = 0; i < m_iCharNum; i++)
				{
					Node = m_MyList->FindNode(i);
					eState = Node->st_cChar.ReturnState();
					MapManager::GetInstance()->MoveChar(Cursorpt);
					if ((*eState) < STATE_VICTORY)
					{
						(*eState) = STATE_WAIT;
						if (!Node->st_bDeploy)
						{
							Node->st_bDrawSD = false;
							Node->st_cChar.PointSet(CHAR_XY, 0, 0);
						}
					}
				}
				if (!m_stCom->st_bDeploy)
				{
					MapManager::GetInstance()->MoveChar(Cursorpt);
					m_stCom->st_carrAinme[m_stCom->st_eState].Reset();
					m_stCom->st_eState = STATE_SET;
					JEngine::POINT Mpt = MapManager::GetInstance()->XYPoint(PLAYER_COM_DEFAULT_X, PLAYER_COM_DEFAULT_Y);	//커맨더는 위치는 고정되어 있다.
					CommanderSet(Mpt.x + MAP_MIDDLE_X, Mpt.y + MAP_MIDDLE_Y);
					MapManager::GetInstance()->KeyInput(m_stCom->st_cCharPoint[CHAR_MIDDLE_XY], Mpt, true);
					m_stCom->st_bDeploy = true;
				}
			}
			sbClick = false;
			iClickNum = -1; //맵 위치가 아니면 초기화
		}
	}

	if (JEngine::InputManager::GetInstance()->isKeyDown(VK_RBUTTON)) //팔기
	{
		for (int i = 0; i < m_iCharNum; i++)
		{
			Node = m_MyList->FindNode(i);
			if (Node->st_cRect.isPtin(Cursorpt))
			{
				eState = Node->st_cChar.ReturnState();
				int iGold = Node->st_cChar.ReturnGold();
				if (*eState == STATE_DIE)	//죽은 상태라면 반값
					iGold /= 2;
				m_MyList->DeleteNode(i);
				m_iGold += iGold;
				m_iCharNum--;
			}
		}
		for (int i = 0; i < m_iCharNum; i++)	//팔고 나서 초상화 위치들을 새로 셋팅
		{
			Node = m_MyList->FindNode(i);
			Node->st_cChar.PointSet(CHAR_PORT_XY, 0, 50 + ((i + 1) * (Node->st_cChar.ReturnPortY() + 30)));
			Node->st_cChar.PointSet(CHAR_NAME_XY, Node->st_cChar.ReturnPortX() + 5, (55 + ((i + 1) * (Node->st_cChar.ReturnPortY() + 30))));
			pt = Node->st_cChar.ReturnPT(CHAR_PORT_XY);
			Node->st_cBackPoint.x = pt.x;
			Node->st_cBackPoint.y = pt.y;
			Node->st_cChar.GageSet(m_cBackBit->GetWidth(), pt);
			Node->st_cRect.Set(Node->st_cBackPoint.x, Node->st_cBackPoint.y,
				Node->st_cBackPoint.x + m_cBackBit->GetWidth(), Node->st_cBackPoint.y + m_cBackBit->GetHeight());
		}
	}

}

void Player::CommanderSet(int iX, int iY)		//좌표를 토대로 계산
{
	JEngine::POINT Mpt = m_stCom->st_carrAinme[m_stCom->st_eState].ReturnPT(POINT_CHAR_MIDDLE);	//캐릭터 중신 좌표
	m_stCom->st_cCharPoint[CHAR_XY].x = iX - Mpt.x;
	m_stCom->st_cCharPoint[CHAR_XY].y = iY - Mpt.y;		//캐릭터를 그려줄 좌표
	m_stCom->st_cCharPoint[CHAR_MIDDLE_XY].x = iX;
	m_stCom->st_cCharPoint[CHAR_MIDDLE_XY].y = iY;		//캐릭터의 중심 좌표 == 맵의 중심 좌표
	m_stCom->st_cRect.Set(m_stCom->st_cCharPoint[CHAR_XY].x, m_stCom->st_cCharPoint[CHAR_XY].y,
		(m_stCom->st_cCharPoint[CHAR_XY].x + m_stCom->st_carrAinme[m_stCom->st_eState].GetSizeX()), (m_stCom->st_cCharPoint[CHAR_XY].y + m_stCom->st_carrAinme[m_stCom->st_eState].GetSizeY()));
	Mpt = m_stCom->st_carrEffect[CHAR_ATTACK].ReturnPT(POINT_CHAR_MIDDLE);
	m_stCom->st_cFirePT.x = m_stCom->st_cRect.right - Mpt.x;
	m_stCom->st_cFirePT.y = m_stCom->st_cRect.top + ((m_stCom->st_cRect.bottom - m_stCom->st_cRect.top) / 2) - Mpt.y;	//발사 임팩트 좌표
}

void Player::Draw()
{
	bool * bSkill;
	JEngine::TextManager::GetInstance()->SetFont("HY견고딕", 15, 0, FW_BOLD);
	JEngine::TextManager::GetInstance()->Draw(1100, 40, "GOLD");
	JEngine::TextManager::GetInstance()->Draw(1100, 60, to_string(m_iGold));	//내가 가진 돈을 드로우
	JEngine::TextManager::GetInstance()->ReleaseFont();

	for (int i = 0; i < m_iCharNum; i++)
	{
		st_ProChar * Node = m_MyList->FindNode(i);
		m_cBackBit->Draw(Node->st_cBackPoint);
		Node->st_cChar.PortDraw();
		Node->st_cChar.NameDraw();
		Node->st_cChar.GageDraw();
		Skill * buf = Node->st_cChar.ReturnSkill();
		bSkill = buf->ReturnUse();
		if ((*bSkill))
			m_cSkillBit->Draw(Node->st_cBackPoint);
	}

	m_stCom->st_carrAinme[m_stCom->st_eState].Draw(m_stCom->st_cCharPoint[CHAR_XY]);	//커맨더 드로우
	m_stCom->st_cComProfile->Draw(m_stCom->st_cCharPoint[CHAR_PORT_XY]);
	m_stCom->st_cGage->Draw();
	if (m_stCom->st_bShot && m_stCom->st_bSet)	//스킬 사용 가능 여부를 드로우
		m_stCom->st_cSkillButton->Draw();
	if(m_stCom->st_iCurEffect > -1)	//스킬 임팩트 드로우
		m_stCom->st_carrEffect[CHAR_HIT].Draw(m_stCom->st_cEffectPT[m_stCom->st_iCurEffect]);
	if (m_stCom->st_bFire)	//발사 임팩트 드로우
		m_stCom->st_carrEffect[CHAR_ATTACK].Draw(m_stCom->st_cFirePT);

	if (!m_bAuto)	//오토 버튼 활성화 여부
		m_cAutoOFF->Draw(m_cAutoPT);
	else
		m_cAutoON->Draw(m_cAutoPT);
}

void Player::Draw(int iIndex)
{
	st_ProChar * Node = m_MyList->FindNode(iIndex);	//캐릭터 드로우
	if (Node->st_bDrawSD)
		Node->st_cChar.Draw();
}

void Player::Release()
{
	if (m_MyList != NULL)
	{
		m_MyList->Release();
		m_iCharNum = 0;
		memset(&m_MyList, NULL, sizeof(m_MyList));
	}
	if (m_stCom != NULL)
		ComRelease();
}

void Player::ComRelease()
{
	m_stCom->st_eState = STATE_SET;
	JEngine::POINT Mpt = MapManager::GetInstance()->XYPoint(PLAYER_COM_DEFAULT_X, PLAYER_COM_DEFAULT_Y);
	CommanderSet(Mpt.x + MAP_MIDDLE_X, Mpt.y + MAP_MIDDLE_Y);
	MapManager::GetInstance()->KeyInput(m_stCom->st_cCharPoint[CHAR_MIDDLE_XY], Mpt, true);
	m_stCom->st_bDeploy = true;
	m_stCom->st_bShot = false;
	m_stCom->st_bSet = false;
	m_stCom->st_bFire = false;
	m_stCom->st_iCurEffect = -1;
	m_stCom->st_cCurSkill = 0.0f;
	memset(&m_stCom->st_barrEffect, false, sizeof(bool));
}

void Player::Reset(int iStage)
{
	int iCount = m_iCharNum;
	CHARACTER_STATE* eState;
	JEngine::POINT pt;
	for (int i = 0; i < m_iCharNum; i++)
	{
		st_ProChar* Node = m_MyList->FindNode(i);
		eState = Node->st_cChar.ReturnState();
		if ((*eState) == STATE_DIE)			//캐릭터가 죽은 상태라면
		{
			Node->st_bDeploy = false;
			Node->st_bDrawSD = false;
			pt = Node->st_cChar.ReturnPT(CHAR_MIDDLE_XY);
			MapManager::GetInstance()->MoveChar(pt);	//맵 배치 포인트 초기화
			iCount--;
		}
		else
		{
			Node->st_cChar.AnimeReset();
			(*eState) = STATE_WAIT;
		}
	}
	m_stCom->st_cCurSkill += m_stCom->st_cGage->ReturnCurUP() * iCount * iStage * 100;	
	//게임이 끝나고 캐릭터가 몇 명이 살았는 지에 따라서 커맨더의 스킬 게이지를 상승 스킨다.
}

JEngine::POINT Player::ReturnPt(int iIndex, int iPoint)
{
	st_ProChar * Node = m_MyList->FindNode(iIndex);
	return Node->st_cChar.ReturnPT(iPoint);
	//해당 캐릭터의 포인트 좌표를 리턴
}

Character* Player::ReturnNode(int iIndex)
{
	st_ProChar* Node = m_MyList->FindNode(iIndex);
	return &Node->st_cChar;
}

bool Player::ReturnDeploy(int iIndex)
{
	st_ProChar* Node = m_MyList->FindNode(iIndex);
	return Node->st_bDeploy;
	//맵에 배치된 상황인가를 확인
}

bool Player::LoseCheck()
{
	int iCount = 0;
	CHARACTER_STATE* eState;
	for (int i = 0; i < m_iCharNum; i++)
	{
		st_ProChar* Node = m_MyList->FindNode(i);
		eState = Node->st_cChar.ReturnState();

		if ((*eState) == STATE_DIE || !Node->st_bDeploy)
			iCount++;
	}
	if (iCount >= m_iCharNum || m_iCharNum == 0)	//내 캐릭터가 전부 죽었거나, 아무것도 없다.
	{
		JEngine::POINT Mpt;
		m_stCom->st_carrAinme[m_stCom->st_eState].Reset();
		m_stCom->st_eState = STATE_DIE;
		MapManager::GetInstance()->KeyInput(m_stCom->st_cCharPoint[CHAR_MIDDLE_XY], Mpt, true);
		CommanderSet(Mpt.x + MAP_MIDDLE_X, Mpt.y + MAP_MIDDLE_Y);
		return true;
	}
	return false;
}

Player::~Player()
{
	delete m_MyList;
	delete m_stCom->st_cSkillButton;
	delete m_stCom->st_cGage;
	delete m_stCom;
	delete m_cAutoButton;
	DestroyInstance();
}
