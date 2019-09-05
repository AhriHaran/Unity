#pragma once
#include "GlobalDefine.h"
#include "POINT.h"
#include "SingleTon.h"

namespace JEngine
{
	class TextManager : public SingleTon<TextManager>
	{
	private:
		int						m_iRed;		//����
		int						m_iGreen;	//�׸�
		int						m_iBlue;	//���
		HFONT					m_hFont;	//��Ʈ
		HFONT					m_hOldFont;	//�õ� ��Ʈ
	public:
		TextManager();
		void							SetColor(int iRed, int iGreen, int iBlue);	//���� ����
		void							SetFont(string strText = "�������", int iHeight = 0, int iAngle = 0, int iThick = FW_NORMAL, bool bItalic = false, bool bUnder = false, bool bPenetrate = false);
		void							Draw(int x, int y, string strText);
		void							Draw(JEngine::POINT pt, string strText);	
		void							ReleaseFont();
		~TextManager();
	};

}
