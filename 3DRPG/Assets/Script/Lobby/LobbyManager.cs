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

    private List<UIPanel> m_ListPanel = new List<UIPanel>();
    private GameObject m_MainChar;   //메인 캐릭터 위치
    public Transform m_MainCharTR;
    string m_strCharSelect;
    //위치는 고정

    private void Start()
    {
        for (int i = (int)UI_PANEL_INDEX.PANEL_START; i < (int)UI_PANEL_INDEX.PANEL_END; i++)
        {
            UIPanel Node = GameObject.Find("LobbyUI").transform.GetChild(i + 1).GetComponent<UIPanel>();
            Node.gameObject.SetActive(false);
            m_ListPanel.Add(Node);
        }

        int iCount = UserInfo.instance.GetMyCharCount();
        string[] strIndex = new string[iCount];
        for (int i = 0; i < iCount; i++)
        {
            try
            {
                string route = UserInfo.instance.GetCharData(CharacterData.CHAR_ENUM.CHAR_ROUTE, i).ToString();
                string name = UserInfo.instance.GetCharData(CharacterData.CHAR_ENUM.CHAR_NAME, i).ToString();
                strIndex[i] = "Player/" +route + "Prefabs/" + name;
            }
            catch (System.NullReferenceException ex)
            {
                Debug.Log(ex);
            }
        }
        PoolManager.instance.Set(EnumClass.POOL_INDEX.USER_CHAR_POOL.ToString(), strIndex, iCount);
        //내가 가진 캐릭터 모델링들을 미리 셋팅
        PoolManager.instance.Set(EnumClass.POOL_INDEX.CHAR_INFO_POOL.ToString(), "Prefabs/CharInfoButton", UserInfo.instance.GetMyCharCount());
        //풀 매니저로 캐릭터 선택 패널을 미리 만들어 놓는다.

        PanelOnOff(UI_PANEL_INDEX.PANEL_LOBBY);
        MainCharSet(true);
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

    private void OnDisable()
    {
        PoolManager.instance.Clear();
        //해당 씬에서 생성했던 모든 풀 삭제
    }

    void MainCharSet(bool bSet)
    {
        int iMainCount = UserInfo.instance.GetMainCharIndex();
        if (bSet)
        {
            m_MainChar = PoolManager.instance.PopFromPool(EnumClass.POOL_INDEX.USER_CHAR_POOL.ToString(), iMainCount);
            m_MainChar.SetActive(true);
            m_MainChar.transform.SetParent(m_MainCharTR, false); //메인 캐릭터의 하위 오브젝트로 설정
            //메인으로 지정된 캐릭터 불러오기
            m_MainChar.GetComponent<Animator>().runtimeAnimatorController = UserInfo.instance.GetCharAnimator(UserInfo.instance.GetMainCharIndex(),
                CharacterData.CHAR_ANIMATOR.CHAR_LOBBY_ANIMATOR) as RuntimeAnimatorController;
            //애니메이터 변경
        }
        else
        {
            if(m_MainChar != null)
                PoolManager.instance.PushToPool(EnumClass.POOL_INDEX.USER_CHAR_POOL.ToString(), iMainCount, m_MainChar);
        }
    }

    public void OnClick()
    {
        //로비에서 사용되는 버튼들은 여기서 구분한다.
        
        string Button = UIButton.current.name;
      
        if (Button == "StageButton")
        {
            PanelOnOff(UI_PANEL_INDEX.PANEL_STAGE);
            MainCharSet(false);
        }
        else if(Button == "HomeButton")
        {
            PanelOnOff(UI_PANEL_INDEX.PANEL_LOBBY);
            MainCharSet(true);
        }
        else if(Button.Contains("Stage_"))  //스테이지 버튼 중 하나를 클릭
        {
            string [] split = Button.Split('_');
            string stage = split[split.Length - 1];  
            GameManager.instance.StageSelect(int.Parse(stage)); //내가 선택한 스테이지 임시 저장
            PanelOnOff(UI_PANEL_INDEX.PANEL_STAGE_READY);
            //캐릭터 선택 패널로 넘어가며 여기서 내가 가진 캐릭터로 세팅 할 수 있다.
        }
        else if(Button.Contains("SelectChar_")) //유저가 캐릭터 선택 창을 클릭하였다.
        {
            //무슨 캐릭터 선택창을 선택했는가를 확인
            string[] split = Button.Split('_');
            m_strCharSelect = split[split.Length - 1];   //내가 선택한 캐릭터 선택창 임시 저장
            PanelOnOff(UI_PANEL_INDEX.PANEL_CHAR_SELECT);   //캐릭터 선택창을 선택하면 해당 스크립트가 실행되면서 활동한다.
        }
        else if(Button == "SelectButton")
        {
            //캐릭터 선택 버튼을 눌렀으면 이전 패널로 돌아가며, 스프라이트에 해당 캐릭터의 그림과 이름이 올라간다.
            GameManager.instance.CharSelectComplete(int.Parse(m_strCharSelect));
            //해당 패널을 제 샛팅
            string strPanel = "SelectChar_" + m_strCharSelect;
            m_ListPanel[(int)UI_PANEL_INDEX.PANEL_STAGE_READY].transform.Find(strPanel).Find("Name").GetComponent<UILabel>().text
                = UserInfo.instance.GetCharData(CharacterData.CHAR_ENUM.CHAR_NAME, GameManager.instance.GetCharIndex(int.Parse(m_strCharSelect))) as string; 
            PanelOnOff(UI_PANEL_INDEX.PANEL_STAGE_READY);
        }
        else if(Button == "StageStart")
        {
            if(GameManager.instance.StageReady())
            {
                GameManager.instance.GameStart();   //게임 시작
            }
        }   
    }
}