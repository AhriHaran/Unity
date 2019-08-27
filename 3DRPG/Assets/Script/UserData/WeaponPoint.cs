using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPoint : MonoBehaviour
{
    public List<GameObject> m_WeaponPoint = new List<GameObject>();
    //
    // Start is called before the first frame update
    void Start()
    {
        //오른손은 0 왼손은 1
    }

    public void WeaponSet(int iIndex, ITEM_TYPE eType)
    {
        for(int i = 0; i < m_WeaponPoint.Count; i++)
        {
            string strRoute = string.Empty;
            if (i == 0)
                strRoute = "Equipment/" + eType.ToString() + "/" + Util.ConvertToString(iIndex) + "/R";
            else if(i == 1)
                strRoute = "Equipment/" + eType.ToString() + "/" + Util.ConvertToString(iIndex) + "/L";
            GameObject Weapon;
            if (m_WeaponPoint[i].transform.childCount > 0)
            {
                for (int j = 0; j < m_WeaponPoint[i].transform.childCount; j++)
                {
                    Weapon = m_WeaponPoint[i].transform.GetChild(j).gameObject;
                    Weapon.transform.SetParent(null);
                    Destroy(Weapon);
                }
                //장비 교체등으로 원래 있던 모델을 파괴
            }
            Weapon = ResourceLoader.CreatePrefab(strRoute);
            Weapon.transform.SetParent(m_WeaponPoint[i].transform, false);
        }
    }

    public void ViewWeapon()
    {
        for (int i = 0; i < m_WeaponPoint.Count; i++)
        {
            m_WeaponPoint[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
