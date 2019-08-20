using System.Collections.Generic;
using System;

public class UserCharData
{
    List<CharacterData> m_ListChar = new List<CharacterData>(); //내가 가진 캐릭터 정보 리스트

    public UserCharData(List<Dictionary<string, object>> CharTable)
    {
        CharInfoData Char = JSON.JsonUtil.LoadJson<CharInfoData>("UserCharInfoData");
        CharacterData Node = new CharacterData(Char, CharTable);
        m_ListChar.Add(Node);   //내가 가진 캐릭터 인덱스 값
    }

    /// <summary>
    /// 캐릭터 데이터 획득
    /// </summary>
    /// <returns></returns>
    public List<CharacterData> GetMyCharList()
    {
        return m_ListChar;
    }
    public int GetMyCharCount()
    {
        return m_ListChar.Count;
    }
    public object GetChar(CHAR_DATA eIndex, int iIndex) //캐릭터의 인덱스 기반으로 반환
    {
        for(int i = 0; i < m_ListChar.Count;i++)
        {
           int CharIndex = Util.ConvertToInt(m_ListChar[i].GetCharData(CHAR_DATA.CHAR_INDEX));
           if (CharIndex == iIndex) //캐릭터 인덱스 기반
                return m_ListChar[iIndex].GetCharData(eIndex);
        }
        return null;
    }

    /// <summary>
    /// 캐릭터 데이터 업데이트
    /// </summary>
    /// <param name="eIndex"></param>
    /// <param name="UpdateData"></param>
    /// <param name="iIndex"></param>
    public void CharUpdate(CHAR_DATA eIndex, object UpdateData, int iIndex)
    {
        //캐릭터 업데이트
        m_ListChar[iIndex].CharUpdate(eIndex, UpdateData);
    }
    public bool ifCharLevelUp(int iIndex, List<Dictionary<string, object>> Table)
    {
        return m_ListChar[iIndex].ifCharLevelUP(Table);
    }


    /// <summary>
    /// Save data
    /// </summary>
    public void Save()
    {
        //내가 가진 모든 캐릭터 세이브
        foreach(var L in m_ListChar)
        {
            CharInfoData Node = new CharInfoData();
            Node.CharIndex = Util.ConvertToInt(L.GetCharData(CHAR_DATA.CHAR_INDEX));
            Node.CharCurEXP = Util.ConvertToInt(L.GetCharData(CHAR_DATA.CHAR_CUR_EXP));
            Node.CharWeapon = Util.ConvertToInt(L.GetCharData(CHAR_DATA.CHAR_WEAPON_INDEX));
            Node.CharStigmaTop = Util.ConvertToInt(L.GetCharData(CHAR_DATA.CHAR_STIGMA_TOP_INDEX));
            Node.CharStigmaCenter = Util.ConvertToInt(L.GetCharData(CHAR_DATA.CHAR_STIGMA_CENTER_INDEX));
            Node.CharStigmaBottom = Util.ConvertToInt(L.GetCharData(CHAR_DATA.CHAR_STIGMA_BOTTOM_INDEX));
        }

    }
}