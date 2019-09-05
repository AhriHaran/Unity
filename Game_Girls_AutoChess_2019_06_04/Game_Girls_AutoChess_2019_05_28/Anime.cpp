#include "Anime.h"



Anime::Anime()
{
	memset(&m_stAnime, NULL, sizeof(m_stAnime));
	m_fDeltaTime = 0.0f;
	m_fCurTime = 0.0f;
	m_iCurX = 0;
	m_iCurNum = 0;
}

void Anime::Init(string strName, int *iarr, float fDelta)
{
	if (strName != "NULL")
	{
		m_stAnime = new st_Anime;
		m_stAnime->st_arrPointp[POINT_BIT_SIZE].x = iarr[0];
		m_stAnime->st_arrPointp[POINT_BIT_SIZE].y = iarr[1];
		//하나의 캐릭터의 사이즈
		m_stAnime->st_arrPointp[POINT_CHAR_MIDDLE].x = iarr[2];
		m_stAnime->st_arrPointp[POINT_CHAR_MIDDLE].y = iarr[3];
		//캐릭터 중심 좌표
		m_stAnime->st_iMaxBitMap = iarr[4];
		//총 갯수
		m_stAnime->st_arrPointp[POINT_DC_START].x = 0;
		m_stAnime->st_arrPointp[POINT_DC_START].y = 0;
		//DC 시작 위치
		m_Image = JEngine::ResoucesManager::GetInstance()->GetBitmap(strName);
		//이미지
		m_stAnime->st_arrPointp[POINT_BIT_NUM].x = m_Image->GetWidth() / m_stAnime->st_arrPointp[POINT_BIT_SIZE].x;
		m_stAnime->st_arrPointp[POINT_BIT_NUM].y = m_Image->GetHeight() / m_stAnime->st_arrPointp[POINT_BIT_SIZE].y;
		//가로 세로 캐릭터의 갯수
		m_fDeltaTime = fDelta;
	}
}

void Anime::Update(float fDelta, bool & bLoop)
{
	m_fCurTime += fDelta;
	if (m_fCurTime >= m_fDeltaTime)
	{
		if (m_iCurNum < m_stAnime->st_iMaxBitMap - 1)
		{
			if (m_iCurX < m_stAnime->st_arrPointp[POINT_BIT_NUM].x - 1)
			{
				m_stAnime->st_arrPointp[POINT_DC_START].x += m_stAnime->st_arrPointp[POINT_BIT_SIZE].x;	//x 위치로 좌표를 옮겨 준다.
				m_iCurX++;
			}
			else
			{
				//x의 끝에 다다랐다면?
				m_iCurX = 0;
				m_stAnime->st_arrPointp[POINT_DC_START].x = 0;	//x 위치로 좌표를 옮겨 준다.s
				m_stAnime->st_arrPointp[POINT_DC_START].y += m_stAnime->st_arrPointp[POINT_BIT_SIZE].y;
				//x 초기화 해주고 밑으로 한 칸 내림
			}
			m_iCurNum++;
		}
		else
		{
			//전체적으로 다다랐다면 모조리 초기화
			m_iCurX = 0;
			m_stAnime->st_arrPointp[POINT_DC_START].x = 0;
			m_stAnime->st_arrPointp[POINT_DC_START].y = 0;
			m_iCurNum = 0;
			bLoop = true;	//한 바퀴 돌았다.
		}
		m_fCurTime = 0.0f;
	}
}

void Anime::Draw(JEngine::POINT pt)	//시작 위치는 바뀔 수 도 있으니 외부에서 받아 온다.
{
	m_Image->Draw(pt, m_stAnime->st_arrPointp[POINT_BIT_SIZE], m_stAnime->st_arrPointp[POINT_DC_START]);
}

void Anime::Reset()
{
	m_fCurTime = 0.0f;
	m_iCurX = 0;
	m_stAnime->st_arrPointp[POINT_DC_START].x = 0;
	m_stAnime->st_arrPointp[POINT_DC_START].y = 0;
	m_iCurNum = 0;
}

Anime::~Anime()
{
	delete m_stAnime;
}
