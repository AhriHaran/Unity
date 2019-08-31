using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPoint : MonoBehaviour
{
    public List<GameObject> m_WeaponPoint = new List<GameObject>();
    public int m_iCharIndex;
    private ITEM_TYPE m_eType;
    private int m_iWeaponIndex = -1;    //캐릭터가 차고 있는 무기의 리스트 인덱스
    // Start is called before the first frame update

    //다시 한 번 봐야하는 부분
    private void OnEnable()
    {
        //처음 시작할 때 셋팅
        m_iWeaponIndex = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_WEAPON_INDEX, m_iCharIndex));
        //캐릭터의 웨폰 인덱스(리스트 순서)
        m_eType = (ITEM_TYPE)UserInfo.instance.GetCharData(CHAR_DATA.CHAR_WEAPON_TYPE, m_iCharIndex);
        //캐릭터 웨폰 타입
        int iIndex = Util.ConvertToInt(UserInfo.instance.GetItemForList(m_iWeaponIndex, INVENTORY_TYPE.INVENTORY_WEAPON, ITEM_DATA.ITEM_INDEX));
        //웨폰의 실제 인덱스
        GameObject Weapon;
        for (int i = 0; i < m_WeaponPoint.Count; i++)
        {
            string strRoute = string.Empty;
            string WeaponIndex = Util.ConvertToString(iIndex);
            
            if (i == 0)
                strRoute = "Equipment/" + m_eType.ToString() + "/" + WeaponIndex + "/R";    //0번쨰는 R
            else if (i == 1)
                strRoute = "Equipment/" + m_eType.ToString() + "/" + WeaponIndex + "/L";    //1번째는 L

            Weapon = ResourceLoader.CreatePrefab(strRoute);
            Weapon.transform.SetParent(m_WeaponPoint[i].transform, false);
            Weapon.SetActive(true);
            Weapon.transform.Find("Effect").gameObject.SetActive(false);
            //
        }
    }

    private void OnDisable()
    {
    }

    public void WeaponChange(int iIndex)
    {
        GameObject Weapon;
        if(m_iWeaponIndex != iIndex && iIndex >= 0) //현재 장비랑 중복 장착이 아니다.
        {
            for (int i = 0; i < m_WeaponPoint.Count; i++)
            {
                Weapon = m_WeaponPoint[i].transform.GetChild(0).gameObject ?? null;
                if (Weapon != null)
                {
                    Weapon.transform.SetParent(null);
                    Destroy(Weapon);
                }

                string strRoute = string.Empty;
                string WeaponIndex = Util.ConvertToString(UserInfo.instance.GetItemForList(iIndex, INVENTORY_TYPE.INVENTORY_WEAPON, ITEM_DATA.ITEM_INDEX));
                if (i == 0)
                    strRoute = "Equipment/" + m_eType.ToString() + "/" + WeaponIndex + "/R";
                else if (i == 1)
                    strRoute = "Equipment/" + m_eType.ToString() + "/" + WeaponIndex + "/L";
                Weapon = ResourceLoader.CreatePrefab(strRoute);
                Weapon.transform.SetParent(m_WeaponPoint[i].transform, false);
                Weapon.SetActive(false);
                Weapon.transform.Find("Effect").gameObject.SetActive(false);
            }
            //현재 장착 중인 장비를 부순다.
            m_iWeaponIndex = iIndex;
        }
        //웨폰 인덱스는 무조건 인벤토리 리스트 순서 이므로 크게 상관 하지 말것
    }

    public void ViewEffect(bool bEffect)
    {
        for (int i = 0; i < m_WeaponPoint.Count; i++)
        {
            if(m_WeaponPoint[i].transform.childCount > 0)
            {
                GameObject Weapon = m_WeaponPoint[i].transform.GetChild(0).gameObject ?? null;
                if (Weapon != null)
                {
                    Weapon.transform.Find("Effect").gameObject.SetActive(bEffect);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
