using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VALKYRJA_UI
{
    VALKYRJA_UI_START,
    VALKYRJA_UI_INFO = VALKYRJA_UI_START,
    VALKYRJA_UI_LEVEL,
    VALKYRJA_UI_WEAPON,
    VALKYRJA_UI_Stigma,
    VALKYRJA_UI_END
}   //캐릭터 정보창

public class ValkyrjaPanel : MonoBehaviour
{
    private GameObject m_GridPanel = null;
    private GameObject m_GridChar = null;   //그리드 정렬

    private GameObject m_SelectButton = null;   //선택 버튼

    private GameObject[] m_ThisUI = new GameObject[(int)VALKYRJA_UI.VALKYRJA_UI_END];   //화면에 띄워진 UI

    private int m_iCharIndex = -1;  //선택한 캐릭터 인덱스

    // Start is called before the first frame update
    private void Awake()
    {
        //하위 오브젝트인 그리드를 받아온다.
        m_GridPanel = transform.GetChild(0).gameObject; //ui 패널
        m_GridChar = m_GridPanel.transform.GetChild(0).gameObject;   //해당 패널이 가지는 차일드 패널(캐릭터 선택 스크롤뷰를 위해서), 이 오브젝트의 하위에 정렬된다.

        m_SelectButton = transform.GetChild(1).gameObject;

        for (int i = (int)VALKYRJA_UI.VALKYRJA_UI_START; i < (int)VALKYRJA_UI.VALKYRJA_UI_END; i++)
        {
            m_ThisUI[i] = transform.GetChild(i + 2).gameObject;
        }
        m_iCharIndex = -1;
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
            CharInfo.GetComponent<CharInfoButton>().Setting(iData);
            CharInfo.GetComponent<CharInfoButton>().SetCallBack(CharInfoSelect);//캐릭터의 고유 인덱스 기반
        }

        m_GridChar.GetComponent<UIGrid>().Reposition(); //리 포지셔닝으로 그리드 재정렬
        m_GridPanel.GetComponent<UIScrollView>().ResetPosition();
        m_GridPanel.GetComponent<UIPanel>().Refresh();

        CharInfoSelect(Util.ConvertToInt(CharList[0].GetCharData(CHAR_DATA.CHAR_INDEX)));  //첫번째로 셋팅
    }

    private void OnDisable()
    {
        m_iCharIndex = -1;
        m_GridChar.transform.DestroyChildren();
    }

    void CharInfoSelect(int iIndex)
    {
        if(m_iCharIndex != iIndex)  //중복체크 방지
        {
            m_iCharIndex = iIndex;
            GameManager.instance.DestroyModel();
            GameManager.instance.CreateModel(iIndex);
            GameManager.instance.CharSelect(iIndex);    //현재 선택한 캐릭터 인덱스를 저장

            //현재 선택된 캐릭터의 정보를 표시
            string strData = Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, iIndex));
            m_ThisUI[(int)VALKYRJA_UI.VALKYRJA_UI_INFO].transform.GetChild(0).GetComponent<UILabel>().text = strData;
            //이름 셋팅

            strData = "Lv." + Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_LEVEL, iIndex));
            m_ThisUI[(int)VALKYRJA_UI.VALKYRJA_UI_LEVEL].transform.GetChild(0).GetComponent<UILabel>().text = strData;
            //레벨 셋팅
            strData = "ATK." + Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_ATK, iIndex));
            m_ThisUI[(int)VALKYRJA_UI.VALKYRJA_UI_LEVEL].transform.GetChild(1).GetComponent<UILabel>().text = strData;
            //공격력 셋팅

            int iWeapon = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_WEAPON_INDEX, iIndex));
            if(iWeapon != -1)
            {
                strData = Util.ConvertToString(UserInfo.instance.GetItemForList(iWeapon, INVENTORY_TYPE.INVENTORY_WEAPON, ITEM_DATA.ITEM_NAME));
                m_ThisUI[(int)VALKYRJA_UI.VALKYRJA_UI_WEAPON].transform.GetChild(0).GetComponent<UILabel>().text = strData;
                //무기
            }
            else
            {
                strData = "WeaponNone";
                m_ThisUI[(int)VALKYRJA_UI.VALKYRJA_UI_WEAPON].transform.GetChild(0).GetComponent<UILabel>().text = strData;
            }
            //장착한 무기 셋팅

            int T = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_TOP_INDEX, iIndex));
            int C = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_CENTER_INDEX, iIndex));
            int B = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_STIGMA_BOTTOM_INDEX, iIndex));
            //해당 아이템은 스프라이트로 처리
        }
    }
}

