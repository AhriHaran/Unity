using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WEAPON_DATA
{
    WEAPON_TYPE,
    WEAPON_NAME,
    WEAPON_ROUTE,
    WEAPON_INDEX,
    WEAPON_LEVEl,
    WEAPON_MAX_EXP,
    WEAPON_CUR_EXP,
    WEAPON_ATK,
    WEAPON_CRI,
}

public class WeaponData
{
    //무기 데이터
    //무기는 공격력과 크리
    private const string m_TableRoute = "Excel/WeaponTable/";
    List<Dictionary<WEAPON_DATA, object>> m_WeaponData = new List<Dictionary<WEAPON_DATA, object>>();
    public WeaponData(ItemInfoData Data)
    {
        string Route = m_TableRoute + Data.ItemType + "_" + Data.ItemIndex;
        var Table = EXCEL.ExcelLoad.Read(Route);
        //해당 인덱스 데이터의 엑셀
        NodeSetting(WEAPON_DATA.WEAPON_TYPE, Data.ItemType);
        NodeSetting(WEAPON_DATA.WEAPON_NAME, Data.ItemName);
        NodeSetting(WEAPON_DATA.WEAPON_ROUTE, Data.ItemRoute);
        NodeSetting(WEAPON_DATA.WEAPON_INDEX, Data.ItemIndex);
        NodeSetting(WEAPON_DATA.WEAPON_LEVEl, Data.ItemLevel);
        NodeSetting(WEAPON_DATA.WEAPON_MAX_EXP, Table[Data.ItemLevel][WEAPON_DATA.WEAPON_MAX_EXP.ToString()]);
        NodeSetting(WEAPON_DATA.WEAPON_CUR_EXP, Data.ItemCurEXP);
        NodeSetting(WEAPON_DATA.WEAPON_ATK, Table[Data.ItemLevel][WEAPON_DATA.WEAPON_ATK.ToString()]);
        NodeSetting(WEAPON_DATA.WEAPON_CRI, Table[Data.ItemLevel][WEAPON_DATA.WEAPON_CRI.ToString()]);
    }

    void NodeSetting(WEAPON_DATA eIndex, object Data)
    {
        Dictionary<WEAPON_DATA, object> node = new Dictionary<WEAPON_DATA, object>();
        node.Add(eIndex, Data);
        m_WeaponData.Add(node);
    }

    public object GetWeaponData(WEAPON_DATA eIndex)
    {   //해당 캐릭터 정보 오브젝트 반환
        return m_WeaponData[(int)eIndex][eIndex];
    }
}