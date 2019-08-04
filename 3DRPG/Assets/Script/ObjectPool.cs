using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    //하나의 오브젝트를 대응하는 풀
    public string m_strPoolName = string.Empty; //오브젝트 풀 이름
    private int m_iPoolCount = 0;
    private Transform m_trParent;
    private List<GameObject> m_ListPool = new List<GameObject>();

    public void Init(string strName, int iCount, Transform parent = null)
    {
        m_strPoolName = strName;
        m_iPoolCount = iCount;
        m_trParent = parent;
        //초기화
        for (int i = 0; i < m_iPoolCount; i++)
        {
            m_ListPool.Add(CreateItem(parent));
        }
    }

    public void PushToPool(GameObject item, Transform parent = null)
    {
        //오브젝를 사용후 반환 할 때
        item.transform.SetParent(parent);
        item.SetActive(false);
        m_ListPool.Add(item);
    }

    public GameObject PopFromPoll(Transform parent = null)
    {
        if (m_ListPool.Count == 0)
            m_ListPool.Add(CreateItem(parent));
        GameObject item = m_ListPool[0];
        m_ListPool.RemoveAt(0);
        return item;
    }

    private GameObject CreateItem(Transform parent = null)
    {
        //크리에이트
        GameObject Item = ResourceLoader.CreatePrefab(m_strPoolName, parent);
        Item.name = m_strPoolName;
        Item.transform.SetParent(parent);   //보무의 위치를 기준으로 설정, 뒤에 false 면 현재 값 유지
        Item.SetActive(false);
        return Item;
    }
}
