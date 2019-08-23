using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectSprite : MonoBehaviour
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
    private int m_iItemIndex;
    private ITEM_TYPE m_eItemType;
    private INVENTORY_TYPE m_eInvenType;

    private void Awake()
    {
        m_ItemSprite = transform.GetChild(1).GetComponent<UISprite>();
        m_ItemLabel = transform.GetChild(3).GetComponent<UILabel>();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
