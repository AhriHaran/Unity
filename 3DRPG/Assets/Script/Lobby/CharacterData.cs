using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharacterData
{
    List<Dictionary<string, object>> m_CharInfo = new List<Dictionary<string, object>>();   //위의 인덱스들을 키로 가지는 리스트값

    public CharacterData(int iIndex, List<Dictionary<string, object>> Read)   //캐릭터 인덱스
    {
        foreach (CHAR_DATA e in Enum.GetValues(typeof(CHAR_DATA)))
        {
            Dictionary<string, object> node = new Dictionary<string, object>();
            node.Add(e.ToString(), Read[iIndex][e.ToString()]);
            m_CharInfo.Add(node);
        }
    }

    public object GetCharData(CHAR_DATA eIndex)
    {   //해당 캐릭터 정보 오브젝트 반환
        return m_CharInfo[(int)eIndex][eIndex.ToString()];
    }
}
