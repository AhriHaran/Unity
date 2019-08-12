using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    
    ////일반 클래스 유저의 정보를 담는 용도로만 사용된다.
    //유저가 필요한 거, 레벨에 따른 재화 정보
    //가지고 있는 캐릭터의 수와 그 레벨
    //가지고 있는 장비의 수와 그 레벨
    //기타 아이템과 그 수
    List<string> m_UserInfo = new List<string>();   //리스트와 enum 기반의 유저 데이터 값
    List<CharacterData> m_ListChar = new List<CharacterData>(); //내가 가진 캐릭터 정보 리스트
    public UserData(List<Dictionary<string, object>> UserInfo, List<Dictionary<string, object>> UserChar, List<Dictionary<string, object>> UserTable)
    {
        foreach (USER_INFO e in System.Enum.GetValues(typeof(USER_INFO)))
        {
            string Node;
            if (e == USER_INFO.USER_INFO_MAX_ENERGY || e == USER_INFO.USER_INFO_MAX_EXP)
            {
                Node = m_UserInfo[(int)USER_INFO.USER_INFO_LEVEL];
                Node = UserTable[int.Parse(Node)][e.ToString()].ToString();
                //해당 밸류는 유저 테이블 기반
            }
            else
            {
                Node = UserInfo[0][e.ToString()].ToString();
            }
            m_UserInfo.Add(Node);
        }

        for (int i = 0; i < UserChar.Count; i++)//내가 가진 캐릭터 테이블
        {
            CharacterData Node = new CharacterData(i, UserChar);
            m_ListChar.Add(Node);   //내가 가진 캐릭터 인덱스 값
        }
    }

    public object GetCharData(CHAR_DATA eIndex, int CharIndex)
    {
        //내가 가진 캐릭터 정보
        return m_ListChar[CharIndex].GetCharData(eIndex);
    }

    public string GetUserData(USER_INFO eIndex)
    {
        return m_UserInfo[(int)eIndex];
    }

    public List<CharacterData> GetMyCharList()
    {
        return m_ListChar;  //내가 가진 전체 리스트
    }

    public int GetMyCharCount()
    {
        return m_ListChar.Count;
    }

}
