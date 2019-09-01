using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStigmaUI : MonoBehaviour
{
    private UISprite m_StigmaT;
    private UISprite m_StigmaC;
    private UISprite m_StigmaB;

    private UILabel m_StigmaTName;
    private UILabel m_StigmaCName;
    private UILabel m_StigmaBName;

    // Start is called before the first frame update
    private void Awake()
    {
        m_StigmaT = transform.GetChild(0).transform.GetChild(0).GetComponent<UISprite>();
        m_StigmaC = transform.GetChild(1).transform.GetChild(0).GetComponent<UISprite>();
        m_StigmaB = transform.GetChild(2).transform.GetChild(0).GetComponent<UISprite>();

        m_StigmaTName = transform.GetChild(0).transform.GetChild(1).GetComponentInChildren<UILabel>();
        m_StigmaCName = transform.GetChild(1).transform.GetChild(1).GetComponentInChildren<UILabel>();
        m_StigmaBName = transform.GetChild(2).transform.GetChild(1).GetComponentInChildren<UILabel>();
    }

    public void OnFinished()
    {
        int iIndex = GameManager.instance.ReturnCurSelectChar();
        //현재 정보를 보려는 캐릭터
        StigmaView(iIndex, CHAR_DATA.CHAR_STIGMA_TOP_INDEX, "_T", "(T)");
        StigmaView(iIndex, CHAR_DATA.CHAR_STIGMA_CENTER_INDEX, "_C", "(C)");
        StigmaView(iIndex, CHAR_DATA.CHAR_STIGMA_BOTTOM_INDEX, "_B", "(B)");
        //onfinish가 되면 매번 셋팅
    }

    private void StigmaView(int iIndex, CHAR_DATA eType, string strType, string strName)
    {
        int iData = Util.ConvertToInt(UserInfo.instance.GetCharData(eType, iIndex));
        //해당 캐릭터가 가지고있는 부위의 리스트 기반 인덱스
        if (iData >= 0)
        {
            int Stigma = Util.ConvertToInt(UserInfo.instance.GetItemForList(iData, INVENTORY_TYPE.INVENTORY_STIGMA, ITEM_DATA.ITEM_INDEX));
            //해당 리스트에 속해 있는 아이템의 실제 인덱스
            string spriteName = Util.ConvertToString(Stigma) + strType;
            m_StigmaB.spriteName = spriteName;
            //스프라이트 교체
            m_StigmaBName.text = Util.ConvertToString(UserInfo.instance.GetItemForList(iData, INVENTORY_TYPE.INVENTORY_STIGMA, ITEM_DATA.ITEM_NAME)) + strName;
            //이름 교체
        }
        else
        {
            m_StigmaB.spriteName = "CrossHair";
            m_StigmaBName.text = "None";
        }
    }

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
