using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectPanel : MonoBehaviour
{
    private UIPanel m_GridPanel = null;
    private GameObject m_GridChar = null;
    private GameObject m_SelectChar;
    // Start is called before the first frame update
    void Start()
    {
        //하위 오브젝트인 그리드를 받아온다.
        m_GridPanel = transform.GetChild(1).GetComponent<UIPanel>(); //ui 패널
        m_SelectChar = transform.GetChild(3).GetComponent<GameObject>();  //선택된 캐릭터를 보여준는 자리
        m_GridChar = m_GridPanel.transform.GetChild(0).gameObject;   //해당 패널이 가지는 차일드 패널(캐릭터 선택 스크롤뷰를 위해서), 이 오브젝트의 하위에 정렬된다.
    }

    private void OnEnable()
    {
        //활성화 될 때 호출\
        var charList = UserInfo.instance.GetMyCharList();

        for (var i = 0; i < charList.Count; i++)
        {
            GameObject CharInfo = PoolManager.instance.PopFromPool("Prefabs/CharInfoButton");//만들어 놓은 오브젝트를 다시 가져와서 쓴다.
            CharInfo.SetActive(true);
            CharInfo.transform.SetParent(m_GridChar.transform); //부모 트랜스폼 새로 설정
            CharInfo.GetComponent<CharInfoButton>().SetCallBack(CharInfoSelect, i);
            CharInfo.GetComponentInChildren<UILabel>().text = UserInfo.instance.GetCharData(CharacterData.CHAR_ENUM.CHAR_NAME, i) as string;
        }
        m_GridPanel.Refresh();
        m_GridChar.GetComponent<UIGrid>().Reposition(); //리 포지셔닝으로 그리드 재정렬
        CharInfoSelect(0);  //첫번째로 셋팅
    }

    private void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void CharInfoSelect(int iIndex)
    {
        GameObject Main = UserInfo.instance.GetCharPrefabs(m_SelectChar.transform, iIndex);
        //현재 지정한 캐릭터를 세팅
        Main.GetComponent<Animator>().runtimeAnimatorController = UserInfo.instance.GetCharAnimator(iIndex, CharacterData.CHAR_ANIMATOR.CHAR_LOBBY_ANIMATOR) as RuntimeAnimatorController;
        //캐릭터 선택하면 메인 화면에 해당 캐릭터의 프리펩이 뜬다.
        GameManager.instance.CharSelect(iIndex);    //현재 선택한 캐릭터를 임시 저장
    }
}
