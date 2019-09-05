#ifndef _H_TITLE_SCENE_H_
#define _H_TITLE_SCENE_H_
//타이틀 씬으로 타이틀만을 비춰준다.
#include "GlobalDefine.h"
#include "JEngine.h"
#include "Mecro.h"
#include "POINT.h"
#include "ResoucesManager.h"

class TitleScene : public JEngine::Scene
{
private:
	JEngine::BitMap		*	m_pTitleScene;		//타이틀 비트맵
	JEngine::POINTF			m_pfPoint;			//시작 위치
public:
	TitleScene();
	virtual void Init(HWND hWnd);
	virtual bool Input(float fETime);
	virtual void Update(float fETime);
	virtual void Draw(HDC hdc);
	virtual void Release();

	virtual ~TitleScene();
};

#endif