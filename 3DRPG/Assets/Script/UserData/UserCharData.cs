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
    
}