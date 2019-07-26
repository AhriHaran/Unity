using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    //하나의 오브젝트를 대응하는 풀
    public string poolItemName = string.Empty;
    public GameObject prefab = null;    //프리팹 오브젝트
    public int ipoolCount = 0;
    private List<GameObject> poolList = new List<GameObject>();

    public void Init(Transform parent = null)
    {
        //초기화
        for(int i = 0; i < ipoolCount; i++)
        {
            poolList.Add(CreateItem(parent));
        }
    }

    public void PushToPool(GameObject item, Transform parent = null)
    {
        //오브젝를 사용후 반환 할 때
        item.transform.SetParent(parent);
        item.SetActive(false);
        poolList.Add(item);
    }

    public GameObject PopFromPoll(Transform parent = null)
    {
        if (poolList.Count == 0)
            poolList.Add(CreateItem(parent));
        GameObject item = poolList[0];
        poolList.RemoveAt(0);
        return item;
    }

    private GameObject CreateItem(Transform parent = null)
    {
        //크리에이트
        GameObject Item = Object.Instantiate(prefab) as GameObject; //as는 인스턴트를 만들 때 게임 오브젝트 형식으로 GetCompanet로 한다는 소리
        Item.name = poolItemName;
        Item.transform.SetParent(parent);   //보무의 위치를 기준으로 설정, 뒤에 false 면 현재 값 유지
        Item.SetActive(false);
        return Item;
    }
}
