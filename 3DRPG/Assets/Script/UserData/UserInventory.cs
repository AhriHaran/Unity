using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum INVENTORY_TYPE
{
    INVENTORY_START,
    INVENTORY_WEAPON = INVENTORY_START,
    INVENTORY_STIGMA,
    INVENTORY_END,
}

public class UserInventory
{
    //무기, 성흔 상 중 하
    //무기 인벤토리, 성흔 인벤토리\
    private const string m_strRoute = "Equipment/"; //데이터 저장 루트(공통)
    List<ItemData>[] m_ListInven = new List<ItemData>[(int)INVENTORY_TYPE.INVENTORY_END];
    public UserInventory()
    {
        //JSON 데이터와 테이블 데이터를 통해서 셋팅
        //UserStigmaData, UserWeaponData -> Json
        ItemInfoData[] Weapon = JSON.JsonUtil.LoadArrJson<ItemInfoData>(INVENTORY_TYPE.INVENTORY_WEAPON.ToString());   //웨폰 리스트
        m_ListInven[(int)INVENTORY_TYPE.INVENTORY_WEAPON] = new List<ItemData>();

        foreach (var W in Weapon)
        {
            ItemData Data = new ItemData(W);
            m_ListInven[(int)INVENTORY_TYPE.INVENTORY_WEAPON].Add(Data);
        }
        //무기 셋팅

        ItemInfoData[] stigma = JSON.JsonUtil.LoadArrJson<ItemInfoData>(INVENTORY_TYPE.INVENTORY_STIGMA.ToString());  //스티그마 리스트
        m_ListInven[(int)INVENTORY_TYPE.INVENTORY_STIGMA] = new List<ItemData>();

        foreach (var S in stigma)
        {
            ItemData Item = new ItemData(S);
            m_ListInven[(int)INVENTORY_TYPE.INVENTORY_STIGMA].Add(Item);
        }
        //성흔 셋팅
    }
    public object GetItemForList(int inventoryIndex, INVENTORY_TYPE eType, ITEM_DATA eIndex)
    {
        //인벤토리 아이템은 중복 획득이 가능하므로 리스트 순서 기반으로 한다.
        return m_ListInven[(int)eType][inventoryIndex].GetItemData(eIndex);
    }
    public List<ItemData> GetInventoryList(INVENTORY_TYPE eType)
    {
        return m_ListInven[(int)eType]; //전체 리스트
    }
    public void InventoryUpdate(string ItemType, int invenType, int itemIndex)  //새로운 아이템 획득
    {
        //아이템이나 스티그마 획득 시
        /*새로운 아이템을 얻을 시 해당 아이템의 인덱스를 기반으로 새로운 데이터 기반을 생성하고
         * 플레이어의 제이슨을 갱신하며
         * 인벤토리에 순서대로 넣어준다.
         * 그리고 해당 아이템을 장착할 시에는 캐릭터 인덱스는 내가 해당 아이템을 가진 인덱스 순서대로 넣어준다.
         */
        ItemInfoData ItemInfo = new ItemInfoData(ItemType, itemIndex);    //새로운 아이템 생성
        ItemData Data = new ItemData(ItemInfo);
        m_ListInven[invenType].Add(Data);
        //새로운 데이터 셋팅
    }
    public void InventoryUpdate(int invenType, int itemIndex)
    {
        //기존 아이템 업데이트, 레벨 업 등
    }


    public void Save()
    {
        for(INVENTORY_TYPE i = INVENTORY_TYPE.INVENTORY_START; i < INVENTORY_TYPE.INVENTORY_END; i++)
        {
            int iIndex = (int)i;
            int iCount = m_ListInven[iIndex].Count;
            try
            {
                ItemInfoData[] Item = new ItemInfoData[iCount];
                for (int j = 0; j < iCount; j++)
                {
                    Item[j] = new ItemInfoData
                    {
                        ItemType = Util.ConvertToString(m_ListInven[iIndex][j].GetItemData(ITEM_DATA.ITEM_TYPE)),
                        ItemIndex = Util.ConvertToInt(m_ListInven[iIndex][j].GetItemData(ITEM_DATA.ITEM_INDEX)),
                        ItemLevel = Util.ConvertToInt(m_ListInven[iIndex][j].GetItemData(ITEM_DATA.ITEM_LEVEl)),
                        ItemCurEXP = Util.ConvertToInt(m_ListInven[iIndex][j].GetItemData(ITEM_DATA.ITEM_CUR_EXP))
                    };
                }
                string Inven = JSON.JsonUtil.ToJson<ItemInfoData>(Item);
                Debug.Log(Inven);
                JSON.JsonUtil.CreateJson(i.ToString(), Inven);
            }
            catch(System.NullReferenceException ex)
            {
                Debug.Log(ex);
            }
        }
    }

}
