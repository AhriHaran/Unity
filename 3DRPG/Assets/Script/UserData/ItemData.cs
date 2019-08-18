using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public enum ITEM_DATA
{
    ITEM_TYPE,
    ITEM_NAME,
    ITEM_ROUTE,
    ITEM_INDEX,
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
    //성흔 데이터
    //성흔은 총 HP와 방어력
    List<Dictionary<ITEM_DATA, object>> m_StigmaData = new List<Dictionary<ITEM_DATA, object>>();
    public ItemData(string TableRoute, ItemInfoData Data)
    {
        string Route = TableRoute + Data.ItemType + "_" + Data.ItemIndex;
        try
        {
            var Table = EXCEL.ExcelLoad.Read(Route);
            //해당 인덱스 데이터의 엑셀
            NodeSetting(ITEM_DATA.ITEM_TYPE, Data.ItemType);
            NodeSetting(ITEM_DATA.ITEM_NAME, Data.ItemName);
            NodeSetting(ITEM_DATA.ITEM_ROUTE, Data.ItemRoute);
            NodeSetting(ITEM_DATA.ITEM_INDEX, Data.ItemIndex);
            NodeSetting(ITEM_DATA.ITEM_LEVEl, Data.ItemLevel);
            NodeSetting(ITEM_DATA.ITEM_MAX_EXP, Table[Data.ItemLevel - 1][ITEM_DATA.ITEM_MAX_EXP.ToString()]);
            NodeSetting(ITEM_DATA.ITEM_CUR_EXP, Data.ItemCurEXP);
            NodeSetting(ITEM_DATA.ITEM_HP, Table[Data.ItemLevel - 1][ITEM_DATA.ITEM_HP.ToString()]);
            NodeSetting(ITEM_DATA.ITEM_ATK, Table[Data.ItemLevel - 1][ITEM_DATA.ITEM_ATK.ToString()]);
            NodeSetting(ITEM_DATA.ITEM_DEF, Table[Data.ItemLevel - 1][ITEM_DATA.ITEM_DEF.ToString()]);
            NodeSetting(ITEM_DATA.ITEM_CRI, Table[Data.ItemLevel - 1][ITEM_DATA.ITEM_CRI.ToString()]);
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
        m_StigmaData.Add(node);
    }

    public object GetItemData(ITEM_DATA eIndex)
    {   //해당 캐릭터 정보 오브젝트 반환
        return m_StigmaData[(int)eIndex][eIndex];
    }

}