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
    private string m_strNickName;   //유저 닉네임
    private int m_iLevel = 0;   //유저 레벨
    private int m_iCurEnergy = 0;   //유저 현재 에너지
    private int m_iMaxEnergy = 0;   //유저 맥스 에너지
    private int m_iCurEXP = 0;  //유저 현재 경험치
    private int m_iMaxEXP = 0;  //유저 레벨당 경험치
    private int m_iGold = 0;    //유저 재화
    private string m_strMainChar;    //유저 메인 캐릭터 인덱스
    List<CharacterData> m_ListChar = new List<CharacterData>(); //내가 가진 캐릭터 리스트
                                    //인벤토리
    public UserData(List<Dictionary<string, object>> UserInfo, List<Dictionary<string, object>> UserChar, List<Dictionary<string, object>> UserTable)
    {
        //생성자
        m_strNickName = UserInfo[0]["NickName"].ToString();
        m_iLevel = int.Parse(UserInfo[0]["Level"].ToString());
        m_iCurEnergy = int.Parse(UserInfo[0]["CurEnergy"].ToString());
        m_iGold = int.Parse(UserInfo[0]["Gold"].ToString());
        m_iCurEXP = int.Parse(UserInfo[0]["CurExp"].ToString());
        m_strMainChar = UserInfo[0]["UserMainChar"].ToString(); //메인 캐릭터

        for (int i = 0; i < UserChar.Count; i++)//내가 가진 캐릭터 테이블
        {
            CharacterData Node = new CharacterData(i, UserChar);
            m_ListChar.Add(Node);   //내가 가진 캐릭터 인덱스 값
        }

        m_iMaxEnergy = int.Parse(UserTable[m_iLevel]["Energy"].ToString());
        m_iMaxEXP = int.Parse(UserTable[m_iLevel]["Exp"].ToString());
    }

    public string GetRoute()
    {
        return m_ListChar[int.Parse(m_strMainChar)].GetRoute();
        //해당 캐릭터의 루트.
    }

    public string GetName()
    {
        return m_ListChar[int.Parse(m_strMainChar)].GetName();
    }

}
