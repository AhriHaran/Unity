#ifndef _H_STORE_MANAGER_H_
#define _H_STORE_MANAGER_H_
#include "JEngine.h"
#include "ResoucesManager.h"
#include "GlobalDefine.h"
#include "POINT.h"
#include "Mecro.h"
#include "List.h"
#include "Character.h"
#include "boolButton.h"
#include "InputManager.h"
#include "Player.h"

typedef struct _kalina_
{
	Anime					st_cAnime;
	JEngine::POINT			st_cStartXY;
	JEngine::boolButton *	st_bButton;
}st_Kalina;

typedef struct _page_char_
{
	JEngine::POINT			st_cStartXY;			//백 그라운드 스타트 xy
	JEngine::boolButton *	st_bButton;				//구매를 확인하기 위한 불 버튼
	Character				st_cChar;				//캐릭터 클래스
}st_chPage;
//스토어가 열리면 스토어 페이지의 작은 캐릭터 창

typedef List<st_chPage> STORE_LIST;	//상점의 캐릭터 창

class StoreManager
{
private:
	st_Kalina			m_stKalina;		//카리나
	bool				m_bUse;			//상점은 라운드 시작후 잠시만 이용이 가능하다.
	bool				m_bOpenStore;	//카리나를 클릭하면 상점이 열리고 다른 곳을 클릭하면 상점이 닫힌다.
	JEngine::BitMap	*	m_cCharBack;	//백 그라운드
	JEngine::BitMap *	m_bBackStore;	//백그라운드 스토어
	JEngine::BitMap	*	m_cStore;		//스토어 글씨
	JEngine::POINT		m_cStPoint;		//스토어 글씨 포인트
	JEngine::POINT		m_cPoint;		//백그라운드 포인트
	STORE_LIST		*	m_lisCharPage;	//캐릭터 창
public:
	StoreManager();
	void Init();
	void StoreSet();
	void Update(float fDelta);
	void KeyInput(JEngine::POINT Cursorpt);
	void Draw();
	void Release();
	~StoreManager();
};

#endif