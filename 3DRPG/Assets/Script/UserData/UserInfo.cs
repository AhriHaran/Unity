using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class UserInfo : GSingleton<UserInfo>
{
    private UserData m_UserData;    //유저 데이터
    private UserInventory m_UserInventory;  //유저 인벤토리, 아이템등의 데이터
    private UserCharData m_UserCharData;    //유저 소유 캐릭터 데이터
    // Start is called before the first frame upda te

    /// <summary>
    /// 셋팅 관련
    /// </summary>
    public void Init()
    {
        FirstLoadScene first = GameObject.Find("FirstLoad").GetComponent<FirstLoadScene>();
        UnityEvent Event = new UnityEvent();
        Event.AddListener(first.UserInfoComplete);

        if (JSON.JsonUtil.FileCheck("UserInfoData"))
        {
            //해당 데이터를 확인,
            //유저 데이터를 모두 셋팅 하면 호출할 이벤트 설정
            var UserTable = EXCEL.ExcelLoad.Read("Excel/Table/UserTable");
            m_UserData = new UserData(UserTable);
        }

        //내가 가진 캐릭터
        var CharTable = EXCEL.ExcelLoad.Read("Excel/CharacterExcel/0_Index_Char");
        m_UserCharData = new UserCharData(CharTable);
        //처음 셋팅은 이정도만
        
        Event.Invoke();
    }
    public void InvenSetting()
    {
        m_UserInventory = new UserInventory();
    }

    /// <summary>
    /// 유저 데이터 관련 반환
    /// </summary>
    /// <returns></returns>
    public List<CharacterData> GetMyCharList()
    {
        return m_UserCharData.GetMyCharList();  //내가 가진 전체 리스트
    }
    public int GetMyCharCount()
    {
        return m_UserCharData.GetMyCharCount();
    }
    public object GetCharData(CHAR_DATA eIndex, int iIndex)
    {
        return m_UserCharData.GetCharData(eIndex, iIndex);
    }
    public string GetUserData(USER_INFO eIndex)
    {
        return m_UserData.GetUserData(eIndex);//유저의 데이터
    }
    public UnityEngine.Object GetCharAnimator(int iIndex, CHAR_ANIMATOR eIndex)
    {
        try
        {
            string route = GetCharData(CHAR_DATA.CHAR_ROUTE, iIndex).ToString();
            string strTmp = "Player/" + route + "Animators/" + eIndex.ToString();
            return ResourceLoader.LoadResource(strTmp);
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Animator is NULL");
            return null;
        }
    }
    public object GetInventoryItem(int inventoryIndex, INVENTORY_TYPE eType, ITEM_DATA eIndex)
    {
        //인벤토리 인덱스 순서
        return m_UserInventory.GetInventoryItem(inventoryIndex, eType, eIndex);
    }
    public List<ItemData> GetInventoryList(INVENTORY_TYPE eType)
    {
        return m_UserInventory.GetInventoryList(eType);
    }

    /// <summary>
    /// 유저 데이터 업데이트
    /// </summary>
    /// <param name="eIndex"></param>
    /// <param name="UpdateData"></param>
    public void UserUpdate(USER_INFO eIndex, object UpdateData)
    {
        m_UserData.UserUpdate(eIndex, UpdateData);
    }
    public bool ifUserLevelUP()
    {
        var UserTable = EXCEL.ExcelLoad.Read("Excel/Table/UserTable");
        return m_UserData.ifUserLevelUP(UserTable);
    }
    public void CharUpdate(CHAR_DATA eIndex, object UpdateData, int iIndex)
    {
        m_UserCharData.CharUpdate(eIndex, UpdateData, iIndex);
    }
    public bool ifCharLevelUp(int iIndex)
    {
        var CharTable = EXCEL.ExcelLoad.Read("Excel/CharacterExcel/0_Index_Char");
        return m_UserCharData.ifCharLevelUp(iIndex, CharTable);
    }
    public void InventoryUpdate(int itemIndex, INVENTORY_TYPE eType, string ItemType)
    {
        m_UserInventory.InventoryUpdate(itemIndex, eType, ItemType);
    }

    
}
