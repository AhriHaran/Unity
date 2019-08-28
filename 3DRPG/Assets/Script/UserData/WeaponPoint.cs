using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPoint : MonoBehaviour
{
    public List<GameObject> m_WeaponPoint = new List<GameObject>();
    private int m_iWeaponIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        //오른손은 0 왼손은 1
    }

    public void WeaponSet(int iIndex, ITEM_TYPE eType)
    {
        if(m_iWeaponIndex != iIndex && iIndex >= 0)
        {
            for (int i = 0; i < m_WeaponPoint.Count; i++)
            {
                string strRoute = string.Empty;
                if (i == 0)
                    strRoute = "Equipment/" + eType.ToString() + "/" + Util.ConvertToString(iIndex) + "/R";
                else if (i == 1)
                    strRoute = "Equipment/" + eType.ToString() + "/" + Util.ConvertToString(iIndex) + "/L";
                GameObject Weapon;

                if (m_WeaponPoint[i].transform.childCount > 1)
                {
                    Weapon = m_WeaponPoint[i].transform.GetChild(1).gameObject;
                    Weapon.transform.SetParent(null);
                    Destroy(Weapon);
                    //장비 교체등으로 원래 있던 모델을 파괴
                }

                Weapon = ResourceLoader.CreatePrefab(strRoute);
                Weapon.transform.SetParent(m_WeaponPoint[i].transform, false);
                Weapon.SetActive(false);
            }
            m_iWeaponIndex = iIndex;
        }
        //웨폰 인덱스는 무조건 인벤토리 리스트 순서 이므로 크게 상관 하지 말것
    }

    public void ViewWeapon(bool bView, bool bEffect)
    {
        for (int i = 0; i < m_WeaponPoint.Count; i++)
        {
            for(int j = 0; j < m_WeaponPoint[i].transform.childCount; j++)
            {
                if(j == 0)
                    m_WeaponPoint[i].transform.GetChild(j).gameObject.SetActive(bEffect);
                else if(j == 1)
                    m_WeaponPoint[i].transform.GetChild(j).gameObject.SetActive(bView);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
