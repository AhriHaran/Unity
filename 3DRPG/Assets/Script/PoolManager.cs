using UnityEngine;
using System.Collections.Generic;
using System;

public class PoolManager : MonoSingleton<PoolManager>
{
    public List<ObjectPool> m_PoolManger = new List<ObjectPool>();

    public void Set(string strPoolName, string strPrefabs, int iObjectCount, Transform Parent = null)
    {
        try
        {
            GameObject ObjectPool = ResourceLoader.CreatePrefab("Prefabs/ObejctPool");   //오브젝트 풀링
            ObjectPool.name = strPoolName;
            ObjectPool Pool = ObjectPool.GetComponent<ObjectPool>();
            Pool.Init(strPrefabs, iObjectCount, Parent);
            m_PoolManger.Add(Pool);
            //게임 오브젝트로 생성한 뒤
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Object is NULL");
        }
    }

    public void Set(string strPoolName, string [] strPrefabs, int iObjectCount, Transform Parent = null)
    {
        //서로 다른 오브젝트를 풀링으로 관리
        try
        {
            GameObject ObjectPool = ResourceLoader.CreatePrefab("Prefabs/ObejctPool");   //오브젝트 풀링
            ObjectPool.name = strPoolName;
            ObjectPool Pool = ObjectPool.GetComponent<ObjectPool>();
            Pool.Init(strPrefabs, iObjectCount, Parent);
            m_PoolManger.Add(Pool);
            //게임 오브젝트로 생성한 뒤 하위 컴포넌트로 셋팅
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Object is NULL");
        }
    }

    public bool PushToPool(string strPoolName, GameObject item, Transform parent = null)
    {
        //기본 반납
        ObjectPool pool = GetPoolItem(strPoolName);
        if (pool.name == string.Empty)
            return false;

        if (parent == null)
            parent = transform;

        pool.PushToPool(item, parent);
        return true;
    }

    public bool PushToPool(string strPoolName, int iIndex, GameObject item, Transform parent = null)
    {
        //인덱스 기반 반납
        ObjectPool pool = GetPoolItem(strPoolName);
        if (pool.name == string.Empty)
            return false;

        if (parent == null)
            parent = transform;

        pool.PushToPool(item, iIndex, parent);
        return true;
    }


    public GameObject PopFromPool(string strPoolName, Transform parent = null)
    {
        //기본 대출
        ObjectPool pool = GetPoolItem(strPoolName);
        if (pool == null)
            return null;

        return pool.PopFromPoll(parent);
    }

    public GameObject PopFromPool(string strPoolName, int iIndex, Transform parent = null)
    {
        //인덱스 기반 대출
        ObjectPool pool = GetPoolItem(strPoolName);
        if (pool == null)
            return null;

        return pool.PopFromPoll(iIndex, parent);
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
