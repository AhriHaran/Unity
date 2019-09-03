using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharWeaponUI : MonoBehaviour
{
    private UISprite m_WeaponSprite;
    private UILabel m_WeaponLevel;
    // Start is called before the first frame update
    private void Awake()
    {
        m_WeaponSprite = transform.GetChild(3).GetComponent<UISprite>();
        m_WeaponLevel = transform.GetChild(2).GetComponent<UILabel>();
    }

    public void OnFinished()
    {
        int iIndex = GameManager.instance.ReturnCurSelectChar();    //현재 선택된 캐릭터
        int iItem = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_WEAPON_INDEX, iIndex));

        if(iItem >= 0)
        {
            m_WeaponLevel.text = "LV." +
                Util.ConvertToString(UserInfo.instance.GetItemForList(iItem, INVENTORY_TYPE.INVENTORY_WEAPON, ITEM_DATA.ITEM_LEVEl));

            //해당 장비의 스프라이트 이미지
                string strName = Util.ConvertToString(UserInfo.instance.GetItemForList(iItem, INVENTORY_TYPE.INVENTORY_WEAPON, ITEM_DATA.ITEM_INDEX)) + "_" +
                    Util.ConvertToString(UserInfo.instance.GetItemForList(iItem, INVENTORY_TYPE.INVENTORY_WEAPON, ITEM_DATA.ITEM_TYPE)) + "_Icon";
            m_WeaponSprite.spriteName = strName;
        }
        else
            m_WeaponSprite.spriteName = "CrossHair";
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
