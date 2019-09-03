using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStigmaUI : MonoBehaviour
{
    private UISprite[] m_Stigma = new UISprite[3];
    private UILabel[] m_StigmaName = new UILabel[3];

    // Start is called before the first frame update
    private void Awake()
    {
        for(int i = 0; i < 3; i++)
        {
            m_Stigma[i] = transform.GetChild(i).transform.GetChild(0).GetComponent<UISprite>();
            m_StigmaName[i] = transform.GetChild(i).transform.GetChild(1).GetComponent<UILabel>();
        }

    }

    public void OnFinished()
    {
        int iIndex = GameManager.instance.ReturnCurSelectChar();
        //현재 정보를 보려는 캐릭터
        StigmaView(iIndex, CHAR_DATA.CHAR_STIGMA_TOP_INDEX, 0, "_T", "(T)");
        StigmaView(iIndex, CHAR_DATA.CHAR_STIGMA_CENTER_INDEX, 1, "_C", "(C)");
        StigmaView(iIndex, CHAR_DATA.CHAR_STIGMA_BOTTOM_INDEX, 2, "_B", "(B)");
        //onfinish가 되면 매번 셋팅
    }

    private void StigmaView(int iIndex, CHAR_DATA eType, int iList, string strType, string strName)
    {
        int iData = Util.ConvertToInt(UserInfo.instance.GetCharData(eType, iIndex));
        //해당 캐릭터가 가지고있는 부위의 리스트 기반 인덱스
        if (iData >= 0)
        {
            int Stigma = Util.ConvertToInt(UserInfo.instance.GetItemForList(iData, INVENTORY_TYPE.INVENTORY_STIGMA, ITEM_DATA.ITEM_INDEX));
            //해당 리스트에 속해 있는 아이템의 실제 인덱스
            string spriteName = Util.ConvertToString(Stigma) + strType;
            m_Stigma[iList].spriteName = spriteName;
            //스프라이트 교체
            m_StigmaName[iList].text = Util.ConvertToString(UserInfo.instance.GetItemForList(iData, INVENTORY_TYPE.INVENTORY_STIGMA, ITEM_DATA.ITEM_NAME)) + strName;
            //이름 교체
        }
        else
        {
            m_Stigma[iList].spriteName = "CrossHair";
            m_StigmaName[iList].text = "None";
        }
    }

}
