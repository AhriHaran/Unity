using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    ////일반 클래스 유저의 정보를 담는 용도로만 사용된다.
    List<string> m_UserInfo = new List<string>();   //리스트와 enum 기반의 유저 데이터 값
    public UserData(List<Dictionary<string, object>> UserTable)
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
    }

    public string GetUserData(USER_INFO eIndex)
    {
        return m_UserInfo[(int)eIndex];
    }

    public void UserDataUpdate(USER_INFO eIndex, object UpdateData)
    {
        //유저의 업데이트 데이터
    }
}
