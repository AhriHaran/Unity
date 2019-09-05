#pragma once
#include "GlobalDefine.h"
#include "UIObject.h"
#include "BitMap.h"


namespace JEngine
{
	class boolButton : public JEngine::UIObject
	{
	private:
		ANCHOR					m_anchor;
		POINT					m_ptDraw;
		RECT					m_rcCol;
		BitMap*					m_pDefault;
		bool					m_bClick;
	public:
		void			Init(int x, int y, string btnImg, bool bClick);
		virtual bool	Update();
		virtual void	Draw();

		boolButton();
		virtual ~boolButton();
	};

}
