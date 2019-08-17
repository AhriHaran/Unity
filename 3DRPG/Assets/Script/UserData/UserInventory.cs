using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserInventory
{
    //무기, 성흔 상 중 하
    //무기 인벤토리, 성흔 인벤토리
    List<WeaponData> m_ListWeapon = new List<WeaponData>();
    List<StigmaData> m_ListStigma = new List<StigmaData>();
    //로비에서 데이터 셋팅
    //아이템 스프라이트는 110 x 110
    /*
     *그리드는 6 x 4
     */
    public UserInventory()
    {
        //JSON 데이터와 테이블 데이터를 통해서 셋팅
        //UserStigmaData, UserWeaponData -> Json
        ItemInfoData Weapon = JSON.JsonUtil.LoadJson<ItemInfoData>("UserWeaponData");   //웨폰 리스트
        WeaponData Data = new WeaponData(Weapon);
        m_ListWeapon.Add(Data);

        ItemInfoData[] stigma = JSON.JsonUtil.LoadArrJson<ItemInfoData>("UserStigmaData");  //스티그마 리스트
        foreach(var S in stigma)
        {
            StigmaData Item = new StigmaData(S);
            m_ListStigma.Add(Item);
        }
    }

    public void GetNewObject(int iIndex, string ItemType)
    {
        //아이템이나 스티그마 획득 시
        /*새로운 아이템을 얻을 시 해당 아이템의 인덱스를 기반으로 새로운 데이터 기반을 생성하고
         * 플레이어의 제이슨을 갱신하며
         * 인벤토리에 순서대로 넣어준다.
         * 그리고 해당 아이템을 장착할 시에는 캐릭터 인덱스는 내가 해당 아이템을 가진 인덱스 순서대로 넣어준다.
         */
    }

    public object GetStigmaData(int iIndex, STIGMA_DATA eIndex)
    {
        return m_ListStigma[iIndex].GetStigmaData(eIndex);
    }

    public object GetWeaponData(int iIndex, WEAPON_DATA eIndex)
    {
        return m_ListWeapon[iIndex].GetWeaponData(eIndex);
    }
}
