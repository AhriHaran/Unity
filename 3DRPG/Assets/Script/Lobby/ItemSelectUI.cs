using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectUI : MonoBehaviour
{
    private UILabel m_ChangeItem;   //바꾸려는 아이템의 명칭
    private UILabel m_CurItemName;  //아이템 이름
    private UILabel m_CurItemLevel;

    private int m_iCurSelectChar;
    private ITEM_TYPE m_eSelectType;
    private INVENTORY_TYPE m_eInvenType;

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
    }

    private void OnEnable()
    {
        m_iCurSelectChar = GameManager.instance.ReturnCurSelectChar();    //바꾸려고 하는 캐릭터
        m_eSelectType = GameManager.instance.ReturnSelectType(); //바꾸고자 하는 것
        m_eInvenType = INVENTORY_TYPE.INVENTORY_START;

        m_ChangeItem.text = Util.ConvertToString(m_eSelectType);    //현재 바꾸려는 것

        if (m_eSelectType >= ITEM_TYPE.ITEM_STIGMA_TOP && m_eSelectType <= ITEM_TYPE.ITEM_STIGMA_BOTTOM)
            m_eInvenType = INVENTORY_TYPE.INVENTORY_STIGMA;
        else
            m_eInvenType = INVENTORY_TYPE.INVENTORY_WEAPON;

        List<ItemData> Data = UserInfo.instance.GetInventoryList(m_eInvenType);
        
        for(int i = 0; i < Data.Count; i++)
        {
            ITEM_TYPE eType = (ITEM_TYPE)Data[i].GetItemData(ITEM_DATA.ITEM_TYPE);
            if (eType == m_eSelectType)
            {
                //장비 장착은 현재 아이템 인벤토리에 획득 순서를 기반으로 한다.
                //스프라이트의 선택은 레이캐스트와 콜백을 사용한다
                //콜백으로 현재 선택한 아이템의 리스트 인덱스를 저장한 뒤 선택 버튼을 누르면 갱신된다.
                GameObject Item = ResourceLoader.CreatePrefab("Prefabs/ItemSelectSprite");
                Item.transform.SetParent(m_GridChar.transform, false);
            }
        }
    }
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
