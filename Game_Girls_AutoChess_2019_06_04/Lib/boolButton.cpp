#include "boolButton.h"
#include "ResoucesManager.h"
#include "InputManager.h"

namespace JEngine
{
	boolButton::boolButton()
	{
	}


	boolButton::~boolButton()
	{
	}

	bool boolButton::Update()
	{
		if (InputManager::GetInstance()->isKeyUp(VK_LBUTTON))
		{
			if (m_rcCol.isPtin(InputManager::GetInstance()->GetMousePoint()))
				return m_bClick;
		}

		return false;
	}

	void boolButton::Init(int x, int y, string btnImg, bool bClick)
	{
		m_pDefault = ResoucesManager::GetInstance()->GetBitmap(btnImg);
		m_bClick = bClick;

		m_ptDraw.x = x;
		m_ptDraw.y = y;

		m_rcCol.left = x;
		m_rcCol.top = y;
		m_rcCol.right = m_rcCol.left + m_pDefault->GetWidth();
		m_rcCol.bottom = m_rcCol.top + m_pDefault->GetHeight();
	}

	void boolButton::Draw()
	{
		m_pDefault->Draw(m_ptDraw);
	}

}