﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectUI : MonoBehaviour
{
    private UILabel m_ChangeItem;   //바꾸려는 아이템의 명칭
    private UILabel m_CurItemName;  //아이템 이름
    private UILabel m_CurItemLevel;
    public UIButton m_SelectButton;

    private int m_iCurSelectChar;
    private int m_iCurSelectItem;
    private ITEM_TYPE m_eSelectType;
    private INVENTORY_TYPE m_eInven;

    private GameObject m_ItemView = null;
    private GameObject m_GridChar = null;

    // Start is called before the first frame update
    private void Awake()
    {
        m_ChangeItem = transform.GetChild(1).GetComponent<UILabel>();
        m_CurItemName = transform.GetChild(2).GetComponent<UILabel>();
        m_CurItemLevel = transform.GetChild(3).GetComponent<UILabel>();
        m_ItemView = transform.GetChild(4).gameObject;
        m_GridChar = m_ItemView.transform.GetChild(0).gameObject;
        m_iCurSelectItem = -1;
    }

    private void OnEnable()
    {
        m_iCurSelectItem = -1;
        while (m_GridChar.transform.childCount != 0)
        {
            GameObject game = m_GridChar.transform.GetChild(0).gameObject ?? null;
            if (game != null)
            {
                game.transform.SetParent(null);
                NGUITools.Destroy(game);
            }
        }
        m_GridChar.transform.DetachChildren();

        m_CurItemName.text = "";
        m_CurItemLevel.text = "Lv.";
        m_iCurSelectChar = GameManager.instance.ReturnCurSelectChar();    //바꾸려고 하는 캐릭터
        m_eSelectType = GameManager.instance.ReturnSelectType(); //바꾸고자 하는 것

        m_ChangeItem.text = Util.ConvertToString(m_eSelectType);    //현재 바꾸려는 것
        m_eInven = GameManager.instance.ReturnInvenType();

        List<ItemData> Data = UserInfo.instance.GetInventoryList(m_eInven);
        
        for(int i = 0; i < Data.Count; i++)
        {
            ITEM_TYPE eType = (ITEM_TYPE)Data[i].GetItemData(ITEM_DATA.ITEM_TYPE);
            if (eType == m_eSelectType)
            {
                //장비 장착은 현재 아이템 인벤토리에 획득 순서를 기반으로 한다.
                //스프라이트의 선택은 레이캐스트와 콜백을 사용한다
                //콜백으로 현재 선택한 아이템의 리스트 인덱스를 저장한 뒤 선택 버튼을 누르면 갱신된다.
                GameObject Item = ResourceLoader.CreatePrefab("Prefabs/ItemSprite");
                Item.transform.SetParent(m_GridChar.transform, false);
                int iIndex = Util.ConvertToInt(UserInfo.instance.GetItemForList(i, m_eInven, ITEM_DATA.ITEM_INDEX));
                Item.GetComponent<ItemSprite>().Setting(iIndex, i, (ITEM_TYPE)Data[i].GetItemData(ITEM_DATA.ITEM_TYPE), m_eInven);
                Item.GetComponent<ItemSprite>().SetCallBack(ItemSelect);
            }
        }

        m_GridChar.GetComponent<UIGrid>().Reposition(); //리 포지셔닝으로 그리드 재정렬
        m_ItemView.GetComponent<UIScrollView>().ResetPosition();
        m_ItemView.GetComponent<UIPanel>().Refresh();
    }

    void ItemSelect(int iIndex) //콜백으로 선언
    {
        //리스트 기준의 아이템 인덱스와 리스트 인덱스
        //선택 시        
        if (m_iCurSelectItem != iIndex)
        {
            int iList = Util.ConvertToInt(UserInfo.instance.GetItemIndexForList(iIndex, m_eInven));
            int iEquip = Util.ConvertToInt(UserInfo.instance.GetItemForList(iList, m_eInven, ITEM_DATA.ITEM_EQUIP_CHAR));
            //해당 아이템을 장비하였는가?
            bool bRelease = false;
            if (iEquip == m_iCurSelectChar && m_eSelectType < ITEM_TYPE.ITEM_GAUNTLET)    //무기의 경우 해제 불가능
            {
                //해제버튼
                m_SelectButton.transform.GetChild(0).GetComponent<UISprite>().spriteName = "해제";
                m_SelectButton.normalSprite = "해제";
                bRelease = true;    //해제
            }
            else
            {
                m_SelectButton.transform.GetChild(0).GetComponent<UISprite>().spriteName = "변경";
                m_SelectButton.normalSprite = "변경";
                bRelease = false;   //변경
            }

            for(int i = 0; i < m_GridChar.transform.childCount; i++)
            {
                if (iIndex != i) //현재 선택 된 것과 다르다면
                    m_GridChar.transform.GetChild(i).GetComponent<ItemSprite>().ButtonSelect(false);
                //나머지 버튼들은 비 선택 처리를 해줘라
            }
            
            INVENTORY_TYPE eInven = GameManager.instance.ReturnInvenType();
            m_iCurSelectItem = iIndex;

            transform.parent.GetChild(1).GetComponent<ItemInfoUI>().ItemInfo((int)ITEM_INFO_UI.ITEM_INFO_SELECT, iIndex);
            //아이템 유아이에 장착 시 오를 능력치를 보여준다.
            m_CurItemName.text = Util.ConvertToString(UserInfo.instance.GetItemForList(iIndex, eInven, ITEM_DATA.ITEM_NAME));
            m_CurItemLevel.text = "Lv." + Util.ConvertToString(UserInfo.instance.GetItemForList(iIndex, eInven, ITEM_DATA.ITEM_LEVEl));
            //현재 선택한 아이템의 이름과 레벨
            GameManager.instance.SelectCurItem(iIndex);
            GameManager.instance.SelectCurItemEqipType(bRelease);
            //선택한 인덱스를 게임 매니저가 저장한다.
        }
    }
    public void ResetList()
    {
        for (int i = 0; i < m_GridChar.transform.childCount; i++)
        {
            m_GridChar.transform.GetChild(i).GetComponent<ItemSprite>().ButtonSelect(false);
            //모든 버튼을 비 선택 처리 해주고
        }
        m_SelectButton.transform.GetChild(0).GetComponent<UISprite>().spriteName = "변경";
        m_SelectButton.normalSprite = "변경";
        m_iCurSelectItem = -1;
    }
}
