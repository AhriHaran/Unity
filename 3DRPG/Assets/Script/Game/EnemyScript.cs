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
    public float m_fAttackArea = 7.5f;  //적과 나의 거리
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
        StartCoroutine(StateCheck(0.2f));
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

    IEnumerator StateCheck(float fTime)
    {
        while(m_eCurState != ENEMY_STATE.STATE_DIE)
        {
            yield return new WaitForSeconds(fTime);

            float Distance = Vector3.Distance(m_PlayerTR.position, transform.position);

            if (Distance <= m_fAttackArea)
            {
                //거리가 사정거리보다 짧아지면 공격 스테이트
                m_eCurState = ENEMY_STATE.STATE_ATTACK;
                transform.LookAt(m_PlayerTR); //플레이어를 본다.
            }

            if (Distance >= m_fAttackArea)
            {
                //거리가 사정거리보다 길어지면 추적 스테이트
                m_eCurState = ENEMY_STATE.STATE_TRACE;
            }
        }
    }

    IEnumerator StateAction()
    {
        while(m_eCurState != ENEMY_STATE.STATE_DIE)
        {
            switch (m_eCurState)
            {
                case ENEMY_STATE.STATE_WAIT:    //대기 상태
                    m_Animator.SetBool("Attack", false);
                    m_Animator.SetBool("Moving", false);
                    m_NavMeshAgent.isStopped = true;
                    break;
                case ENEMY_STATE.STATE_ATTACK:  
                    m_Animator.SetBool("Attack", true);
                    m_Animator.SetBool("Moving", false);
                    m_NavMeshAgent.isStopped = true;
                    break;
                case ENEMY_STATE.STATE_TRACE:
                    m_Animator.SetBool("Attack", false);
                    m_Animator.SetBool("Moving", true);
                    m_NavMeshAgent.SetDestination(m_PlayerTR.position);
                    m_NavMeshAgent.isStopped = false;
                    break;
                case ENEMY_STATE.STATE_DIE:
                    break;
            }
            yield return null;
        }
    }

    public void Hit()
    {
        //여기서 콜리더 히트 체크
        //Collider[] colliders = Physics.OverlapSphere(transform.position, 2, 1 << LayerMask.NameToLayer("PLAYER"));
    }

    public void Damege(float fATK, float fCRI)
    {
        //데미지 계산은 나중에 현재는 모든 적을 잡고 나면 루틴을 돌리는 것을 해야한다.
        /*
         * 현재 필요한 것들
         * 적을 죽이면 일정 확률로 아이템이 떨어지고 이것이 나의 인벤토리로 가야 한다.
         */

        //임시함수
        m_eCurState = ENEMY_STATE.STATE_DIE;
        gameObject.SetActive(false);
    }
}
