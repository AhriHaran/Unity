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
	JEngine::POINT		st_iPoint;		//�� ��ǥ
	JEngine::BitMap *	st_Image;		//�� �׸�
	bool				st_bDraw;		//��ο�
	bool				st_bChar;		//ĳ���Ͱ� �ش� ��ǥ�� �����Ѵ�.
}st_Map;
//��

class MapManager : public SingleTon<MapManager>
{
private:
	st_Map		**		m_starrMap;			//�� ���� �迭 ����ü, �ʰ��� ��ȣ�ۿ��� �����Ƿ� ���� ������ �����Ѵ�.
	JEngine::POINT		m_iSIze;			//�� �׸� ������
	int					m_iTmpX;			//������ �� X
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