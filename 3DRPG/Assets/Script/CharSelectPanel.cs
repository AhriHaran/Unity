using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectPanel : MonoBehaviour
{
    private UIPanel m_GridPanel = null;
    private GameObject m_GridChar = null;
    private GameObject m_SelectCharMain;
    private GameObject m_SelectChar;    //내가 선택한 캐릭터의 프리팹


    private List<GameObject> m_SelectPanel = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        //하위 오브젝트인 그리드를 받아온다.
        m_GridPanel = transform.GetChild(1).GetComponent<UIPanel>(); //ui 패널
        m_SelectCharMain = transform.GetChild(3).GetComponent<GameObject>();  //선택된 캐릭터를 보여준는 자리
        m_GridChar = m_GridPanel.transform.GetChild(0).gameObject;   //해당 패널이 가지는 차일드 패널(캐릭터 선택 스크롤뷰를 위해서), 이 오브젝트의 하위에 정렬된다.

        //활성화 될 때 호출\
        int iCount = UserInfo.instance.GetMyCharCount();

        for (var i = 0; i < iCount; i++)
        {
            GameObject CharInfo = PoolManager.instance.PopFromPool("Prefabs/CharInfoButton");//만들어 놓은 오브젝트를 다시 가져와서 쓴다.
            CharInfo.SetActive(true);
            CharInfo.transform.SetParent(m_GridChar.transform); //부모 트랜스폼 새로 설정
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
            PoolManager.instance.PushToPool("Prefabs/CharInfoButton", m_SelectPanel[i]);
            //패널에 쓰였던 오브젝트를 다시 반환
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void CharInfoSelect(int iIndex)
    {
        m_SelectChar = UserInfo.instance.GetCharPrefabs(iIndex); //해당 인덱스를 반환해서 크리에이트
        //현재 지정한 캐릭터를 세팅
        m_SelectChar.transform.SetParent(m_SelectCharMain.transform);
        m_SelectChar.transform.localPosition = new Vector3(0, 0, 0);
        m_SelectChar.transform.localRotation = new Quaternion();
        m_SelectChar.transform.localScale = new Vector3(1, 1, 1);

        m_SelectChar.GetComponent<Animator>().runtimeAnimatorController = UserInfo.instance.GetCharAnimator(iIndex, CharacterData.CHAR_ANIMATOR.CHAR_LOBBY_ANIMATOR) as RuntimeAnimatorController;
        //캐릭터 선택하면 메인 화면에 해당 캐릭터의 프리펩이 뜬다.
        GameManager.instance.CharSelect(iIndex);    //현재 선택한 캐릭터를 임시 저장
    }
}
