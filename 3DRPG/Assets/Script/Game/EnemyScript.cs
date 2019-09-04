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

    public ENEMY_STATE m_eCurState = ENEMY_STATE.STATE_WAIT; //적의 현재 스테이트
    public float m_fAttackArea = 7.0f;  //적과 나의 거리
    int m_iIndex = 0;
    //에너미 움직임을 담당하는 자체 스크립트
    // Start is called before the first frame update

    private void Start()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_HpSlider = GameObject.Find("GameUI").transform.GetChild(3).GetComponent<UISlider>();

        if (GameManager.instance.ReturnStageData(MAP_DATA.MAP_TYPE) as string != "Infiltration")
            m_eCurState = ENEMY_STATE.STATE_TRACE;
        else
            m_eCurState = ENEMY_STATE.STATE_WAIT;

        StartCoroutine("StateCheck", 0.2f);
        StartCoroutine("StateAction");
    }

    public void Setting(int iIndex, List<Dictionary<string, object>> CharInfo)
    {
        m_iIndex = iIndex;
        m_CharData = new CharacterData(m_iIndex, CharInfo);
        m_fMaxHP = Util.ConvertToInt(m_CharData.GetEnemyData(CHAR_DATA.CHAR_MAX_HP));
        m_fCurHP = m_fMaxHP;
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
                
                int RNum = Random.Range(0, 10);

                if (RNum >= 0 && RNum <= 2)
                {
                    //hp드랍
                    DropItem(POOL_INDEX.POOL_HP_ITEM);
                }
                else if (RNum >= 3 && RNum <= 5)
                {
                    //sp드랍
                    DropItem(POOL_INDEX.POOL_SP_ITEM);
                }
                else if (RNum >= 6 && RNum <= 8)
                {
                    //둘다 드랍
                    DropItem(POOL_INDEX.POOL_HP_ITEM);
                    DropItem(POOL_INDEX.POOL_SP_ITEM);
                }
            }
            
            //죽었다.
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
                    transform.LookAt(m_PlayerTR);
                    m_NavMeshAgent.isStopped = true;
                    m_eCurState = ENEMY_STATE.STATE_WAIT;
                    break;
                case ENEMY_STATE.STATE_TRACE:
                    m_Animator.SetBool("Attack", false);
                    m_Animator.SetBool("Moving", true);
                    m_NavMeshAgent.SetDestination(m_PlayerTR.position);
                    m_NavMeshAgent.isStopped = false;
                    break;
            }
            yield return null;
        }
    }

    public void Hit()
    {
        ////여기서 콜리더 히트 체크
        //Collider[] colliders = Physics.OverlapSphere(transform.position, 2, 1 << LayerMask.NameToLayer("Player"));

        //if (colliders.Length != 0)
        //{
        //    float fATK = float.Parse(m_CharData.GetEnemyData(CHAR_DATA.CHAR_ATK).ToString());
        //    float fCRI = float.Parse(m_CharData.GetEnemyData(CHAR_DATA.CHAR_CRI).ToString());

        //    for (int i = 0; i < colliders.Length; i++)
        //    {
        //        PlayerScript script = colliders[i].GetComponent<PlayerScript>() ?? null;
        //        if (script != null)
        //            script.Damege(fATK, fCRI);
        //        //해당 함수 호출
        //    }
        //}
    }

    public void Damege(float fATK, float fCRI)
    {
        //적이 죽으면 일정확률로 체력 회복 아이템과 SP 회복 아이템을 드랍한다.
        if(m_eCurState != ENEMY_STATE.STATE_DIE)
        {
            m_HpSlider.gameObject.SetActive(true);
            //부딪힌 것의 HP바를 보여준다.
            int iCri = Random.Range(0, 100);
            int iAtk = (int)fATK;

            if (iCri == (int)fCRI)
            {
                iAtk += (iCri * 10);    //크리티컬!
            }

            m_fCurHP -= (float)iAtk;

            if (m_fCurHP <= 0.0f)   //모든 HP가 다 떨어지면
            {
                m_fCurHP = 0.0f;
                m_eCurState = ENEMY_STATE.STATE_DIE;
                m_Animator.SetBool("Attack", false);
                m_Animator.SetBool("Moving", false);
                m_Animator.SetTrigger("Death");
                m_NavMeshAgent.isStopped = true;
                StopAllCoroutines();
            }

            float fHP = ((float)m_fCurHP / (float)m_fMaxHP);
            m_HpSlider.value = fHP;

            m_HpSlider.GetComponentInChildren<UILabel>().text = Util.ConvertToString(m_CharData.GetEnemyData(CHAR_DATA.CHAR_NAME));//에너미 이름
        }
    }

    void DropItem(POOL_INDEX eIndex)
    {
        GameObject Item = PoolManager.instance.PopFromPool(eIndex.ToString());
        Vector3 Pos = transform.position;
        Pos.y = 2;
        Item.transform.position = Pos;
        Item.SetActive(true);
    }

    public void Stop()
    {
        m_eCurState = ENEMY_STATE.STATE_WAIT;
        StopAllCoroutines();
        m_PlayerTR = null;
    }

    public void ReStart()
    {
        StartCoroutine("StateCheck", 0.2f);
        StartCoroutine("StateAction");
    }
}
