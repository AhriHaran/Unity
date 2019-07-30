using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    enum KEY_ENUM
    {
        KEY_NONE,   //키 입력 없음
        KEY_LEFT,   //약공격
        KEY_RIGHT,  //강공격
        KEY_ULTI,   //궁극기
        KEY_AVOID,  //회피키

        KEY_INPUT = 100,  //단타
        KEY_PRESS,  //키 입력 유지
    }

    public struct Player_Key
    {
        public int st_Key;
        public int st_Key_Input;
        public int st_Action_Trigger;
    }
    
    public class PlayerControl : MonoBehaviour
    {
        public float m_AniSpeed = 1.5f;
        public float m_ForwardSpeed = 7.0f;
        public float m_BackwardSpeed = 2.0f;
        public float m_RotateSpeed = 2.0f;
        public float m_fComboTime = 0.5f;  //공격을 시작하면 일정 타임 내로 입력이 계속되면 콤보가 유지되고 입력이 끊기면 초기화

        private Animator m_PlayerAnime;
        private Rigidbody m_PlayerRigid;
        private int m_iAttackCount = 0;
        private bool m_bCombo = false;
        private bool m_bPress = false;
        private float m_fComboCurTime = 0.0f;
        private List<Player_Key[]> m_KeyList = new List<Player_Key[]>();
        static char[] SPRIT_EXCEL = { ',', ';' }; //공백제거용
        // Start is called before the first frame update
        void Start()
        {
            m_PlayerAnime = GetComponent<Animator>();
            m_PlayerRigid = GetComponent < Rigidbody>();
            const string File = "Excel/0Player_Combo_Key";

            List<Dictionary<string, object>> tmp = ExcelLoad.Read(File);

            for(int i = 0; i < tmp.Count; i++)
            {
                //Index	Key
                string [] Key = tmp[i]["Key"].ToString().Split(SPRIT_EXCEL);
                //"Key"의 밸류를 , 로 스플릿

                for(int j = 0; j < Key.Length; j++)
                {
                    string [] Value = Key[i].Split(SPRIT_EXCEL);
                    //타입 단타 타입, 프레스 타입


                }

                int KeyCount = int.Parse(Key);

                Player_Key[] Node = new Player_Key[KeyCount];//키 카운트
                for (int j = 0; j < KeyCount; j++)
                {
                    string str = "Key" + j;
                    string KeyValue = tmp[i][str].ToString(); //무슨 키/ 무슨 방식/ 얼마나
                    string[] arrKey = KeyValue.Split(new char[] { '/' });

                    Node[j].st_Key = int.Parse(arrKey[0]);  //무슨 키
                    Node[j].st_Key_Input = int.Parse(arrKey[1]);    //무슨 입력 방식
                    Node[j].st_Action_Trigger = int.Parse(arrKey[2]);   //무슨 액션 트리거
                }
                m_KeyList.Add(Node);
            }
            //선택된 캐릭터는 게임매니저가 들고 있다.
        }

        // Update is called once per frame
        void Update()
        {
            KeyInput();

            if(m_bCombo)
            {
                m_fComboCurTime += Time.deltaTime;
                if(m_fComboCurTime >= m_fComboTime) //콤보 입력 시간 초과
                {
                    ComboReset();
                }
            }

        }

        /*
         * 키 입력은 키가 업이 될 때 확인
         * 키 인풋에서 이를 모두 확인하고 트리거를 반환
         * 
         */
        private bool KeyInput(int KeyCode, int KeyInput, ref int ComboIndex)
        {
            for(int i =0; i < m_KeyList.Count; i++)
            {
                if ((m_KeyList[i][m_iAttackCount].st_Key == KeyCode) &&
                    (m_KeyList[i][m_iAttackCount].st_Key_Input == KeyInput))
                {
                    ComboIndex = i;
                    //맞는 키와 맞는 입력 방식
                    return true;
                }
            }
            return false;
        }

        private void ComboReset()
        {
            m_iAttackCount = 0;
            m_PlayerAnime.SetInteger("AttackCount", 0);    //현재 캐릭터의 액션 트리거
            m_fComboCurTime = 0.0f;
            m_bCombo = false;
            m_bPress = false;
        }

        private void KeyInput()
        {
            int ComboKey = 0;
            if (Input.GetMouseButtonUp(0))
            {
                if (KeyInput((int)KEY_ENUM.KEY_LEFT, (int)KEY_ENUM.KEY_INPUT,ref ComboKey))
                {
                    m_PlayerAnime.SetInteger("AttackCount", m_KeyList[ComboKey][m_iAttackCount].st_Action_Trigger);    //현재 캐릭터의 액션 트리거
                    m_iAttackCount++;
                    m_bCombo = true;
                }
                else
                {
                    ComboReset();
                    //리셋
                }
            }

            if (Input.GetMouseButton(0)) //키입력 유지
            {
                if(KeyInput((int)KEY_ENUM.KEY_LEFT, (int)KEY_ENUM.KEY_PRESS, ref ComboKey))
                {
                    m_PlayerAnime.SetInteger("AttackCount", m_KeyList[ComboKey][m_iAttackCount].st_Action_Trigger);    //현재 캐릭터의 액션 트리거
                    m_bPress = true;
                }
            }
            
        }

        private void FixedUpdate()  //움직임
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

   
        }
    }
}
