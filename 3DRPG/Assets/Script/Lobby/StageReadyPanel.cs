using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageReadyPanel : MonoBehaviour
{
    private string m_strSprite = "Select";
    private UIPanel m_UIPanel;
    private UIButton [] m_selectCharBT = new UIButton[3];
    // Start is called before the first frame update
    private void Awake()
    {
        m_UIPanel = gameObject.GetComponent<UIPanel>();
        for (int i = 0; i <3; i++)
        {
            m_selectCharBT[i] = transform.GetChild(i + 1).GetComponent<UIButton>();
        }
    }
    void Start()
    {
    }

    private void OnEnable()
    {
        int[] iarr = GameManager.instance.ReturnPlayerList();
        for (int i = 0; i < 3; i++)
        {
            if(iarr[i] != -1)
            {
                string strName = UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, GameManager.instance.GetCharIndex(i)) as string;
                m_selectCharBT[i].GetComponentInChildren<UILabel>().text = strName;
                strName += m_strSprite;
                m_selectCharBT[i].GetComponentInChildren<UISprite>().spriteName = strName;
                m_selectCharBT[i].normalSprite = strName;
            }
        }
        m_UIPanel.Refresh();
    }

    private void OnDisable()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void SelectChar(string strSelect)
    {
        int iSelect = int.Parse(strSelect);
        string strName = UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, GameManager.instance.GetCharIndex(iSelect)) as string;
        m_selectCharBT[iSelect].GetComponentInChildren<UILabel>().text = strName;
        //텍스트를 바꿔주고
        ////해당 캐릭터의 이름
        strName += m_strSprite;
        ////스프라이트 바꿈
        m_selectCharBT[iSelect].GetComponentInChildren<UISprite>().spriteName = strName;
        m_selectCharBT[iSelect].normalSprite = strName;
        //해당 스프라이트를 바꿔준다.
        m_UIPanel.Refresh();
    }
    
}
