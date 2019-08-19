using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum CHAR_DATA
{
    CHAR_NAME,
    CHAR_ROUTE,                 //캐릭터 리소스 경로
    CHAR_INDEX,
    CHAR_LEVEL,                 //캐릭터 레벨
    CHAR_MAX_HP,                //캐릭터 HP
    CHAR_MAX_SP,
    CHAR_MAX_EXP,
    CHAR_CUR_EXP,
    CHAR_ATK,
    CHAR_DEF,
    CHAR_CRI,
    CHAR_WEAPON_INDEX,          //캐릭터가 가지고 있는 무기 인덱스 값
    CHAR_STIGMA_TOP_INDEX,      //스티그마 상
    CHAR_STIGMA_CENTER_INDEX,   //스티그마 중
    CHAR_STIGMA_BOTTOM_INDEX,   //스티그마 하
}
//캐릭터의 데이터를 관리, 적 아군 둘다 사용가능

public class CharacterData
{
    private List<Dictionary<CHAR_DATA, object>> m_CharInfo = new List<Dictionary<CHAR_DATA, object>>();   //위의 인덱스들을 키로 가지는 리스트값
    public bool m_bLevelUP = false;

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
        //영구적인 데이터 업데이트
        //결과창에서 EXP 데이터를 업데이트 하기 위해서
        m_CharInfo[(int)eIndex][eIndex] = UpdateData;   //업데이트 데이터
        if(eIndex == CHAR_DATA.CHAR_CUR_EXP)    //만약 현재 데이터가 EXP 라면?
        {
            int iCurEXP = Convert.ToInt32(m_CharInfo[(int)CHAR_DATA.CHAR_CUR_EXP][CHAR_DATA.CHAR_CUR_EXP]);
            int iMaxEXP = Convert.ToInt32(m_CharInfo[(int)CHAR_DATA.CHAR_MAX_EXP][CHAR_DATA.CHAR_MAX_EXP]);
            int iLevel = Convert.ToInt32(m_CharInfo[(int)CHAR_DATA.CHAR_LEVEL][CHAR_DATA.CHAR_LEVEL]);
            if (iCurEXP >= iMaxEXP) //현재 EXP가 레벨 당 EXP를 넘어섰다->레벨업
            {
                iLevel++;
                iCurEXP -= iMaxEXP;
                m_bLevelUP = true;
            }
        }
        //이후 캐릭터 세이브 시 해당 캐릭터가 레벌 업 상태라면 해당 테이블 기반의 데이터를 다시 셋팅
    }
    public void CharUpdate(List<Dictionary<string, object>> Table)
    {
        int iLevel = Convert.ToInt32(m_CharInfo[(int)CHAR_DATA.CHAR_LEVEL][CHAR_DATA.CHAR_LEVEL]);
        //데이터 셋팅
        CharUpdate(CHAR_DATA.CHAR_MAX_HP, Table[iLevel][CHAR_DATA.CHAR_MAX_HP.ToString()]);
        CharUpdate(CHAR_DATA.CHAR_MAX_SP, Table[iLevel][CHAR_DATA.CHAR_MAX_SP.ToString()]);
        CharUpdate(CHAR_DATA.CHAR_MAX_EXP, Table[iLevel][CHAR_DATA.CHAR_MAX_EXP.ToString()]);
        CharUpdate(CHAR_DATA.CHAR_ATK, Table[iLevel][CHAR_DATA.CHAR_ATK.ToString()]);
        CharUpdate(CHAR_DATA.CHAR_DEF, Table[iLevel][CHAR_DATA.CHAR_DEF.ToString()]);
        CharUpdate(CHAR_DATA.CHAR_CRI, Table[iLevel][CHAR_DATA.CHAR_CRI.ToString()]);
        //레벨업 기반 데이터 셋팅
        m_bLevelUP = false;
    }

    public void CharSave(List<Dictionary<string, object>> Table)
    {

    }
}
