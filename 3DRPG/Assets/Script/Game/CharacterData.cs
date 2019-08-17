using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharacterData
{
    List<Dictionary<CHAR_DATA, object>> m_CharInfo = new List<Dictionary<CHAR_DATA, object>>();   //위의 인덱스들을 키로 가지는 리스트값

    public CharacterData(CharInfoData Data, List<Dictionary<string, object>> Table)   //캐릭터 인덱스
    {
        int iLevel = Data.CharLevel;
        NodeSetting(CHAR_DATA.CHAR_NAME, Data.CharName);    //캐릭터 이름
        NodeSetting(CHAR_DATA.CHAR_ROUTE, Data.CharRoute);  //캐릭터 저장소
        NodeSetting(CHAR_DATA.CHAR_INDEX, Data.CharIndex);  //캐릭터 인덱스
        NodeSetting(CHAR_DATA.CHAR_LEVEL, Data.CharLevel);  //캐릭터 레벨
        NodeSetting(CHAR_DATA.CHAR_MAX_HP, Table[iLevel][CHAR_DATA.CHAR_MAX_HP.ToString()]);   //캐릭터 HP
        NodeSetting(CHAR_DATA.CHAR_MAX_SP, Table[iLevel][CHAR_DATA.CHAR_MAX_SP.ToString()]);    //캐릭터 SP
        NodeSetting(CHAR_DATA.CHAR_MAX_EXP, Table[iLevel][CHAR_DATA.CHAR_MAX_EXP.ToString()]);   //캐릭터 EXP
        NodeSetting(CHAR_DATA.CHAR_CUR_EXP, Data.CharCurEXP);   //캐릭터 현재 EXP
        NodeSetting(CHAR_DATA.CHAR_ATK, Table[iLevel][CHAR_DATA.CHAR_ATK.ToString()]);  //공격력
        NodeSetting(CHAR_DATA.CHAR_DEF, Table[iLevel][CHAR_DATA.CHAR_DEF.ToString()]);  //방어력
        NodeSetting(CHAR_DATA.CHAR_CRI, Table[iLevel][CHAR_DATA.CHAR_CRI.ToString()]);  //크리
        NodeSetting(CHAR_DATA.CHAR_WEAPON_INDEX, Data.CharWeapon);  //웨폰
        NodeSetting(CHAR_DATA.CHAR_STIGMA_TOP_INDEX, Data.CharStigmaTop);   //상 
        NodeSetting(CHAR_DATA.CHAR_STIGMA_CENTER_INDEX, Data.CharStigmaCenter); //중
        NodeSetting(CHAR_DATA.CHAR_STIGMA_BOTTOM_INDEX, Data.CharStigmaBottom); //하
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
        //캐릭터 업데이트
    }
}
