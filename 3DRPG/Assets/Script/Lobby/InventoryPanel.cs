using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject m_ItemView = null;
    private GameObject m_GridChar = null;
    private List<GameObject>[] m_ListItem = new List<GameObject>[(int)INVENTORY_TYPE.INVENTORY_END];

    private void Awake()
    {
        try
        {
            m_ItemView = transform.GetChild(3).gameObject;
            m_GridChar = m_ItemView.transform.GetChild(0).gameObject;
        }
        catch(System.NullReferenceException ex)
        {
            Debug.Log(ex);
        }
        
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        //처음에 리스트에 각각 빈 오브젝트들을 셋팅
        List<ItemData> Data = UserInfo.instance.GetInventoryList(INVENTORY_TYPE.INVENTORY_WEAPON);
        foreach (var W in Data)
        {
            GameObject Item = ResourceLoader.CreatePrefab("Prefabs/ItemSprite");
            Item.GetComponent<UISprite>().spriteName = "GauntletBronze";
            m_ListItem[(int)INVENTORY_TYPE.INVENTORY_WEAPON].Add(Item);
        }

        Data = UserInfo.instance.GetInventoryList(INVENTORY_TYPE.INVENTORY_STIGMA);
        foreach (var S in Data)
        {
            GameObject Item = ResourceLoader.CreatePrefab("Prefabs/ItemSprite");
            m_ListItem[(int)INVENTORY_TYPE.INVENTORY_STIGMA].Add(Item);
        }

        //여기서 각 오브젝트에 맞게 셋팅
        ViewInventory(INVENTORY_TYPE.INVENTORY_WEAPON); //디폴트는 항상 위에 있는 웨폰
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        m_ListItem[(int)INVENTORY_TYPE.INVENTORY_WEAPON].Clear();
        m_ListItem[(int)INVENTORY_TYPE.INVENTORY_STIGMA].Clear();
    }

    public void OnClick()
    {
        string Button = UIButton.current.name;

        if (Button == "WeaponButton")
        {
            //유저 정보를 바탕으로 그리드 내의 정보를 변경
            ViewInventory(INVENTORY_TYPE.INVENTORY_WEAPON); //디폴트는 항상 위에 있는 웨폰
        }
        else if (Button == "StigmaButton")
        {
            ViewInventory(INVENTORY_TYPE.INVENTORY_STIGMA); //디폴트는 항상 위에 있는 웨폰
        }
    }

    void ViewInventory(INVENTORY_TYPE eType)
    {
        //무슨 인벤토리를 보여줄 것인가.



    }
}
