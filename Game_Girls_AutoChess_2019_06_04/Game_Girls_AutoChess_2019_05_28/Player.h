#ifndef _H_PLAYER_H_
#define _H_PLAYER_H_
#include "JEngine.h"
#include "GlobalDefine.h"
#include "Mecro.h"
#include "Character.h"
#include "List.h"
#include "SingleTon.h"
#include "Gage.h"
#include "boolButton.h"
typedef struct _profile_char_
{
	Character				st_cChar;				//캐릭터
	JEngine::POINT			st_cBackPoint;			//프로필 백 그라운드 포인트
	JEngine::RECT			st_cRect;				//렉트 체크
	bool					st_bDrawSD;				//SD 캐릭터를 그려줘라
	bool					st_bDeploy;				//맵 배치 완료
	bool					st_bSkill;				//스킬 사용
}st_ProChar;										//캐릭터 프로필 창

typedef List<st_ProChar> MY_LIST;

typedef struct _commander_
{
	Anime					st_carrAinme[STATE_ALL_END];	//지휘관 캐릭터의 애니메이션
	Anime					st_carrEffect[CHAR_H_A_END];	//스킬 임팩트 애니메이셔
	CHARACTER_STATE			st_eState;						//지휘관 캐릭터의 상태
	JEngine::POINT			st_cCharPoint[CHAR_NAME_XY];	//캐릭터 포인트
	JEngine::POINT			st_cEffectPT[3];				//임팩트 포인트
	JEngine::POINT			st_cFirePT;						//발사 임팩트
	JEngine::RECT			st_cRect;						//캐릭터 렉트
	JEngine::BitMap		*	st_cComProfile;					//캐릭터 프로필 이미지
	JEngine::boolButton	*	st_cSkillButton;				//스킬 버튼
	bool					st_bDeploy;						//맵 배치 완료
	bool					st_bSet;						//배치 완료
	bool					st_bShot;						//사용 가능
	bool					st_barrEffect[3];				//임팩트 확인
	bool					st_bFire;						//발사
	Gage				*	st_cGage;						//스킬 게이지
	float					st_cCurSkill;					//현재 스킬 게이지
	float					st_cMaxSkill;					//맥스 스킬 게이지
	int						st_iCurEffect;					//임팩트
	int						st_iAtk;						//스킬 공격력
}st_Commander;
/*
지휘관 캐릭터로, 캐릭터가 사망하면 게임이 완전히 끝이 난다.
지휘관 캐릭터는 스킬 처럼 포격 지원이 가능하다
*/


class Player : public SingleTon <Player>
{
private:
	JEngine::BitMap		*	m_cSkillBit;		//스킬 사용 가능 여부 표시
	JEngine::BitMap		*	m_cAutoON;			//오토 버튼 ON
	JEngine::BitMap		*	m_cAutoOFF;			//오토 버튼 OFF
	JEngine::BitMap		*	m_cBackBit;			//백 프로필 비트맵
	JEngine::boolButton	*	m_cAutoButton;		//오토 버튼
	JEngine::POINT			m_cAutoPT;
	int						m_iGold;			//내가 현재 보유한 골드
	int						m_iCharNum;			//현재 보유 중인 캐릭터
	int						m_iSkillNum;		//스킬 발동 캐릭터
	bool					m_bAuto;			//오토 체크
	MY_LIST				*	m_MyList;			//내 캐릭터 리스트
	st_Commander		*	m_stCom;			//지휘관 캐릭터
public:
	Player();
	void Init();
	void BuyChar(int iIndex, int iGold);		//캐릭터 구매
	void Update(float fDelta, bool bGame);
	void CommanderSet(int iX, int iY);
	void Draw();
	void Draw(int iIndex);
	void KeyInput(JEngine::POINT Cursorpt);
	void Release();
	void ComRelease();
	JEngine::POINT ReturnPt(int iIndex, int iPoint);
	bool LoseCheck();
	Character* ReturnNode(int iIndex);
	bool ReturnDeploy(int iIndex);
	st_Commander * ReturnCom()
	{
		return m_stCom;
	}
	void Reset(int iStage);
	inline int ReturnCurChar()
	{
		return m_iCharNum;
	}
	int * ReturnSkillNum()
	{
		return &m_iSkillNum;
	}
	~Player();
};


#endif // !_H_PLAYER_H_