using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharWeaponUI : MonoBehaviour
{
    private UISprite m_WeaponSprite;
    private UILabel m_WeaponName;
    // Start is called before the first frame update
    private void Awake()
    {
        m_WeaponSprite = transform.GetChild(3).GetComponent<UISprite>();
        m_WeaponName = transform.GetChild(2).GetComponent<UILabel>();
    }

    public void OnFinished()
    {
        int iIndex = GameManager.instance.ReturnCurSelectChar();    //현재 선택된 캐릭터
        m_WeaponName.text = 
            Util.ConvertToString(UserInfo.instance.GetItemForList(iIndex, INVENTORY_TYPE.INVENTORY_WEAPON, ITEM_DATA.ITEM_NAME));
        //해당 장비의 스프라이트 이미지
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
