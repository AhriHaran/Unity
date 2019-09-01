using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSprite : MonoBehaviour
{
    //아이템을 표시 해주는 스프라이트로, 다음과 같은 것이 필요하다

    /// <summary>
    /// 자동으로 스프라이트 갱신
    /// 선택 시 스프라이트 갱신
    /// 인벤토리에서 선택 시에는 해당 아이템의 정보 창이 뜨고
    /// 장비 선택창에서 선택시 콜백을 작동 시키고, 선택 시 스프라이트만 갱신한다.
    /// </summary>
    /// 
    public delegate void CallBack(int iIndex);
    private CallBack m_CallBack = null;
    //콜백 함수
    private UISprite m_ItemSprite;
    private UILabel m_ItemLabel;

    private GameObject m_Equip;

    private int m_iItemIndex;
    private ITEM_TYPE m_eItemType;
    private INVENTORY_TYPE m_eInvenType;

    private void Awake()
    {
        m_ItemSprite = transform.GetChild(1).GetComponent<UISprite>();
        m_ItemLabel = transform.GetChild(3).GetComponent<UILabel>();
        m_Equip = transform.GetChild(4).gameObject;
        m_Equip.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetCallBack(CallBack call)
    {
        m_CallBack = call;
    }

    public void Setting(int iIndex, ITEM_TYPE eType, INVENTORY_TYPE eInven)
    {
        m_iItemIndex = iIndex;  
        m_eItemType = eType;
        m_eInvenType = eInven;
    }

    public void ShowInfo()
    {
        string sprite = string.Empty;
        sprite = Util.ConvertToString(UserInfo.instance.GetItemForList(m_iItemIndex, m_eInvenType, ITEM_DATA.ITEM_INDEX)) + "_" + m_eItemType.ToString() + "_Icon";
        m_ItemSprite.spriteName = sprite;   //스프라이트 교체
        m_ItemLabel.text = Util.ConvertToString(UserInfo.instance.GetItemForList(m_iItemIndex, m_eInvenType, ITEM_DATA.ITEM_NAME));
        //이름 교채
        
        int iEquip = Util.ConvertToInt(UserInfo.instance.GetItemForList(m_iItemIndex, m_eInvenType, ITEM_DATA.ITEM_EQUIP_CHAR));
        if (iEquip >= 0)
            m_Equip.gameObject.SetActive(true);
        else
            m_Equip.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ShowInfo();
    }

    public void OnClick()
    {
        ButtonSelect(true);
        if (m_CallBack != null)
            m_CallBack(m_iItemIndex);
        //해당 패널을 클릭하였다.
    }

    public void ButtonSelect(bool bClick)   //해당 버튼을 선택하였다는 가시적인 보여줌
    {
        if(bClick)
        {
            gameObject.transform.GetChild(0).GetComponent<UISprite>().spriteName = "Window05Gold";
            gameObject.GetComponent<UIButton>().normalSprite = "Window05Gold";
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<UISprite>().spriteName = "Window05Grey";
            gameObject.GetComponent<UIButton>().normalSprite = "Window05Grey";
        }
    }
}
