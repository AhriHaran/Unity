using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public enum ITEM_DATA
{
    ITEM_TYPE,
    ITEM_NAME,
    ITEM_INDEX,
    ITEM_EQUIP_CHAR,    //현재 이 장비를 장착한 캐릭터
    ITEM_LEVEl,
    ITEM_MAX_EXP,
    ITEM_CUR_EXP,
    ITEM_HP,
    ITEM_ATK,
    ITEM_DEF,
    ITEM_CRI,
}

public class ItemData
{
    List<Dictionary<ITEM_DATA, object>> m_ItemData = new List<Dictionary<ITEM_DATA, object>>();
    public ItemData(ItemInfoData Data)
    {
        string Route = "Excel/Table/" + Data.ItemType + "_" + Data.ItemIndex;
        try
        {
            var Table = EXCEL.ExcelLoad.Read(Route);
            int iTableLevel = Data.ItemLevel - 1;
            //해당 인덱스 데이터의 엑셀
            NodeSetting(ITEM_DATA.ITEM_TYPE, Data.ItemType);
            NodeSetting(ITEM_DATA.ITEM_NAME, Table[iTableLevel][ITEM_DATA.ITEM_NAME.ToString()]);
            NodeSetting(ITEM_DATA.ITEM_INDEX, Data.ItemIndex);
            NodeSetting(ITEM_DATA.ITEM_EQUIP_CHAR, Data.ItemEquipChar);
            NodeSetting(ITEM_DATA.ITEM_LEVEl, Data.ItemLevel);
            NodeSetting(ITEM_DATA.ITEM_MAX_EXP, Table[iTableLevel][ITEM_DATA.ITEM_MAX_EXP.ToString()]);
            NodeSetting(ITEM_DATA.ITEM_CUR_EXP, Data.ItemCurEXP);
            NodeSetting(ITEM_DATA.ITEM_HP, Table[iTableLevel][ITEM_DATA.ITEM_HP.ToString()]);
            NodeSetting(ITEM_DATA.ITEM_ATK, Table[iTableLevel][ITEM_DATA.ITEM_ATK.ToString()]);
            NodeSetting(ITEM_DATA.ITEM_DEF, Table[iTableLevel][ITEM_DATA.ITEM_DEF.ToString()]);
            NodeSetting(ITEM_DATA.ITEM_CRI, Table[iTableLevel][ITEM_DATA.ITEM_CRI.ToString()]);
        }
        catch(System.NullReferenceException ex)
        {
            Debug.Log(ex);
        }
    }

    void NodeSetting(ITEM_DATA eIndex, object Data)
    {
        Dictionary<ITEM_DATA, object> node = new Dictionary<ITEM_DATA, object>();
        node.Add(eIndex, Data);
        m_ItemData.Add(node);
    }

    public object GetItemData(ITEM_DATA eIndex)
    {   //해당 아이템 정보 오브젝트 반환
        return m_ItemData[(int)eIndex][eIndex];
    }

    public void ItemUpdate(ITEM_DATA eIndex, object Data)   //아이템 정보 갱신
    {

    }
}