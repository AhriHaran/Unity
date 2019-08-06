using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class UserInfo : GSingleton<UserInfo>
{
    private UserData m_UserData;    //유저 데이터

    // Start is called before the first frame upda te
    public void Init()
    {
        FirstLoadScene first = GameObject.Find("FirstLoad").GetComponent<FirstLoadScene>();
        UnityEvent Event = new UnityEvent();
        Event.AddListener(first.UserInfoComplete);
        //유저 데이터를 모두 셋팅 하면 호출할 이벤트 설정

        var Userinfo = EXCEL.ExcelLoad.Read("Excel/UserInfo");
        var UserCharData = EXCEL.ExcelLoad.Read("Excel/UserCharData");
        var UserTable = EXCEL.ExcelLoad.Read("Excel/UserTable");

        m_UserData = new UserData(Userinfo, UserCharData, UserTable);
        Event.Invoke();
    }

    public object GetCharData(CharacterData.CHAR_ENUM eIndex, int iIndex)
    {
        return m_UserData.GetCharData(eIndex, iIndex);
    }

    public UnityEngine.Object GetCharAnimator(int iIndex, CharacterData.CHAR_ANIMATOR eIndex)
    {
        try
        {
            string route = m_UserData.GetCharData(CharacterData.CHAR_ENUM.CHAR_ROUTE, iIndex).ToString();
            string strTmp = route + "Animators/" + eIndex.ToString();
            return ResourceLoader.LoadResource(strTmp);
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Animator is NULL");
            return null;
        }
    }

    public int GetMainCharIndex()
    {
        return m_UserData.GetMainCharIndex();
    }


    public List<CharacterData> GetMyCharList()
    {
        return m_UserData.GetMyCharList();  //내캐릭터 전체 리스트
    }

    public int GetMyCharCount()
    {
        return m_UserData.GetMyCharCount();
    }
}
