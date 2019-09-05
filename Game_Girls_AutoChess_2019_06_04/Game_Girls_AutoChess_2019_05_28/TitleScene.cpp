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
	//등록
	JEngine::InputManager::GetInstance()->Clear();
	JEngine::InputManager::GetInstance()->RegistKeyCode(VK_LBUTTON);
	JEngine::InputManager::GetInstance()->RegistKeyCode(VK_SPACE);
	JEngine::InputManager::GetInstance()->RegistKeyCode(VK_ESCAPE);
	//타이틀 화면에서 버튼을 눌러서 메인 씬으로 넘어간다.
	//esc는 종료 키
	m_pTitleScene = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//titlescene.bmp");
	//타이틀 화면 등록
	m_pfPoint.x = (WINDOW_SIZE_X - m_pTitleScene->GetWidth()) / 2;
	m_pfPoint.y = (WINDOW_SIZE_Y - m_pTitleScene->GetHeight()) / 2;
	//시작 좌표 등록 막약 그림이 전체 크기보다 작은 경우에는 중앙으로 오도록 배치
	//타이틀 씬 무엇을 눌려도 게임 씬으로 넘어간다.
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
	JEngine::TextManager::GetInstance()->SetFont("HY견고딕", 25, 10, FW_BOLD);
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
