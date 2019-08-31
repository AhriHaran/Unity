using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButton : MonoBehaviour
{
    private UISprite m_KeySprite;
    private UISprite m_KeyLockSprite;
    private int m_iCharIndex;   //해당 캐릭터의 인덱스
    public int m_iListCount;    //해당 버튼이 상징하는 캐릭터 리스트 순서
    // Start is called before the first frame update
    private void Awake()
    {
        m_KeySprite = transform.GetChild(1).GetComponent<UISprite>();
        m_KeyLockSprite = transform.GetChild(2).GetComponent<UISprite>();
        m_KeyLockSprite.gameObject.SetActive(false);
        int[] iarr = GameManager.instance.ReturnPlayerList();
        
        m_iCharIndex = iarr[m_iListCount];

        if(m_iCharIndex != -1)
        {
            string strIcon = Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, m_iCharIndex));
            m_KeySprite.spriteName = strIcon + "Icon";
            gameObject.GetComponent<UIEventTrigger>().onClick.Add(new EventDelegate(GameObject.Find("GameScene").GetComponent<GameScene>(), "ChangeChar"));
            //이벤트 트리거 셋팅
            gameObject.SetActive(true);
            //활성화
        }
        else if(m_iCharIndex == -1)
        {
            gameObject.SetActive(false);
        }
    }

    private void Change(int iIndex)
    {
        //교대 해야 한다.
        string strIcon = Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, iIndex));
        m_iCharIndex = iIndex;
        m_KeySprite.spriteName = strIcon + "Icon";
        //아이콘 스프라이트 교체
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
