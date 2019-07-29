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

        private Animator m_PlayerAnime;
        private Rigidbody m_PlayerRigid;

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


            Vector3 Speed = new Vector3(0, 0, v);   //움직임
            Speed = transform.TransformDirection(Speed);    //로컬 공간 -> 월드 공간

            if(v > 0.1) //
            {
                Speed *= m_ForwardSpeed;    //전진
            }
            else if(v < -0.1)
            {
                Speed *= m_BackwardSpeed;   //후진
            }

            transform.localPosition += Speed * Time.deltaTime;
            transform.Rotate(0, h * m_RotateSpeed, 0);

        }
    }
}
