using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharLevelUI : MonoBehaviour
{
    private UILabel m_CharHP;
    private UILabel m_CharSP;
    private UILabel m_CharATK;
    private UILabel m_CharDEF;
    private UILabel m_CharCRI;

    // Start is called before the first frame update
    private void Awake()
    {
        m_CharHP = transform.GetChild(2).transform.GetChild(1).GetComponent<UILabel>();
        m_CharSP = transform.GetChild(3).transform.GetChild(1).GetComponent<UILabel>();
        m_CharATK = transform.GetChild(4).transform.GetChild(1).GetComponent<UILabel>();
        m_CharDEF = transform.GetChild(5).transform.GetChild(1).GetComponent<UILabel>();
        m_CharCRI = transform.GetChild(6).transform.GetChild(1).GetComponent<UILabel>();
    }

    public void OnFinished()
    {
        int iIndex = GameManager.instance.ReturnCurSelectChar();

        m_CharHP.text = Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_MAX_HP, iIndex));
        m_CharSP.text = Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_MAX_SP, iIndex));
        m_CharATK.text = Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_ATK, iIndex));
        m_CharDEF.text = Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_DEF, iIndex));
        m_CharCRI.text = Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_CRI, iIndex));
    }

}
