#ifndef _H_GAGE_H_
#define _H_GAGE_H_
#include "JEngine.h"
#include "GlobalDefine.h"
#include "ResoucesManager.h"
#include "POINT.h"
class Gage
{
private:
	JEngine::BitMap * m_cBar;		//게이지 바
	JEngine::BitMap * m_cUnderBar;	//게이지 언더 바
	JEngine::BitMap	* m_cGage;		//게이지
	JEngine::POINT	  m_cGagePT;	//게이지 드로우 시작 포인트
	JEngine::POINT	  m_cGageEPT;	//게이지 끝 포인트
	float			  m_fCurUP;		//게이지의 상승 하강 Cur
	float			  m_fMax;		//게이지의 최댓값
public:
	Gage();
	void Set(string strGage, int iX, int iY, float fMax);
	void Update(float fCur);
	void Draw();
	float ReturnCurUP()
	{
		return m_fCurUP;
	}
	~Gage();
};


#endif // !_H_GAGE_H_