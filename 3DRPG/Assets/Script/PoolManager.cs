using UnityEngine;
using System.Collections.Generic;
using System;

public class PoolManager : GSingleton<PoolManager>
{
    public List<ObjectPool> m_PoolManger = new List<ObjectPool>();

    //생성 시 부모는 해당 오브젝트 풀로 지정하고 설정한다.
    public void Set(string strPoolName, string strPrefabs, int iObjectCount)
    {
        try
        {
            GameObject ObjectPool = ResourceLoader.CreatePrefab("Prefabs/ObejctPool");   //오브젝트 풀링
            ObjectPool.name = strPoolName;
            ObjectPool Pool = ObjectPool.GetComponent<ObjectPool>();
            Pool.Init(strPrefabs, iObjectCount, Pool.transform);
            m_PoolManger.Add(Pool);
            //게임 오브젝트로 생성한 뒤
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex);
        }
    }

    public void Set(string strPoolName, string [] strPrefabs, int iObjectCount)
    {
        //서로 다른 오브젝트를 풀링으로 관리
        try
        {
            GameObject ObjectPool = ResourceLoader.CreatePrefab("Prefabs/ObejctPool");   //오브젝트 풀링
            ObjectPool.name = strPoolName;
            ObjectPool Pool = ObjectPool.GetComponent<ObjectPool>();
            Pool.Init(strPrefabs, iObjectCount, Pool.transform);
            m_PoolManger.Add(Pool);
            //게임 오브젝트로 생성한 뒤 하위 컴포넌트로 셋팅
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex);
        }
    }

    public bool PushToPool(string strPoolName, GameObject item)
    {
        //기본 반납
        ObjectPool pool = GetPoolItem(strPoolName);
        if (pool.name == string.Empty)
            return false;

        pool.PushToPool(item, pool.transform);
        return true;
    }

    public bool PushToPool(string strPoolName, int iIndex, GameObject item)
    {
        //인덱스 기반 반납
        ObjectPool pool = GetPoolItem(strPoolName);
        if (pool.name == string.Empty)
            return false;

        pool.PushToPool(item, iIndex, pool.transform);
        return true;
    }


    public GameObject PopFromPool(string strPoolName)
    {
        //기본 대출
        ObjectPool pool = GetPoolItem(strPoolName);
        if (pool == null)
            return null;
        //부족할 떄 만든다.
        return pool.PopFromPoll(pool.transform);
    }

    public GameObject PopFromPool(string strPoolName, int iIndex)
    {
        //인덱스 기반 대출
        ObjectPool pool = GetPoolItem(strPoolName);
        if (pool == null)
            return null;

        return pool.PopFromPoll(iIndex);
    }


    ObjectPool GetPoolItem(string strPoolName)
    {
        //오브젝트 풀 클래스 탐색
        for(int i = 0; i < m_PoolManger.Count; i++)
        {
            if (m_PoolManger[i].name.Equals(strPoolName))   //해당 아이템의 이름
                return m_PoolManger[i];
        }
        return null;
    }

}
