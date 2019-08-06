using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectPanel : MonoBehaviour
{
    private UIPanel m_GridPanel = null;
    private GameObject m_GridChar = null;
    private GameObject m_SelectCharMain;
    private GameObject m_SelectChar = null;    //내가 선택한 캐릭터의 프리팹
    private int m_iCharIndex;
    private List<GameObject> m_SelectPanel = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //하위 오브젝트인 그리드를 받아온다.
        m_GridPanel = transform.GetChild(2).GetComponent<UIPanel>(); //ui 패널
        m_SelectCharMain = transform.Find("SelectCharModel").GetComponent<GameObject>();  //선택된 캐릭터를 보여준는 자리
        m_GridChar = m_GridPanel.transform.GetChild(0).gameObject;   //해당 패널이 가지는 차일드 패널(캐릭터 선택 스크롤뷰를 위해서), 이 오브젝트의 하위에 정렬된다.

        ////활성화 될 때 호출\
        int iCount = UserInfo.instance.GetMyCharCount();

        for (int i = 0; i < iCount; i++)
        {
            GameObject CharInfo = PoolManager.instance.PopFromPool(EnumClass.POOL_INDEX.CHAR_INFO_POOL.ToString());//만들어 놓은 오브젝트를 다시 가져와서 쓴다.
            CharInfo.SetActive(true);
            CharInfo.transform.SetParent(m_GridChar.transform, false); //부모 트랜스폼 새로 설정
            CharInfo.GetComponent<CharInfoButton>().SetCallBack(CharInfoSelect, i);
            CharInfo.GetComponentInChildren<UILabel>().text = UserInfo.instance.GetCharData(CharacterData.CHAR_ENUM.CHAR_NAME, i) as string;
            m_SelectPanel.Add(CharInfo);
        }
        m_GridPanel.Refresh();
        m_GridChar.GetComponent<UIGrid>().Reposition(); //리 포지셔닝으로 그리드 재정렬
        //CharInfoSelect(0);  //첫번째로 셋팅
    }

    private void OnDisable()
    {
        for(int i = 0; i < m_SelectPanel.Count; i++)
        {
            PoolManager.instance.PushToPool(EnumClass.POOL_INDEX.CHAR_INFO_POOL.ToString(), m_SelectPanel[i]);
            //패널에 쓰였던 오브젝트를 다시 반환
        }

        if(m_SelectChar != null)
        {
            PoolManager.instance.PushToPool(EnumClass.POOL_INDEX.USER_CHAR_POOL.ToString(), m_iCharIndex, m_SelectChar);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void CharInfoSelect(int iIndex)
    {
        m_SelectChar = PoolManager.instance.PopFromPool(EnumClass.POOL_INDEX.USER_CHAR_POOL.ToString(), iIndex); //해당 인덱스를 반환해서 크리에이트
        //현재 지정한 캐릭터를 세팅
        m_iCharIndex = iIndex;
        m_SelectChar.transform.SetParent(m_SelectCharMain.transform, false);
        m_SelectChar.GetComponent<Animator>().runtimeAnimatorController = UserInfo.instance.GetCharAnimator(iIndex, CharacterData.CHAR_ANIMATOR.CHAR_LOBBY_ANIMATOR) as RuntimeAnimatorController;
        //캐릭터 선택하면 메인 화면에 해당 캐릭터의 프리펩이 뜬다.
        GameManager.instance.CharSelect(iIndex);    //현재 선택한 캐릭터를 임시 저장
    }
}
