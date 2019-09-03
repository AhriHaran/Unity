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
        public KEY_TYPE st_iKey; //키
        public KEY_INPUT st_eInput;  //키 입력 타입
    }

    public struct st_Ultimate
    {
        public KEY_TYPE st_iKey; //키
        public KEY_INPUT st_eInput;  //키 입력 타입
        public int st_iSpendSP;
    }

    public float m_fSpeed = 5.0f;   //이동속도
    public float m_fRotateSpeed = 2.0f; //회전속도
    public float m_fAniSpeed = 1.5f;    //애니메이션 속도
    public int m_iIndex;    //플레이어 캐릭터 인덱스
    public bool m_bDie = false;
    public int m_iRange;

    private CharacterController m_Controller;
    private Animator m_PlayerAnimator;
    private GameObject m_UltimateEffect;
    private UISlider m_HpSlider = null;
    private UISlider m_SpSlider = null;
    private UIJoystick m_Input = null;
    public float m_fMaxHP
    {
        get { return m_fMaxHP; }
        set { m_fMaxHP = 0.0f; }
    }
    public float m_fCurHP
    {
        get { return m_fCurHP; }
        set { m_fCurHP = 0.0f; }
    }
    public float m_fMaxSP
    {
        get { return m_fMaxSP; }
        set { m_fMaxSP = 0.0f; }
    }
    public float m_fCurSP
    {
        get { return m_fCurSP; }
        set { m_fCurSP = 0.0f; }
    }

    private List<List<st_Key>> m_ListComboKey = new List<List<st_Key>>();
    private List<GameObject> m_ListKey = new List<GameObject>();
    private st_Ultimate m_UltimateSkill = new st_Ultimate();

    private bool m_bAttack;
    private bool m_bInvincible = false;
    private KEY_INPUT m_eInput;
    public int m_iCurKey;  //현재 콤보 단계
    public float m_fCurAttackTime = 0.0f;   //현재 공격 후 걸린시간.
    private float m_fAttackTime = 1.5f;  //공격 유지 시간
    public float m_fCurPressTime = 0.0f;
    private float m_fPressTime = 0.6f;

    private bool m_bUltimateReady = true;
    private bool m_bInvincibleReady = true;
    
    private float m_fCurInvisible = 0.0f;
    private float m_fInvisibleTime = 0.5f;

    //플레이어의 조종에 따른 스크립트
    // Start is called before the first frame update
    private void Awake()
    {
        m_Controller = GetComponent<CharacterController>();
        m_PlayerAnimator = GetComponent<Animator>();
    }
    
    private void OnEnable()
    {
        if(!m_bDie)
        {
            ResetData();
            StartCoroutine(CheckAttackState());
        }
    }
    /*  캐릭터 변경 시에 어떻게 할까 처리 해야 할 것들
     *  OnEnable에서 설정
     * 현재 캐릭터 좌표로 설정 -> GameManger에 좌표를 저장 해두고 이를 토대로 변경
     * 키 입력은 런타임으로 설정
     * 
     */

    public void SliderUpdate()
    {
        float fHP = ((float)m_fCurHP / (float)m_fMaxHP);
        float fSP = ((float)m_fCurSP / (float)m_fMaxSP);

        m_HpSlider.value = fHP; //현재 HP
        m_HpSlider.GetComponentInChildren<UILabel>().text = (m_fCurHP.ToString() + "/" + m_fMaxHP.ToString());//라벨
        m_SpSlider.value = fSP; //현재 SP
        m_SpSlider.GetComponentInChildren<UILabel>().text = (m_fCurSP.ToString() + "/" + m_fMaxSP.ToString());//라벨
    }

    public void PlayerSet()
    {
        if (m_HpSlider != null && m_SpSlider != null)
        {
            SliderUpdate();
        }
        //hp와 sp를 설정하고

        //버튼 키를 설정한다.
        for(int i = 0; i < 3; i++)
        {
            UIEventTrigger Event = m_ListKey[i].GetComponent<UIEventTrigger>();
            Event.onPress.Clear();
            Event.onClick.Clear();
            Event.onRelease.Clear();

            switch ((KEY_TYPE)i)
            {
                case KEY_TYPE.KEY_ATTACK:
                    Event.onPress.Add(new EventDelegate(gameObject.GetComponent<PlayerScript>(), "OnPress"));
                    Event.onRelease.Add(new EventDelegate(gameObject.GetComponent<PlayerScript>(), "OnAttack"));
                    break;
                case KEY_TYPE.KEY_EVASION:
                    m_ListKey[i].transform.GetChild(2).GetComponent<CoolTime>().CallBackSet(InvincibleCallBack);
                    Event.onClick.Add(new EventDelegate(gameObject.GetComponent<PlayerScript>(), "OnEvasion"));
                    break;
                case KEY_TYPE.KEY_UITIMATE:
                    m_ListKey[i].transform.GetChild(2).GetComponent<CoolTime>().CallBackSet(UltimateReady);
                    Event.onPress.Add(new EventDelegate(gameObject.GetComponent<PlayerScript>(), "OnPress"));
                    Event.onRelease.Add(new EventDelegate(gameObject.GetComponent<PlayerScript>(), "OnUltimate"));
                    break;
            }
        }
    }

    public void PlayerInit(GameObject Ultimate)
    {
        GameObject UI = GameObject.Find("GameUI");
        GameObject playerUI = UI.transform.GetChild(6).gameObject;
        for (int i = 1; i < 4; i++)
        {
            m_ListKey.Add(playerUI.transform.GetChild(i).gameObject);
        }
<<<<<<< HEAD
        m_fMaxHP = float.Parse(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_MAX_HP, m_iIndex).ToString());
        m_fCurHP = m_fMaxHP;
=======

        if (m_HpSlider == null && m_SpSlider == null)
        {
            m_HpSlider = UI.transform.GetChild(1).GetComponent<UISlider>();//hp 바
            //m_SpSlider = UI.transform.GetChild(2).GetComponent<UISlider>();//sp 바
>>>>>>> parent of 03330eb... 2019_09_03_First

            m_fMaxHP = float.Parse(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_MAX_HP, m_iIndex).ToString());
            m_fCurHP = m_fMaxHP;

            m_fMaxSP = float.Parse(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_MAX_SP, m_iIndex).ToString());
            m_fCurSP = 0.0f;
        }
        m_Input = playerUI.GetComponentInChildren<UIJoystick>();
        //초기 셋팅

        string strExcel = "Excel/CharacterExcel/" + Util.ConvertToString(m_iIndex) + "_KeyControl";
        var Key = EXCEL.ExcelLoad.Read(strExcel);
        string[] KeyList = Key[0]["Key"].ToString().Split('/');
        KeyList = KeyList[0].Split(';');

        m_UltimateSkill.st_iKey = (KEY_TYPE)Util.ConvertToInt(KeyList[0]);
        m_UltimateSkill.st_eInput = (KEY_INPUT)Util.ConvertToInt(KeyList[1]);
        m_UltimateSkill.st_iSpendSP = Util.ConvertToInt(KeyList[2]);
        //키, 입력 타입, 소모값

        for (int i = 1; i < Key.Count; i++)
        {
            string index = Key[i]["Index"].ToString();
            KeyList = Key[i]["Key"].ToString().Split('/');

            List<st_Key> ListNode = new List<st_Key>();
            for (int j = 0; j < KeyList.Length; j++)
            {
                st_Key Node = new st_Key();
                string[] NodeKey = KeyList[j].Split(';');

                Node.st_iIndex = j;
                Node.st_iKey = (KEY_TYPE)Util.ConvertToInt(NodeKey[0]);
                Node.st_eInput = (KEY_INPUT)Util.ConvertToInt(NodeKey[1]);
                ListNode.Add(Node);
            }
            m_ListComboKey.Add(ListNode);
        }
        //여기서 플레이어 키 셋팅
        m_UltimateEffect = Ultimate;
    }
    
    
    //trail renderer

    void Start() //셋팅
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_bAttack)
            KeyControll();
    }
    
    void OnPress()
    {
        StartCoroutine("OnPressTime");
    }

    IEnumerator OnPressTime()
    {
        while(true)
        {
            yield return null;
            m_fCurPressTime += Time.deltaTime;
            //처음 누를때 시간만 체크하고
            if (m_fCurPressTime >= m_fPressTime)
                m_eInput = KEY_INPUT.KEY_PRESS;
            else if (m_fCurPressTime < m_fPressTime)
                m_eInput = KEY_INPUT.KEY_CLICK;
        }
    }

    void OnAttack()
    {
        //공격 키
        //OnCl
        m_bAttack = true;
        if (CollectKeyInput())
        {
            //제대로 클릭 하였다.
            m_PlayerAnimator.SetBool("Attack", true);
            m_PlayerAnimator.SetInteger("ComboCount", m_iCurKey);
            m_fCurAttackTime = 0.0f;    //콤보 성공시 초기화
            m_fCurPressTime = 0.0f;
            m_iCurKey++;
        }
        else
        {
            m_fCurAttackTime = m_fAttackTime;    //콤보 성공시 초기화
            m_fCurPressTime = 0.0f;
            m_iCurKey = 0;
            m_PlayerAnimator.SetBool("Attack", false);
            m_PlayerAnimator.SetInteger("ComboCount", m_iCurKey);
        }
        StopCoroutine("OnPressTime");
    }

    void OnEvasion()
    {
        //회피 버튼, 회피 시에는 무적
        if(m_bInvincibleReady)
        {
            m_PlayerAnimator.SetTrigger("Slide");
            m_bInvincible = true;
            m_bInvincibleReady = false;
            m_ListKey[(int)KEY_TYPE.KEY_EVASION].transform.GetChild(2).GetComponent<CoolTime>().OnClick();
            StartCoroutine("CheckInvincible");
        }
    }

    public void InvincibleCallBack()
    {
        m_bInvincibleReady = true;
    }

    void OnUltimate()
    {
        //궁극기
        //SP가 특정 이상이면 궁극기 발동 하지만 SP가 특정 이하면 기본 공격 콤보
        m_bAttack = true;
        if(m_fCurSP >= m_UltimateSkill.st_iSpendSP && m_UltimateSkill.st_eInput == m_eInput && m_bUltimateReady)
        {
            //sp가 다 모이고 올바른 키 타입이면
            m_PlayerAnimator.SetTrigger("UltimateActive");
            m_fCurSP -= m_UltimateSkill.st_iSpendSP;
            m_UltimateEffect.transform.position = transform.position;
            m_UltimateEffect.gameObject.SetActive(true);
            m_bUltimateReady = false;
            m_ListKey[(int)KEY_TYPE.KEY_UITIMATE].transform.GetChild(2).GetComponent<CoolTime>().OnClick();
            
            ResetData();
            //궁쓰면 일단 초기화
            SliderUpdate();
        }
        else
        {
            if (CollectKeyInput())
            {
                //제대로 클릭 하였다.
                m_PlayerAnimator.SetBool("Ultimate", true);
                m_PlayerAnimator.SetInteger("ComboCount", m_iCurKey);
                m_fCurAttackTime = 0.0f;    //콤보 성공시 초기화
                m_fCurPressTime = 0.0f;
                m_iCurKey++;
            }
            else
            {
                m_fCurAttackTime = m_fAttackTime;    //콤보 실패
                m_fCurPressTime = 0.0f;
                m_iCurKey = 0;
                m_PlayerAnimator.SetBool("Ultimate", false);
                m_PlayerAnimator.SetInteger("ComboCount", m_iCurKey);
            }
        }
        StopCoroutine("OnPressTime");
    }

    public void UltimateReady()
    {
        m_bUltimateReady = true;
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

    IEnumerator CheckInvincible()
    {
        while(m_bInvincible)
        {
            yield return null;

            m_fCurInvisible += Time.deltaTime;

            if(m_fCurInvisible >= m_fInvisibleTime)
            {
                m_bInvincible = false;
                m_fCurInvisible = 0.0f;
            }
        }
        //무적 유지 시간
    }

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
                    ResetData();
                }
            }
            
        }
    }

    public void Hit()
    {
        //애니메이션 이벤트, 충돌 처리 등을 체크
        //여기서 콜리더 히트 체크
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_iRange, 1 << LayerMask.NameToLayer("Enemy"));

        if(colliders.Length != 0)
        {
            int iCurChar = GameManager.instance.ReturnCurPlayer();
            int[] List = GameManager.instance.ReturnPlayerList();
            float fATK = float.Parse(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_ATK, List[iCurChar]).ToString());
            float fCRI = float.Parse(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_CRI, List[iCurChar]).ToString());

            for(int i = 0; i < colliders.Length; i++)
            {
                EnemyScript script = colliders[i].GetComponent<EnemyScript>() ?? null;
                if (script != null)
                    script.Damege(fATK, fCRI);
                //해당 함수 호출
            }      
        }
    }

    public void DropItem(POOL_INDEX eIndex, int iUpFactor, GameObject Item)
    {
        switch (eIndex)
        {
            case POOL_INDEX.POOL_HP_ITEM:   //hp회복
                m_fCurHP += (float)iUpFactor;
                if (m_fCurHP >= m_fMaxHP)
                    m_fCurHP = m_fMaxHP;
                break;
            case POOL_INDEX.POOL_SP_ITEM:
                m_fCurSP += (float)iUpFactor;
                if (m_fCurSP >= m_fMaxSP)
                    m_fCurSP = m_fMaxSP;
                break;
        }
        PoolManager.instance.PushToPool(eIndex.ToString(), Item);
        SliderUpdate();
    }

    public void ResetData()
    {
        m_bAttack = false;
        m_eInput = KEY_INPUT.KEY_NONE;
        m_PlayerAnimator.SetBool("Attack", false);
        m_PlayerAnimator.SetBool("Ultimate", false);
        m_iCurKey = 0;
        m_PlayerAnimator.SetInteger("ComboCount", m_iCurKey);
        m_fCurAttackTime = 0.0f;
        m_fCurPressTime = 0.0f;
    }

    public void Damege(float fATK, float fCRI)
    {
        //적이 죽으면 일정확률로 체력 회복 아이템과 SP 회복 아이템을 드랍한다.
        if (!m_bDie && !m_bInvincible)
        {
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
                m_bDie = true;
                m_PlayerAnimator.SetTrigger("Die");
                ResetData();
                StopAllCoroutines();
            }
            SliderUpdate();
        }
    }
}