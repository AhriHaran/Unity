#ifndef _H_ANIME_H_
#define _H_ANIME_H_
#include "JEngine.h"
#include "ResoucesManager.h"
#include "POINT.h"
#include "GlobalDefine.h"
#include "Mecro.h"

typedef struct _anime_
{
	JEngine::POINT		st_arrPointp[POINT_END];//�׸��� ���� ����Ʈ, DC ���� ����Ʈ, �׷��� ��Ʈ�� ������, ���� ��Ʈ��,��Ʈ���� ����(��¥ ��Ʈ�� ���� �� �� ���� �� ���ΰ�)
	int					st_iMaxBitMap;
}st_Anime;
//���� ����� �������� �̸� ����

class Anime
{
private:
	JEngine::BitMap		*	m_Image;		//�̹���
	st_Anime			*	m_stAnime;		//�̹��� ������ ������ ����
	int						m_iCurX;		//���� x ����
	int						m_iCurNum;		//���� ����
	float					m_fDeltaTime;	//�ִϸ��̼� ���� �ð�
	float					m_fCurTime;		//���� �� Ÿ��
public:
	Anime();
	void Init(string strName, int *iarr, float fDelta);
	void Update(float fDelta, bool & bLoop);
	void Draw(JEngine::POINT pt);
	void Reset();
	inline int GetSizeX()
	{
		return m_stAnime->st_arrPointp[POINT_BIT_SIZE].x;
		//�̹��� ������ x
	}
	inline int GetSizeY()
	{
		return m_stAnime->st_arrPointp[POINT_BIT_SIZE].y;
		//�̹��� ������ y
	}
	JEngine::POINT ReturnPT(int iIndex)
	{
		return m_stAnime->st_arrPointp[iIndex];
	}
	~Anime();
};


#endif // !_H_ANIME_H_