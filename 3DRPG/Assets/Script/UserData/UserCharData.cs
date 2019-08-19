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
    public object GetCharData(CHAR_DATA eIndex, int iIndex)
    {
        return m_ListChar[iIndex].GetCharData(eIndex);
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
        if (m_ListChar[iIndex].m_bLevelUP)
        {
            m_ListChar[iIndex].CharUpdate(Table);
            return true;
        }
        else
            return false;
    }

    public void CharSave()
    {

    }
}