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
    private static char[] SPLIT_CHAR = new char []{ ',', ';' }; //스플릿 문자열
    List<CharacterData> m_ListChar; //내가 가진 캐릭터 리스트
    //인벤토리
    
    public UserData()
    {
        //생성자
        //현재 유저 레벨에 의한 자원 재화
        List<Dictionary<string, object>> Read = ExcelLoad.Read("Excel/UserInfo");

        m_strNickName = Read[0]["NickName"].ToString();
        m_iLevel = int.Parse(Read[0]["Level"].ToString());
        m_iCurEnergy = int.Parse(Read[0]["CurEnergy"].ToString());
        m_iGold = int.Parse(Read[0]["Gold"].ToString());
        m_iCurEXP = int.Parse(Read[0]["CurExp"].ToString());
        m_strMainChar = Read[0]["UserMainChar"].ToString(); //메인 캐릭터

        int iIndex = int.Parse(Read[0]["CharNum"].ToString());
        List<Dictionary<string, object>> m_Read = ExcelLoad.Read("Excel/UserCharData");
        for (int i = 0; i < iIndex; i++)
        {
            CharacterData Node = new CharacterData(i, m_Read);
            m_ListChar.Add(Node);   //내가 가진 캐릭터 인덱스 값
        }
        Read = ExcelLoad.Read("Excel/UserData");    //유저의 데이터들

        m_iMaxEnergy = int.Parse(Read[m_iLevel]["Energy"].ToString());
        m_iMaxEXP = int.Parse(Read[m_iLevel]["Exp"].ToString());
    }
    
    //정보를 반환


}
