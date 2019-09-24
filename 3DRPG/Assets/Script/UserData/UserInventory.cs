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
        for(int i = 0; i < (int)INVENTORY_TYPE.INVENTORY_END; i++)
        {
            m_ListInven[i] = new List<ItemData>();
        }
        
        //JSON 데이터와 테이블 데이터를 통해서 셋팅
        //UserStigmaData, UserWeaponData -> Json
        if(JSON.JsonUtil.FileCheck(INVENTORY_TYPE.INVENTORY_WEAPON.ToString()))
        {
            ItemInfoData[] Weapon = JSON.JsonUtil.LoadArrJson<ItemInfoData>(INVENTORY_TYPE.INVENTORY_WEAPON.ToString());   //웨폰 리스트
            foreach (var W in Weapon)
            {
                ItemData Data = new ItemData(W);
                m_ListInven[(int)INVENTORY_TYPE.INVENTORY_WEAPON].Add(Data);
            }
            //무기 셋팅
        }

        if (JSON.JsonUtil.FileCheck(INVENTORY_TYPE.INVENTORY_STIGMA.ToString()))
        {
            ItemInfoData[] stigma = JSON.JsonUtil.LoadArrJson<ItemInfoData>(INVENTORY_TYPE.INVENTORY_STIGMA.ToString());  //스티그마 리스트
            foreach (var S in stigma)
            {
                ItemData Item = new ItemData(S);
                m_ListInven[(int)INVENTORY_TYPE.INVENTORY_STIGMA].Add(Item);
            }
            //성흔 TCB로 미리 구분
        }
    }
    public object GetItemForList(int inventoryIndex, INVENTORY_TYPE eType, ITEM_DATA eIndex)
    {
        //인벤토리 아이템은 중복 획득이 가능하므로 리스트 순서 기반으로 한다.
        return m_ListInven[(int)eType][inventoryIndex].GetItemData(eIndex);
    }
    public int GetItemIndexForList(int itemIndex, INVENTORY_TYPE eType)
    {
        for (int i = 0; i < m_ListInven[(int)eType].Count; i++)
        {
            int iIndex = Util.ConvertToInt(m_ListInven[(int)eType][i].GetItemData(ITEM_DATA.ITEM_INDEX));
            if (iIndex == itemIndex)
            {
                return i;
            }
        }
        return -1;
    }
    public List<ItemData> GetInventoryList(INVENTORY_TYPE eType)
    {
        return m_ListInven[(int)eType]; //전체 리스트
    }
    public void InventoryUpdate(ITEM_TYPE ItemType, INVENTORY_TYPE eInven, int itemIndex)  //새로운 아이템 획득
    {
        //아이템이나 스티그마 획득 시
        /*새로운 아이템을 얻을 시 해당 아이템의 인덱스를 기반으로 새로운 데이터 기반을 생성하고
         * 플레이어의 제이슨을 갱신하며
         * 인벤토리에 순서대로 넣어준다.
         * 그리고 해당 아이템을 장착할 시에는 캐릭터 인덱스는 내가 해당 아이템을 가진 인덱스 순서대로 넣어준다.
         */
        ItemInfoData ItemInfo = new ItemInfoData(ItemType, itemIndex);    //새로운 아이템 생성
        ItemData Data = new ItemData(ItemInfo);
        m_ListInven[(int)eInven].Add(Data);
        //새로운 데이터 셋팅
    }
    public void InventoryUpdate(INVENTORY_TYPE eInven, int itemIndex, ITEM_DATA eData, object UpdateData)
    {
        //기존 아이템 업데이트, 레벨 업 등->리스트 기반
        m_ListInven[(int)eInven][itemIndex].ItemUpdate(eData, UpdateData);
    }
    public void ItemUpdateForChar(INVENTORY_TYPE eInven, int itemIndex, bool bUpgrade)
    {
        //현재 해당 아이템을 장착한 캐릭터의 능력치를 상승시켜준다.
        int iInven = (int)eInven;
        int iCurChar = Util.ConvertToInt(m_ListInven[iInven][itemIndex].GetItemData(ITEM_DATA.ITEM_EQUIP_CHAR));

        if(iCurChar >= 0)
        {
            int iData = Util.SumData(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_MAX_HP, iCurChar), m_ListInven[iInven][itemIndex].GetItemData(ITEM_DATA.ITEM_HP), bUpgrade);
            UserInfo.instance.CharUpdate(CHAR_DATA.CHAR_MAX_HP, iData, iCurChar);

            iData = Util.SumData(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_ATK, iCurChar), m_ListInven[iInven][itemIndex].GetItemData(ITEM_DATA.ITEM_ATK), bUpgrade);
            UserInfo.instance.CharUpdate(CHAR_DATA.CHAR_ATK, iData, iCurChar);

            iData = Util.SumData(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_DEF, iCurChar), m_ListInven[iInven][itemIndex].GetItemData(ITEM_DATA.ITEM_DEF), bUpgrade);
            UserInfo.instance.CharUpdate(CHAR_DATA.CHAR_DEF, iData, iCurChar);

            iData = Util.SumData(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_CRI, iCurChar), m_ListInven[iInven][itemIndex].GetItemData(ITEM_DATA.ITEM_CRI), bUpgrade);
            UserInfo.instance.CharUpdate(CHAR_DATA.CHAR_CRI, iData, iCurChar);
        }
        //아이템 기준으로 정보 업데이트
    }


    public void Save()
    {
        for(int i = 0; i < m_ListInven.Length; i++)
        {
            if(m_ListInven[i] != null)
            {
                int iCount = m_ListInven[i].Count;
                try
                {
                    ItemInfoData[] Item = new ItemInfoData[iCount];
                    for (int j = 0; j < iCount; j++)
                    {
                        Item[j] = new ItemInfoData
                        {
                            ItemType = (ITEM_TYPE)m_ListInven[i][j].GetItemData(ITEM_DATA.ITEM_TYPE),
                            ItemIndex = Util.ConvertToInt(m_ListInven[i][j].GetItemData(ITEM_DATA.ITEM_INDEX)),
                            ItemEquipChar = Util.ConvertToInt(m_ListInven[i][j].GetItemData(ITEM_DATA.ITEM_EQUIP_CHAR)),
                            ItemLevel = Util.ConvertToInt(m_ListInven[i][j].GetItemData(ITEM_DATA.ITEM_LEVEl)),
                            ItemCurEXP = Util.ConvertToInt(m_ListInven[i][j].GetItemData(ITEM_DATA.ITEM_CUR_EXP))
                        };
                    }
                    string Inven = JSON.JsonUtil.ToJson<ItemInfoData>(Item);
                    Debug.Log(Inven);
                    INVENTORY_TYPE eType = (INVENTORY_TYPE)i;
                    JSON.JsonUtil.CreateJson(eType.ToString(), Inven);
                }
                catch (System.NullReferenceException ex)
                {
                    Debug.Log(ex);
                }
            }
        }
    }

}
