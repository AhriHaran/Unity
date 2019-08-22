using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CHAR_INFO_UI
{
    CHAR_INFO_UI_START,
    CHAR_INFO_UI_INFO = CHAR_INFO_UI_START,
    CHAR_INFO_UI_LEVEL,
    CHAR_INFO_UI_WEAPON,
    CHAR_INFO_UI_STIGMA,
    CHAR_INFO_UI_END
}

/// <summary>
///아이템 셀렉트 시에는 셀렉트 오브젝트는 오른쪽에서, info는 왼쪽에서
///버튼은 밑으로 내려간다.
/// </summary>


public class CharInfoPanel : MonoBehaviour
{
    private GameObject[] m_UIList = new GameObject[(int)CHAR_INFO_UI.CHAR_INFO_UI_END];

    private void Awake()
    {
        for (int i = 0; i < (int)CHAR_INFO_UI.CHAR_INFO_UI_END; i++)
        {
            m_UIList[i] = transform.GetChild(i + 1).gameObject;
        }
    }

    private void OnEnable()
    {
    }

    public void UiOnOff(CHAR_INFO_UI eIndex)
    {
        int iIndex = (int)eIndex;

        for(int i = (int)CHAR_INFO_UI.CHAR_INFO_UI_LEVEL; i < (int)CHAR_INFO_UI.CHAR_INFO_UI_END; i++)
        {
            if(i == iIndex)
            {
                m_UIList[i].GetComponent<TweenControl>().TweenStart(TWEEN_SET.TWEEN_START);
            }
            else
            {
                m_UIList[i].GetComponent<TweenControl>().TweenStart(TWEEN_SET.TWEEN_REVERSE);
            }
        }
    }

    public void OnClick()
    {
        string Button = UIButton.current.name;

        if (Button == "Level")
        {
            UiOnOff(CHAR_INFO_UI.CHAR_INFO_UI_LEVEL);
        }
        else if(Button == "Weapon")
        {
            UiOnOff(CHAR_INFO_UI.CHAR_INFO_UI_WEAPON);
        }
        else if (Button == "Stigma")
        {
            UiOnOff(CHAR_INFO_UI.CHAR_INFO_UI_STIGMA);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
