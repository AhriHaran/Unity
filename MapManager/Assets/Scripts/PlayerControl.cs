using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerControl : MonoBehaviour
    {
        public float m_AniSpeed = 1.5f;
        public float m_ForwardSpeed = 7.0f;
        public float m_BackwardSpeed = 2.0f;
        public float m_RotateSpeed = 2.0f;
        public float m_fComboTime = 0.7f;  //공격을 시작하면 일정 타임 내로 입력이 계속되면 콤보가 유지되고 입력이 끊기면 초기화
        public float m_fPressTime = 0.1f;

        private Animator m_PlayerAnime;
        private Rigidbody m_PlayerRigid;
        private int m_iAttackCount = 0;
        private bool m_bCombo = false;
        private float m_fComboCurTime = 0.0f;
        private float m_fPressCurTime = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            m_PlayerAnime = GetComponent<Animator>();
            m_PlayerRigid = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void FixedUpdate()
        {
            //리지드 바디를 이용해서 캐릭터의 움직임을 조절하므로 fixed
            float h = Input.GetAxis("Horizontal");  //회전값
            float v = Input.GetAxis("Vertical");    //속도값

            m_PlayerAnime.SetFloat("Speed", v);
            m_PlayerAnime.SetFloat("Direction", h);
            m_PlayerAnime.speed = m_AniSpeed;   //애니메이션 속도

            if(!m_bCombo)
            {
                Vector3 Speed = new Vector3(0, 0, v);   //움직임
                Speed = transform.TransformDirection(Speed);    //로컬 공간 -> 월드 공간

                if (v > 0.1) //
                {
                    Speed *= m_ForwardSpeed;    //전진
                }
                else if (v < -0.1)
                {
                    Speed *= m_BackwardSpeed;   //후진
                }

                transform.localPosition += Speed * Time.deltaTime;
                transform.Rotate(0, h * m_RotateSpeed, 0);
                //공격시는 움직이지 않는다.
            }
            else
            {
                //콤보가 끊기는가?
                m_fComboCurTime += Time.deltaTime;

                if (m_fComboCurTime >= m_fComboTime) //콤보 타임 초과
                {
                    m_fComboCurTime = 0.0f;
                    m_fPressCurTime = 0.0f;
                    m_iAttackCount = 0;
                    m_bCombo = false;
                }
            }

            if (Input.GetMouseButton(0))
            {
                if(m_iAttackCount == 1) //두번째 입력부터 꾸욱 눌렀다가 때야지 콤보 유지
                {
                    m_fPressCurTime += Time.deltaTime;
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                if((m_fPressCurTime >= m_fPressTime && m_iAttackCount == 1) || m_iAttackCount == 0)
                    m_iAttackCount++;
                else if(m_iAttackCount >= 2)
                    m_iAttackCount = 1;

                m_bCombo = true;
                m_fComboCurTime = 0.0f;
                m_fPressCurTime = 0.0f;
            }

            m_PlayerAnime.SetInteger("AttackCount", m_iAttackCount);
        }
    }
}
