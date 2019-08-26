using System.Collections.Generic;
using UnityEngine;
using System;

public class UserCharData
{
    List<CharacterData> m_ListChar = new List<CharacterData>(); //내가 가진 캐릭터 정보 리스트

    public UserCharData()
    {
    }
    public void Init(CharInfoData Char, List<Dictionary<string, object>> CharTable)
    {
        CharacterData Node = new CharacterData(Char, CharTable);
        m_ListChar.Add(Node);   //내가 가진 캐릭터 인덱스 값
        Node.CharUpdate();
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
    public object GetCharData(CHAR_DATA eIndex, int iIndex) //캐릭터의 인덱스 기반으로 반환
    {
        int iListIndex = GetIndexToCharIndx(iIndex);
        if (iListIndex >= 0)
            return m_ListChar[iListIndex].GetCharData(eIndex);
        else
            return null;
    }
    public int GetIndexToCharIndx(int CharIndex)    //캐릭터 인덱스를 기반으로 현재 리스트 인덱스 호출
    {
        for(int i = 0; i < m_ListChar.Count; i++)
        {
            int Char = Util.ConvertToInt(m_ListChar[i].GetCharData(CHAR_DATA.CHAR_INDEX));
            if (CharIndex == Char)
                return i;
        }
        return -1;
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
        int iListIndex = GetIndexToCharIndx(iIndex);
        if (iListIndex >= 0)
            m_ListChar[iListIndex].CharUpdate(eIndex, UpdateData);
    }
    public bool ifCharLevelUp(int iIndex, List<Dictionary<string, object>> Table)
    {
        int iListIndex = GetIndexToCharIndx(iIndex);
        if (iListIndex >= 0)
            return m_ListChar[iListIndex].ifCharLevelUP(Table);
        else
            return false;
    }
    //캐릭터 인덱스 기반 데이터 이므로
    
    /// <summary>
    /// Save data
    /// </summary>
    public void Save()
    {
        //내가 가진 모든 캐릭터 세이브
        int iCount = m_ListChar.Count;

        CharInfoData[] Node = new CharInfoData[iCount];
        try
        {
            for (int i = 0; i < iCount; i++)
            {
                Node[i] = new CharInfoData
                {
                    CharIndex = Util.ConvertToInt(m_ListChar[i].GetCharData(CHAR_DATA.CHAR_INDEX)),
                    CharLevel = Util.ConvertToInt(m_ListChar[i].GetCharData(CHAR_DATA.CHAR_LEVEL)),
                    CharCurEXP = Util.ConvertToInt(m_ListChar[i].GetCharData(CHAR_DATA.CHAR_CUR_EXP)),
                    CharWeaponType = (ITEM_TYPE)Util.ConvertToInt(m_ListChar[i].GetCharData(CHAR_DATA.CHAR_WEAPON_TYPE)),
                    CharWeapon = Util.ConvertToInt(m_ListChar[i].GetCharData(CHAR_DATA.CHAR_WEAPON_INDEX)),
                    CharStigmaTop = Util.ConvertToInt(m_ListChar[i].GetCharData(CHAR_DATA.CHAR_STIGMA_TOP_INDEX)),
                    CharStigmaCenter = Util.ConvertToInt(m_ListChar[i].GetCharData(CHAR_DATA.CHAR_STIGMA_CENTER_INDEX)),
                    CharStigmaBottom = Util.ConvertToInt(m_ListChar[i].GetCharData(CHAR_DATA.CHAR_STIGMA_BOTTOM_INDEX))
                };
            }

            string Char = JSON.JsonUtil.ToJson<CharInfoData>(Node);
            Debug.Log(Char);
            JSON.JsonUtil.CreateJson("UserCharInfoData", Char);
        }
        catch(System.NullReferenceException ex)
        {
            Debug.Log(ex);
        }
    }
}