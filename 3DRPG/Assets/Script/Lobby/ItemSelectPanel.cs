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
        Vector3 vec = Camera.main.transform.position;
        vec.x = 0;
        Camera.main.transform.position = vec;

        m_ItemSelect.GetComponent<TweenControl>().TweenStart(TWEEN_SET.TWEEN_REVERSE);
        m_ItemInfo.GetComponent<TweenControl>().TweenStart(TWEEN_SET.TWEEN_REVERSE);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        //버튼 클릭은 여기서 처리
        int iCurItem = GameManager.instance.ReturnCurSelectItem();
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

            int iWeaponEquip = Util.ConvertToInt(UserInfo.instance.GetItemForList(iCurItem, eInven, ITEM_DATA.ITEM_EQUIP_CHAR));  
            //해당 아이템을 장착한 캐릭터가 있는가?
            int iSelectWeapon = Util.ConvertToInt(UserInfo.instance.GetCharData(eData, iSelectChar));
            //무언가 장착한 것이 있는가?

            bool bSet = false;

            if (iWeaponEquip != iSelectWeapon) //중복 장착이 아니지만
                bSet = true;
            else if(iWeaponEquip < 0 && iSelectWeapon < 0) //만약 둘 다 처음 장착이라는 특수한 상황이라면
                bSet = true;

            if (bSet)
            {
                //장착했다는 의미이므로 캐릭터의 데이터를 갱신해준다.
                m_ItemInfo.GetComponent<ItemInfoUI>().ItemInfo((int)ITEM_INFO_UI.ITEM_INFO_CUR, iCurItem);
                m_ItemInfo.GetComponent<ItemInfoUI>().ItemInfo((int)ITEM_INFO_UI.ITEM_INFO_SELECT, -1);
                //장착 했으니 정보를 갱신해주고

                //해당 아이템을 장착한 캐릭터가 있다. 그렇다면 두명의 데이터를 교환 해준다.
                if(iWeaponEquip >= 0) 
                {
                    UserInfo.instance.CharUpdate(eData, iSelectWeapon, iWeaponEquip);
                    //해당 아이템을 장착한 캐릭터는 현재 캐릭터의 장비를 이어 받고
                    if(iSelectWeapon >= 0)   //해당 캐릭터가 무언가를 장착한 상태라면
                    {
                        UserInfo.instance.InventoryUpdate(eInven, iSelectWeapon, ITEM_DATA.ITEM_EQUIP_CHAR, iWeaponEquip);
                        //현재 캐릭터의 장비 또한, 해당 아이템을 장착한 캐릭터로 갱신된다.
                        UserInfo.instance.ItemUpdateForChar(eInven, iSelectWeapon, true);
                        //그에따라서 바뀐 장비 만큼의 능력치가 업데이트 된다.
                    }
                    else
                    {
                        //해당 캐릭터가 장착한 아이템이 없을 시에는 아이템이 빼진 것이기에 아이템의 능력치만큼 뺴준다.
                        UserInfo.instance.ItemUpdateForChar(eInven, iCurItem, false);
                    }
                }
                UserInfo.instance.CharUpdate(eData, iCurItem, iSelectChar);  //해당 아이템을 장착할 캐릭터
                UserInfo.instance.InventoryUpdate(eInven, iCurItem, ITEM_DATA.ITEM_EQUIP_CHAR, iSelectChar);   //아이템 또한 해당 캐릭터가 장착했다는 것을 알린다
                UserInfo.instance.ItemUpdateForChar(eInven, iCurItem, true);    //해당 아이템 기준으로 능력치 업데이트
                UserInfo.instance.UserCharSave();
                UserInfo.instance.UserInvenSave();
                //세이브 데이터
            }
            //아이템 중복 장착을 피해주고, 현재 누군가 장착 중인 아이템이라면 해당 캐릭터에게 빼주고 새로 장착 시켜준다.
        }
        //데이터 갱신
    }
}
