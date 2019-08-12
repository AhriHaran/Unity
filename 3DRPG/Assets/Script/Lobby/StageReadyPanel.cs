using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageReadyPanel : MonoBehaviour
{
    private string m_strSprite = "Select";
    private UIPanel m_UIPanel;
    private UIButton [] m_selectCharBT = new UIButton[3];
    // Start is called before the first frame update
    void Start()
    {
        m_UIPanel = gameObject.GetComponent<UIPanel>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            m_selectCharBT[i] = transform.GetChild(i + 1).GetComponent<UIButton>();
        }
    }

    private void OnDisable()
    {
        //해당 버튼의 라벨과 스프라이트를 리셋

        for(int i = 0; i < 3; i++)
        {
            m_selectCharBT[i].GetComponentInChildren<UILabel>().text = "Name";  //이름
            m_selectCharBT[i].GetComponentInChildren<UISprite>().spriteName = "EmptySelect";   //바꿔 줄 스프라이트
        }
        m_UIPanel.Refresh();
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
        m_selectCharBT[iSelect].GetComponentInChildren<UISprite>().spriteName = "UnityChanSelect";
        //해당 스프라이트를 바꿔준다.
        //m_UIPanel.Refresh();
    }
    
}
