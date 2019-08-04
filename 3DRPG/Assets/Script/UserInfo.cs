using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserInfo : MonoSingleton<UserInfo>
{
    private UserData m_UserData;    //유저 데이터
    private List<GameObject> m_ListCharObject = new List<GameObject>();  //유저가 가진 모든 캐릭터 오브젝트
    // Start is called before the first frame upda te
    void Start()
    {
        var Userinfo = EXCEL.ExcelLoad.Read("Excel/UserInfo");
        var UserCharData = EXCEL.ExcelLoad.Read("Excel/UserCharData");
        var UserTable = EXCEL.ExcelLoad.Read("Excel/UserTable");
        m_UserData = new UserData(Userinfo, UserCharData, UserTable);
        //리스트로 미리 게임 캐릭터 프리펩을 모두 담아놓고 필요할 때 빼서 쓰자

        int iCount = m_UserData.GetMyCharCount();
        for (int i = 0; i < iCount; i++)
        {
            try
            {
                string route = m_UserData.GetCharData(CharacterData.CHAR_ENUM.CHAR_ROUTE, i).ToString();
                string name = m_UserData.GetCharData(CharacterData.CHAR_ENUM.CHAR_NAME, i).ToString();
                string strTmp = route + "Prefabs/" + name;
                GameObject Char = ResourceLoader.CreatePrefab(strTmp);
                Char.SetActive(false);
                m_ListCharObject.Add(Char); //내가 가진 캐릭터들
            }
            catch (NullReferenceException ex)
            {
                Debug.Log("Char Prefabs is NULL");
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public object GetCharData(CharacterData.CHAR_ENUM eIndex, int iIndex)
    {
        return m_UserData.GetCharData(eIndex, iIndex);
    }

    public GameObject GetCharPrefabs(int iIndex)
    {
        GameObject Object = m_ListCharObject[iIndex];
        m_ListCharObject.RemoveAt(iIndex);
        Object.SetActive(true);
        return Object;
    }

    public void ReturnCharPrefabs(int iIndex, GameObject Object)
    {
        m_ListCharObject.Insert(iIndex, Object);
        //사용한 것들을 재삽입

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
