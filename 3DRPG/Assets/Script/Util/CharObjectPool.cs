using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CharObject
{
    public GameObject st_Object;
    public int st_Index;
}

public class CharObjectPool : MonoBehaviour
{
    //하나의 오브젝트를 대응하는 풀
    private string m_strName;
    private int m_iPoolCount = 0;
    private int m_iCurIndex = 0;
    public List<CharObject> m_ListPool = new List<CharObject>();

    public void Init(string [] strName, int [] arrIndex, int iCount, Transform Parent)
    {
        m_iPoolCount = iCount;
        //초기화
        for (int i = 0; i < m_iPoolCount; i++)
        {
            m_strName = strName[i];
            m_iCurIndex = arrIndex[i];
            m_ListPool.Add(CreateItem(Parent));
        }
    }
    public void PushToPool(GameObject item, int iIndex, Transform Parent)    //순서가 중요
    {
        item.transform.SetParent(Parent, false);
        item.transform.localRotation = Quaternion.identity;
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;
        //로컬 트랜스폼 리셋
        item.transform.rotation = Quaternion.identity;
        item.transform.position = Vector3.zero;
        item.SetActive(false);

        CharObject Struct = FindCharObject(iIndex);
        if (Struct != null)
        {
            //반환
            Struct.st_Object = item;
        }
    }

    public GameObject PopFromPoll(int iIndex)  //순서가 중요한 오브젝트
    {
        if (iIndex > m_ListPool.Count || m_ListPool.Count == 0)  //인덱스 초과
            return null;

        CharObject Class = FindCharObject(iIndex);
        if(Class != null)
        {
            GameObject Object = Class.st_Object;
            Class.st_Object = null;
            return Object;  //해당 캐릭터의 오브젝트
        }
        else
            return null;
    }

    private CharObject FindCharObject(int iIndex)
    {
        foreach (var s in m_ListPool)
        {
            if (s.st_Index == iIndex)   //해당 캐릭터의 인덱스와 맞는가?
            {
                return s;   //반환
            }
        }
        return null;
    }

    private CharObject CreateItem(Transform parent)
    {
        //크리에이트
        CharObject Item = new CharObject();
        Item.st_Object = ResourceLoader.CreatePrefab(m_strName, parent);
        //무기관련 정보가 있다면 셋팅
        Item.st_Object.SetActive(false);
        Item.st_Index = m_iCurIndex;
        return Item;
    }

    public void Clear()
    {
        m_ListPool.Clear();
    }
}
