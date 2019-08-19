using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    private UIPanel m_ResultPanel;
    private List<GameObject> m_ListChar = new List<GameObject>();   //캐릭터
    private GameObject m_PlayerResource;    //플레이어가 얻는 자원
    private GameObject m_ItemRoot;  //아이템 자원
    private UILabel m_StageLabel;

    // Start is called before the first frame update
    private void Awake()
    {
        m_ResultPanel = GameObject.Find("GameUI").GetComponentInChildren<UIPanel>();
        for(int i = 1; i < 4; i++)
        {
            m_ListChar.Add(transform.GetChild(i).gameObject);
        }
        m_PlayerResource = transform.GetChild(4).gameObject;
        m_ItemRoot = transform.GetChild(5).gameObject;
        m_StageLabel = transform.GetChild(6).GetComponent<UILabel>();
    }

    void Start()
    {
        //해당 패널이 액티브 되면 활성화
        int [] ListChar = GameManager.instance.ReturnPlayerList();
        int ClearExp = System.Convert.ToInt32(GameManager.instance.ReturnStageData(MAP_DATA.MAP_CLEAR_EXP));
        int ClearGold = System.Convert.ToInt32(GameManager.instance.ReturnStageData(MAP_DATA.MAP_CLEAR_GOLD));
        int ClearEnergy = System.Convert.ToInt32(GameManager.instance.ReturnStageData(MAP_DATA.MAP_ENERGY));
        string [] ClearItem = System.Convert.ToString(GameManager.instance.ReturnStageData(MAP_DATA.MAP_CLEAR_ITEM)).Split(',');
        //클리어 아이템들은 ;과 ,으로 구분 되어 있으니 잘 쪼개어 사용

        m_StageLabel.text = GameManager.instance.ReturnStage() + "Stage Clear"; //스테이지

        //플레이어 리소스 관련
        m_PlayerResource.transform.GetChild(0).GetComponent<UILabel>().text = "+" + ClearExp.ToString();    //획득 경험치
        UserResourceUpdate(USER_INFO.USER_INFO_CUR_EXP, ClearExp);

        m_PlayerResource.transform.GetChild(1).GetComponent<UILabel>().text = "+" + ClearGold.ToString();    //획득 골드
        UserResourceUpdate(USER_INFO.USER_INFO_GOLD, ClearGold);

        UserResourceUpdate(USER_INFO.USER_INFO_GOLD, -ClearEnergy); //클리어 시 소모 에너지

        if (UserInfo.instance.ifUserLevelUP())  //캐릭터 레벨업을 하였는가?
        {
            m_PlayerResource.transform.GetChild(3).gameObject.SetActive(true);    //레벨업
        }

        GameObject Slider = m_PlayerResource.transform.GetChild(2).gameObject;  //경험치 바 업데이트
        string strEXP = UserInfo.instance.GetUserData(USER_INFO.USER_INFO_CUR_EXP).ToString() + "/" + UserInfo.instance.GetUserData(USER_INFO.USER_INFO_MAX_EXP).ToString();
        Slider.GetComponentInChildren<UILabel>().text = strEXP; //현재 경험치 / 맥스 경험


        //플레이어 캐릭터 관련 업데이트
        for (int i = 0;  i < 3; i++)
        {
            if(ListChar[i] != -1)
            {
                m_ListChar[i].gameObject.SetActive(true);
                string strName = UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, ListChar[i]) as string;
                m_ListChar[i].transform.GetChild(0).GetComponent<UISprite>().spriteName = strName + "Select";
                //스프라이트를 바꾸고

                //경험치 만큼 업데이트를 하고
                Slider = m_ListChar[i].transform.GetChild(1).gameObject;
                int iCurEXP = System.Convert.ToInt32(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_CUR_EXP, ListChar[i]));
                iCurEXP += ClearExp;
                UserInfo.instance.CharUpdate(CHAR_DATA.CHAR_CUR_EXP, iCurEXP, ListChar[i]); //EXP 업데이트
                if(UserInfo.instance.ifCharLevelUp(ListChar[i]))
                {
                    //해당 캐릭터가 레벨업을 하였는가?
                    m_ListChar[i].transform.GetChild(3).gameObject.SetActive(true);
                }

                float fCur = System.Convert.ToInt32(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_CUR_EXP, ListChar[i]));    //업데이트 이후 CurEXp
                float fMax = System.Convert.ToInt32(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_MAX_EXP, ListChar[i]));
                float fValue = fCur / fMax;
                Slider.GetComponent<UISlider>().value = fValue; //경험치 value
                Slider.GetComponentInChildren<UILabel>().text = "+" + ClearExp.ToString();  //경험치 획득
                m_ListChar[i].transform.GetChild(2).GetComponent<UILabel>().text = "Lv." + UserInfo.instance.GetCharData(CHAR_DATA.CHAR_LEVEL, ListChar[i]).ToString();
                //캐릭터 레벨
            }
            else
            {
                m_ListChar[i].gameObject.SetActive(false);
            }
        }


        //인벤토리 업데이트
    }

    void UserResourceUpdate(USER_INFO eIndex, int iData)
    {
        int CurData = System.Convert.ToInt32(UserInfo.instance.GetUserData(eIndex));
        CurData += iData;
        UserInfo.instance.UserUpdate(eIndex, CurData);    //클리어 데이터 업데이트
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
