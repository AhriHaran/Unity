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

    public object GetCharData(CharacterData.CHAR_ENUM eIndex, int iIndex)
    {
        return m_UserData.GetCharData(eIndex, iIndex);
    }

    public GameObject GetCharPrefabs(Transform Parent, int iIndex)
    {
        GameObject MainChar = new GameObject();
        try
        {
            string route = m_UserData.GetCharData(CharacterData.CHAR_ENUM.CHAR_ROUTE, iIndex).ToString();
            string name = m_UserData.GetCharData(CharacterData.CHAR_ENUM.CHAR_NAME, iIndex).ToString();
            string strTmp = route + "Prefabs/" + name;
            MainChar = ResourceLoader.CreatePrefab(strTmp, Parent);
            return MainChar;
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Char Prefabs is NULL");
            return null;
        }
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
}
