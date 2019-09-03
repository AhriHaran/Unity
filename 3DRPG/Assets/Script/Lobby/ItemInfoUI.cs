using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM_INFO_UI
{
    ITEM_INFO_CUR,
    ITEM_INFO_SELECT,
}

public class ItemInfoUI : MonoBehaviour
{
    private UILabel [,] m_ItemInfo = new UILabel[4,2];
    private int m_iCurSelectChar;   //현재 장비를 바꾸려는 캐릭터   
    private ITEM_TYPE m_eSelectType;    //현재 바꾸려는 장비 타입
    
    // Start is called before the first frame update
    private void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            m_ItemInfo[i, (int)ITEM_INFO_UI.ITEM_INFO_CUR] = transform.GetChild(0).transform.GetChild(i + 0).GetComponent<UILabel>();
            m_ItemInfo[i, (int)ITEM_INFO_UI.ITEM_INFO_SELECT] = transform.GetChild(0).transform.GetChild(i + 4).GetComponent<UILabel>();
        }
    }

    private void OnEnable()
    {
        m_iCurSelectChar = GameManager.instance.ReturnCurSelectChar();    //바꾸려고 하는 캐릭터
        m_eSelectType = GameManager.instance.ReturnSelectType(); //바꾸고자 하는 타입

        int iCurItemIndex = 0;  //현재 인덱스
        if (m_eSelectType == ITEM_TYPE.ITEM_STIGMA_TOP)
            iCurItemIndex = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_TOP_INDEX, m_iCurSelectChar));
        else if (m_eSelectType == ITEM_TYPE.ITEM_STIGMA_CENTER)
            iCurItemIndex = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_CENTER_INDEX, m_iCurSelectChar));
        else if (m_eSelectType == ITEM_TYPE.ITEM_STIGMA_BOTTOM)
            iCurItemIndex = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_BOTTOM_INDEX, m_iCurSelectChar));
        else            //무기 타입 등
            iCurItemIndex = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_WEAPON_INDEX, m_iCurSelectChar));

        ItemInfo((int)ITEM_INFO_UI.ITEM_INFO_CUR, iCurItemIndex);
        ItemInfo((int)ITEM_INFO_UI.ITEM_INFO_SELECT, -1);   //초기 셋팅
        //우선 현재 장착된 장비를 상태로 데이터를 보여준다.
    }

    public void ItemInfo(int iarrPos, int iWeaponIndx)
    {
        string strHP = "0", strATK = "0", strDEF = "0", strCRI = "0";
        if(iWeaponIndx >= 0)
        {
            INVENTORY_TYPE eType = GameManager.instance.ReturnInvenType();
            strHP = Util.ConvertToString(UserInfo.instance.GetItemForList(iWeaponIndx, eType, ITEM_DATA.ITEM_HP));
            strATK = Util.ConvertToString(UserInfo.instance.GetItemForList(iWeaponIndx, eType, ITEM_DATA.ITEM_ATK));
            strDEF = Util.ConvertToString(UserInfo.instance.GetItemForList(iWeaponIndx, eType, ITEM_DATA.ITEM_DEF));
            strCRI = Util.ConvertToString(UserInfo.instance.GetItemForList(iWeaponIndx, eType, ITEM_DATA.ITEM_CRI));
        }

        m_ItemInfo[0, iarrPos].text = strHP;
        m_ItemInfo[1, iarrPos].text = strATK;
        m_ItemInfo[2, iarrPos].text = strDEF;
        m_ItemInfo[3, iarrPos].text = strCRI;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
