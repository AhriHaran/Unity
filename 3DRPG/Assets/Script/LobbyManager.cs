using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public enum UI_PANEL_INDEX
    {
        PANEL_START,
        PANEL_LOBBY = PANEL_START,    //로비 패널
        PANEL_STAGE,    //스테이지 패널
        PANEL_ATTACK,   //어택 패널
        PANEL_END,
        PANEL_CUR,      //이전 로비
    }

    private UIPanel m_LobbyPanel;
    private UIPanel m_StagePanel;

    //private UI_PANEL_INDEX m_eLastUI = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_LobbyPanel = GameObject.Find("LobbyPanel").GetComponent<UIPanel>();
        m_StagePanel = GameObject.Find("StagePanel").GetComponent<UIPanel>();
        m_LobbyPanel.gameObject.SetActive(true);
        m_StagePanel.gameObject.SetActive(false);
        //m_eLastUI = UI_PANEL_INDEX.PANEL_LOBBY;   //현재 패널
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        string Button = UIButton.current.name;
        if (Button == "StageButton")
        {
            m_LobbyPanel.gameObject.SetActive(false);
            m_StagePanel.gameObject.SetActive(true);
        }
        else if(Button == "HomeButton")
        {
            m_LobbyPanel.gameObject.SetActive(true);
            m_StagePanel.gameObject.SetActive(false);

        }
        else
        {
            string [] Stage = Button.Split('_');

            int index = int.Parse(Stage[Stage.Length - 1]);
            GameManager.instance.StageSelect(index);    //게임 매니저에게 스테이지를 셋팅 하라고 보내준다.
        }
    }
}
