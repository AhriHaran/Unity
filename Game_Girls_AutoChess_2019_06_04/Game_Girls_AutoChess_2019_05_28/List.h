#ifndef _H_LIST_H_
#define _H_LIST_H_
#include <list>

template <typename T>
class List
{
private:
	list <T*> m_lisClass;
	int m_iNumOfData = 0;
public:
	T * CreateNode()
	{
		return new T;
	}
	T * FindNode(int iFind)
	{
		int iCount = 0;
		typename list<T*>::iterator iter;
		for (iter = m_lisClass.begin(); iter != m_lisClass.end(); iter++)
		{
			if (iCount == iFind)
				return (*iter);
			iCount++;
		}
		return NULL;
	}
	void PushNode(T * Node)
	{
		m_lisClass.push_back(Node);
		m_iNumOfData += 1;
	}
	int ReturnNum()
	{
		return this->m_iNumOfData;
	}
	void DeleteNode(int iIndex)	//i번쨰 인덱스 제거
	{
		int iCount = 0;
		typename list<T*>::iterator iter;
		for (iter = m_lisClass.begin(); iter != m_lisClass.end(); iter++)
		{
			if (iIndex == iCount)
				break;
			iCount++;
		}
		remove(m_lisClass.begin(), m_lisClass.end(), (*iter));
		m_lisClass.pop_back();
		m_iNumOfData--;
	}
	void Release()
	{
		typename list<T*>::iterator iter;
		for (iter = m_lisClass.begin(); iter != m_lisClass.end(); iter++)
			delete (*iter);
		m_lisClass.clear();
		m_iNumOfData = 0;
	}
};



#endif // !_H_LIST_H_
