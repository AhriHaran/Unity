#pragma once
#include "GlobalDefine.h"
#include "POINT.h"
#include "SingleTon.h"

namespace JEngine
{
	class TextManager : public SingleTon<TextManager>
	{
	private:
		int						m_iRed;		//레드
		int						m_iGreen;	//그린
		int						m_iBlue;	//블루
		HFONT					m_hFont;	//폰트
		HFONT					m_hOldFont;	//올드 폰트
	public:
		TextManager();
		void							SetColor(int iRed, int iGreen, int iBlue);	//색깔 지정
		void							SetFont(string strText = "맑은고딕", int iHeight = 0, int iAngle = 0, int iThick = FW_NORMAL, bool bItalic = false, bool bUnder = false, bool bPenetrate = false);
		void							Draw(int x, int y, string strText);
		void							Draw(JEngine::POINT pt, string strText);	
		void							ReleaseFont();
		~TextManager();
	};

}
