using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UI_PANEL_INDEX
{
    PANEL_START,
    PANEL_LOBBY = PANEL_START,    //로비 패널
    PANEL_STAGE,        //스테이지 패널
    PANEL_STAGE_READY,  //스테이지 준비 패널
    PANEL_VALKYRJA,     //캐릭터 선택 패널
    PANEL_CHAR_INFO,    //캐릭터 정보 패널ㄴ
    PANEL_ITEM_SELECT,  //장비 패널
    PANEL_EQUIPMENT,    //장비 패널
    PANEL_BUTTON,
    PANEL_END,
}

public class LobbyManager : MonoBehaviour
{

    public UILabel m_UserLevel;
    public UILabel m_UserNickName;
    public UISlider m_UserExpBar;
    public UILabel m_UserGold;
    public UILabel m_UserEnergy;
    public Transform m_MainCharTR;

    private List<UIPanel> m_ListPanel = new List<UIPanel>();
    private GameObject m_MainChar;   //메인 캐릭터 위치
    private string m_strCharSelect;
    private int m_iItemSelect = -1;
    private Stack<UI_PANEL_INDEX> m_StackPanel = new Stack<UI_PANEL_INDEX>();
    private UI_PANEL_INDEX m_eCurPanel; //플레이어의 현재 패널

    private void Awake()
    {
        GameManager.instance.Init();
    }

    private void Start()
    {
        GameObject LobbyUI = GameObject.Find("LobbyUI");
        m_eCurPanel = UI_PANEL_INDEX.PANEL_LOBBY;
        for (int i = (int)UI_PANEL_INDEX.PANEL_START; i < (int)UI_PANEL_INDEX.PANEL_END; i++)
        {
            UIPanel Node = LobbyUI.transform.GetChild(i + 1).GetComponent<UIPanel>();
            Node.gameObject.SetActive(false);
            m_ListPanel.Add(Node);
        }

        var CharList = UserInfo.instance.GetMyCharList();
        int iCount = CharList.Count;
        string[] strIndex = new string[CharList.Count];
        int[] iarr = new int[CharList.Count];
        //내가 가진 캐릭터 리스트 기반
        for (int i = 0; i < iCount; i++)
        {
            try
            {
                string route = Util.ConvertToString(CharList[i].GetCharData(CHAR_DATA.CHAR_INDEX));
                string name = Util.ConvertToString(CharList[i].GetCharData(CHAR_DATA.CHAR_NAME));
                strIndex[i] = "Player/" +route + "/Prefabs/" + name;
                iarr[i] = Util.ConvertToInt(CharList[i].GetCharData(CHAR_DATA.CHAR_INDEX));
            }
            catch (System.NullReferenceException ex)
            {
                Debug.Log(ex);
            }
        }
        CharPoolManager.instance.Set(POOL_INDEX.POOL_USER_CHAR.ToString(), strIndex, iarr, iCount);
        //내가 가진 캐릭터 모델링들을 미리 셋팅

        PanelOnOff(UI_PANEL_INDEX.PANEL_LOBBY, false);
        
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

    void PanelOnOff(UI_PANEL_INDEX eindex, bool bStack = true)
    {
        if(bStack)
            m_StackPanel.Push(m_eCurPanel);
        m_eCurPanel = eindex;   //현재 패널
        
        if (eindex != UI_PANEL_INDEX.PANEL_VALKYRJA && eindex != UI_PANEL_INDEX.PANEL_CHAR_INFO
            && eindex != UI_PANEL_INDEX.PANEL_ITEM_SELECT)
        {
            GameManager.instance.DestroyModel();
            //이 외의 패널은 모두 현재 선택된 캐릭터를 다시 반납한다.
        }
        
        if (eindex == UI_PANEL_INDEX.PANEL_LOBBY)
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

        for (int i = (int)UI_PANEL_INDEX.PANEL_START; i < (int)UI_PANEL_INDEX.PANEL_END - 1; i++)
        {
            if (i == (int)eindex)   //켜고자 하는 거 외에는 모두 꺼라
            {
                m_ListPanel[i].gameObject.SetActive(true);
            }
            else
                m_ListPanel[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        CharPoolManager.instance.Clear();
        m_StackPanel.Clear();
        //해당 씬에서 생성했던 모든 풀 삭제
    }

    void MainCharSet(bool bSet)
    {
        string strMain = UserInfo.instance.GetUserData(USER_INFO.USER_INFO_MAIN_CHAR);
        int iMainCount = int.Parse(strMain);
        if (bSet)
        {
            if (m_MainChar == null)
            {
                m_MainChar = CharPoolManager.instance.PopFromPool(POOL_INDEX.POOL_USER_CHAR.ToString(), iMainCount);
                m_MainChar.SetActive(true);
                m_MainChar.transform.SetParent(m_MainCharTR, false); //메인 캐릭터의 하위 오브젝트로 설정
                                                                     //메인으로 지정된 캐릭터 불러오기
                m_MainChar.GetComponent<Animator>().runtimeAnimatorController = UserInfo.instance.GetCharAnimator(iMainCount,
                    CHAR_ANIMATOR.CHAR_LOBBY_ANIMATOR) as RuntimeAnimatorController;
                //로비에서는 아이템과 임팩트가 보일 이유는 없다.
            }
            //애니메이터 변경
        }
        else
        {
            if(m_MainChar != null)
            {
                CharPoolManager.instance.PushToPool(POOL_INDEX.POOL_USER_CHAR.ToString(), iMainCount, m_MainChar);
                m_MainChar = null;
            }
        }
    }

    public void OnClickCharInfo()
    {
        string Button = UIButton.current.name;

        if (Button == "CharInfo" || Button == "CharLevel")
        {
            PanelOnOff(UI_PANEL_INDEX.PANEL_CHAR_INFO);
            m_iItemSelect = -1;
            m_ListPanel[(int)UI_PANEL_INDEX.PANEL_CHAR_INFO].GetComponent<CharInfoPanel>().UiOnOff(CHAR_INFO_UI.CHAR_INFO_UI_LEVEL);
        }
        else if (Button == "CharWeapon")
        {
            PanelOnOff(UI_PANEL_INDEX.PANEL_CHAR_INFO);
            m_iItemSelect = 0;
            m_ListPanel[(int)UI_PANEL_INDEX.PANEL_CHAR_INFO].GetComponent<CharInfoPanel>().UiOnOff(CHAR_INFO_UI.CHAR_INFO_UI_WEAPON);
        }
        else if (Button == "CharStigma")
        {
            PanelOnOff(UI_PANEL_INDEX.PANEL_CHAR_INFO);
            m_iItemSelect = 1;
            m_ListPanel[(int)UI_PANEL_INDEX.PANEL_CHAR_INFO].GetComponent<CharInfoPanel>().UiOnOff(CHAR_INFO_UI.CHAR_INFO_UI_STIGMA);
        }
    }

    public void OnClickItemSelect()
    {
        string Button = UIButton.current.name;

        if(Button == "Change_Weapon")   //캐릭터의 장비를 바꾼다.
        {
            GameManager.instance.ItemSelect(GameManager.instance.ReturnSelectCharType());
            PanelOnOff(UI_PANEL_INDEX.PANEL_ITEM_SELECT);
        }
        else if(Button == "Change_T")
        {
            GameManager.instance.ItemSelect(ITEM_TYPE.ITEM_STIGMA_TOP);
            PanelOnOff(UI_PANEL_INDEX.PANEL_ITEM_SELECT);
        }
        else if (Button == "Change_C")
        {
            GameManager.instance.ItemSelect(ITEM_TYPE.ITEM_STIGMA_CENTER);
            PanelOnOff(UI_PANEL_INDEX.PANEL_ITEM_SELECT);
        }
        else if (Button == "Change_B")
        {
            GameManager.instance.ItemSelect(ITEM_TYPE.ITEM_STIGMA_BOTTOM);
            PanelOnOff(UI_PANEL_INDEX.PANEL_ITEM_SELECT);
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
            UI_PANEL_INDEX eCur = m_eCurPanel;
            UI_PANEL_INDEX eIndex = m_StackPanel.Pop();
            PanelOnOff(eIndex, false);
            if (eCur == UI_PANEL_INDEX.PANEL_ITEM_SELECT) //현재 패널이 아이템 셀렉이고 이전 패널로 돌아간다면?
            {
                if(m_iItemSelect == 0)
                {
                    m_ListPanel[(int)UI_PANEL_INDEX.PANEL_CHAR_INFO].GetComponent<CharInfoPanel>().UiOnOff(CHAR_INFO_UI.CHAR_INFO_UI_WEAPON);
                }
                else if(m_iItemSelect == 1)
                {
                    m_ListPanel[(int)UI_PANEL_INDEX.PANEL_CHAR_INFO].GetComponent<CharInfoPanel>().UiOnOff(CHAR_INFO_UI.CHAR_INFO_UI_STIGMA);
                }
                m_iItemSelect = -1;
            }
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
            PanelOnOff(UI_PANEL_INDEX.PANEL_VALKYRJA);   //캐릭터 선택창을 선택하면 해당 스크립트가 실행되면서 활동한다.
            m_ListPanel[(int)UI_PANEL_INDEX.PANEL_VALKYRJA].GetComponent<ValkyrjaPanel>().ButtonOnOff(true);
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
        else if(Button == "Valkyrja")
        {
            //캐릭터 창, 여기서 장비 장착등이 가능하다.
            PanelOnOff(UI_PANEL_INDEX.PANEL_VALKYRJA);
            m_ListPanel[(int)UI_PANEL_INDEX.PANEL_VALKYRJA].GetComponent<ValkyrjaPanel>().ButtonOnOff(false);
        }
        else if(Button == "Equipment")
        {
            PanelOnOff(UI_PANEL_INDEX.PANEL_EQUIPMENT); 
        }
    }
}