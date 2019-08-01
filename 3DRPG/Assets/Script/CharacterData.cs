﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharacterData
{
    public enum CHAR_ENUM
    {
        CHAR_NAME,
        CHAR_ROUTE, //캐릭터 리소스 경로
        CHAR_LEVEL,
        CHAR_MAX_HP,
        CHAR_CUR_HP,
        CHAR_MAX_SP,
        CHAR_CUR_SP,
        CHAR_MAX_EXP,
        CHAR_CUR_EXP,
        CHAR_ATK,
        CHAR_DEF,
        CHAR_CRI,
        CHAR_WEAPON_INDEX,  //캐릭터가 가지고 있는 무기 인덱스 값
        CHAR_STIGMA_TOP_INDEX,  //스티그마 상
        CHAR_STIGMA_CENTER_INDEX,   //스티그마 중
        CHAR_STIGMA_BOTTOM_INDEX,   //스티그마 하
    }

    List<Dictionary<string, object>> m_CharInfo = new List<Dictionary<string, object>>();   //위의 인덱스들을 키로 가지는 리스트값

    public CharacterData()
    {
    }

    public CharacterData(int iIndex, List<Dictionary<string, object>> Read)   //캐릭터 인덱스
    {
        foreach (CHAR_ENUM e in Enum.GetValues(typeof(CHAR_ENUM)))
        {
            Dictionary<string, object> node = new Dictionary<string, object>();
            node.Add(e.ToString(), Read[iIndex][e.ToString()]);
            m_CharInfo.Add(node);
        }
    }
    //

    public string GetRoute()
    {
        return m_CharInfo[(int)CHAR_ENUM.CHAR_ROUTE][CHAR_ENUM.CHAR_ROUTE.ToString()].ToString(); //프리펩 경로
    }

    public string GetName()
    {
        return m_CharInfo[(int)CHAR_ENUM.CHAR_NAME][CHAR_ENUM.CHAR_NAME.ToString()].ToString(); //프리펩 경로
    }


}