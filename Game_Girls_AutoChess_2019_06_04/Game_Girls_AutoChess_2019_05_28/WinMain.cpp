#include <Windows.h>
#include "EngineMain.h"
#include "SceneManager.h"
#include "TitleScene.h"
#include "GameScene.h"


int APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpszCmdParam, int nCmdShow)
{
	srand((unsigned)time(NULL));
	JEngine::EngineMain GEngine("GirlsAutoChess", WINDOW_SIZE_X, WINDOW_SIZE_Y);
	JEngine::SceneManager::GetInstance()->RegistScene(new TitleScene);
	JEngine::SceneManager::GetInstance()->RegistScene(new GameScene);


	return GEngine.StartEngine(hInstance);
}