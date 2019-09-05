#include "Gage.h"



Gage::Gage()
{
}

void Gage::Set(string strGage, int iX, int iY, float fMax)
{
	m_cBar = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//bar.bmp");
	m_cUnderBar = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//underbar.bmp");
	m_cGage = JEngine::ResoucesManager::GetInstance()->GetBitmap(strGage);
	//그림 세 개 셋팅
	m_cGagePT.x = iX;
	m_cGagePT.y = iY;
	//그림의 게이지의 시작 x, y
	m_fMax = fMax;
	m_fCurUP = m_cGage->GetWidth() / m_fMax;
	//게이지 맥스와 curUP을 셋팅
	m_cGageEPT.y = m_cGage->GetHeight();
}

void Gage::Update(float fCur)
{
	float fX = 0.0f;

	if (fCur  * m_fCurUP >= m_fMax)
		fX = m_cGage->GetWidth();
	else if (fCur  * m_fCurUP < 0.0f)
		fX = 0.0f;
	else
		fX = fCur * m_fCurUP;
	m_cGageEPT.x = fX;
}

void Gage::Draw()
{
	m_cUnderBar->Draw(m_cGagePT);
	m_cGage->Draw(m_cGagePT, m_cGageEPT);
	m_cBar->Draw(m_cGagePT);
}

Gage::~Gage()
{
}
