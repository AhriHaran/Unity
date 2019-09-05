#ifndef _H_ANIME_H_
#define _H_ANIME_H_
#include "JEngine.h"
#include "ResoucesManager.h"
#include "POINT.h"
#include "GlobalDefine.h"
#include "Mecro.h"

typedef struct _anime_
{
	JEngine::POINT		st_arrPointp[POINT_END];//그리기 시작 포인트, DC 시작 포인트, 그려줄 비트맵 사이즈, 현재 비트맵,비트맵의 갯수(통짜 시트에 가로 몇 개 세로 몇 개인가)
	int					st_iMaxBitMap;
}st_Anime;
//파일 입출력 형식으로 이를 관리

class Anime
{
private:
	JEngine::BitMap		*	m_Image;		//이미지
	st_Anime			*	m_stAnime;		//이미지 프레임 저장한 파일
	int						m_iCurX;		//현재 x 갯수
	int						m_iCurNum;		//현재 갯수
	float					m_fDeltaTime;	//애니메이션 설정 시간
	float					m_fCurTime;		//현재 컬 타임
public:
	Anime();
	void Init(string strName, int *iarr, float fDelta);
	void Update(float fDelta, bool & bLoop);
	void Draw(JEngine::POINT pt);
	void Reset();
	inline int GetSizeX()
	{
		return m_stAnime->st_arrPointp[POINT_BIT_SIZE].x;
		//이미지 사이즈 x
	}
	inline int GetSizeY()
	{
		return m_stAnime->st_arrPointp[POINT_BIT_SIZE].y;
		//이미지 사이즈 y
	}
	JEngine::POINT ReturnPT(int iIndex)
	{
		return m_stAnime->st_arrPointp[iIndex];
	}
	~Anime();
};


#endif // !_H_ANIME_H_