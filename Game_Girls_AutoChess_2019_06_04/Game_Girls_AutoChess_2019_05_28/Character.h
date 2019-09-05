#ifndef _H_CHARACTER_H_
#define _H_CHARACTER_H_
#include "JEngine.h"
#include "Anime.h"
#include "Skill.h"
#include "Gage.h"
enum CHAR_POINT
{
	CHAR_START,
	CHAR_XY = CHAR_START,		//캐릭터 그림 좌표
	CHAR_MIDDLE_XY,				//캐릭터 중심 좌표
	CHAR_PORT_XY,				//캐릭터 초상화 xy
	CHAR_NAME_XY,				//캐릭터 네임 xy
	CHAR_STATS_XY,				//캐릭터 스탯 xy
	CHAR_END
};

enum CHAR_PORTRAITS
{
	PORTRAITS_START,
	PORTRAITS_NORMAL = PORTRAITS_START,
	PORTRAITS_DEMAGE,
	PORTRAITS_END
};
//캐릭터 초상화

enum CHAR_HIT_ATTACK
{
	CHAR_H_A_START,
	CHAR_ATTACK = CHAR_H_A_START,
	CHAR_HIT,
	CHAR_H_A_END
};

enum PLAYER_GAGE
{
	GAGE_START,
	GAGE_HP = GAGE_START,
	GAGE_SKILL,
	GAGE_END
};

//적과 내가 공통 적으로 가지는 캐릭터들의 정보

//스킬은 버프 형식으로 HP, ATK, DEF, SkillGage, 공속

class Character
{
private:
	string				m_strName;					//캐릭터 이름
	string				m_strType;					//총기타입

	Anime				m_cAnime[STATE_END];		//애니메이션
	Anime				m_cHitAttack[CHAR_H_A_END];	//공격 피격 애니메이션
	Gage				m_cArrGage[GAGE_END];		//게이지
	Skill		*		m_cSkill;					//스킬

	int					m_iarrStats[STATS_END];		//스탯
	int					m_iarrSkillUP[SKILL_BUF_END];//상승 스킬 게이지
	int					m_iIndex;					//도감 인덱스
	int					m_iRare;					//레어도
	int					m_iGold;					//가격
	int					m_iSkillType;				//스킬의 존재 여부를 확인

	float				m_fHp;						//캐릭터의 MAX HP
	float				m_fCurHp;					//캐릭터의 현재 HP
	float				m_fSkillGage;				//캐릭터 스킬 게이지
	float				m_fCurSkillGage;			//캐릭터의 현재 스킬 게이지
	float				m_fAtkSpeed;				//캐릭터 공속 스피드
	float				m_fCurSpeed;				//캐릭터의 공속 계산

	CHARACTER_STATE		m_eState;					//캐릭터 상태
	CHAR_PORTRAITS		m_ePort;					//초상화 상태
	SKILL_TYPE			m_eType;					//해당 캐릭터가 받은 스킬 버프, 디버프

	JEngine::BitMap	*	m_carrPort[PORTRAITS_END];	//캐릭터 초상화
	JEngine::BitMap *	m_cRangeBit;				//사정거리 비트맵
	JEngine::POINT		m_carrPoint[CHAR_END];		//캐릭터 각 드로우
	JEngine::RECT		m_cRect;					//캐릭터 렉트

	bool				m_bAttack;					//공격 가능
	bool				m_bHit;						//맞는 중
	bool				m_bAnime;					//애니메이션 출력 가능
	bool				m_bSkill;					//스킬 발동
public:
	Character();
	void Init(string FileName, string strIndex, float fDelta);	//열 파일 이름을 보내준다. 인덱스 도감 넘버
	void AnimeSet(ifstream * Load, Anime * pAni, int iSize, int iAnimeSize);
	void GageSet(int iWidth, JEngine::POINT pt);
	void Update(float fDelta);					//업데이트
	void Update();								//게이지 업데이트
	void Draw();								//캐릭터 드로우
	void StatsDraw();							//스탯 드로우
	void NameDraw();							//이름 드로우
	void PortDraw();							//초상화 드로우
	void RangeDraw();							//사거리관련 드로우
	void GageDraw();
	void PointSet(int iIndex, int iX, int iY);	//캐릭터 위치 조정
	void SkillUP(int * iarr, SKILL_TYPE eType);
	void SkillReset();
	void HitCheck(int iATK);
	bool RangeCheck(JEngine::POINT Ept);
	bool RectCheck(JEngine::POINT pt);
	void AnimeReset()
	{
		m_cAnime[m_eState].Reset();
	}
	JEngine::POINT ReturnPT(int iIndex)
	{
		return m_carrPoint[iIndex];				//포인트 반환
	}
	JEngine::POINT ReturnAPT(int iIndex)
	{
		return m_cAnime[m_eState].ReturnPT(iIndex);//애니메이션 포인트 반환
	}
	Skill * ReturnSkill()
	{
		return m_cSkill;
	}
	string ReturnType()
	{
		return m_strType;
	}
	bool* ReturnAttack()
	{
		return &m_bAttack;						//공격 가능 상태를 반환 받는다. 포인트 형식
	}
	CHARACTER_STATE* ReturnState()				//캐릭터 상태 반화
	{
		return &m_eState;
	}
	inline int ReturnStats(int iIndex)
	{
		return m_iarrStats[iIndex];
	}
	inline int ReturnIndex()
	{
		return m_iIndex;
	}
	inline int ReturnGold()
	{
		return m_iGold;
	}
	int ReturnPortX()
	{
		return m_carrPort[m_ePort]->GetWidth();
	}
	int ReturnPortY()
	{
		return m_carrPort[m_ePort]->GetHeight();
	}
	int ReturnAnimeX()
	{
		return m_cAnime[m_eState].GetSizeX();
	}
	int ReturnAnimeY()
	{
		return m_cAnime[m_eState].GetSizeY();
	}
	~Character();
};

#endif