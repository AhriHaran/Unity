using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KEY_INPUT
{
    KEY_CLICK,  //한 번 입력
    KEY_PRESS,  //입력 유지
    KEY_NONE,
}

public enum KEY_TYPE
{
    KEY_ATTACK,
    KEY_EVASION,
    KEY_UITIMATE,
}
//소모 SP

public class PlayerScript : MonoBehaviour
{
    public struct st_Key
    {
        public int st_iIndex;  //콤보 인덱스
        public int st_iKey; //키
        public KEY_INPUT st_eInput;  //키 입력 타입
    }

    public struct st_Ultimate
    {
        public int st_iKey; //키
        public KEY_INPUT st_eInput;  //키 입력 타입
        public int st_iSpendSP;
    }

    public float m_fSpeed = 1.0f;   //이동속도
    public float m_fRotateSpeed = 2.0f; //회전속도
    public float m_fAniSpeed = 1.5f;    //애니메이션 속도
    public int m_iIndex;    //플레이어 캐릭터 인덱스

    private CharacterController m_Controller;
    private Animator m_PlayerAnimator;
    private bool m_bDie;
    private UISlider m_HpSlider = null;
    private UISlider m_SpSlider = null;
    private UIJoystick m_Input = null;
    private float m_fMaxHP = 0.0f; //맥스 HP
    private float m_fCurHP = 0.0f; //현재 HP
    private float m_fMaxSP = 0.0f; //맥스 SP
    private float m_fCurSP = 0.0f; //현재 SP

    //공격 관련
    private List<List<st_Key>> m_ListComboKey = new List<List<st_Key>>();
    private List<GameObject> m_ListKey = new List<GameObject>();
    private st_Ultimate m_UltimateSkill = new st_Ultimate();
    private KEY_INPUT m_eInput; //입력 방식
    private bool m_bAttack;
    public int m_iCurKey;  //현재 콤보 단계
    public float m_fAttackTime = 1.0f;  //공격 유지 시간
    public float m_fCurAttackTime = 0.0f;   //현재 공격 후 걸린시간.

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
        m_iCurKey = 0;
        m_fCurAttackTime = 0.0f;
        m_eInput = KEY_INPUT.KEY_NONE;

        StartCoroutine(CheckAttackState());
    }
    /*  캐릭터 변경 시에 어떻게 할까 처리 해야 할 것들
     *  OnEnable에서 설정
     * 현재 캐릭터 좌표로 설정 -> GameManger에 좌표를 저장 해두고 이를 토대로 변경
     * 키 입력은 런타임으로 설정
     * 
     */
    public void PlayerSet()
    {
        if (m_HpSlider != null && m_SpSlider != null)
        {
            float fHP = ((float)m_fCurHP / (float)m_fMaxHP);
            float fSP = ((float)m_fCurSP / (float)m_fMaxSP);

            m_HpSlider.value = fHP; //현재 HP
            m_HpSlider.GetComponentInChildren<UILabel>().text = (m_fCurHP.ToString() + "/" + m_fMaxHP.ToString());//라벨
            m_SpSlider.value = fSP; //현재 SP
            m_SpSlider.GetComponentInChildren<UILabel>().text = (m_fCurSP.ToString() + "/" + m_fMaxSP.ToString());//라벨
        }
        //hp와 sp를 설정하고

        //버튼 키를 설정한다.
        foreach(var v in m_ListKey)
        {
            v.GetComponent<PlayerKeyButton>().KeySetting(m_iIndex); //키 버튼 인덱스 교체
        }
    }

    public void PlayerInit()
    {
        GameObject UI = GameObject.Find("GameUI");
        GameObject playerUI = UI.transform.GetChild(5).gameObject;
        EventDelegate onClick = new EventDelegate(gameObject.GetComponent<PlayerScript>(), "OnClick");
        EventDelegate onPress = new EventDelegate(gameObject.GetComponent<PlayerScript>(), "OnPress");
        for (int i = 1; i < 4; i++)
        {
            m_ListKey.Add(playerUI.transform.GetChild(i).gameObject);
        }

        m_ListKey[0].GetComponent<UIEventTrigger>().onClick.Add(new EventDelegate(gameObject.GetComponent<PlayerScript>(), "OnAttack"));
        m_ListKey[1].GetComponent<UIEventTrigger>().onClick.Add(new EventDelegate(gameObject.GetComponent<PlayerScript>(), "OnEvasion"));
        m_ListKey[2].GetComponent<UIEventTrigger>().onClick.Add(new EventDelegate(gameObject.GetComponent<PlayerScript>(), "OnUltimate"));

        if (m_HpSlider == null && m_SpSlider == null)
        {
            m_HpSlider = UI.transform.GetChild(1).GetComponent<UISlider>();//hp 바
            m_SpSlider = UI.transform.GetChild(2).GetComponent<UISlider>();//sp 바

            m_fMaxHP = float.Parse(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_MAX_HP, m_iIndex).ToString());
            m_fCurHP = m_fMaxHP;

            m_fMaxSP = float.Parse(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_MAX_SP, m_iIndex).ToString());
            m_fCurSP = 0.0f;
        }
        m_Input = UI.transform.GetChild(5).GetComponentInChildren<UIJoystick>();
        //초기 셋팅

        string strExcel = "Excel/CharacterExcel/" + Util.ConvertToString(m_iIndex) + "_KeyControl";
        var Key = EXCEL.ExcelLoad.Read(strExcel);
        string[] KeyList = Key[0]["Key"].ToString().Split(',');
        KeyList = KeyList[0].Split(';');

        m_UltimateSkill.st_eInput = (KEY_INPUT)Util.ConvertToInt(KeyList[0]);
        m_UltimateSkill.st_iKey = Util.ConvertToInt(KeyList[1]);
        m_UltimateSkill.st_iSpendSP = Util.ConvertToInt(KeyList[2]);
        //키, 입력 타입, 소모값

        for (int i = 1; i < Key.Count; i++)
        {
            string index = Key[i]["Index"].ToString();
            KeyList = Key[i]["Key"].ToString().Split(',');
            
            List<st_Key> ListNode = new List<st_Key>();
            for (int j = 0; j < KeyList.Length; j++)
            {
                st_Key Node = new st_Key();
                string [] NodeKey = KeyList[j].Split(';');
    
                Node.st_iIndex = j;
                Node.st_iKey = Util.ConvertToInt(NodeKey[0]);
                Node.st_eInput = (KEY_INPUT)Util.ConvertToInt(NodeKey[1]);
                ListNode.Add(Node);
            }
            m_ListComboKey.Add(ListNode);
        }
        //여기서 플레이어 키 셋팅
    }

    //trail renderer

    void Start() //셋팅
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_bAttack)
            KeyControll();
        
    }
    
    void OnAttack()
    {
        //공격 키
        //OnCl
        m_eInput = KEY_INPUT.KEY_CLICK;
        m_bAttack = true;
        if (CollectKeyInput())
        {
            //제대로 클릭 하였다.
            m_PlayerAnimator.SetBool("Attack", true);
            m_PlayerAnimator.SetInteger("ComboCount", m_iCurKey);
            m_fCurAttackTime = 0.0f;    //콤보 성공시 초기화
            m_iCurKey++;
        }
        else
        {
            m_iCurKey = 0;
            m_PlayerAnimator.SetBool("Attack", false);
            m_PlayerAnimator.SetInteger("ComboCount", m_iCurKey);
        }
    }

    void OnEvasion()
    {
        //회피 버튼

    }

    void OnUltimate()
    {
        //궁극기
        //SP가 특정 이상이면 궁극기 발동 하지만 SP가 특정 이하면 기본 공격 콤보
        m_eInput = KEY_INPUT.KEY_CLICK;
        m_bAttack = true;
        if (CollectKeyInput())
        {
            //제대로 클릭 하였다.
            m_PlayerAnimator.SetBool("Ultimate", true);
            m_PlayerAnimator.SetInteger("ComboCount", m_iCurKey);
            m_fCurAttackTime = 0.0f;    //콤보 성공시 초기화
            m_iCurKey++;
        }
        else
        {
            m_iCurKey = 0;
            m_PlayerAnimator.SetBool("Ultimate", false);
            m_PlayerAnimator.SetInteger("ComboCount", m_iCurKey);
        }
    }

    bool CollectKeyInput()
    {
        if(m_bAttack)   //공격 유지 시간 내에
        {
            for (int i = 0; i < m_ListComboKey.Count; i++)  //내가 가진 콤보 리스트 중
            {
                if (m_ListComboKey[i].Count > m_iCurKey && m_ListComboKey[i][m_iCurKey].st_eInput == m_eInput)
                {
                    //알맞은 키 입력 방식을 현재단계에서 사용하였다.
                    return true;
                }
            }
        }
        return false;
    }

    void KeyControll()
    {
        var Input = m_Input.GetInput();

        if (Input != Vector2.zero)
        {
            m_PlayerAnimator.SetTrigger("Run");
            m_PlayerAnimator.speed = m_fAniSpeed;
            var Angle = new Vector3(transform.eulerAngles.x, Mathf.Atan2(Input.x, Input.y) * Mathf.Rad2Deg, transform.eulerAngles.z);
            transform.eulerAngles = Angle;
            m_Controller.Move(transform.forward * m_fSpeed * Time.deltaTime);
        }
        else
        {
            m_PlayerAnimator.SetTrigger("Stand");
            m_PlayerAnimator.speed = m_fAniSpeed;
        }
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
                if(m_fCurAttackTime >= m_fAttackTime)   //모든 게 초기화
                {
                    m_bAttack = false;
                    m_PlayerAnimator.SetBool("Attack", false);
                    m_PlayerAnimator.SetBool("Ultimate", false);
                    m_iCurKey = 0;
                    m_PlayerAnimator.SetInteger("ComboCount", m_iCurKey);
                    m_fCurAttackTime = 0.0f;
                }
            }
            
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
                EnemyScript script = colliders[i].GetComponent<EnemyScript>() ?? null;
                if (script != null)
                    script.Damege(fATK, fCRI);
                //해당 함수 호출
            }      
        }
    }
}
