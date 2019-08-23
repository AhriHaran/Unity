using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject m_ItemView = null;
    private GameObject m_GridChar = null;
    private int m_iCurPannel = -1;

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
        m_iCurPannel = -1;
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        //여기서 각 오브젝트에 맞게 셋팅
        ViewInventory(INVENTORY_TYPE.INVENTORY_WEAPON); //디폴트는 항상 위에 있는 웨폰
        m_iCurPannel = (int)INVENTORY_TYPE.INVENTORY_WEAPON;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        //하위 그리드를 모두 뺴준다.
        InventoryReset();
        //그리드를 모두 빼준다.
        m_iCurPannel = -1;
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
        if (m_iCurPannel != (int)eType) //버튼 중복 클릭을 방지
        {
            InventoryReset();
            //무슨 인벤토리를 보여줄 것인가.
            List<ItemData> Data = UserInfo.instance.GetInventoryList(eType);

            for (int i = 0; i < Data.Count; i++)
            {
                GameObject Item = ResourceLoader.CreatePrefab("Prefabs/ItemSprite");
                Item.transform.SetParent(m_GridChar.transform, false);
                Item.GetComponent<ItemSelectSprite>().Setting(i, (ITEM_TYPE)Data[i].GetItemData(ITEM_DATA.ITEM_TYPE), eType);
            }

            //if (eType == INVENTORY_TYPE.INVENTORY_WEAPON)
            //{
            //    for(int i = 0; i < Data.Count; i++)
            //    {
            //        GameObject Item = ResourceLoader.CreatePrefab("Prefabs/ItemSelectSprite");
            //        Item.transform.SetParent(m_GridChar.transform, false);
            //        Item.GetComponent<ItemSelectSprite>().Setting(i, (ITEM_TYPE)Data[i].GetItemData(ITEM_DATA.ITEM_TYPE), eType);
            //    }
            //}
            //else if (eType == INVENTORY_TYPE.INVENTORY_STIGMA)
            //{
            //    foreach (var S in Data)
            //    {
            //        GameObject Item = ResourceLoader.CreatePrefab("Prefabs/ItemSprite");
            //        Item.transform.SetParent(m_GridChar.transform, false);
            //        string sprite = string.Empty;
            //        if (S.GetItemData(ITEM_DATA.ITEM_TYPE).ToString().Equals(ITEM_TYPE.ITEM_STIGMA_TOP.ToString()))
            //            sprite = "HelmetBronze";
            //        else if (S.GetItemData(ITEM_DATA.ITEM_TYPE).ToString().Equals(ITEM_TYPE.ITEM_STIGMA_CENTER.ToString()))
            //            sprite = "ArmorBronze";
            //        else if (S.GetItemData(ITEM_DATA.ITEM_TYPE).ToString().Equals(ITEM_TYPE.ITEM_STIGMA_BOTTOM.ToString()))
            //            sprite = "GreaveBronze";
            //        Item.GetComponent<UISprite>().spriteName = sprite;
            //    }
            //}

            m_GridChar.GetComponent<UIGrid>().Reposition(); //리 포지셔닝으로 그리드 재정렬
            m_ItemView.GetComponent<UIScrollView>().ResetPosition();
            m_ItemView.GetComponent<UIPanel>().Refresh();
            m_iCurPannel = (int)eType;
        }
    }

    void InventoryReset()
    {
        if (m_iCurPannel > -1)
        {
            while (m_GridChar.transform.childCount != 0)
            {
                GameObject game = m_GridChar.transform.GetChild(0).gameObject;
                game.transform.SetParent(null);
                NGUITools.Destroy(game);
            }
            m_GridChar.transform.DetachChildren();
        }
    }
}
