using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private NavMeshAgent m_NavMeshAgent;    //내비메쉬 기반 플레이어 추적
    private CharacterData m_CharData;   //나의 캐릭터 정보
    private Transform m_PlayerTR;
    int m_iIndex = 0;
    //에너미 움직임을 담당하는 자체 스크립트
    // Start is called before the first frame update
    void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Setting(int iIndex, List<Dictionary<string, object>> CharInfo, Transform Player)
    {
        m_iIndex = iIndex;
        m_CharData = new CharacterData(iIndex, CharInfo);
        m_PlayerTR = Player;
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어를 향해 달려온다.
    }
}
