#ifndef _H_MAP_MANAGER_H_
#define  _H_MAP_MANAGER_H_
#include "JEngine.h"
#include "Mecro.h"
#include "GlobalDefine.h"
#include "ResoucesManager.h"
#include "POINT.h"
#include "SingleTon.h"

typedef struct _map_
{
	JEngine::POINT		st_iPoint;		//맵 좌표
	JEngine::BitMap *	st_Image;		//맵 그림
	bool				st_bDraw;		//드로우
	bool				st_bChar;		//캐릭터가 해당 좌표에 존재한다.
}st_Map;
//맵

class MapManager : public SingleTon<MapManager>
{
private:
	st_Map		**		m_starrMap;			//맵 이중 배열 구조체, 맵과의 상호작용이 없으므로 게임 씬에서 관리한다.
	JEngine::POINT		m_iSIze;			//맵 그림 사이즈
	int					m_iTmpX;			//여분의 맵 X
	int					m_iCount;
	int					m_iCount1;
	float				m_fMapTime;
public:
	MapManager();
	void Init();
	void Update(float fDelta);
	void Draw();
	bool KeyInput(JEngine::POINT pt, JEngine::POINT & Dept, bool bChar = false);
	void MoveChar(JEngine::POINT pt);
	JEngine::POINT XYPoint(int iX, int iY);
	bool RangeCheck(int & iX, int & iY);
	void Release();
	inline int ReturnSizeX()
	{
		return m_iSIze.x;
	}
	inline int ReturnSizeY()
	{
		return m_iSIze.y;
	}
	inline int ReturnTmpX()
	{
		return m_iTmpX;
	}
	~MapManager();
};

#endif