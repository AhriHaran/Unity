using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameScene : MonoBehaviour
{
    enum OBJECT_INDEX
    {
        OBJECT_START,
        OBJECT_BACKGROUND = OBJECT_START,
        OBJECT_ENEMY,
        OBJECT_PLAYER,
        OBJECT_PARTICLE,
        OBJECT_END,
    }

    private GameObject[] m_arrObject; //게임 오브젝트 배열
    private MapManager m_MapManager;
    private PlayerManager m_PlayerManager;
    private EnemyManager m_EnemyMangaer;
    public delegate void CallBack(Transform tr);    //캐릭터 변경등의 상황에서 카메라 셋팅
    private CallBack m_CallBack = null;
    private ChangeButton m_Change = null;
    private UIPanel m_ResultPanel;
    private UIPanel m_FailPanel;
    private int m_iTmpIndex;
    private bool m_bChanging =true;

    private void Awake()
    {
        m_ResultPanel = GameObject.Find("GameUI").transform.GetChild(4).GetComponent<UIPanel>();    //결과창s   
        m_FailPanel = GameObject.Find("GameUI").transform.GetChild(5).GetComponent<UIPanel>();    //결과창
        int ChildCount = transform.childCount;
        m_arrObject = new GameObject[(int)OBJECT_INDEX.OBJECT_END];
        for (int i = 0; i < ChildCount; i++)
        {
            m_arrObject[i] = transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        OnSp();
    }

    private void OnSp()
    {
        if(Input.GetKeyUp(KeyCode.G))
        {
            GameObject Item = PoolManager.instance.PopFromPool(POOL_INDEX.POOL_SP_ITEM.ToString());
            Vector3 Pos = transform.position;
            Pos.y = 2;
            Item.transform.position = Pos;
            Item.SetActive(true);
        }
    }


    void Start()
    {
        m_CallBack = Camera.main.GetComponent<FollowCam>().CameraSet;   //카메라 셋팅 콜백

        string strStage = Util.ConvertToString(GameManager.instance.ReturnStage()); //첫시작    
        string strFile = "Excel/StageExcel/" + strStage + "/Map_Object";  //해당스테이지의 맵 Info
        List<Dictionary<string, object>> Info = EXCEL.ExcelLoad.Read(strFile);
        strFile = "Excel/Table/Stage_Table";   //전체 맵의 table 데이터
        List<Dictionary<string, object>> Table = EXCEL.ExcelLoad.Read(strFile);
        strFile = "Excel/StageExcel/" + strStage + "/Event_Pos";   //해당 맵의 시작 등의 좌표
        List<Dictionary<string, object>> Pos = EXCEL.ExcelLoad.Read(strFile);
        m_MapManager = new MapManager(m_arrObject[(int)OBJECT_INDEX.OBJECT_BACKGROUND].transform, Info, Table, Pos);
        //배경 오브젝트 설정
        m_arrObject[(int)OBJECT_INDEX.OBJECT_BACKGROUND].GetComponent<NavMeshSurface>().BuildNavMesh();
        //네비메쉬 서페이스로 런타임 베이크

        m_PlayerManager = new PlayerManager(m_arrObject[(int)OBJECT_INDEX.OBJECT_PLAYER].transform, m_arrObject[(int)OBJECT_INDEX.OBJECT_PARTICLE].transform);
        //플레이어 셋팅

        strFile = "Excel/StageExcel/" + strStage + "/Enemy_Pos";
        Pos = EXCEL.ExcelLoad.Read(strFile);
        strFile = "Excel/StageExcel/" + strStage + "/Enemy_Info";
        Info = EXCEL.ExcelLoad.Read(strFile);
        m_EnemyMangaer = new EnemyManager(m_arrObject[(int)OBJECT_INDEX.OBJECT_ENEMY].transform, Pos, Info);
        //에너미 셋팅

        var vecPos = m_MapManager.ReturnEventPos();
        m_PlayerManager.PlayerSet(0, vecPos[0], JumpEnd);  //가장 첫번째 캐릭터와, 포지션 셋팅
        //스타트에서 처음 포지셔닝을 셋팅
        
        m_CallBack(m_PlayerManager.GetCharTR());    //카메라 콜백 함수 선언
        m_EnemyMangaer.TrSetting(m_PlayerManager.GetCharTR()); //타겟 셋팅
        //m_EnemyMangaer.ActiveWave();    //액티브
        
        PoolManager.instance.Set(POOL_INDEX.POOL_HP_ITEM.ToString(), "Prefabs/HP", 10);
        PoolManager.instance.Set(POOL_INDEX.POOL_SP_ITEM.ToString(), "Prefabs/SP", 10);

        InvokeRepeating("WaveClear", 2.0f, 1.0f);
        InvokeRepeating("PlayerDie", 2.0f, 1.0f);
    }

    void WaveClear()
    {
        WAVE_STATE eStae =WAVE_STATE.WAVE_NONE;
        m_EnemyMangaer.WaveClear(ref eStae);
        switch (eStae)
        {
            case WAVE_STATE.WAVE_CLEAR:
                m_EnemyMangaer.TrSetting(m_PlayerManager.GetCharTR());
                m_EnemyMangaer.ActiveWave();
                break;
            case WAVE_STATE.WAVE_END:
                /*
                 * 현재는 바로 이전 화면으로 로딩해주지만 아래와 같은 것이 필요
                 * 스테이지를 클리어하였기에 경험치와 아이템들을 정산해줘야 하며
                 * 캐릭터의 스테이터스 등을 상승시켜줄 필요가있다.
                 */
                Time.timeScale = 0.0f;
                m_ResultPanel.gameObject.SetActive(true);
                CancelInvoke("WaveClear");
                //결과창을띄워준다.
                break;
        }
    }

    void PlayerDie()
    {
        if (m_PlayerManager.PlayerDie())
        {
            Time.timeScale = 0.0f;
            m_FailPanel.gameObject.SetActive(true);
        }
    }

    public void OnClick()
    {
        //유저 인포 세이브
        //세이브 데이터
        //변경된 데이터를 저장하고 로비로 돌아간다.
        UserInfo.instance.AllSave();
        GameManager.instance.ResetData();
        Time.timeScale = 1.0f;
        LoadScene.SceneLoad("LobbyScene");
    }

    public void ChangeChar()
    {
        GameObject cur = UIEventTrigger.current.gameObject;
        ChangeButton Button = cur.GetComponent<ChangeButton>();
        if (Button.ChangeOK && m_bChanging)   //바꿔도 됨
        {
            m_Change = Button;
            m_bChanging = false;
            //캐릭터 체인지
            m_EnemyMangaer.Stop();
            //우선 적들을 멈춰 주고
            m_iTmpIndex = Button.ListIndex;
            m_PlayerManager.JumpStart();
        }
    }

    public void JumpEnd(bool bDie)
    {
        //점프 모션이 끝나면 호출
        Transform tr = m_PlayerManager.GetCharTR();

        if(bDie)
        {
            m_EnemyMangaer.Stop();
            m_iTmpIndex = m_PlayerManager.DontDie();
        }

        if(m_iTmpIndex >= 0)
        {
            int[] iarr = GameManager.instance.ReturnPlayerList();
            int iCurList = GameManager.instance.ReturnCurPlayer();

            m_Change.Change(iarr[iCurList], iCurList, m_PlayerManager.GetPlayerData(PLAYER_DATA.PLAYER_CUR_HP),
  m_PlayerManager.GetPlayerData(PLAYER_DATA.PLAYER_MAX_HP), m_PlayerManager.GetPlayerData(PLAYER_DATA.PLAYER_CUR_SP),
  m_PlayerManager.GetPlayerData(PLAYER_DATA.PLAYER_MAX_SP));

            m_PlayerManager.PlayerSet(m_iTmpIndex, tr.localPosition, JumpEnd);
            //캐릭터의 위치와 교대하고
            m_EnemyMangaer.TrSetting(m_PlayerManager.GetCharTR());
            m_EnemyMangaer.Start();
            m_CallBack(m_PlayerManager.GetCharTR());    //카메라 콜백 함수 선언
            m_bChanging = true;
            m_Change = null;
        }
        else
        {
            PlayerDie();
        }
    }
}

/*
 * 아이템 드랍
 * 유저가 죽을 때 처리
 * 궁극기
 */