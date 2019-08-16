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
    public UserData(List<Dictionary<string, object>> UserTable, List<Dictionary<string, object>> CharTable)
    {
        //유저 테이블과 캐릭터 테이블은 엑셀
        UserInfoData Data = JSON.JsonUtil.LoadJson<UserInfoData>("UserInfoData");

        m_UserInfo.Add(Data.NickName);  //닉네임
        int iLevel = Data.Level -1;
        m_UserInfo.Add(Data.Level.ToString());  //레벨
        m_UserInfo.Add(Data.CurEnergy.ToString());  //유저 현재 에너지
        m_UserInfo.Add(UserTable[iLevel][USER_INFO.USER_INFO_MAX_ENERGY.ToString()].ToString());    //맥스 에너지
        m_UserInfo.Add(Data.CurEXP.ToString());    //유저 현재 경험치
        m_UserInfo.Add(UserTable[iLevel][USER_INFO.USER_INFO_MAX_EXP.ToString()].ToString());   //유저 전체 경험치
        m_UserInfo.Add(Data.Gold.ToString());    //유저 현재 경험치
        m_UserInfo.Add(Data.MainChar.ToString());   //유저 메인 캐릭터
        
        //내가 가진 캐릭터 JSon
        UserCharInfoData Char = JSON.JsonUtil.LoadJson<UserCharInfoData>("UserCharInfoData");

        CharacterData Node = new CharacterData(Char, CharTable);
        m_ListChar.Add(Node);   //내가 가진 캐릭터 인덱스 값
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
