using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoSingleton<PoolManager>
{
    public List<ObjectPool> Objectpools = new List<ObjectPool>();

    void Awake()
    {
        for(int i = 0; i < Objectpools.Count; i++)
        {
            Objectpools[i].Init(transform);
        }
    }

    public bool PushToPool(string itemName, GameObject item, Transform parent = null)
    {
        ObjectPool pool = GetPoolItem(itemName);
        if (pool == null)
            return false;

        if (parent == null)
            parent = transform;

        pool.PushToPool(item, parent);
        return true;
    }

    public GameObject PopFromPool(string itemName, Transform parent = null)
    {
        ObjectPool pool = GetPoolItem(itemName);
        if (pool == null)
            return null;

        return pool.PopFromPoll(parent);
    }

    ObjectPool GetPoolItem(string itemName)
    {
        for(int i = 0; i < Objectpools.Count; i++)
        {
            if (Objectpools[i].poolItemName.Equals(itemName))   //해당 아이템의 이름
                return Objectpools[i];
        }
        return null;
    }
}
