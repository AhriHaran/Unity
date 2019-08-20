using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectPanel : MonoBehaviour
{
    private GameObject m_GridPanel = null;
    private GameObject m_GridChar = null;

    private GameObject m_SelectCharMain;
    private GameObject m_SelectChar = null;    //내가 선택한 캐릭터의 프리팹
    private int m_iCharIndex = -1;
    private List<GameObject> m_SelectPanel = new List<GameObject>();
    private string m_strSprite = "Icon";
    // Start is called before the first frame update
    private void Awake()
    {
        //하위 오브젝트인 그리드를 받아온다.
        m_SelectCharMain = GameObject.Find("SelectCharModel");  //선택된 캐릭터를 보여준는 자리
        m_GridPanel = transform.GetChild(0).gameObject; //ui 패널
        m_GridChar = m_GridPanel.transform.GetChild(0).gameObject;   //해당 패널이 가지는 차일드 패널(캐릭터 선택 스크롤뷰를 위해서), 이 오브젝트의 하위에 정렬된다.
    }

    void Start()
    {
        ////활성화 될 때 호출\
        ///
        var CharList = UserInfo.instance.GetMyCharList();
        int iCount = CharList.Count;

        for (int i = 0; i < iCount; i++)
        {
            GameObject CharInfo =ResourceLoader.CreatePrefab("Prefabs/CharInfoButton");//만들어 놓은 오브젝트를 다시 가져와서 쓴다.
            CharInfo.SetActive(true);
            CharInfo.transform.SetParent(m_GridChar.transform, false); //부모 트랜스폼 새로 설정
            CharInfo.GetComponent<CharInfoButton>().SetCallBack(CharInfoSelect, i);
            string strIcon = Util.ConvertToString(CharList[i].GetCharData(CHAR_DATA.CHAR_NAME));
            CharInfo.GetComponentInChildren<UILabel>().text = strIcon;
            strIcon += m_strSprite;
            CharInfo.GetComponentInChildren<UISprite>().spriteName = strIcon;
            m_SelectPanel.Add(CharInfo);
        }
        m_GridChar.GetComponent<UIGrid>().Reposition(); //리 포지셔닝으로 그리드 재정렬
        m_GridPanel.GetComponent<UIScrollView>().ResetPosition();
        m_GridPanel.GetComponent<UIPanel>().Refresh();
    }

    private void OnEnable()
    {
        CharInfoSelect(0);  //첫번째로 셋팅
    }

    private void OnDisable()
    {
        if(m_SelectChar != null)
        {
            PoolManager.instance.PushToPool(POOL_INDEX.POOL_USER_CHAR.ToString(), m_iCharIndex, m_SelectChar);
            m_SelectChar = null;
            m_iCharIndex = -1;
            //사용한 캐릭터 오브젝트 반납
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void CharInfoSelect(int iIndex)
    {
        if(m_iCharIndex != iIndex)  //중복체크 방지
        {
            m_SelectChar = PoolManager.instance.PopFromPool(POOL_INDEX.POOL_USER_CHAR.ToString(), iIndex); //해당 인덱스를 반환해서 크리에이트
                                                                                                                     //현재 지정한 캐릭터를 세팅
            m_SelectChar.SetActive(true);
            m_iCharIndex = iIndex;
            m_SelectChar.transform.SetParent(m_SelectCharMain.transform, false);
            m_SelectChar.GetComponent<Animator>().runtimeAnimatorController = UserInfo.instance.GetCharAnimator(iIndex, CHAR_ANIMATOR.CHAR_LOBBY_ANIMATOR) as RuntimeAnimatorController;
            //캐릭터 선택하면 메인 화면에 해당 캐릭터의 프리펩이 뜬다.
            GameManager.instance.CharSelect(iIndex);    //현재 선택한 캐릭터 인덱스를 저장
        }
    }
}
