#include "TextManager.h"
#include "ResoucesManager.h"

namespace JEngine
{
	TextManager::TextManager()
	{
		m_iRed = 0;
		m_iGreen = 0;
		m_iBlue = 0;
	}

	TextManager::~TextManager()
	{
		DestroyInstance();
	}

	void TextManager::SetColor(int iRed, int iGreen, int iBlue)
	{
		m_iRed = iRed;
		m_iGreen = iGreen;
		m_iBlue = iBlue;
	}

	void TextManager::SetFont(string strText, int iHeight, int iAngle, int iThick, bool bItalic, bool bUnder, bool bPenetrate)
	{
		//폰트 높이, 폰트 각도, 폰트의 두깨, 기울임, 밑줄, 관통선, 글꼴
		m_hFont = CreateFont(iHeight, 0, iAngle, 0, iThick, bItalic, bUnder, bPenetrate, HANGUL_CHARSET, 0, 0, 0, VARIABLE_PITCH | FF_ROMAN, strText.c_str());
		m_hOldFont = (HFONT)SelectObject(JEngine::ResoucesManager::GetInstance()->GetBackDC(), m_hFont);
	}

	void TextManager::Draw(int x, int y, string strText)
	{
		SetTextColor(JEngine::ResoucesManager::GetInstance()->GetBackDC(), RGB(m_iRed, m_iGreen, m_iBlue));
		SetBkMode(JEngine::ResoucesManager::GetInstance()->GetBackDC(), TRANSPARENT);
		TextOut(ResoucesManager::GetInstance()->GetBackDC(), x, y, strText.c_str(), strlen(strText.c_str()));
	}

	void TextManager::ReleaseFont()
	{
		SelectObject(JEngine::ResoucesManager::GetInstance()->GetBackDC(), m_hOldFont);
		DeleteObject(m_hFont);
	}

	void TextManager::Draw(JEngine::POINT pt, string strText)
	{
		Draw(pt.x, pt.y, strText);
	}

}