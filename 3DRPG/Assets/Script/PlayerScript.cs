using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float m_fSpeed = 7.0f;
    public float m_fRotateSpeed = 2.0f;
    public float m_fAniSpeed = 1.5f;

    private Rigidbody m_Rigidbody;
    private Animator m_PlayerAnimator;
    private Vector3 m_Vec3;
    private bool m_bAttack;
    private string m_strCurAnime;
    private string m_strCurTrigger;
    //플레이어의 조종에 따른 스크립트
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_PlayerAnimator = GetComponent<Animator>();
        m_Rigidbody.useGravity = true;
        m_bAttack = false;
        m_strCurAnime = string.Empty;
        m_strCurTrigger = string.Empty;
        StartCoroutine(CheckAnimationState());
    }

    // Update is called once per frame
    void Update()
    {
        KeyInput();
    }

    void KeyInput()
    {
        //공격 키

        if (Input.GetMouseButtonUp(0))
        {
            //왼쪽 키 입력
            m_strCurAnime = "Base Layer.Jab";
            m_strCurTrigger = "WeakAttack";
            m_PlayerAnimator.SetBool("WeakAttack", true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            //오른쪽 키 입력
            m_strCurAnime = "Base Layer.Hikick";
            m_strCurTrigger = "StrongAttack";
            m_PlayerAnimator.SetBool("StrongAttack", true);
        }
    }

    IEnumerator CheckAnimationState()   //애니메이션이 끝난는 가를 확인
    {
        while(true)
        {
            if (m_PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName(m_strCurAnime))
            {
                while (m_PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
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

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        m_PlayerAnimator.SetFloat("Speed", v);
        m_PlayerAnimator.SetFloat("Direction", h);
        m_PlayerAnimator.speed = m_fAniSpeed;   //애니메이션 속도
        //움직임에 따른 애니메이션

        m_Vec3.Set(0, 0, v);
        m_Vec3 = transform.TransformDirection(m_Vec3);
        if (v > 0.1)
        {
            m_Vec3 *= m_fSpeed;
        }
        //else if (v < -0.1)
        //{
        //    m_Vec3 *= backwardSpeed;  // 移動速度を掛ける
        //}

        transform.localPosition += m_Vec3 * Time.fixedDeltaTime;
        transform.Rotate(0, h * m_fRotateSpeed, 0);
    }
}
