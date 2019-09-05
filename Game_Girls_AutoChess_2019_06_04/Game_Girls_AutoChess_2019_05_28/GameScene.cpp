#include "GameScene.h"
#include "SceneManager.h"
#include "MapManager.h"
#include "InputManager.h"
#include "ResoucesManager.h"
#include "TextManager.h"
#include "Player.h"
#define MAX_SET_TIME 20.0f
#define MAX_RESULT_TIME 3.0f

GameScene::GameScene()
{
	m_iStage = 0;		//스테이지 초기화
	m_fSetTime = 0.0f;	//타임 체크
	memset(&m_stCursor, NULL, sizeof(m_stCursor));
}

void GameScene::Init(HWND hWnd)
{
	JEngine::InputManager::GetInstance()->Clear();
	JEngine::InputManager::GetInstance()->RegistKeyCode(VK_LBUTTON);
	JEngine::InputManager::GetInstance()->RegistKeyCode(VK_RBUTTON);
	JEngine::InputManager::GetInstance()->RegistKeyCode(VK_ESCAPE);
	//우선 마우스 키만 등록

	m_pTitleScene = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//gamebackground.bmp");
	m_pTitlePoint.x = (WINDOW_SIZE_X - m_pTitleScene->GetWidth()) / 2;
	m_pTitlePoint.y = (WINDOW_SIZE_Y - m_pTitleScene->GetHeight()) / 2;
	//백그라운드

	m_cStore.Init();
	//상점 셋팅

	m_stCursor.st_CursorBit = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//crosshair.bmp");
	CursorUpdate();
	//커서 업데이트

	MapManager::GetInstance()->Init();
	//맵 셋팅

	Player::GetInstance()->Init();
	//플레이어 셋팅

	GameSet();

	m_cLose = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//lose.bmp");
	m_cWin = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//win.bmp");

	m_cTipButton = new JEngine::boolButton;
	m_cTipButton->Init(0, 0, "RES//tips.bmp", true);

	m_cTips = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//how_to_play.bmp");
	m_bTip = true;
}

void GameScene::GameSet()
{
	m_eState = PLAY_NONE;
	m_fSetTime = MAX_SET_TIME;					//셋팅 타임 설정
	m_cStore.StoreSet();						//상점 셋팅
	m_bGameStart = false;						//아직 게임 시작이 아님
	//게임 셋팅
}

void GameScene::CursorUpdate()
{
	m_stCursor.st_Cursorpt = JEngine::InputManager::GetInstance()->GetMousePoint();
	m_stCursor.st_CursorDrawpt.x = m_stCursor.st_Cursorpt.x - (m_stCursor.st_CursorBit->GetWidth() / 2);
	m_stCursor.st_CursorDrawpt.y = m_stCursor.st_Cursorpt.y - (m_stCursor.st_CursorBit->GetHeight() / 2);
}

bool GameScene::Input(float fETime)
{
	//키인 풋 시
	m_cStore.KeyInput(m_stCursor.st_Cursorpt);
	Player::GetInstance()->KeyInput(m_stCursor.st_Cursorpt);			//플레이어 인풋
	if (JEngine::InputManager::GetInstance()->isKeyUp(VK_ESCAPE))
		JEngine::SceneManager::GetInstance()->LoadScene(TITLE_SCENE);	//타이틀 씬으로 돌아감

	if (m_bTip && JEngine::InputManager::GetInstance()->isKeyUp(VK_LBUTTON))	//팁이 열린 상태에서 어디든 클릭하면 닫힌다.
		m_bTip = false;
	if (m_cTipButton->Update())	//팁 버튼을 눌렀다.
		m_bTip = true;
	return false;
}

void GameScene::Update(float fETime)
{
	static float fResultCur = 0.0f;
	int iPlayer = 0, iEnmey = 0;
	Character* Node, *Node1;
	CHARACTER_STATE* eState;
	bool bDeploy = false, bHit = false;

	iPlayer = Player::GetInstance()->ReturnCurChar();	//플레이어의 총 갯수
	iEnmey = m_cEManger.ReturnCurEnemy();				//에너미 총 갯수

	if (!m_bTip)	//팁이 열리면 pause
	{
		if (m_eState == PLAY_NONE)						//죽은 상태가 아니다.
		{
			if (m_fSetTime <= 0.0f && !m_bGameStart)	//시간이 다되면 상점을 이용하지 못하며 적이 출현한다.
			{
				m_fSetTime = 0.0f;
				m_cStore.Release();				//상점을 아예 닫아주고
				m_cEManger.EnemySet(m_iStage);	//에너미 셋팅
				m_bGameStart = true;
			}
			else if (m_fSetTime > 0.0f)
				m_fSetTime -= fETime;
			//게임이 시작되면 철혈들이 그려진다.

			if (m_bGameStart)	//게임이 시작되면 에너미들을 계속 업데이트
			{
				if (Player::GetInstance()->LoseCheck())
				{
					m_eState = PLAY_LOSE;
					m_bGameStart = false;
				}
				if (m_cEManger.LoseCheck())
				{
					m_eState = PLAY_WIN;
					for (int i = 0; i < iPlayer; i++)
					{
						Node = Player::GetInstance()->ReturnNode(i);
						eState = Node->ReturnState();
						if ((*eState) < STATE_VICTORY)
							(*eState) = STATE_VICTORY;
					}
					//승리 상태면 어느 정도 모션을 진행한 뒤 초기화 한다.
					m_bGameStart = false;
				}
				//승리 패배 체크

				HitCheck(Player::GetInstance()->ReturnCurChar(), m_cEManger.ReturnCurEnemy(), true);
				HitCheck(m_cEManger.ReturnCurEnemy(), Player::GetInstance()->ReturnCurChar(), false);

				st_Commander * Com = Player::GetInstance()->ReturnCom();
				for (int i = 0; i < iEnmey; i++)
				{
					Node = m_cEManger.ReturnNode(i);
					if (Com->st_iCurEffect > -1)
						Node->HitCheck(Com->st_iAtk);
				}
				//지휘관 스킬 발동 시 모든 적군에게 데미지

				for (int i = 0; i < iPlayer; i++)
				{
					Node = Player::GetInstance()->ReturnNode(i);
					bDeploy = Player::GetInstance()->ReturnDeploy(i);
					eState = Node->ReturnState();
					if (bDeploy && (*eState) < STATE_VICTORY)	//해당 캐릭터가 맵에 배치된 상황인가를 확인, 배치 되지 않았다면 아래의 연산은 하지 않는다.
					{
						int * iSkillNode = Player::GetInstance()->ReturnSkillNum();
						//스킬 발동 여부 확인
						Skill * Buf = Node->ReturnSkill();
						//스킬 클래스를 뺀 뒤
						bool *bUse = Buf->ReturnUse();
						//스킬의 사용 가능 여부

						if ((*bUse) && (*iSkillNode) == i)	//스킬이 발동 되었다.
						{
							string strType = Node->ReturnType();				//총기 타입을 뺀다.
							SKILL_TYPE eType = Buf->ReturnType();					//권총은 범위가 모든 적과 아군이며 나머지는 자신이다.
							int * iarr = Buf->ReturnBuf();
							//버프 목록을 빼냄
							Node->SkillReset();
							if (strType == "HG")
							{
								if (eType == SKILL_BUF)
								{
									for (int p = 0; p < iPlayer; p++)
									{
										Node1 = Player::GetInstance()->ReturnNode(p);
										//캐릭터 노드를 빼서
										Node1->SkillUP(iarr, eType);
									}
								}
								//아군에게 모든 버프를 돌린다.
							}
							else
								Node->SkillUP(iarr, eType);	//권총 외에는 버프든 디버프든 모두 자기 자신
							(*bUse) = false;
							(*iSkillNode) = -1;
						}
					}
				}						
				//버프 사용
			}
		}
		else
			fResultCur += fETime;

		if (fResultCur >= MAX_RESULT_TIME)
		{
			if (m_eState == PLAY_WIN)
			{
				//플레이어가 이기면 죽은 캐릭터들을 리셋 해주고 턴을 올려준다.
				Player::GetInstance()->Reset(m_iStage + 1);
				m_cEManger.Release();
				m_iStage++;
				GameSet();

			}
			else if (m_eState == PLAY_LOSE)
			{
				//플레이어가 지면 모조리 리셋
				m_iStage = 0;
				Release();
				GameSet();
			}
			fResultCur = 0.0f;
		}

		if (m_bGameStart)	//게임이 시작되면 에너미들을 계속 업데이트
			m_cEManger.Update(fETime);	//에너미 매니저 업데이트

		MapManager::GetInstance()->Update(fETime);		//초반에 체스판을 그려주는 것
		m_cStore.Update(fETime);
		Player::GetInstance()->Update(fETime, m_bGameStart);
	}
	CursorUpdate();
}

void GameScene::HitCheck(int iAttack, int iHit, bool bPlayer)
{
	Character* Attack, *Hit;
	CHARACTER_STATE* eState;
	bool bDeploy = false, bHit = false;
	bool* bAttack;
	JEngine::POINT Hpt;
	for (int i = 0; i < iAttack; i++)
	{
		if (bPlayer)
		{
			Attack = Player::GetInstance()->ReturnNode(i);
			bDeploy = Player::GetInstance()->ReturnDeploy(i);
		}
		else
		{
			Attack = m_cEManger.ReturnNode(i);
			bDeploy = m_cEManger.ReturnDeploy(i);
		}
		eState = Attack->ReturnState();
		bAttack = Attack->ReturnAttack();		//공격자의 공격 가능 상태, 공속에 따라서
		bHit = false;
		if ((*eState) < STATE_VICTORY && bDeploy)	//공격하려는 노드가 죽지 않고, 맵에 배치된 상태이다.
		{
			for (int j = 0; j < iHit; j++)
			{
				if (bPlayer)
				{
					Hit = m_cEManger.ReturnNode(j);
					bDeploy = m_cEManger.ReturnDeploy(j);
				}
				else
				{
					Hit = Player::GetInstance()->ReturnNode(j);
					bDeploy = Player::GetInstance()->ReturnDeploy(j);

				}
				eState = Hit->ReturnState();
				Hpt = Hit->ReturnPT(CHAR_XY);			//피격자의 좌표 위치
				if ((*eState) < STATE_VICTORY && bDeploy && Attack->RangeCheck(Hpt) && (*bAttack))	//피격받는 노드가 죽지 않고 맵에 배치되었다.
				{
					Hit->HitCheck(Attack->ReturnStats(STATS_ATK));
					(*bAttack) = false;	//공속을 초기화
				}
				else
					bHit = true;

			}
			if (!bHit)
			{
				eState = Attack->ReturnState();
				if ((*eState) != STATE_MOVE)
					(*eState) = STATE_WAIT;
			}
		}
	}
}

void GameScene::Draw(HDC hdc)
{
	char buf[256];
	int iWidth = MapManager::GetInstance()->ReturnSizeY();
	int iPNum = Player::GetInstance()->ReturnCurChar();
	int iENum = m_cEManger.ReturnCurEnemy();
	JEngine::POINT pt;
	m_pTitleScene->DrawBitblt(m_pTitlePoint.x, m_pTitlePoint.y);
	MapManager::GetInstance()->Draw();

	for (int i = 0; i < TILE_MAP_SIZE; i++)
	{
		//캐릭터 중싱점이 해당 y 선상에 존재한가?
		for (int p = 0; p < iPNum; p++)
		{
			pt = Player::GetInstance()->ReturnPt(p, CHAR_MIDDLE_XY);
			//플레이어 노드
			if ((i * iWidth) <= pt.y && pt.y <= ((i + 1)*iWidth))	//해당 칸에 캐릭터가 존재한느가?
				Player::GetInstance()->Draw(p);
		}

		if (m_bGameStart)
		{
			for (int e = 0; e < iENum; e++)
			{
				pt = m_cEManger.ReturnPt(e, CHAR_MIDDLE_XY);
				//적 노드
				if ((i * iWidth) >= pt.y && pt.y <= ((i + 1)*iWidth))	//해당 칸에 캐릭터가 존재한느가?
					m_cEManger.Draw(e);
			}
		}
	}
	//위에서부터 내려오면서 밑의 캐릭터가 위의 캐릭터보다 앞에 있는 것으로 표현하기 위해서

	Player::GetInstance()->Draw();
	//플레이어의 돈을 그림
	m_cTipButton->Draw();
	m_cStore.Draw();

	JEngine::TextManager::GetInstance()->SetFont("HY견고딕", 15,0,FW_BOLD);
	JEngine::TextManager::GetInstance()->SetColor(255, 255, 255);
	sprintf(buf, "%0.2f Time", m_fSetTime);
	JEngine::TextManager::GetInstance()->Draw(1100, 0, buf);
	//시간
	JEngine::TextManager::GetInstance()->SetColor(255, 255, 255);
	sprintf(buf, "%d Stage", m_iStage + 1);
	JEngine::TextManager::GetInstance()->Draw(1100, 20, buf);
	//스테이지
	JEngine::TextManager::GetInstance()->ReleaseFont();

	if (m_bTip)
		m_cTips->Draw((WINDOW_SIZE_X - m_cTips->GetWidth()) /2, (WINDOW_SIZE_Y - m_cTips->GetHeight()) / 2);

	m_stCursor.st_CursorBit->Draw(m_stCursor.st_CursorDrawpt);
	if (m_eState == PLAY_WIN)
		m_cWin->Draw(0, (WINDOW_SIZE_Y - m_cWin->GetHeight()) / 2);
	else if (m_eState == PLAY_LOSE)
		m_cLose->Draw(0, (WINDOW_SIZE_Y - m_cLose->GetHeight()) / 2);
}
void GameScene::Release()
{
	m_cStore.Release();
	m_cEManger.Release();
	Player::GetInstance()->Release();
	delete m_cTipButton;
}


GameScene::~GameScene()
{
	MapManager::GetInstance()->Release();
}

/*
1. 커맨더가 사망하면 게임은 무조건 끝나며
2. 스테이지 진행이나 적의 상태에 따라서 게이지가 차면서 커맨더 스킬을 사용할 수 있다.
3. 커맨더 스킬은 맵 전체에 강력한 포격 지원을 가하는 형식이다.
4. 오토 버튼
5. stage clear와 die 시 괜찮은 UI 띄우기
*/