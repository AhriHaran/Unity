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
        PANEL_VALKYRJA,     //발키리 패널
        PANEL_EQUIPMENT,    //장비 패널
        PANEL_BUTTON,
        PANEL_END,
    }

    public UILabel m_UserLevel;
    public UILabel m_UserNickName;
    public UISlider m_UserExpBar;
    public UILabel m_UserGold;
    public UILabel m_UserEnergy;
    public Transform m_MainCharTR;

    private List<UIPanel> m_ListPanel = new List<UIPanel>();
    private GameObject m_MainChar;   //메인 캐릭터 위치
    private string m_strCharSelect;
    private UI_PANEL_INDEX m_eCurPanel;
    //위치는 고정

    private void Start()
    {
        UserInfo.instance.InvenSetting();

        GameObject LobbyUI = GameObject.Find("LobbyUI");
        for (int i = (int)UI_PANEL_INDEX.PANEL_START; i < (int)UI_PANEL_INDEX.PANEL_END; i++)
        {
            UIPanel Node = LobbyUI.transform.GetChild(i + 1).GetComponent<UIPanel>();
            Node.gameObject.SetActive(false);
            m_ListPanel.Add(Node);
        }

        int iCount = UserInfo.instance.GetMyCharCount();
        string[] strIndex = new string[iCount];
        for (int i = 0; i < iCount; i++)
        {
            try
            {
                string route = UserInfo.instance.GetCharData(CHAR_DATA.CHAR_ROUTE, i).ToString();
                string name = UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, i).ToString();
                strIndex[i] = "Player/" +route + "Prefabs/" + name;
            }
            catch (System.NullReferenceException ex)
            {
                Debug.Log(ex);
            }
        }
        PoolManager.instance.Set(POOL_INDEX.POOL_USER_CHAR.ToString(), strIndex, iCount);
        //내가 가진 캐릭터 모델링들을 미리 셋팅

        PanelOnOff(UI_PANEL_INDEX.PANEL_LOBBY);
        
        m_UserLevel.text += UserInfo.instance.GetUserData(USER_INFO.USER_INFO_LEVEL);       //유저 레벨
        m_UserNickName.text = UserInfo.instance.GetUserData(USER_INFO.USER_INFO_NICKNAME);  //유저 닉네임

        string cur = UserInfo.instance.GetUserData(USER_INFO.USER_INFO_CUR_EXP);
        string max = UserInfo.instance.GetUserData(USER_INFO.USER_INFO_MAX_EXP);
        int iValue = int.Parse(cur) / int.Parse(max);
        m_UserExpBar.value = iValue;
        m_UserExpBar.GetComponentInChildren<UILabel>().text = (cur + "/" + max);

        cur = UserInfo.instance.GetUserData(USER_INFO.USER_INFO_CUR_ENERGY);
        max = UserInfo.instance.GetUserData(USER_INFO.USER_INFO_MAX_ENERGY);
        m_UserEnergy.text = (cur + "/" + max);

        m_UserGold.text = UserInfo.instance.GetUserData(USER_INFO.USER_INFO_GOLD);
    }

    void PanelOnOff(UI_PANEL_INDEX eindex)
    {
        m_eCurPanel = eindex;   //현재 패널
        for (int i = (int)UI_PANEL_INDEX.PANEL_START; i < (int)UI_PANEL_INDEX.PANEL_END; i++)
        {
            if (i == (int)eindex)   //켜고자 하는 거 외에는 모두 꺼라
                m_ListPanel[i].gameObject.SetActive(true);
            else
                m_ListPanel[i].gameObject.SetActive(false);
        }

        if(eindex == UI_PANEL_INDEX.PANEL_LOBBY)
        {
            MainCharSet(true);
            m_ListPanel[(int)UI_PANEL_INDEX.PANEL_BUTTON].gameObject.SetActive(false);
        }
        else
        {
            MainCharSet(false);
            m_ListPanel[(int)UI_PANEL_INDEX.PANEL_BUTTON].gameObject.SetActive(true);
            //무슨 패널이라도 무조건 켜주는 버튼 패널

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        PoolManager.instance.Clear();
        //해당 씬에서 생성했던 모든 풀 삭제
    }

    void MainCharSet(bool bSet)
    {
        string strMain = UserInfo.instance.GetUserData(USER_INFO.USER_INFO_MAIN_CHAR);
        int iMainCount = int.Parse(strMain);
        if (bSet)
        {
            m_MainChar = PoolManager.instance.PopFromPool(POOL_INDEX.POOL_USER_CHAR.ToString(), iMainCount);
            m_MainChar.SetActive(true);
            m_MainChar.transform.SetParent(m_MainCharTR, false); //메인 캐릭터의 하위 오브젝트로 설정
            //메인으로 지정된 캐릭터 불러오기
            m_MainChar.GetComponent<Animator>().runtimeAnimatorController = UserInfo.instance.GetCharAnimator(iMainCount,
                CHAR_ANIMATOR.CHAR_LOBBY_ANIMATOR) as RuntimeAnimatorController;
            //애니메이터 변경
        }
        else
        {
            if(m_MainChar != null)
            {
                PoolManager.instance.PushToPool(POOL_INDEX.POOL_USER_CHAR.ToString(), iMainCount, m_MainChar);
                m_MainChar = null;
            }
        }
    }

    public void OnClick()
    {
        //로비에서 사용되는 버튼들은 여기서 구분한다.        
        string Button = UIButton.current.name;

        if (Button == "StageButton")
        {
            PanelOnOff(UI_PANEL_INDEX.PANEL_STAGE);
        }
        else if (Button == "HomeButton")
        {
            PanelOnOff(UI_PANEL_INDEX.PANEL_LOBBY);
            GameManager.instance.ResetData();
        }
        else if (Button == "BackButton")
        {
            //이전 패널
            PanelOnOff(m_eCurPanel - 1);
            if (m_eCurPanel == UI_PANEL_INDEX.PANEL_LOBBY || m_eCurPanel == UI_PANEL_INDEX.PANEL_STAGE)
                GameManager.instance.ResetData();
        }
        else if (Button.Contains("Stage_"))  //스테이지 버튼 중 하나를 클릭
        {
            string[] split = Button.Split('_');
            string stage = split[split.Length - 1];
            GameManager.instance.StageSelect(int.Parse(stage)); //내가 선택한 스테이지 임시 저장
            PanelOnOff(UI_PANEL_INDEX.PANEL_STAGE_READY);
            //캐릭터 선택 패널로 넘어가며 여기서 내가 가진 캐릭터로 세팅 할 수 있다.
        }
        else if (Button.Contains("SelectChar_")) //유저가 캐릭터 선택 창을 클릭하였다.
        {
            //무슨 캐릭터 선택창을 선택했는가를 확인
            string[] split = Button.Split('_');
            m_strCharSelect = split[split.Length - 1];   //내가 선택한 캐릭터 선택창 임시 저장
            PanelOnOff(UI_PANEL_INDEX.PANEL_CHAR_SELECT);   //캐릭터 선택창을 선택하면 해당 스크립트가 실행되면서 활동한다.
        }
        else if (Button == "SelectButton")
        {
            //캐릭터 선택 버튼을 눌렀으면 이전 패널로 돌아가며, 스프라이트에 해당 캐릭터의 그림과 이름이 올라간다.
            if (GameManager.instance.CharSelectComplete(int.Parse(m_strCharSelect)))
            {
                m_ListPanel[(int)UI_PANEL_INDEX.PANEL_STAGE_READY].GetComponent<StageReadyPanel>().SelectChar(m_strCharSelect);
                PanelOnOff(UI_PANEL_INDEX.PANEL_STAGE_READY);
            }
        }
        else if (Button == "StageStart")
        {
            if (GameManager.instance.StageReady())
            {
                GameManager.instance.GameStart();   //게임 시작
            }
        }
        else if(Button == "")
        {
        }
        else if(Button == "Equipment")
        {
            PanelOnOff(UI_PANEL_INDEX.PANEL_EQUIPMENT);   //캐릭터 선택창을 선택하면 해당 스크립트가 실행되면서 활동한다.
        }
    }
}