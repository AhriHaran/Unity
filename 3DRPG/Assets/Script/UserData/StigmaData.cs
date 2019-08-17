using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public enum STIGMA_DATA
{
    STIGMA_TYPE,
    STIGMA_NAME,
    STIGMA_ROUTE,
    STIGMA_INDEX,
    STIGMA_LEVEl,
    STIGMA_MAX_EXP,
    STIGMA_CUR_EXP,
    STIGMA_HP,
    STIGMA_DEF,
}

public class StigmaData
{
    //성흔 데이터
    //성흔은 총 HP와 방어력
    private const string m_TableRoute = "Excel/StigmaTable/Stigma_";
    List<Dictionary<STIGMA_DATA, object>> m_StigmaData = new List<Dictionary<STIGMA_DATA, object>>();
    public StigmaData(ItemInfoData Data)
    {
        string Route = m_TableRoute + Data.ItemType + "_" + Data.ItemIndex;
        var Table = EXCEL.ExcelLoad.Read(Route);
        //해당 인덱스 데이터의 엑셀
        NodeSetting(STIGMA_DATA.STIGMA_TYPE, Data.ItemType);
        NodeSetting(STIGMA_DATA.STIGMA_NAME, Data.ItemName);
        NodeSetting(STIGMA_DATA.STIGMA_ROUTE, Data.ItemRoute);
        NodeSetting(STIGMA_DATA.STIGMA_INDEX, Data.ItemIndex);
        NodeSetting(STIGMA_DATA.STIGMA_LEVEl, Data.ItemLevel);
        NodeSetting(STIGMA_DATA.STIGMA_MAX_EXP, Table[Data.ItemLevel][STIGMA_DATA.STIGMA_MAX_EXP.ToString()]);
        NodeSetting(STIGMA_DATA.STIGMA_CUR_EXP, Data.ItemCurEXP);
        NodeSetting(STIGMA_DATA.STIGMA_HP, Table[Data.ItemLevel][STIGMA_DATA.STIGMA_HP.ToString()]);
        NodeSetting(STIGMA_DATA.STIGMA_DEF, Table[Data.ItemLevel][STIGMA_DATA.STIGMA_DEF.ToString()]);
    }

    void NodeSetting(STIGMA_DATA eIndex, object Data)
    {
        Dictionary<STIGMA_DATA, object> node = new Dictionary<STIGMA_DATA, object>();
        node.Add(eIndex, Data);
        m_StigmaData.Add(node);
    }

    public object GetStigmaData(STIGMA_DATA eIndex)
    {   //해당 캐릭터 정보 오브젝트 반환
        return m_StigmaData[(int)eIndex][eIndex];
    }

}