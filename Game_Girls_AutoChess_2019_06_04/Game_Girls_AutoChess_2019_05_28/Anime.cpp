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
		//�ϳ��� ĳ������ ������
		m_stAnime->st_arrPointp[POINT_CHAR_MIDDLE].x = iarr[2];
		m_stAnime->st_arrPointp[POINT_CHAR_MIDDLE].y = iarr[3];
		//ĳ���� �߽� ��ǥ
		m_stAnime->st_iMaxBitMap = iarr[4];
		//�� ����
		m_stAnime->st_arrPointp[POINT_DC_START].x = 0;
		m_stAnime->st_arrPointp[POINT_DC_START].y = 0;
		//DC ���� ��ġ
		m_Image = JEngine::ResoucesManager::GetInstance()->GetBitmap(strName);
		//�̹���
		m_stAnime->st_arrPointp[POINT_BIT_NUM].x = m_Image->GetWidth() / m_stAnime->st_arrPointp[POINT_BIT_SIZE].x;
		m_stAnime->st_arrPointp[POINT_BIT_NUM].y = m_Image->GetHeight() / m_stAnime->st_arrPointp[POINT_BIT_SIZE].y;
		//���� ���� ĳ������ ����
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
				m_stAnime->st_arrPointp[POINT_DC_START].x += m_stAnime->st_arrPointp[POINT_BIT_SIZE].x;	//x ��ġ�� ��ǥ�� �Ű� �ش�.
				m_iCurX++;
			}
			else
			{
				//x�� ���� �ٴٶ��ٸ�?
				m_iCurX = 0;
				m_stAnime->st_arrPointp[POINT_DC_START].x = 0;	//x ��ġ�� ��ǥ�� �Ű� �ش�.s
				m_stAnime->st_arrPointp[POINT_DC_START].y += m_stAnime->st_arrPointp[POINT_BIT_SIZE].y;
				//x �ʱ�ȭ ���ְ� ������ �� ĭ ����
			}
			m_iCurNum++;
		}
		else
		{
			//��ü������ �ٴٶ��ٸ� ������ �ʱ�ȭ
			m_iCurX = 0;
			m_stAnime->st_arrPointp[POINT_DC_START].x = 0;
			m_stAnime->st_arrPointp[POINT_DC_START].y = 0;
			m_iCurNum = 0;
			bLoop = true;	//�� ���� ���Ҵ�.
		}
		m_fCurTime = 0.0f;
	}
}

void Anime::Draw(JEngine::POINT pt)	//���� ��ġ�� �ٲ� �� �� ������ �ܺο��� �޾� �´�.
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
