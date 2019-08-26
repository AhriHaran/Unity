using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum CHAR_DATA
{
    CHAR_NAME,
    CHAR_INDEX,
    CHAR_LEVEL,                 //캐릭터 레벨
    CHAR_MAX_HP,                //캐릭터 HP
    CHAR_MAX_SP,
    CHAR_MAX_EXP,
    CHAR_CUR_EXP,
    CHAR_ATK,
    CHAR_DEF,
    CHAR_CRI,
    CHAR_WEAPON_TYPE,
    CHAR_WEAPON_INDEX,          //캐릭터가 가지고 있는 무기 인덱스 값
    CHAR_STIGMA_TOP_INDEX,      //스티그마 상
    CHAR_STIGMA_CENTER_INDEX,   //스티그마 중
    CHAR_STIGMA_BOTTOM_INDEX,   //스티그마 하
}
//캐릭터의 데이터를 관리, 적 아군 둘다 사용가능

public class CharacterData
{
    private List<Dictionary<CHAR_DATA, object>> m_CharInfo = new List<Dictionary<CHAR_DATA, object>>();   //위의 인덱스들을 키로 가지는 리스트값

    public CharacterData(CharInfoData Data, List<Dictionary<string, object>> Table)   //캐릭터 인덱스
    {
        int iLevel = Data.CharLevel - 1;
        NodeSetting(CHAR_DATA.CHAR_NAME, Table[iLevel][CHAR_DATA.CHAR_NAME.ToString()]);    //캐릭터 이름
        NodeSetting(CHAR_DATA.CHAR_INDEX, Data.CharIndex);  //캐릭터 인덱스
        NodeSetting(CHAR_DATA.CHAR_LEVEL, Data.CharLevel);  //캐릭터 레벨
        NodeSetting(CHAR_DATA.CHAR_MAX_HP, Table[iLevel][CHAR_DATA.CHAR_MAX_HP.ToString()]);   //캐릭터 HP
        NodeSetting(CHAR_DATA.CHAR_MAX_SP, Table[iLevel][CHAR_DATA.CHAR_MAX_SP.ToString()]);    //캐릭터 SP
        NodeSetting(CHAR_DATA.CHAR_MAX_EXP, Table[iLevel][CHAR_DATA.CHAR_MAX_EXP.ToString()]);   //캐릭터 EXP
        NodeSetting(CHAR_DATA.CHAR_CUR_EXP, Data.CharCurEXP);   //캐릭터 현재 EXP
        NodeSetting(CHAR_DATA.CHAR_ATK, Table[iLevel][CHAR_DATA.CHAR_ATK.ToString()]);  //공격력
        NodeSetting(CHAR_DATA.CHAR_DEF, Table[iLevel][CHAR_DATA.CHAR_DEF.ToString()]);  //방어력
        NodeSetting(CHAR_DATA.CHAR_CRI, Table[iLevel][CHAR_DATA.CHAR_CRI.ToString()]);  //크리
        NodeSetting(CHAR_DATA.CHAR_WEAPON_TYPE, Data.CharWeaponType);
        NodeSetting(CHAR_DATA.CHAR_WEAPON_INDEX, Data.CharWeapon);  //웨폰
        NodeSetting(CHAR_DATA.CHAR_STIGMA_TOP_INDEX, Data.CharStigmaTop);   //상 
        NodeSetting(CHAR_DATA.CHAR_STIGMA_CENTER_INDEX, Data.CharStigmaCenter); //중
        NodeSetting(CHAR_DATA.CHAR_STIGMA_BOTTOM_INDEX, Data.CharStigmaBottom); //하
    }

    public CharacterData(List<Dictionary<string, object>> Table)
    {
        //에너미 등 테이블 기반의 데이터

    }

    void NodeSetting(CHAR_DATA eIndex, object Data)
    {
        Dictionary<CHAR_DATA, object>  node = new Dictionary<CHAR_DATA, object>();
        node.Add(eIndex, Data);
        m_CharInfo.Add(node);
    }

    public object GetCharData(CHAR_DATA eIndex)
    {   //해당 캐릭터 정보 오브젝트 반환
        return m_CharInfo[(int)eIndex][eIndex];
    }

    public void CharUpdate(CHAR_DATA eIndex, object UpdateData)
    {
        //영구적인 데이터 업데이트
        //결과창에서 EXP 데이터를 업데이트 하기 위해서
        m_CharInfo[(int)eIndex][eIndex] = UpdateData;
        //임시로 저장 해준 뒤, 밑에서 처리한다.
    }
    public void CharUpdate()
    {
        int iData = Util.ConvertToInt(GetCharData(CHAR_DATA.CHAR_WEAPON_INDEX));
        if (iData >= 0)
            UserInfo.instance.ItemUpdateForChar(INVENTORY_TYPE.INVENTORY_WEAPON, iData, true);

        iData = Util.ConvertToInt(GetCharData(CHAR_DATA.CHAR_STIGMA_TOP_INDEX));
        if (iData >= 0)
            UserInfo.instance.ItemUpdateForChar(INVENTORY_TYPE.INVENTORY_STIGMA, iData, true);

        iData = Util.ConvertToInt(GetCharData(CHAR_DATA.CHAR_STIGMA_CENTER_INDEX));
        if (iData >= 0)
            UserInfo.instance.ItemUpdateForChar(INVENTORY_TYPE.INVENTORY_STIGMA, iData, true);

        iData = Util.ConvertToInt(GetCharData(CHAR_DATA.CHAR_STIGMA_BOTTOM_INDEX));
        if (iData >= 0)
            UserInfo.instance.ItemUpdateForChar(INVENTORY_TYPE.INVENTORY_STIGMA, iData, true);
        //데이터 로딩 시 해당 캐릭터가 무기 등의 정보를 가진다면
    }

    public bool ifCharLevelUP(List<Dictionary<string, object>> CharTable)
    {
        int iCurEXP = Util.ConvertToInt(GetCharData(CHAR_DATA.CHAR_CUR_EXP));
        int iLevel = Util.ConvertToInt(GetCharData(CHAR_DATA.CHAR_LEVEL));
        int iTableLevel = 0;
        bool bLevelUp = false;

        while (true)
        {
            iTableLevel = iLevel - 1;
            int iMaxEXP = int.Parse(CharTable[iTableLevel][CHAR_DATA.CHAR_MAX_EXP.ToString()].ToString()); 
            //현재 레벨당 최대 경험치 대비
            if (iCurEXP >= iMaxEXP)
            {
                iLevel++;
                iCurEXP -= iMaxEXP;
                bLevelUp = true;
            }
            else
                break;
        }

        iTableLevel = iLevel - 1;
        CharUpdate(CHAR_DATA.CHAR_LEVEL, iLevel);//현재 레벨
        CharUpdate(CHAR_DATA.CHAR_MAX_HP, CharTable[iTableLevel][CHAR_DATA.CHAR_MAX_HP.ToString()]); //현재 레벨 HP
        CharUpdate(CHAR_DATA.CHAR_MAX_SP, CharTable[iTableLevel][CHAR_DATA.CHAR_MAX_HP.ToString()]); //현재 레벨 SP
        CharUpdate(CHAR_DATA.CHAR_CUR_EXP, iCurEXP); //현재 경험치
        CharUpdate(CHAR_DATA.CHAR_MAX_EXP, CharTable[iTableLevel][CHAR_DATA.CHAR_MAX_EXP.ToString()]); //현재 레벨 max EXP
        CharUpdate(CHAR_DATA.CHAR_ATK, CharTable[iTableLevel][CHAR_DATA.CHAR_ATK.ToString()]); //현재 레벨ATK
        CharUpdate(CHAR_DATA.CHAR_DEF, CharTable[iTableLevel][CHAR_DATA.CHAR_DEF.ToString()]); //현재 레벨 DEF
        CharUpdate(CHAR_DATA.CHAR_CRI, CharTable[iTableLevel][CHAR_DATA.CHAR_CRI.ToString()]); //현재 레벨 CRI
        //업데이트 후, 해당 캐릭터가 장비가 있다면 장비 스테이터스도 업데이트
        
        return bLevelUp;
    }
}
