﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStigmaUI : MonoBehaviour
{
    private UISprite m_StigmaT;
    private UISprite m_StigmaC;
    private UISprite m_StigmaB;

    private UILabel m_StigmaTName;
    private UILabel m_StigmaCName;
    private UILabel m_StigmaBName;

    // Start is called before the first frame update
    private void Awake()
    {
        m_StigmaT = transform.GetChild(0).transform.GetChild(0).GetComponent<UISprite>();
        m_StigmaC = transform.GetChild(1).transform.GetChild(0).GetComponent<UISprite>();
        m_StigmaB = transform.GetChild(2).transform.GetChild(0).GetComponent<UISprite>();

        m_StigmaTName = transform.GetChild(0).transform.GetChild(1).GetComponentInChildren<UILabel>();
        m_StigmaCName = transform.GetChild(1).transform.GetChild(1).GetComponentInChildren<UILabel>();
        m_StigmaBName = transform.GetChild(2).transform.GetChild(1).GetComponentInChildren<UILabel>();
    }

    public void OnFinished()
    {
        int iIndex = GameManager.instance.ReturnCurSelectChar();
        //해당 캐릭터
        
        int iData = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_TOP_INDEX, iIndex));
        string spriteName = string.Empty;
        if(iData >= 0)
        {
            spriteName = Util.ConvertToString(iData) + "_T";
            m_StigmaT.spriteName = spriteName;
            m_StigmaTName.text = Util.ConvertToString(UserInfo.instance.GetItemForList(iData, INVENTORY_TYPE.INVENTORY_STIGMA, ITEM_DATA.ITEM_NAME)) + "(T)";
        }

        iData = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_CENTER_INDEX, iIndex));
        if(iData >= 0)
        {
            spriteName = Util.ConvertToString(iData) + "_C";
            m_StigmaC.spriteName = spriteName;
            m_StigmaCName.text = Util.ConvertToString(UserInfo.instance.GetItemForList(iData, INVENTORY_TYPE.INVENTORY_STIGMA, ITEM_DATA.ITEM_NAME)) + "(C)";
        }

        iData = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_BOTTOM_INDEX, iIndex));
        if (iData >= 0)
        {
            spriteName = Util.ConvertToString(iData) + "_B";
            m_StigmaB.spriteName = spriteName;
            m_StigmaBName.text = Util.ConvertToString(UserInfo.instance.GetItemForList(iData, INVENTORY_TYPE.INVENTORY_STIGMA, ITEM_DATA.ITEM_NAME)) + "(B)";
        }
        else
        {
            m_StigmaB.spriteName = "CrossHair";
            m_StigmaBName.text = "None";
        }
        //onfinish가 되면 매번 셋팅
    }

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}