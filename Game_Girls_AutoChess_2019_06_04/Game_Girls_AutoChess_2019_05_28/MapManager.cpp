#include "MapManager.h"
#include "InputManager.h"


MapManager::MapManager()
{
	m_iTmpX = 0;
	memset(&m_starrMap, NULL, sizeof(m_starrMap));
}

void MapManager::Init()
{
	m_starrMap = new st_Map*[TILE_MAP_SIZE];
	for (int i = 0; i < TILE_MAP_SIZE; i++)
		m_starrMap[i] = new st_Map[TILE_MAP_SIZE];
	//���� �Ҵ����� �� ����
	JEngine::BitMap * Black, * White, * BlockCur, *BLock00, *BLock01;
	Black = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//chess_00.bmp");
	White = JEngine::ResoucesManager::GetInstance()->GetBitmap("RES//chess_01.bmp");
	for (int y = 0; y < TILE_MAP_SIZE; y++)
	{
		if (y % 2 == 0)
		{
			BLock00 = Black;
			BLock01 = White;
		}
		else if (y % 2 != 0)
		{
			BLock00 = White;
			BLock01 = Black;
		}

		for (int x = 0; x < TILE_MAP_SIZE; x++)
		{
			if (x == 0 || x % 2 == 0)
				BlockCur = BLock00;
			else if (x % 2 != 0)
				BlockCur = BLock01;
			m_starrMap[y][x].st_Image = BlockCur;
			m_starrMap[y][x].st_bDraw = false;
			m_starrMap[y][x].st_bChar = false;

			if (m_iTmpX == 0)
			{
				m_iTmpX = (WINDOW_SIZE_X - (m_starrMap[y][x].st_Image->GetWidth() * TILE_MAP_SIZE)) / 2;
				m_iSIze.x = m_starrMap[y][x].st_Image->GetWidth();
				m_iSIze.y = m_starrMap[y][x].st_Image->GetHeight();
			}
			m_starrMap[y][x].st_iPoint.x = m_iTmpX + (x *m_iSIze.x);
			m_starrMap[y][x].st_iPoint.y = y * m_iSIze.y;

		}
	}
	//���������� ó�� ���� �� ������ ���� ������ ���ؼ�
	m_iCount = 0;
	m_iCount1 = 0;
	m_fMapTime = 0.0f;
}

void MapManager::Update(float fDelta)
{
	if (m_iCount < 19)
	{
		int iX = 0, iY = 0;
		m_fMapTime += fDelta;
		if (m_fMapTime >= 0.05f)
		{
			if (m_iCount < TILE_MAP_SIZE)
			{
				iX = m_iCount;
				m_iCount1++;
			}
			else
			{
				iX = TILE_MAP_SIZE - 1;
				iY = m_iCount - iX;
				m_iCount1--;
			}
			for (int i = 0; i < m_iCount1; i++)
			{
				m_starrMap[iY][iX].st_bDraw = true;
				iX--;
				iY++;
			}
			m_fMapTime = 0.0f;
			m_iCount++;
		}
	}
}

void MapManager::Draw()
{
	for (int y = 0; y < TILE_MAP_SIZE; y++)
	{
		for (int x = 0; x < TILE_MAP_SIZE; x++)
		{
			if (m_starrMap[y][x].st_bDraw)
				m_starrMap[y][x].st_Image->DrawBitblt(m_starrMap[y][x].st_iPoint.x, m_starrMap[y][x].st_iPoint.y);
		}
	}
	//�� ��ο�
}

bool MapManager::KeyInput(JEngine::POINT pt, JEngine::POINT & Dept, bool bChar)
{
	if (RangeCheck(pt.x, pt.y))	//���� ���ΰ�?
	{
		Dept.x = m_starrMap[pt.y][pt.x].st_iPoint.x;
		Dept.y = m_starrMap[pt.y][pt.x].st_iPoint.y;	//���� ������ �ϴ� ��ġ�� ���� �� ���� ���� ��ġ�� ��ȯ
		if (!m_starrMap[pt.y][pt.x].st_bChar)	
		{
			if(bChar)//ĳ���͸� ���� ����
				m_starrMap[pt.y][pt.x].st_bChar = true;
			return true;
		}
	}
	return false;
}

void MapManager::MoveChar(JEngine::POINT pt)
{
	//���� ��
	if (RangeCheck(pt.x, pt.y))	//���� ���ΰ�?
		m_starrMap[pt.y][pt.x].st_bChar = false;
	//ĳ���Ͱ� �����Ͻ� ù Ŭ�� ��ġ���� ������
}

JEngine::POINT MapManager::XYPoint(int iX, int iY)
{
	//�̱� �������� ��ǥ�� �̾��� ��
	JEngine::POINT pt;
	pt.x = m_iTmpX + (iX * m_iSIze.x);
	pt.y = (iY * m_iSIze.y);
	return pt;
}

bool MapManager::RangeCheck(int & iX, int & iY)//�ش� XY ��ǥ�� ��ü �� ���� ����� �װ��� ã�Ƽ� �迭 xy �ѹ��� �ٲ��ش�.
{
	int iSizeX = m_iTmpX + (TILE_MAP_SIZE * m_iSIze.x);
	int iSizeY = (TILE_MAP_SIZE * m_iSIze.y);
	if ((iX >= m_iTmpX && iX < iSizeX) && (iY >= 0 && iY < iSizeY))
	{
		iX = (iX - m_iTmpX) / m_iSIze.x;
		iY = iY / m_iSIze.y;
		return true;
	}
	return false;
}

void MapManager::Release()
{
	if (m_starrMap != NULL)
	{
		for (int i = 0; i < TILE_MAP_SIZE; i++)
			delete[] m_starrMap[i];
		delete[] m_starrMap;
		memset(&m_starrMap, NULL, sizeof(m_starrMap));
	}
}

MapManager::~MapManager()
{
	DestroyInstance();
}
