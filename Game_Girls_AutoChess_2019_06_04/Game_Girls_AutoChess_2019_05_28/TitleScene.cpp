#include "TitleScene.h"
#include "InputManager.h"
#include "SceneManager.h"
#include "TextManager.h"



TitleScene::TitleScene()
{
	m_pfPoint.x = 0.0f;
	m_pfPoint.y = 0.0f;
}

void TitleScene::Init(HWND hWnd)
{
	//���
	JEngine::InputManager::GetInstance()->Clear();
	JEngine::InputManager::GetInstance()->RegistKeyCode(VK_LBUTTON);
	JEngine::InputManager::GetInstance()->RegistKeyCode(VK_SPACE);
	JEngine::InputManager::GetInstance()->RegistKeyCode(VK_ESCAPE);
	//Ÿ��Ʋ ȭ�鿡�� ��ư�� ������ ���� ������ �Ѿ��.
	//esc�� ���� Ű
	m_pTitleScene = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//titlescene.bmp");
	//Ÿ��Ʋ ȭ�� ���
	m_pfPoint.x = (WINDOW_SIZE_X - m_pTitleScene->GetWidth()) / 2;
	m_pfPoint.y = (WINDOW_SIZE_Y - m_pTitleScene->GetHeight()) / 2;
	//���� ��ǥ ��� ���� �׸��� ��ü ũ�⺸�� ���� ��쿡�� �߾����� ������ ��ġ
	//Ÿ��Ʋ �� ������ ������ ���� ������ �Ѿ��.
}
bool TitleScene::Input(float fETime)
{
	if (JEngine::InputManager::GetInstance()->isKeyDown(VK_LBUTTON) || JEngine::InputManager::GetInstance()->isKeyDown(VK_SPACE))
		JEngine::SceneManager::GetInstance()->LoadScene(GAME_SCENE);
	else if (JEngine::InputManager::GetInstance()->isKeyDown(VK_ESCAPE))
		return true;
	return false;
}
void TitleScene::Update(float fETime)
{

}
void TitleScene::Draw(HDC hdc)
{
	m_pTitleScene->DrawBitblt((int)m_pfPoint.x, (int)m_pfPoint.y);
	JEngine::TextManager::GetInstance()->SetFont("HY�߰��", 25, 10, FW_BOLD);
	JEngine::TextManager::GetInstance()->SetColor(255, 255, 255);
	JEngine::TextManager::GetInstance()->Draw(470, 500, "Space or Click to Start");
	JEngine::TextManager::GetInstance()->ReleaseFont();
}
void TitleScene::Release()
{

}

TitleScene::~TitleScene()
{
}
