using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CHAR_INFO_UI
{
    CHAR_INFO_UI_START,
    CHAR_INFO_UI_INFO = CHAR_INFO_UI_START,
    CHAR_INFO_UI_LEVEL,
    CHAR_INFO_UI_WEAPON,
    CHAR_INFO_UI_Stigma,
    CHAR_INFO_UI_END
}   //캐릭터 정보창

public class ValkyrjaPanel : MonoBehaviour
{
    private GameObject m_GridPanel = null;
    private GameObject m_GridChar = null;   //그리드 정렬

    private GameObject m_SelectCharMain = null;
    private GameObject m_SelectChar = null;    //내가 선택한 캐릭터의 프리팹

    private GameObject m_SelectButton = null;   //선택 버튼

    private GameObject[] m_CharUI = new GameObject[(int)CHAR_INFO_UI.CHAR_INFO_UI_END];

    private int m_iCharIndex = -1;
    private string m_strSprite = "Icon";
    // Start is called before the first frame update
    private void Awake()
    {
        //하위 오브젝트인 그리드를 받아온다.
        m_SelectCharMain = GameObject.Find("SelectCharModel");  //선택된 캐릭터를 보여준는 자리
        m_GridPanel = transform.GetChild(0).gameObject; //ui 패널
        m_GridChar = m_GridPanel.transform.GetChild(0).gameObject;   //해당 패널이 가지는 차일드 패널(캐릭터 선택 스크롤뷰를 위해서), 이 오브젝트의 하위에 정렬된다.

        m_SelectButton = transform.GetChild(1).gameObject;

        for (int i = (int)CHAR_INFO_UI.CHAR_INFO_UI_START; i < (int)CHAR_INFO_UI.CHAR_INFO_UI_END; i++)
        {
            m_CharUI[i] = transform.GetChild(i + 3).gameObject;
        }
        m_iCharIndex = -1;
    }

    void Start()
    {
        ////활성화 될 때 호출\
        //
    }

    public void ButtonOnOff(bool OnOff)
    {
        //호출에 따라서 구분해준다.
        /*
         * 로비에서 캐릭터 버튼으로 호출이면 셀렉트 버튼을 비활성화
         * 스테이지 선택에서 호출이면 셀렉트 버튼을 활성화
         */
        if(OnOff)
        {
            m_SelectButton.gameObject.SetActive(true);
        }
        else
        {
            m_SelectButton.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        var CharList = UserInfo.instance.GetMyCharList();
        int iCount = CharList.Count;

        for (int i = 0; i < iCount; i++)
        {
            GameObject CharInfo = ResourceLoader.CreatePrefab("Prefabs/CharInfoButton");//만들어 놓은 오브젝트를 다시 가져와서 쓴다.
            CharInfo.transform.SetParent(m_GridChar.transform, false); //부모 트랜스폼 새로 설정
            int iData = Util.ConvertToInt(CharList[i].GetCharData(CHAR_DATA.CHAR_INDEX));
            CharInfo.GetComponent<CharInfoButton>().SetCallBack(CharInfoSelect, iData);//캐릭터의 고유 인덱스 기반
            string strIcon = Util.ConvertToString(CharList[i].GetCharData(CHAR_DATA.CHAR_NAME));
            CharInfo.GetComponentInChildren<UILabel>().text = strIcon;
            strIcon += m_strSprite;
            CharInfo.GetComponentInChildren<UISprite>().spriteName = strIcon;
        }

        m_GridChar.GetComponent<UIGrid>().Reposition(); //리 포지셔닝으로 그리드 재정렬
        m_GridPanel.GetComponent<UIScrollView>().ResetPosition();
        m_GridPanel.GetComponent<UIPanel>().Refresh();

        CharInfoSelect(Util.ConvertToInt(CharList[0].GetCharData(CHAR_DATA.CHAR_INDEX)));  //첫번째로 셋팅
    }

    private void OnDisable()
    {
        if(m_SelectChar != null)
        {
            CharPoolManager.instance.PushToPool(POOL_INDEX.POOL_USER_CHAR.ToString(), m_iCharIndex, m_SelectChar);
            m_SelectChar = null;
            m_iCharIndex = -1;
            //사용한 캐릭터 오브젝트 반납
        }

        while (m_GridChar.transform.childCount != 0)
        {
            GameObject game = m_GridChar.transform.GetChild(0).gameObject;
            game.transform.SetParent(null);
            NGUITools.Destroy(game);
        }
        m_GridChar.transform.DetachChildren();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void CharInfoSelect(int iIndex)
    {
        if(m_iCharIndex != iIndex)  //중복체크 방지
        {
            m_SelectChar = CharPoolManager.instance.PopFromPool(POOL_INDEX.POOL_USER_CHAR.ToString(), iIndex); //해당 인덱스를 반환해서 크리에이트
                                                                                                                     //현재 지정한 캐릭터를 세팅
            m_SelectChar.SetActive(true);
            m_iCharIndex = iIndex;
            m_SelectChar.transform.SetParent(m_SelectCharMain.transform, false);
            m_SelectChar.GetComponent<Animator>().runtimeAnimatorController = UserInfo.instance.GetCharAnimator(iIndex, CHAR_ANIMATOR.CHAR_LOBBY_ANIMATOR) as RuntimeAnimatorController;
            //캐릭터 선택하면 메인 화면에 해당 캐릭터의 프리펩이 뜬다.
            GameManager.instance.CharSelect(iIndex);    //현재 선택한 캐릭터 인덱스를 저장

            //현재 선택된 캐릭터의 정보를 표시
            string strData = Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, iIndex));
            m_CharUI[(int)CHAR_INFO_UI.CHAR_INFO_UI_INFO].transform.GetChild(1).GetComponent<UILabel>().text = strData;
            //이름 셋팅

            strData = "Lv." + Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_LEVEL, iIndex));
            m_CharUI[(int)CHAR_INFO_UI.CHAR_INFO_UI_LEVEL].transform.GetChild(1).GetComponent<UILabel>().text = strData;
            //레벨 셋팅
            strData = "ATK." + Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_ATK, iIndex));
            m_CharUI[(int)CHAR_INFO_UI.CHAR_INFO_UI_LEVEL].transform.GetChild(2).GetComponent<UILabel>().text = strData;
            //공격력 셋팅

            int iWeapon = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_WEAPON_INDEX, iIndex));
            if(iWeapon != -1)
            {
                strData = Util.ConvertToString(UserInfo.instance.GetItemForList(iWeapon, INVENTORY_TYPE.INVENTORY_WEAPON, ITEM_DATA.ITEM_NAME));
                m_CharUI[(int)CHAR_INFO_UI.CHAR_INFO_UI_WEAPON].transform.GetChild(2).GetComponent<UILabel>().text = strData;
                //무기
            }
            else
            {
                strData = "WeaponNone";
                m_CharUI[(int)CHAR_INFO_UI.CHAR_INFO_UI_WEAPON].transform.GetChild(2).GetComponent<UILabel>().text = strData;
            }
            //장착한 무기 셋팅

            int T = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_TOP_INDEX, iIndex));
            int C = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_CENTER_INDEX, iIndex));
            int B = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_BOTTOM_INDEX, iIndex));
            //해당 아이템은 스프라이트로 처리
        }
    }

    void OnClick()
    {
        //해당 인포메이션 버튼을 누르면 캐릭터 프리펩이 나오고 해당 인덱스를 저장한다.
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = UICamera.mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                for(int i = (int)CHAR_INFO_UI.CHAR_INFO_UI_START; i < (int)CHAR_INFO_UI.CHAR_INFO_UI_END; i++)
                {
                    if (Object.ReferenceEquals(hit.transform.gameObject, m_CharUI[i].gameObject))
                    {
                        //띄워준 유아이에 하나라도 걸리는 것이 있는가?
                        //그렇다면 현재 패널을 꺼주고, 해당 캐릭터의 인포메이션 패널을 열어줘라

                    }
                }
            }
        }
    }
}

