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
        Event.Invoke();
        if (JSON.JsonUtil.FileCheck("UserInfoData"))
        {
            //해당 데이터를 확인,
            //유저 데이터를 모두 셋팅 하면 호출할 이벤트 설정
            var UserTable = EXCEL.ExcelLoad.Read("Excel/Table/UserTable");
            m_UserData = new UserData(UserTable);
        }

        //인벤토리 셋팅
        m_UserInventory = new UserInventory();

        //내가 가진 캐릭터
        if (JSON.JsonUtil.FileCheck("UserCharInfoData"))
        {
            m_UserCharData = new UserCharData();
            CharInfoData[] Char = JSON.JsonUtil.LoadArrJson<CharInfoData>("UserCharInfoData");
            for (int i = 0; i < Char.Length; i++)
            {
                string file = "Excel/Table/" + Char[i].CharIndex + "_Char_Table";//테이블 데이터
                var CharTable = EXCEL.ExcelLoad.Read(file);
                m_UserCharData.Init(Char[i], CharTable);
            }
        }
        //유저의 캐릭터 데이터를 먼저 받아주고, 인벤토리를 받는다.
       
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
            string route = GetCharData(CHAR_DATA.CHAR_INDEX, iIndex).ToString();
            string strTmp = "Player/" + route + "/Animators/" + eIndex.ToString();
            return ResourceLoader.LoadResource(strTmp);
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Animator is NULL");
            Debug.Log(ex);
            return null;
        }
    }
    public object GetItemForList(int inventoryIndex, INVENTORY_TYPE eType, ITEM_DATA eIndex)
    {
        //인벤토리 인덱스 순서
        return m_UserInventory.GetItemForList(inventoryIndex, eType, eIndex);
    }
    public object GetItemForIndex(int itemIndex, INVENTORY_TYPE eType, ITEM_DATA eIndex)
    {
        //아이템 인덱스 순서
        return m_UserInventory.GetItemForIndex(itemIndex, eType, eIndex);
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
        string file = "Excel/Table/" + iIndex + "_Char_Table";//테이블 데이터
        var CharTable = EXCEL.ExcelLoad.Read(file);
        return m_UserCharData.ifCharLevelUp(iIndex, CharTable);
    }
    public void InventoryUpdate(ITEM_TYPE ItemType, INVENTORY_TYPE eInven, int itemIndex)   //새로운 아이템 획득
    {
        m_UserInventory.InventoryUpdate(ItemType, eInven, itemIndex);//인벤토리 새로운 아이템 획득
    }
    public void InventoryUpdate(INVENTORY_TYPE eInven, int itemIndex, ITEM_DATA eData, object UpdateData)
    {
        //무슨 인벤토리에 무슨 아이템에 무슨 데이터가 업데이트 되었다.
        m_UserInventory.InventoryUpdate(eInven, itemIndex, eData, UpdateData);
    }
    public void ItemUpdateForChar(INVENTORY_TYPE eInven, int itemIndex, bool bUpgrade)  //true 면 장비 장착, false면 장비 해제
    {
        m_UserInventory.ItemUpdateForChar(eInven, itemIndex, bUpgrade);
    }

    /// <summary>
    /// SaveData
    /// </summary>
    /// 
    public void AllSave()
    {
        m_UserData.Save();
        m_UserCharData.Save();
        m_UserInventory.Save();
    }
    public void UserDataSave()
    {
        m_UserData.Save();
    }
    public void UserCharSave()
    {
        m_UserCharData.Save();
    }
    public void UserInvenSave()
    {
        m_UserInventory.Save();
    }
}
