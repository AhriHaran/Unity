using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float m_fSpeed = 7.0f;   //이동속도
    public float m_fRotateSpeed = 2.0f; //회전속도
    public float m_fAniSpeed = 1.5f;    //애니메이션 속도
    public float m_fAttackTime = 0.7f;  //공격 유지 시간
    public float m_fCurAttackTime = 0.0f;   //현재 공격 후 걸린시간.
    public int m_iIndex;    //플레이어 캐릭터 인덱스

    private CharacterController m_Controller;
    private Animator m_PlayerAnimator;
    private Vector3 m_Vec3; //목표 지점
    private bool m_bAttack;
    private bool m_bDie;
    private string m_strCurAnime;
    private string m_strCurTrigger;
    private UISlider m_HpSlider = null;
    private UISlider m_SpSlider = null;
    private float m_fMaxHP = 0.0f; //맥스 HP
    private float m_fCurHP = 0.0f; //현재 HP
    private float m_fMaxSP = 0.0f; //맥스 SP
    private float m_fCurSP = 0.0f; //현재 SP

    //플레이어의 조종에 따른 스크립트
    // Start is called before the first frame update
    private void Awake()
    {
        m_Controller = GetComponent<CharacterController>();
        m_PlayerAnimator = GetComponent<Animator>();
    }
    
    private void OnEnable()
    {
        m_bAttack = false;
        m_bDie = false;
        m_strCurAnime = string.Empty;
        m_strCurTrigger = string.Empty;
        
        StartCoroutine(CheckAnimationState());
        StartCoroutine(CheckAttackState());
    }

    public void PlayerSet()
    {
        if (m_HpSlider == null && m_SpSlider == null)
        {
            GameObject UI = GameObject.Find("GameUI");

            m_HpSlider = UI.transform.GetChild(1).GetComponent<UISlider>();//hp 바
            m_SpSlider = UI.transform.GetChild(2).GetComponent<UISlider>();//sp 바

            m_fMaxHP = float.Parse(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_MAX_HP, m_iIndex).ToString());
            m_fCurHP = m_fMaxHP;

            m_fMaxSP = float.Parse(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_MAX_SP, m_iIndex).ToString());
            m_fCurSP = 0.0f;
        }

        float fHP = ((float)m_fCurHP / (float)m_fMaxHP);
        float fSP = ((float)m_fCurSP / (float)m_fMaxSP);

        m_HpSlider.value = fHP; //현재 HP
        m_HpSlider.GetComponentInChildren<UILabel>().text = (m_fCurHP.ToString() + "/" + m_fMaxHP.ToString());//라벨
        m_SpSlider.value = fSP; //현재 SP
        m_SpSlider.GetComponentInChildren<UILabel>().text = (m_fCurSP.ToString() + "/" + m_fMaxSP.ToString());//라벨
    }
   

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        KeyInput();
        if(!m_bAttack)
            KeyControll();
    }
    
    void KeyInput()
    {
        //공격 키
        if (Input.GetMouseButtonUp(0))
        {
            //왼쪽 키 입력
            m_strCurAnime = "Base Layer.Attack-L1";
            m_strCurTrigger = "WeakAttack";
            m_PlayerAnimator.SetBool(m_strCurTrigger, true);
            m_bAttack = true;
            m_fCurAttackTime = 0.0f;
        }

        if (Input.GetMouseButtonUp(1))
        {
            //오른쪽 키 입력
            m_strCurAnime = "Base Layer.Attack-Kick-R1";
            m_strCurTrigger = "StrongAttack";
            m_PlayerAnimator.SetBool(m_strCurTrigger, true);
            m_bAttack = true;
            m_fCurAttackTime = 0.0f;
        }
    }

    void KeyControll()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        m_PlayerAnimator.SetFloat("Speed", v);
        m_PlayerAnimator.SetFloat("Direction", h);
        m_PlayerAnimator.speed = m_fAniSpeed;   //애니메이션 속도
                                                //움직임에 따른 애니메이션
        m_Vec3.Set(0, 0, v);
        m_Vec3 = transform.TransformDirection(m_Vec3);
        m_Vec3 *= m_fSpeed * Time.deltaTime;
        //이동 거리
        m_Controller.Move(m_Vec3);

        transform.Rotate(0, h * m_fRotateSpeed, 0);
    }

    //백턴

    IEnumerator CheckAttackState()
    {
        while(!m_bDie)
        {
            yield return null;

            if(m_bAttack)
            {
                m_fCurAttackTime += Time.deltaTime;
                if(m_fCurAttackTime >= m_fAttackTime)
                {
                    m_bAttack = false;
                    m_fCurAttackTime = 0.0f;
                }
            }
            
        }
    }

    IEnumerator CheckAnimationState()   //애니메이션이 끝난는 가를 확인
    {
        while(!m_bDie)
        {
            if (m_PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName(m_strCurAnime))
            {
                while (m_PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
                {
                    //애니메이션이 안끝났다.
                    yield return null;
                }
                //애니메이션 종료
                m_PlayerAnimator.SetBool(m_strCurTrigger, false);
            }
            yield return null;
        }
    }

    public void Hit()
    {
        //애니메이션 이벤트, 충돌 처리 등을 체크
        //여기서 콜리더 히트 체크
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2, 1 << LayerMask.NameToLayer("Enemy"));

        if(colliders.Length != 0)
        {
            int iCurChar = GameManager.instance.ReturnCurPlayer();
            float fATK = float.Parse(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_ATK, iCurChar).ToString());
            float fCRI = float.Parse(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_CRI, iCurChar).ToString());

            for(int i = 0; i < colliders.Length; i++)
            {
                colliders[i].GetComponent<EnemyScript>().Damege(fATK, fCRI);
                //데미지 함수 모두 호출
            }      
        }
    }
}
