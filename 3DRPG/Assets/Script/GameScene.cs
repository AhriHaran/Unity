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
        OBJECT_END,
    }
    private GameObject[] m_arrObject; //게임 오브젝트 배열
    private MapManager m_MapManager;
    private PlayerManager m_PlayerManager;
    private EnemyManager m_EnemyMangaer;
    public delegate void CallBack(Transform tr);    //캐릭터 변경등의 상황에서 카메라 셋팅
    private CallBack m_CallBack = null;

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

        m_MapManager = new MapManager(m_arrObject[(int)OBJECT_INDEX.OBJECT_BACKGROUND].transform);
        //배경 오브젝트 설정
        m_arrObject[(int)OBJECT_INDEX.OBJECT_BACKGROUND].GetComponent<NavMeshSurface>().BuildNavMesh();
        //네비메쉬 서페이스로 런타임 베이크
        m_PlayerManager = new PlayerManager(m_arrObject[(int)OBJECT_INDEX.OBJECT_PLAYER].transform);
        //플레이어 셋팅

        //스타트에서 처음 포지셔닝을 셋팅
        var Pos = m_MapManager.ReturnEventPos();
        m_PlayerManager.SetPosition(0, Pos[0]);
        m_CallBack(m_PlayerManager.GetCharTR());    //카메라 콜백 함수 선언

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
