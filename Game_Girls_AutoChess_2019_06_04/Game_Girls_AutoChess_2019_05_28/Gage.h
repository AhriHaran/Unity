#ifndef _H_GAGE_H_
#define _H_GAGE_H_
#include "JEngine.h"
#include "GlobalDefine.h"
#include "ResoucesManager.h"
#include "POINT.h"
class Gage
{
private:
	JEngine::BitMap * m_cBar;		//������ ��
	JEngine::BitMap * m_cUnderBar;	//������ ��� ��
	JEngine::BitMap	* m_cGage;		//������
	JEngine::POINT	  m_cGagePT;	//������ ��ο� ���� ����Ʈ
	JEngine::POINT	  m_cGageEPT;	//������ �� ����Ʈ
	float			  m_fCurUP;		//�������� ��� �ϰ� Cur
	float			  m_fMax;		//�������� �ִ�
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