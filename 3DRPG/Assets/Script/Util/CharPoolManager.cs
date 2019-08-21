using UnityEngine;
using System.Collections.Generic;
using System;

//서로 다른 오브젝트 풀링 기법
public class CharPoolManager : GSingleton<CharPoolManager>
{
    public List<CharObjectPool> m_PoolManger = new List<CharObjectPool>();

    public void Set(string strPoolName, string [] strPrefabs, int [] iarr, int iObjectCount)
    {
        //서로 다른 오브젝트를 풀링으로 관리
        try
        {
            GameObject ObjectPool = ResourceLoader.CreatePrefab("Prefabs/CharObjectPool");   //오브젝트 풀링
            ObjectPool.name = strPoolName;
            CharObjectPool Pool = ObjectPool.GetComponent<CharObjectPool>();
            Pool.Init(strPrefabs, iarr, iObjectCount, Pool.transform);
            m_PoolManger.Add(Pool);
            //게임 오브젝트로 생성한 뒤 하위 컴포넌트로 셋팅
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex);
        }
    }

    public bool PushToPool(string strPoolName, int iIndex, GameObject item)
    {
        //인덱스 기반 반납
        CharObjectPool pool = GetPoolItem(strPoolName);
        if (pool.name == string.Empty)
            return false;

        pool.PushToPool(item, iIndex, pool.transform);
        return true;
    }

    public GameObject PopFromPool(string strPoolName, int iIndex)
    {
        //인덱스 기반 대출
        CharObjectPool pool = GetPoolItem(strPoolName);
        if (pool == null)
            return null;

        return pool.PopFromPoll(iIndex);
    }

    CharObjectPool GetPoolItem(string strPoolName)
    {
        //오브젝트 풀 클래스 탐색
        for(int i = 0; i < m_PoolManger.Count; i++)
        {
            if (m_PoolManger[i].name.Equals(strPoolName))   //해당 아이템의 이름
                return m_PoolManger[i];
        }
        return null;
    }

    public void Clear()
    {
        //리스트 초기화
        foreach (var i in m_PoolManger)
        {
            i.Clear();
        }
        m_PoolManger.Clear();
    }
}
