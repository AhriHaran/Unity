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

    public void UserUpdate(USER_INFO eIndex, object UpdateData)
    {
        m_UserInfo[(int)eIndex] = UpdateData.ToString();
    }
    public bool ifUserLevelUP(List<Dictionary<string, object>> UserTable)
    {
        int iCurEXP = Util.ConvertToInt(m_UserInfo[(int)USER_INFO.USER_INFO_CUR_EXP]);
        int iLevel = Util.ConvertToInt(m_UserInfo[(int)USER_INFO.USER_INFO_LEVEL]);
        bool bLevelUp = false;
        int iTableLevel = iLevel - 1;
        while (true)
        {
            iTableLevel = iLevel - 1;
            int iMaxEXP = Util.ConvertToInt(UserTable[iTableLevel][USER_INFO.USER_INFO_MAX_EXP.ToString()]);  //레벨당 최대 경험치 대비
            if (iCurEXP >= iMaxEXP)
            {
                iLevel++;
                iCurEXP -= iMaxEXP; //
                bLevelUp = true;
            }
            else
                break;
        }
        iTableLevel = iLevel - 1;
        UserUpdate(USER_INFO.USER_INFO_LEVEL, iLevel);//현재 레벨
        UserUpdate(USER_INFO.USER_INFO_CUR_EXP, iCurEXP); //현재 경험치
        UserUpdate(USER_INFO.USER_INFO_MAX_EXP, UserTable[iTableLevel][USER_INFO.USER_INFO_MAX_EXP.ToString()]); //현재 레벨 max EXP
        UserUpdate(USER_INFO.USER_INFO_MAX_ENERGY, UserTable[iTableLevel][USER_INFO.USER_INFO_MAX_ENERGY.ToString()]); //현재 레벨 max EXP
        if (bLevelUp)   //레벨 업 시, 현재 에너지를 맥스로 채워준다.
            UserUpdate(USER_INFO.USER_INFO_CUR_ENERGY, UserTable[iTableLevel][USER_INFO.USER_INFO_MAX_ENERGY.ToString()]);
        
        return bLevelUp;
    }


    public void Save()
    {
        try
        {
            UserInfoData Data = JSON.JsonUtil.LoadJson<UserInfoData>("UserInfoData");
            Data.Level = Util.ConvertToInt(GetUserData(USER_INFO.USER_INFO_LEVEL));
            Data.CurEnergy = Util.ConvertToInt(GetUserData(USER_INFO.USER_INFO_CUR_ENERGY));
            Data.CurEXP = Util.ConvertToInt(GetUserData(USER_INFO.USER_INFO_CUR_EXP));
            Data.Gold = Util.ConvertToInt(GetUserData(USER_INFO.USER_INFO_GOLD));
            Data.Gold = Util.ConvertToInt(GetUserData(USER_INFO.USER_INFO_GOLD));
            string StrData = JSON.JsonUtil.ToJson(Data);
            JSON.JsonUtil.CreateJson("UserInfoData", StrData);
            Debug.Log(StrData);
        }
        catch(System.NullReferenceException ex)
        {
            Debug.Log(ex);
        }
        //json 데이터 갱신
    }
}
