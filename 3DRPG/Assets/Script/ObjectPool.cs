using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    //하나의 오브젝트를 대응하는 풀
    private string m_strName;
    private int m_iPoolCount = 0;
    private Transform m_trParent;
    private List<GameObject> m_ListPool = new List<GameObject>();

    public void Init(string strName, int iCount, Transform parent = null)
    {
        m_strName = strName;
        m_iPoolCount = iCount;
        m_trParent = parent;
        //초기화
        for (int i = 0; i < m_iPoolCount; i++)
        {
            m_ListPool.Add(CreateItem(parent));
        }
    }

    public void Init(string [] strName, int iCount, Transform parent = null)
    {
        m_iPoolCount = iCount;
        m_trParent = parent;
        //초기화
        for (int i = 0; i < m_iPoolCount; i++)
        {
            m_strName = strName[i];
            m_ListPool.Add(CreateItem(parent));
        }
    }

    public void PushToPool(GameObject item, Transform parent = null)    //순서가 중요하지 않음
    {
        //오브젝를 사용후 반환 할 때
        item.transform.SetParent(parent);
        item.SetActive(false);
        m_ListPool.Add(item);
    }

    public void PushToPool(GameObject item, int iIndex, Transform parent = null)    //순서가 중요
    {
        item.transform.SetParent(parent);
        item.SetActive(false);
        m_ListPool.Insert(iIndex, item);
    }

    public GameObject PopFromPoll(Transform parent = null)  //순서가 중요하지 않은 오브젝트
    {
        if (m_ListPool.Count == 0)
            m_ListPool.Add(CreateItem(parent));
        GameObject item = m_ListPool[0];
        m_ListPool.RemoveAt(0);
        return item;
    }

    public GameObject PopFromPoll(int iIndex, Transform parent = null)  //순서가 중요한 오브젝트
    {
        if (iIndex > m_ListPool.Count || m_ListPool.Count == 0)  //인덱스 초과
            return null;
        GameObject item = m_ListPool[iIndex];
        m_ListPool.RemoveAt(iIndex);
        return item;
    }

    private GameObject CreateItem(Transform parent = null)
    {
        //크리에이트
        GameObject Item = ResourceLoader.CreatePrefab(m_strName, parent);
        Item.name = m_strName;
        Item.transform.SetParent(parent);   //보무의 위치를 기준으로 설정, 뒤에 false 면 현재 값 유지
        Item.SetActive(false);
        return Item;
    }
}
