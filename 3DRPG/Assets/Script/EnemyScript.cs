using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public enum ENEMY_STATE
    {
        STATE_WAIT,
        STATE_ATTACK,
        STATE_TRACE,
        STATE_DIE,
    }
    private NavMeshAgent m_NavMeshAgent;    //내비메쉬 기반 플레이어 추적
    private Animator m_Animator;
    private CharacterData m_CharData;   //적의 데이터
    private Transform m_PlayerTR = null;   //플레이어 TR
    public ENEMY_STATE m_eCurState; //적의 현재 스테이트
    int m_iIndex = 0;
    //에너미 움직임을 담당하는 자체 스크립트
    // Start is called before the first frame update
    void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();

        if(GameManager.instance.ReturnStageType() != "Infiltration")
            m_eCurState = ENEMY_STATE.STATE_TRACE;
        else
            m_eCurState = ENEMY_STATE.STATE_WAIT;
        StartCoroutine(StateAction());
    }

    public void Setting(int iIndex, List<Dictionary<string, object>> CharInfo)
    {
        m_iIndex = iIndex;
        m_CharData = new CharacterData(iIndex, CharInfo);
    }

    public void TrSetting(Transform Player)
    {
        m_PlayerTR = Player;
    }
    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator StateAction()
    {
        while(m_eCurState != ENEMY_STATE.STATE_DIE)
        {
            switch (m_eCurState)
            {
                case ENEMY_STATE.STATE_WAIT:
                    m_Animator.SetBool("Attack", false);
                    m_Animator.SetBool("Moving", false);
                    m_NavMeshAgent.isStopped = true;
                    break;
                case ENEMY_STATE.STATE_ATTACK:
                    m_Animator.SetBool("Moving", false);
                    break;
                case ENEMY_STATE.STATE_TRACE:
                    m_Animator.SetBool("Attack", false);
                    m_Animator.SetBool("Moving", true);
                    m_NavMeshAgent.isStopped = false;
                    m_NavMeshAgent.SetDestination(m_PlayerTR.position);
                    break;
                case ENEMY_STATE.STATE_DIE:
                    break;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //공격 사거리 안에 플레이어가 들어서면 스테이트를 바꾼다.
            m_eCurState = ENEMY_STATE.STATE_ATTACK;
        }
    }

    public void Hit()
    {


    }
}
