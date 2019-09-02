using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButton : MonoBehaviour
{
    private UISprite m_KeySprite;
    private UISprite m_KeyLockSprite;
    private UISprite m_HPSprite;
    private UISprite m_SPSprite;
    private CoolTime m_CoolTime;
    private int m_iCharIndex;   //해당 캐릭터의 인덱스
    public int m_iListCount;    //해당 버튼이 상징하는 캐릭터 리스트 순서
    public bool m_bChange = true;
    // Start is called before the first frame update
    private void Awake()
    {
        m_KeySprite = transform.GetChild(1).GetComponent<UISprite>();
        m_HPSprite = transform.GetChild(3).GetComponent<UISprite>();
        m_SPSprite = transform.GetChild(4).GetComponent<UISprite>();
    }

    public void Change(int iIndex, int ListCount, float fHP, float fMaxHP, float fSP, float fMaxSP)
    {
        //교대 해야 한다.
        //교대 할 때 해당 캐릭터의 HP만큼의 잔여분으로 표시해준다.
        
        m_bChange = false;
        m_CoolTime.OnClick();
        string strIcon = Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, iIndex));
        m_iCharIndex = iIndex;
        m_iListCount = ListCount;
        m_KeySprite.spriteName = strIcon + "Icon";
        //아이콘 스프라이트 교체
        m_HPSprite.fillAmount = (fMaxHP - fHP) / fMaxHP;//현재 캐릭터의 남은 HP 표시
    }

    public void ChangeReady()
    {
        m_bChange = true;
    }

    void Start()
    {
        int[] iarr = GameManager.instance.ReturnPlayerList();
        m_iCharIndex = iarr[m_iListCount];

        if (m_iCharIndex != -1)
        {
            string strIcon = Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, m_iCharIndex));
            m_KeySprite.spriteName = strIcon + "Icon";
            gameObject.GetComponent<UIEventTrigger>().onClick.Add(new EventDelegate(GameObject.Find("GameScene").GetComponent<GameScene>(), "ChangeChar"));
            //이벤트 트리거 셋팅
            m_CoolTime = gameObject.GetComponentInChildren<CoolTime>();
            m_CoolTime.CallBackSet(ChangeReady);
            gameObject.SetActive(true);
            //활성화
        }
        else if (m_iCharIndex == -1)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
