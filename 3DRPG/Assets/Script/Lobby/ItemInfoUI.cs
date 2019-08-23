using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoUI : MonoBehaviour
{
    private UILabel [,] m_ItemInfo = new UILabel[4,2];
    private int m_iCurSelectChar;
    private ITEM_TYPE m_eSelectType;
    private INVENTORY_TYPE m_eInvenType;
    
    // Start is called before the first frame update
    private void Awake()
    {
        for(int i = 0; i < 4; i++)
        {
            m_ItemInfo[i, 0] = transform.GetChild(0).transform.GetChild(i + 0).GetComponent<UILabel>();
            m_ItemInfo[i, 1] = transform.GetChild(0).transform.GetChild(i + 4).GetComponent<UILabel>();
        }
    }

    private void OnEnable()
    {
        m_iCurSelectChar = GameManager.instance.ReturnCurSelectChar();    //바꾸려고 하는 캐릭터
        m_eSelectType = GameManager.instance.ReturnSelectType(); //바꾸고자 하는 타입
        m_eInvenType = INVENTORY_TYPE.INVENTORY_START;

        int iCurItemIndex = 0;  //현재 인덱스
        if (m_eSelectType == ITEM_TYPE.ITEM_STIGMA_TOP)
        {
            iCurItemIndex = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_TOP_INDEX, m_iCurSelectChar));
            m_eInvenType = INVENTORY_TYPE.INVENTORY_STIGMA;
        }
        else if (m_eSelectType == ITEM_TYPE.ITEM_STIGMA_CENTER)
        {
            iCurItemIndex = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_CENTER_INDEX, m_iCurSelectChar));
            m_eInvenType = INVENTORY_TYPE.INVENTORY_STIGMA;
        }
        else if (m_eSelectType == ITEM_TYPE.ITEM_STIGMA_BOTTOM)
        {
            iCurItemIndex = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_BOTTOM_INDEX, m_iCurSelectChar));
            m_eInvenType = INVENTORY_TYPE.INVENTORY_STIGMA;
        }
        else
        {
            //무기 타입 등
            iCurItemIndex = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_WEAPON_INDEX, m_iCurSelectChar));
            m_eInvenType = INVENTORY_TYPE.INVENTORY_WEAPON;
        }

        ItemInfo(0, iCurItemIndex);
        //우선 현재 장착된 장비를 상태로 데이터를 보여준다.
    }

    public void ItemInfo(int iarrPos,int iWeaponIndx)
    {
        string strHP = "0", strATK = "0", strDEF = "0", strCRI = "0";
        if(iWeaponIndx >= 0)
        {
            strHP = Util.ConvertToString(UserInfo.instance.GetItemForList(iWeaponIndx, m_eInvenType, ITEM_DATA.ITEM_HP));
            strATK = Util.ConvertToString(UserInfo.instance.GetItemForList(iWeaponIndx, m_eInvenType, ITEM_DATA.ITEM_ATK));
            strDEF = Util.ConvertToString(UserInfo.instance.GetItemForList(iWeaponIndx, m_eInvenType, ITEM_DATA.ITEM_DEF));
            strCRI = Util.ConvertToString(UserInfo.instance.GetItemForList(iWeaponIndx, m_eInvenType, ITEM_DATA.ITEM_CRI));
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
