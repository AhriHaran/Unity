using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoSingleton<PoolManager>
{
    public List<ObjectPool> m_PoolManger = new List<ObjectPool>();

    public void Set(string strPrefabs, int iObjectCount, Transform Parent = null)
    {
        ObjectPool Node = new ObjectPool();
        Node.Init(strPrefabs, iObjectCount, Parent); 
        m_PoolManger.Add(Node);
        //하나의 오브젝트를 여러개 만들 떄,
    }

    public bool PushToPool(string itemName, GameObject item, Transform parent = null)
    {
        ObjectPool pool = GetPoolItem(itemName);
        if (pool.m_strPoolName == string.Empty)
            return false;

        if (parent == null)
            parent = transform;

        pool.PushToPool(item, parent);
        return true;
    }

    public GameObject PopFromPool(string itemName, Transform parent = null)
    {
        ObjectPool pool = GetPoolItem(itemName);
        if (pool.m_strPoolName == string.Empty)
            return null;

        return pool.PopFromPoll(parent);
    }

    ObjectPool GetPoolItem(string itemName)
    {
        for(int i = 0; i < m_PoolManger.Count; i++)
        {
            if (m_PoolManger[i].m_strPoolName.Equals(itemName))   //해당 아이템의 이름
                return m_PoolManger[i];
        }
        return null;
    }
}
