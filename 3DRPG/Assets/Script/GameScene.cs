using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    enum OBJECT_INDEX
    {
        OBJECT_BACKGROUND,
        OBJECT_ENEMY,
        OBJECT_PLAYER,
    }

    public FollowCam m_MainCamera;

    private GameObject[] m_arrObject; //게임 오브젝트 배열
    // Start is called before the first frame update
    void Start()
    {
        //게임신을 가장 마지막에 실행 시킨 후
        int ChildCount = transform.childCount;
        m_arrObject = new GameObject[ChildCount];
        for (int i = 0; i < ChildCount; i++)
        {
            m_arrObject[i] = transform.GetChild(i).gameObject;
        }
        //모든 게임 오브젝트는 여기서 가지고 있고,
        //게임 씬에서 관리하는 모든 게임 오브젝트

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
