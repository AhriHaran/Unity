#ifndef _H_TITLE_SCENE_H_
#define _H_TITLE_SCENE_H_
//Ÿ��Ʋ ������ Ÿ��Ʋ���� �����ش�.
#include "GlobalDefine.h"
#include "JEngine.h"
#include "Mecro.h"
#include "POINT.h"
#include "ResoucesManager.h"

class TitleScene : public JEngine::Scene
{
private:
	JEngine::BitMap		*	m_pTitleScene;		//Ÿ��Ʋ ��Ʈ��
	JEngine::POINTF			m_pfPoint;			//���� ��ġ
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