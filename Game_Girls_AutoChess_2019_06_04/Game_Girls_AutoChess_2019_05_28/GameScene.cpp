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
	m_iStage = 0;		//�������� �ʱ�ȭ
	m_fSetTime = 0.0f;	//Ÿ�� üũ
	memset(&m_stCursor, NULL, sizeof(m_stCursor));
}

void GameScene::Init(HWND hWnd)
{
	JEngine::InputManager::GetInstance()->Clear();
	JEngine::InputManager::GetInstance()->RegistKeyCode(VK_LBUTTON);
	JEngine::InputManager::GetInstance()->RegistKeyCode(VK_RBUTTON);
	JEngine::InputManager::GetInstance()->RegistKeyCode(VK_ESCAPE);
	//�켱 ���콺 Ű�� ���

	m_pTitleScene = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//gamebackground.bmp");
	m_pTitlePoint.x = (WINDOW_SIZE_X - m_pTitleScene->GetWidth()) / 2;
	m_pTitlePoint.y = (WINDOW_SIZE_Y - m_pTitleScene->GetHeight()) / 2;
	//��׶���

	m_cStore.Init();
	//���� ����

	m_stCursor.st_CursorBit = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//crosshair.bmp");
	CursorUpdate();
	//Ŀ�� ������Ʈ

	MapManager::GetInstance()->Init();
	//�� ����

	Player::GetInstance()->Init();
	//�÷��̾� ����

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
	m_fSetTime = MAX_SET_TIME;					//���� Ÿ�� ����
	m_cStore.StoreSet();						//���� ����
	m_bGameStart = false;						//���� ���� ������ �ƴ�
	//���� ����
}

void GameScene::CursorUpdate()
{
	m_stCursor.st_Cursorpt = JEngine::InputManager::GetInstance()->GetMousePoint();
	m_stCursor.st_CursorDrawpt.x = m_stCursor.st_Cursorpt.x - (m_stCursor.st_CursorBit->GetWidth() / 2);
	m_stCursor.st_CursorDrawpt.y = m_stCursor.st_Cursorpt.y - (m_stCursor.st_CursorBit->GetHeight() / 2);
}

bool GameScene::Input(float fETime)
{
	//Ű�� ǲ ��
	m_cStore.KeyInput(m_stCursor.st_Cursorpt);
	Player::GetInstance()->KeyInput(m_stCursor.st_Cursorpt);			//�÷��̾� ��ǲ
	if (JEngine::InputManager::GetInstance()->isKeyUp(VK_ESCAPE))
		JEngine::SceneManager::GetInstance()->LoadScene(TITLE_SCENE);	//Ÿ��Ʋ ������ ���ư�

	if (m_bTip && JEngine::InputManager::GetInstance()->isKeyUp(VK_LBUTTON))	//���� ���� ���¿��� ���� Ŭ���ϸ� ������.
		m_bTip = false;
	if (m_cTipButton->Update())	//�� ��ư�� ������.
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

	iPlayer = Player::GetInstance()->ReturnCurChar();	//�÷��̾��� �� ����
	iEnmey = m_cEManger.ReturnCurEnemy();				//���ʹ� �� ����

	if (!m_bTip)	//���� ������ pause
	{
		if (m_eState == PLAY_NONE)						//���� ���°� �ƴϴ�.
		{
			if (m_fSetTime <= 0.0f && !m_bGameStart)	//�ð��� �ٵǸ� ������ �̿����� ���ϸ� ���� �����Ѵ�.
			{
				m_fSetTime = 0.0f;
				m_cStore.Release();				//������ �ƿ� �ݾ��ְ�
				m_cEManger.EnemySet(m_iStage);	//���ʹ� ����
				m_bGameStart = true;
			}
			else if (m_fSetTime > 0.0f)
				m_fSetTime -= fETime;
			//������ ���۵Ǹ� ö������ �׷�����.

			if (m_bGameStart)	//������ ���۵Ǹ� ���ʹ̵��� ��� ������Ʈ
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
					//�¸� ���¸� ��� ���� ����� ������ �� �ʱ�ȭ �Ѵ�.
					m_bGameStart = false;
				}
				//�¸� �й� üũ

				HitCheck(Player::GetInstance()->ReturnCurChar(), m_cEManger.ReturnCurEnemy(), true);
				HitCheck(m_cEManger.ReturnCurEnemy(), Player::GetInstance()->ReturnCurChar(), false);

				st_Commander * Com = Player::GetInstance()->ReturnCom();
				for (int i = 0; i < iEnmey; i++)
				{
					Node = m_cEManger.ReturnNode(i);
					if (Com->st_iCurEffect > -1)
						Node->HitCheck(Com->st_iAtk);
				}
				//���ְ� ��ų �ߵ� �� ��� �������� ������

				for (int i = 0; i < iPlayer; i++)
				{
					Node = Player::GetInstance()->ReturnNode(i);
					bDeploy = Player::GetInstance()->ReturnDeploy(i);
					eState = Node->ReturnState();
					if (bDeploy && (*eState) < STATE_VICTORY)	//�ش� ĳ���Ͱ� �ʿ� ��ġ�� ��Ȳ�ΰ��� Ȯ��, ��ġ ���� �ʾҴٸ� �Ʒ��� ������ ���� �ʴ´�.
					{
						int * iSkillNode = Player::GetInstance()->ReturnSkillNum();
						//��ų �ߵ� ���� Ȯ��
						Skill * Buf = Node->ReturnSkill();
						//��ų Ŭ������ �� ��
						bool *bUse = Buf->ReturnUse();
						//��ų�� ��� ���� ����

						if ((*bUse) && (*iSkillNode) == i)	//��ų�� �ߵ� �Ǿ���.
						{
							string strType = Node->ReturnType();				//�ѱ� Ÿ���� ����.
							SKILL_TYPE eType = Buf->ReturnType();					//������ ������ ��� ���� �Ʊ��̸� �������� �ڽ��̴�.
							int * iarr = Buf->ReturnBuf();
							//���� ����� ����
							Node->SkillReset();
							if (strType == "HG")
							{
								if (eType == SKILL_BUF)
								{
									for (int p = 0; p < iPlayer; p++)
									{
										Node1 = Player::GetInstance()->ReturnNode(p);
										//ĳ���� ��带 ����
										Node1->SkillUP(iarr, eType);
									}
								}
								//�Ʊ����� ��� ������ ������.
							}
							else
								Node->SkillUP(iarr, eType);	//���� �ܿ��� ������ ������� ��� �ڱ� �ڽ�
							(*bUse) = false;
							(*iSkillNode) = -1;
						}
					}
				}						
				//���� ���
			}
		}
		else
			fResultCur += fETime;

		if (fResultCur >= MAX_RESULT_TIME)
		{
			if (m_eState == PLAY_WIN)
			{
				//�÷��̾ �̱�� ���� ĳ���͵��� ���� ���ְ� ���� �÷��ش�.
				Player::GetInstance()->Reset(m_iStage + 1);
				m_cEManger.Release();
				m_iStage++;
				GameSet();

			}
			else if (m_eState == PLAY_LOSE)
			{
				//�÷��̾ ���� ������ ����
				m_iStage = 0;
				Release();
				GameSet();
			}
			fResultCur = 0.0f;
		}

		if (m_bGameStart)	//������ ���۵Ǹ� ���ʹ̵��� ��� ������Ʈ
			m_cEManger.Update(fETime);	//���ʹ� �Ŵ��� ������Ʈ

		MapManager::GetInstance()->Update(fETime);		//�ʹݿ� ü������ �׷��ִ� ��
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
		bAttack = Attack->ReturnAttack();		//�������� ���� ���� ����, ���ӿ� ����
		bHit = false;
		if ((*eState) < STATE_VICTORY && bDeploy)	//�����Ϸ��� ��尡 ���� �ʰ�, �ʿ� ��ġ�� �����̴�.
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
				Hpt = Hit->ReturnPT(CHAR_XY);			//�ǰ����� ��ǥ ��ġ
				if ((*eState) < STATE_VICTORY && bDeploy && Attack->RangeCheck(Hpt) && (*bAttack))	//�ǰݹ޴� ��尡 ���� �ʰ� �ʿ� ��ġ�Ǿ���.
				{
					Hit->HitCheck(Attack->ReturnStats(STATS_ATK));
					(*bAttack) = false;	//������ �ʱ�ȭ
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
		//ĳ���� �߽����� �ش� y ���� �����Ѱ�?
		for (int p = 0; p < iPNum; p++)
		{
			pt = Player::GetInstance()->ReturnPt(p, CHAR_MIDDLE_XY);
			//�÷��̾� ���
			if ((i * iWidth) <= pt.y && pt.y <= ((i + 1)*iWidth))	//�ش� ĭ�� ĳ���Ͱ� �����Ѵ���?
				Player::GetInstance()->Draw(p);
		}

		if (m_bGameStart)
		{
			for (int e = 0; e < iENum; e++)
			{
				pt = m_cEManger.ReturnPt(e, CHAR_MIDDLE_XY);
				//�� ���
				if ((i * iWidth) >= pt.y && pt.y <= ((i + 1)*iWidth))	//�ش� ĭ�� ĳ���Ͱ� �����Ѵ���?
					m_cEManger.Draw(e);
			}
		}
	}
	//���������� �������鼭 ���� ĳ���Ͱ� ���� ĳ���ͺ��� �տ� �ִ� ������ ǥ���ϱ� ���ؼ�

	Player::GetInstance()->Draw();
	//�÷��̾��� ���� �׸�
	m_cTipButton->Draw();
	m_cStore.Draw();

	JEngine::TextManager::GetInstance()->SetFont("HY�߰��", 15,0,FW_BOLD);
	JEngine::TextManager::GetInstance()->SetColor(255, 255, 255);
	sprintf(buf, "%0.2f Time", m_fSetTime);
	JEngine::TextManager::GetInstance()->Draw(1100, 0, buf);
	//�ð�
	JEngine::TextManager::GetInstance()->SetColor(255, 255, 255);
	sprintf(buf, "%d Stage", m_iStage + 1);
	JEngine::TextManager::GetInstance()->Draw(1100, 20, buf);
	//��������
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
1. Ŀ�Ǵ��� ����ϸ� ������ ������ ������
2. �������� �����̳� ���� ���¿� ���� �������� ���鼭 Ŀ�Ǵ� ��ų�� ����� �� �ִ�.
3. Ŀ�Ǵ� ��ų�� �� ��ü�� ������ ���� ������ ���ϴ� �����̴�.
4. ���� ��ư
5. stage clear�� die �� ������ UI ����
*/