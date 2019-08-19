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
        m_StageLabel.text = GameManager.instance.ReturnStage() + "Stage Clear";
        int [] ListChar = GameManager.instance.ReturnPlayerList();
        int ClearExp = System.Convert.ToInt32(GameManager.instance.ReturnStageData(MAP_DATA.MAP_CLEAR_EXP));
        int ClearGold = System.Convert.ToInt32(GameManager.instance.ReturnStageData(MAP_DATA.MAP_CLEAR_GOLD));
        string [] ClearItem = System.Convert.ToString(GameManager.instance.ReturnStageData(MAP_DATA.MAP_CLEAR_ITEM)).Split(',');
        //클리어 아이템들은 ;과 ,으로 구분 되어 있으니 잘 쪼개어 사용
        for (int i = 0;  i < 3; i++)
        {
            if(ListChar[i] != -1)
            {
                m_ListChar[i].gameObject.SetActive(true);
                string strName = UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, ListChar[i]) as string;
                m_ListChar[i].transform.GetChild(0).GetComponent<UISprite>().spriteName = strName + "Select";
                //스프라이트를 바꾸고

                //경험치 만큼 업데이트를 하고
                GameObject Slider = m_ListChar[i].transform.GetChild(1).gameObject;
                UserInfo.instance.CharUpdate(CHAR_DATA.CHAR_CUR_EXP, ClearExp, ListChar[i]);
                if(UserInfo.instance.ifCharLevelUp(ListChar[i]))
                {
                    //해당 캐릭터가 레벨업을 하였는가?
                    m_ListChar[i].transform.GetChild(3).gameObject.SetActive(true);
                }

                Slider.GetComponent<UISlider>().value = ;
            }
            else
            {
                m_ListChar[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
