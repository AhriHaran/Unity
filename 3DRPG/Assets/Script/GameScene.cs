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
    private PlayerManager m_PlayerManger;

    void Start()
    {
        //정렬할 오브젝트를 받아온뒤
        int ChildCount = transform.childCount;
        m_arrObject = new GameObject[(int)OBJECT_INDEX.OBJECT_END];
        for (int i = 0; i < ChildCount; i++)
        {
            m_arrObject[i] = transform.GetChild(i).gameObject;
        }

        m_MapManager = new MapManager(m_arrObject[(int)OBJECT_INDEX.OBJECT_BACKGROUND].transform);
        //배경 오브젝트 설정
        m_arrObject[(int)OBJECT_INDEX.OBJECT_BACKGROUND].GetComponent<NavMeshSurface>().BuildNavMesh();
        //네비메쉬 서페이스로 런타임 베이크

        m_PlayerManger = new PlayerManager(m_arrObject[(int)OBJECT_INDEX.OBJECT_PLAYER].transform);
        //플레이어 셋팅
        //스타트에서 처음 포지셔닝을 셋팅
        var Pos = m_MapManager.ReturnEventPos();
        m_PlayerManger.SetPosition(Pos[0], 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
