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
	JEngine::POINT			st_cStartXY;			//�� �׶��� ��ŸƮ xy
	JEngine::boolButton *	st_bButton;				//���Ÿ� Ȯ���ϱ� ���� �� ��ư
	Character				st_cChar;				//ĳ���� Ŭ����
}st_chPage;
//���� ������ ����� �������� ���� ĳ���� â

typedef List<st_chPage> STORE_LIST;	//������ ĳ���� â

class StoreManager
{
private:
	st_Kalina			m_stKalina;		//ī����
	bool				m_bUse;			//������ ���� ������ ��ø� �̿��� �����ϴ�.
	bool				m_bOpenStore;	//ī������ Ŭ���ϸ� ������ ������ �ٸ� ���� Ŭ���ϸ� ������ ������.
	JEngine::BitMap	*	m_cCharBack;	//�� �׶���
	JEngine::BitMap *	m_bBackStore;	//��׶��� �����
	JEngine::BitMap	*	m_cStore;		//����� �۾�
	JEngine::POINT		m_cStPoint;		//����� �۾� ����Ʈ
	JEngine::POINT		m_cPoint;		//��׶��� ����Ʈ
	STORE_LIST		*	m_lisCharPage;	//ĳ���� â
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