using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectPanel : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject m_ItemSelect;
    private GameObject m_ItemInfo;
    private void Awake()
    {
        m_ItemSelect = transform.GetChild(0).gameObject;
        m_ItemInfo = transform.GetChild(1).gameObject;
    }

    private void OnEnable()
    {
        int iCurSelect = GameManager.instance.ReturnCurSelectChar();
        GameManager.instance.DestroyModel();
        GameManager.instance.CreateModel(iCurSelect);
        GameManager.instance.ModelRotate(new Vector3(0, -30, 0));
        GameManager.instance.ViewWeapon(true, false);
        Vector3 vec = Camera.main.transform.position;
        vec.x = 1;
        Camera.main.transform.position = vec;

        m_ItemSelect.GetComponent<TweenControl>().TweenStart(TWEEN_SET.TWEEN_START);
        m_ItemInfo.GetComponent<TweenControl>().TweenStart(TWEEN_SET.TWEEN_START);
    }

    private void OnDisable()
    {
        m_ItemSelect.GetComponent<TweenControl>().TweenStart(TWEEN_SET.TWEEN_REVERSE);
        m_ItemInfo.GetComponent<TweenControl>().TweenStart(TWEEN_SET.TWEEN_REVERSE);
    }


    public void OnClick()
    {
        //버튼 클릭은 여기서 처리
        int iCurItem = GameManager.instance.ReturnCurSelectItem();      //장착하려는 장비의 인벤토리 인덱스

        if (iCurItem >= 0)  //선택한 것이 있으며
        {
            int iSelectChar = GameManager.instance.ReturnCurSelectChar();   //장착할 캐릭터 리턴
            INVENTORY_TYPE eInven = GameManager.instance.ReturnInvenType();
            ITEM_TYPE eType = GameManager.instance.ReturnSelectType();
            CHAR_DATA eData;

            if (eType == ITEM_TYPE.ITEM_STIGMA_TOP)
                eData = CHAR_DATA.CHAR_STIGMA_TOP_INDEX;
            else if (eType == ITEM_TYPE.ITEM_STIGMA_CENTER)
                eData = CHAR_DATA.CHAR_STIGMA_CENTER_INDEX;
            else if (eType == ITEM_TYPE.ITEM_STIGMA_BOTTOM)
                eData = CHAR_DATA.CHAR_STIGMA_BOTTOM_INDEX;
            else
                eData = CHAR_DATA.CHAR_WEAPON_INDEX;
            //아이템 타입에 따른 데이터 갱신

            int iList = Util.ConvertToInt(UserInfo.instance.GetItemIndexForList(iCurItem, eInven));
            int iWeaponEquipChar = Util.ConvertToInt(UserInfo.instance.GetItemForList(iList, eInven, ITEM_DATA.ITEM_EQUIP_CHAR));  
            //해당 아이템을 장착한 캐릭터가 있는가?
            int iSelectWeapon = Util.ConvertToInt(UserInfo.instance.GetCharData(eData, iSelectChar));
            //바꾸려는 캐릭터가 무언가를 장착한 것이 있는가?

            bool bRelese = GameManager.instance.ReturnCurItemEqipType();    //장비 장착인가 해제인가
            if (bRelese && iSelectWeapon == iList) //장착 해제
            {
                m_ItemInfo.GetComponent<ItemInfoUI>().ItemInfo((int)ITEM_INFO_UI.ITEM_INFO_CUR, -1);
                m_ItemInfo.GetComponent<ItemInfoUI>().ItemInfo((int)ITEM_INFO_UI.ITEM_INFO_SELECT, -1);

                UserInfo.instance.CharUpdate(eData, -1, iSelectChar); 
                //해당 아이템을 해제할 캐릭터
                UserInfo.instance.InventoryUpdate(eInven, iCurItem, ITEM_DATA.ITEM_EQUIP_CHAR, -1);   
                //아이템 또한 해당 캐릭터가 해제했다는 것을 알린다
                UserInfo.instance.ItemUpdateForChar(eInven, iCurItem, false);    
                //해당 아이템 기준으로 능력치 업데이트

                UserInfo.instance.UserCharSave();
                UserInfo.instance.UserInvenSave();
                //세이브 데이터
            }
            else if(!bRelese && iSelectWeapon != iList)  //장착 한다.
            {
                m_ItemInfo.GetComponent<ItemInfoUI>().ItemInfo((int)ITEM_INFO_UI.ITEM_INFO_CUR, iCurItem);
                m_ItemInfo.GetComponent<ItemInfoUI>().ItemInfo((int)ITEM_INFO_UI.ITEM_INFO_SELECT, -1);

                if (iWeaponEquipChar >= 0)  //해당 아이템을 장착한 캐릭터가 있는가?, 아이템 교체
                {
                    UserInfo.instance.CharUpdate(eData, iSelectWeapon, iWeaponEquipChar);
                    //해당 아이템을 장착한 캐릭터는 현재 캐릭터의 장비를 이어 받고

                    if (iSelectWeapon >= 0)   //해당 캐릭터가 무언가를 장착한 상태라면
                    {
                        UserInfo.instance.InventoryUpdate(eInven, iSelectWeapon, ITEM_DATA.ITEM_EQUIP_CHAR, iWeaponEquipChar);
                        //현재 캐릭터의 장비 또한, 해당 아이템을 장착한 캐릭터로 갱신된다.
                        UserInfo.instance.ItemUpdateForChar(eInven, iSelectWeapon, true);
                        //그에따라서 바뀐 장비 만큼의 능력치가 업데이트 된다.
                    }
                    else
                    {
                        UserInfo.instance.ItemUpdateForChar(eInven, iCurItem, false);
                        //해당 캐릭터가 장착한 아이템이 없을 시에는 아이템이 빼진 것이기에 아이템의 능력치만큼 뺴준다.
                    }
                }

                UserInfo.instance.CharUpdate(eData, iCurItem, iSelectChar);  //해당 아이템을 장착할 캐릭터
                UserInfo.instance.InventoryUpdate(eInven, iCurItem, ITEM_DATA.ITEM_EQUIP_CHAR, iSelectChar);   //아이템 또한 해당 캐릭터가 장착했다는 것을 알린다
                UserInfo.instance.ItemUpdateForChar(eInven, iCurItem, true);    //해당 아이템 기준으로 능력치 업데이트

                UserInfo.instance.UserCharSave();
                UserInfo.instance.UserInvenSave();
            }
            GameManager.instance.SelectCurItemEqipType(false);
            m_ItemSelect.GetComponent<ItemSelectUI>().ResetList();
            //아이템 중복 장착을 피해주고, 현재 누군가 장착 중인 아이템이라면 해당 캐릭터에게 빼주고 새로 장착 시켜준다.
        }
        //데이터 갱신
    }
}
