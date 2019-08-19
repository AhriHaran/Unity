﻿using System.Collections;
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
        OBJECT_END,
    }

    private GameObject[] m_arrObject; //게임 오브젝트 배열
    private MapManager m_MapManager;
    private PlayerManager m_PlayerManager;
    private EnemyManager m_EnemyMangaer;
    public delegate void CallBack(Transform tr);    //캐릭터 변경등의 상황에서 카메라 셋팅
    private CallBack m_CallBack = null;
    private UIPanel m_ResultPanel;

    void Start()
    {
        //정렬할 오브젝트를 받아온뒤
        int ChildCount = transform.childCount;
        m_arrObject = new GameObject[(int)OBJECT_INDEX.OBJECT_END];
        for (int i = 0; i < ChildCount; i++)
        {
            m_arrObject[i] = transform.GetChild(i).gameObject;
        }

        m_CallBack = Camera.main.GetComponent<FollowCam>().CameraSet;   //카메라 셋팅 콜백

        string strStage = GameManager.instance.ReturnStage(); //첫시작 
        string strFile = "Excel/StageExcel/" + strStage + "Stage_Map";
        List<Dictionary<string, object>> MapFile = EXCEL.ExcelLoad.Read(strFile);
        strFile = "Excel/StageExcel/Stage_Table";
        List<Dictionary<string, object>> MapTable = EXCEL.ExcelLoad.Read(strFile);
        strFile = "Excel/StageExcel/" + strStage + "Stage_Event_Pos";
        List<Dictionary<string, object>> MapPos = EXCEL.ExcelLoad.Read(strFile);
        m_MapManager = new MapManager(m_arrObject[(int)OBJECT_INDEX.OBJECT_BACKGROUND].transform, MapFile, MapTable, MapPos);
        //배경 오브젝트 설정

        m_arrObject[(int)OBJECT_INDEX.OBJECT_BACKGROUND].GetComponent<NavMeshSurface>().BuildNavMesh();
        //네비메쉬 서페이스로 런타임 베이크
        m_PlayerManager = new PlayerManager(m_arrObject[(int)OBJECT_INDEX.OBJECT_PLAYER].transform);
        //플레이어 셋팅
        var Pos = m_MapManager.ReturnEventPos();
        m_PlayerManager.SetPosition(0, Pos[0]);
        //스타트에서 처음 포지셔닝을 셋팅

        m_EnemyMangaer = new EnemyManager(m_arrObject[(int)OBJECT_INDEX.OBJECT_ENEMY].transform);
        m_CallBack(m_PlayerManager.GetCharTR());    //카메라 콜백 함수 선언
        m_EnemyMangaer.TrSetting(m_PlayerManager.GetCharTR());
        m_EnemyMangaer.ActiveWave();

        m_ResultPanel = GameObject.Find("GameUI").GetComponentInChildren<UIPanel>();

        InvokeRepeating("WaveClear", 2.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void WaveClear()
    {
        EnemyManager.WAVE_STATE eStae = EnemyManager.WAVE_STATE.WAVE_NONE;
        m_EnemyMangaer.WaveClear(ref eStae);
        switch (eStae)
        {
            case EnemyManager.WAVE_STATE.WAVE_CLEAR:
                m_EnemyMangaer.TrSetting(m_PlayerManager.GetCharTR());
                m_EnemyMangaer.ActiveWave();
                break;
            case EnemyManager.WAVE_STATE.WAVE_END:
                /*
                 * 현재는 바로 이전 화면으로 로딩해주지만 아래와 같은 것이 필요
                 * 스테이지를 클리어하였기에 경험치와 아이템들을 정산해줘야 하며
                 * 캐릭터의 스테이터스 등을 상승시켜줄 필요가있다.
                 */
                m_ResultPanel.gameObject.SetActive(true);
                CancelInvoke("WaveClear");
                //결과창을띄워준다.
                break;
        }
    }

    public void OnClick()
    {
        //유저 인포 세이브
        //변경된 데이터를 저장하고 로비로 돌아간다.
        //LoadScene.SceneLoad("LobbyScene");
    }
}