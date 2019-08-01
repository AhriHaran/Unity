using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserInfo : MonoSingleton<UserInfo>
{
    private UserData m_UserData;    //유저 데이터
    // Start is called before the first frame upda te
    void Start()
    {
        var Userinfo = EXCEL.ExcelLoad.Read("Excel/UserInfo");
        var UserCharData = EXCEL.ExcelLoad.Read("Excel/UserCharData");
        var UserTable = EXCEL.ExcelLoad.Read("Excel/UserTable");
        m_UserData = new UserData(Userinfo, UserCharData, UserTable);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetMainChar(Transform Parent)
    {
        //메인 로비 캐릭터 리턴
        GameObject MainChar = new GameObject();
        try
        {
            string strTmp = m_UserData.GetRoute() + "Prefabs/" + m_UserData.GetName();
            MainChar = ResourceLoader.CreatePrefab(strTmp, Parent);
            return MainChar;
        }
        catch(NullReferenceException ex)
        {
            Debug.Log("Main Char is NULL");
            return null;
        }
    }

    public string GetMainCharLobbyAnimator()
    {
       string strTmp = m_UserData.GetRoute() + "Animators/LobbyAnimator";
        return strTmp;
    }

}
