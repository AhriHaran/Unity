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
        PANEL_STAGE_READY,  //스테이지 준비 패널
        PANEL_CHAR_SELECT,  //캐릭터 선택 패널
        PANEL_END,
    }
    
    List<UIPanel> m_ListPanel;
    private UIPanel m_SelectPanel;
    public GameObject m_MainChar;   //메인 캐릭터 위치
    string m_strStage;
    string m_strCharSelect;
    //위치는 고정

    // Start is called before the first frame update
    void Start()
    {
        UIPanel Lobby = GameObject.Find("LobbyPanel").GetComponent<UIPanel>();
        m_ListPanel.Add(Lobby);
        UIPanel Stage = GameObject.Find("StagePanel").GetComponent<UIPanel>();
        m_ListPanel.Add(Stage);
        UIPanel StageReady = GameObject.Find("StageReadyPanel").GetComponent<UIPanel>();
        m_ListPanel.Add(StageReady);
        UIPanel Select = GameObject.Find("CharSelectPanel").GetComponent<UIPanel>();
        //하위 오브젝트인 그리드를 받아온다.
        m_SelectPanel = Select.transform.GetChild(1).GetComponent<UIPanel>();   //해당 패널이 가지는 차일드 패널(캐릭터 선택 스크롤뷰를 위해서)
        m_ListPanel.Add(Select);
        PanelOnOff(UI_PANEL_INDEX.PANEL_LOBBY);

        GameObject Main = UserInfo.instance.GetMainChar(m_MainChar.transform);
        //메인으로 지정된 캐릭터 불러오기

        Main.GetComponent<Animator>().runtimeAnimatorController = ResourceLoader.LoadResource(UserInfo.instance.GetMainCharLobbyAnimator())as RuntimeAnimatorController;
        //로비 애니메이터로 변경
        //Main.transform.position = new Vector3();
    }

    void PanelOnOff(UI_PANEL_INDEX eindex)
    {
        for(int i = (int)UI_PANEL_INDEX.PANEL_START; i < (int)UI_PANEL_INDEX.PANEL_END; i++)
        {
            if (i == (int)eindex)   //켜고자 하는 거 외에는 모두 꺼라
                m_ListPanel[i].gameObject.SetActive(true);
            else
                m_ListPanel[i].gameObject.SetActive(false);
        }
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
            PanelOnOff(UI_PANEL_INDEX.PANEL_STAGE);
        }
        else if(Button == "HomeButton")
        {
            PanelOnOff(UI_PANEL_INDEX.PANEL_LOBBY);
        }
        else if(Button.Contains("Stage_"))  //스테이지 버튼 중 하나를 클릭
        {
            string [] split = Button.Split('_');
            m_strStage = split[split.Length - 1];   //내가 선택한 스테이지 임시 저장
            PanelOnOff(UI_PANEL_INDEX.PANEL_STAGE_READY);
            //캐릭터 선택 패널로 넘어가며 여기서 내가 가진 캐릭터로 세팅 할 수 있다.
        }
        else if(Button.Contains("SelectChar_")) //유저가 캐릭터 선택 창을 클릭하였다.
        {
            //무슨 캐릭터 선택창을 선택했는가를 확인
            string[] split = Button.Split('_');
            m_strCharSelect = split[split.Length - 1];   //내가 선택한 캐릭터 선택창 임시 저장
            PanelOnOff(UI_PANEL_INDEX.PANEL_CHAR_SELECT);
            //그리드 셋팅
        }
        else if(Button == "StageStart")
        {
            //int index = int.Parse(m_stage[m_stage.Length - 1]);
            //GameManager.instance.StageSelect(index);    //게임 매니저에게 스테이지를 셋팅 하라고 보내준다.   
            //캐릭터를 하나 이상 선택 한 후 스타트 버트
        }
        
    }
}
