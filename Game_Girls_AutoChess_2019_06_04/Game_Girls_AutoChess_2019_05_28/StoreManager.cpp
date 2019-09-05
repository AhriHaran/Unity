#include "StoreManager.h"



StoreManager::StoreManager()
{
	memset(&m_stKalina, NULL, sizeof(m_stKalina));
	memset(&m_lisCharPage, NULL, sizeof(m_lisCharPage));
}

void StoreManager::Init()
{
	ifstream load("FILE//Kalina.csv");
	string strName, strTmp;
	int iarr[5];

	if (load.is_open())
	{
		getline(load, strName, ',');
		for (int i = 0; i < 5; i++)
		{
			getline(load, strTmp, ',');
			iarr[i] = stoi(strTmp);
		}
	}
	load.close();

	m_stKalina.st_cAnime.Init(strName, iarr, ANIME_TIME);
	m_stKalina.st_cStartXY.x = 1100;
	m_stKalina.st_cStartXY.y = 600;
	m_bBackStore = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//strore_background.bmp");
	m_stKalina.st_bButton = new JEngine::boolButton;
	m_stKalina.st_bButton->Init(m_stKalina.st_cStartXY.x, m_stKalina.st_cStartXY.y, "RES//button00.bmp", true);
	m_cPoint.x = (WINDOW_SIZE_X - m_bBackStore->GetWidth()) / 2;
	m_cPoint.y = (WINDOW_SIZE_Y - m_bBackStore->GetHeight()) / 2;
	//중앙 정렬
	m_cStore = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//store.bmp");
	m_cStPoint.x = 1080;
	m_cStPoint.y = 550;
	//스토어 글씨 정렬
	m_cCharBack = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//char_page.bmp");
	//캐릭터 백그라운드 프로필 그림
}

void StoreManager::StoreSet()
{
	//라운드 별로 상점을 새로 셋팅
	int iRNum = 0, iX = 0, iY = 0;
	int iarr[MAX_CHAR_SIZE] = { -1, -1, -1, -1, -1 };	//중복 뽑기 방지용
	bool bCheck; //스토어 오픈 시 새로 뽑기
	JEngine::POINT Mpt;

	m_bUse = true;
	m_bOpenStore = false;
	m_lisCharPage = new STORE_LIST; //라운드 시작 시 새로 상적을 뽑아준다.

	for (int i = 0; i < MAX_CHAR_SIZE; i++)
	{
		while (true)
		{
			bCheck = true;
			iRNum = rand() % INDEX_PLAYER_CHAR_END;	//랜덤으로 수를 뽑는다.
			for (int j = 0; j < i; j++)
			{
				if (iRNum == iarr[j])	//중복수 방지용
					bCheck = false;
			}
			if (bCheck)
				break;
		}
		iarr[i] = iRNum;
		//이제 뽑은 수를 토대로 캐릭터를 생성
		st_chPage * Node = m_lisCharPage->CreateNode();
		iX = m_cPoint.x + ((m_bBackStore->GetWidth() - ((120 + 10) * 5)) / 2);
		iY = m_cPoint.y + ((m_bBackStore->GetHeight() - 300) / 2);
		Node->st_cStartXY.x = iX + ((120 + 10) * i);
		Node->st_cStartXY.y = iY;
		//백 이미지 상태
		string strText = "FILE//MyDoll.csv";
		Node->st_cChar.Init(strText, to_string(iRNum), ANIME_TIME);

		iX = Node->st_cStartXY.x + 15;
		iY = Node->st_cStartXY.y + 40;

		Node->st_cChar.PointSet(CHAR_XY, iX, iY);
		//캐릭터 위치
		Node->st_cChar.PointSet(CHAR_NAME_XY, iX, iY + 120);
		//캐릭터 이름 위치
		Node->st_cChar.PointSet(CHAR_STATS_XY, iX, iY + 155);
		//스탯 위치

		Node->st_bButton = new JEngine::boolButton;
		Node->st_bButton->Init(Node->st_cStartXY.x, (Node->st_cStartXY.y + 300 + 15), "RES//buy.bmp", true);
		m_lisCharPage->PushNode(Node);
	}
}

void StoreManager::Update(float fDelta)
{
	bool bLoop;
	m_stKalina.st_cAnime.Update(fDelta, bLoop);
	if (m_bOpenStore)
	{
		for (int i = 0; i < MAX_CHAR_SIZE; i++)
		{
			st_chPage * Node = m_lisCharPage->FindNode(i);
			Node->st_cChar.Update(fDelta);
		}
	}
}

void StoreManager::KeyInput(JEngine::POINT Cursorpt)
{
	if (m_bUse)
	{
		if (m_stKalina.st_bButton->Update())	//카리나 선택 시 상점 창 오픈
			m_bOpenStore = true;
		//열려 있는 상태에서 다른 곳을 클릭하면 상점이 닫힘
		if (JEngine::InputManager::GetInstance()->isKeyDown(VK_LBUTTON))
		{
			//키를 눌렀다.
			if ((Cursorpt.x < m_cPoint.x || Cursorpt.x >(m_cPoint.x + m_bBackStore->GetWidth())) ||
				(Cursorpt.y < m_cPoint.x || Cursorpt.y >(m_cPoint.y + m_bBackStore->GetHeight())))	//상점 영역 밖이면
				m_bOpenStore = false;
			//상점 페이지 구매를 선택하였다.
		}
		if (m_bOpenStore)
		{
			for (int i = 0; i < MAX_CHAR_SIZE; i++)
			{
				st_chPage * Node = m_lisCharPage->FindNode(i);
				if (Node->st_bButton->Update())
					Player::GetInstance()->BuyChar(Node->st_cChar.ReturnIndex(), Node->st_cChar.ReturnGold());	//캐릭터의 인덱스와 가격을 반환 받는다.
			}
			//구매
		}
	}
}

void StoreManager::Draw()
{
	m_stKalina.st_cAnime.Draw(m_stKalina.st_cStartXY);
	m_stKalina.st_bButton->Draw();
	m_cStore->Draw(m_cStPoint);

	if (m_bOpenStore)
	{
		//이상태에서는 오픈
		m_bBackStore->Draw(m_cPoint);
		//상점의 백그라운드를 그려준다.
		for (int i = 0; i < MAX_CHAR_SIZE; i++)
		{
			st_chPage * Node = m_lisCharPage->FindNode(i);
			m_cCharBack->Draw(Node->st_cStartXY);
			Node->st_cChar.Draw();
			Node->st_cChar.NameDraw();
			Node->st_cChar.StatsDraw();
			Node->st_bButton->Draw();
		}
	}
}

void StoreManager::Release()
{
	m_bOpenStore = false;	//상점을 닫아 주고
	m_bUse = false;			//더 이상 상점을 이용하지 못한다.

	if (m_lisCharPage != NULL)
	{
		for (int i = 0; i < MAX_CHAR_SIZE; i++)
		{
			st_chPage * Node = m_lisCharPage->FindNode(i);
			delete Node->st_bButton;
		}
		m_lisCharPage->Release();

		delete m_lisCharPage;
		memset(&m_lisCharPage, NULL, sizeof(m_lisCharPage));
		//상점을 완전히 폐쇄 시켜주고
	}
}

StoreManager::~StoreManager()
{
	delete m_stKalina.st_bButton;
}
