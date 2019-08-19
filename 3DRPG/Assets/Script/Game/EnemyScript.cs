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
        STATE_NONE,
    }

    private NavMeshAgent m_NavMeshAgent;    //내비메쉬 기반 플레이어 추적
    private Animator m_Animator;
    private CharacterData m_CharData;   //적의 데이터
    private Transform m_PlayerTR = null;   //플레이어 TR
    private UISlider m_HpSlider = null;
    private float m_fCurDeathTime = 0.0f;
    private float m_fDeathTime = 2.0f;
    private float m_fMaxHP = 0.0f; //맥스 HP
    private float m_fCurHP = 0.0f; //현재 HP

    public ENEMY_STATE m_eCurState; //적의 현재 스테이트
    public float m_fAttackArea = 7.0f;  //적과 나의 거리
    int m_iIndex = 0;
    //에너미 움직임을 담당하는 자체 스크립트
    // Start is called before the first frame update
    void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_HpSlider = GameObject.Find("GameUI").transform.GetChild(3).GetComponent<UISlider>();

        if (GameManager.instance.ReturnStageData(MAP_DATA.MAP_TYPE) as string != "Infiltration")
            m_eCurState = ENEMY_STATE.STATE_TRACE;
        else
            m_eCurState = ENEMY_STATE.STATE_WAIT;
        m_fMaxHP = 100.0f;
        m_fCurHP = m_fMaxHP;
        StartCoroutine(StateAction());
        StartCoroutine(StateCheck(0.2f));
    }

    public void Setting(int iIndex, List<Dictionary<string, object>> CharInfo)
    {
        m_iIndex = iIndex;
        //m_CharData = new CharacterData(iIndex, CharInfo);
    }

    public void TrSetting(Transform Player)
    {
        m_PlayerTR = Player;
    }
    // Update is called once per frame
    void Update()
    {
        if (m_eCurState == ENEMY_STATE.STATE_DIE)
        {
            m_fCurDeathTime += Time.deltaTime;
            if (m_fCurDeathTime >= m_fDeathTime)
            {
                m_HpSlider.gameObject.SetActive(false);
                m_eCurState = ENEMY_STATE.STATE_NONE;
                gameObject.SetActive(false);
            }
        }
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
            }

            if (Distance >= m_fAttackArea)
            {
                //거리가 사정거리보다 길어지면 추적 스테이트
                m_eCurState = ENEMY_STATE.STATE_TRACE;
                transform.LookAt(m_PlayerTR);   
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
                    m_NavMeshAgent.Stop();
                    break;
                case ENEMY_STATE.STATE_ATTACK:  
                    m_Animator.SetBool("Attack", true);
                    m_Animator.SetBool("Moving", false);
                    m_NavMeshAgent.Stop();
                    break;
                case ENEMY_STATE.STATE_TRACE:
                    m_Animator.SetBool("Attack", false);
                    m_Animator.SetBool("Moving", true);
                    m_NavMeshAgent.SetDestination(m_PlayerTR.position);
                    m_NavMeshAgent.Resume();
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
        //적이 죽으면 일정확률로 체력 회복 아이템과 SP 회복 아이템을 드랍한다.
        //임시함수

        if(m_eCurState != ENEMY_STATE.STATE_DIE)
        {
            m_HpSlider.gameObject.SetActive(true);
            //부딪힌 것의 HP바를 보여준다.
            int iCri = Random.Range(0, 100);
            int iAtk = (int)fATK;

            if (iCri == (int)fCRI)
            {
                iAtk += (iCri * 10);
            }

            m_fCurHP -= (float)iAtk;

            if (m_fCurHP <= 0.0f)   //모든 HP가 다 떨어지면
            {
                m_fCurHP = 0.0f;
                m_eCurState = ENEMY_STATE.STATE_DIE;
                m_Animator.SetBool("Attack", false);
                m_Animator.SetBool("Moving", false);
                m_Animator.SetTrigger("Death");
                m_NavMeshAgent.Stop();
                StopAllCoroutines();
            }

            float fHP = ((float)m_fCurHP / (float)m_fMaxHP);
            m_HpSlider.value = fHP;

            m_HpSlider.GetComponentInChildren<UILabel>().text = "Enemy";//에너미 이름
        }
    }
}
