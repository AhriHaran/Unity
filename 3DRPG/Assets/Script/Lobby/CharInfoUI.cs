using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInfoUI : MonoBehaviour
{
    // Start is called before the first frame update
    private UILabel m_CharName;
    private UILabel m_CharLevel;
    private UILabel m_CharATK;
    private void Awake()
    {
        m_CharName = transform.GetChild(1).GetComponent<UILabel>();
        m_CharLevel = transform.GetChild(2).GetComponent<UILabel>();
        m_CharATK = transform.GetChild(3).GetComponent<UILabel>();
    }

    private void OnEnable()
    {
        int iIndex = GameManager.instance.ReturnCurSelectChar();
        m_CharName.text = Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, iIndex));
        //이름 셋팅
        m_CharLevel.text = "Lv." + Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_LEVEL, iIndex));
        //레벨 셋팅
        m_CharATK.text = "ATK." + Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_ATK, iIndex));
        //공격력 셋팅
    }

}
