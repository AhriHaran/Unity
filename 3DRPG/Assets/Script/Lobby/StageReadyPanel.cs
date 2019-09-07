using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageReadyPanel : MonoBehaviour
{
    private string m_strSprite = "Select";
    private UIPanel m_UIPanel;
    private UIButton [] m_selectCharBT = new UIButton[3];

    //해당 맵의 인포메이션
    private UILabel m_SelectStageIndex = null;
    private UILabel m_ClearEXP = null;
    private UILabel m_ClearGold = null;
    private UILabel m_ClearEnergy = null;
    private GameObject m_Scroll = null;
    private GameObject m_Grid = null;
    
    // Start is called before the first frame update
    private void Awake()
    {
        m_UIPanel = gameObject.GetComponent<UIPanel>();
        for (int i = 0; i <3; i++)
        {
            m_selectCharBT[i] = transform.GetChild(i + 1).GetComponent<UIButton>();
        }

        GameObject Object = gameObject.transform.Find("StageInfo").gameObject;
        m_SelectStageIndex = Object.transform.GetChild(0).GetComponent<UILabel>();
        m_ClearEXP = Object.transform.GetChild(1).GetComponent<UILabel>();
        m_ClearGold = Object.transform.GetChild(2).GetComponent<UILabel>();
        m_ClearEnergy = Object.transform.GetChild(3).GetComponent<UILabel>();

        m_Scroll = Object.transform.GetChild(4).gameObject;
        m_Grid = m_Scroll.transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        int[] iarr = GameManager.instance.ReturnPlayerList();
        for (int i = 0; i < 3; i++)
        {
            if(iarr[i] >= 0)
            {
                string strName = UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, iarr[i]) as string;
                m_selectCharBT[i].GetComponentInChildren<UILabel>().text = strName;
                strName += m_strSprite;
                m_selectCharBT[i].GetComponentInChildren<UISprite>().spriteName = strName;
                m_selectCharBT[i].normalSprite = strName;
            }
            else
            {
                m_selectCharBT[i].GetComponentInChildren<UILabel>().text = "Name";
                m_selectCharBT[i].GetComponentInChildren<UISprite>().spriteName = "EmptySelect";
                m_selectCharBT[i].normalSprite = "EmptySelect";
            }
        }

        //클리어 골드
        int MapIndex = GameManager.instance.ReturnStage();

        if(m_Grid.transform.childCount > 0)
        {
            while (m_Grid.transform.childCount != 0)
            {
                GameObject game = m_Grid.transform.GetChild(0).gameObject;
                game.transform.SetParent(null);
                NGUITools.Destroy(game);
            }
            m_Grid.transform.DetachChildren();
        }

        if (MapIndex >= 0)
        {
            m_SelectStageIndex.text = Util.ConvertToString(MapIndex + 1) + "Stage"; //현재 선택한 맵 인덱스
            var MapTable = EXCEL.ExcelLoad.Read("Excel/Table/Stage_Table");
            //맵 테이블을 불러오고

            m_ClearEXP.text = "+" + Util.ConvertToString(MapTable[MapIndex][MAP_DATA.MAP_CLEAR_EXP.ToString()]);
            m_ClearGold.text = "+" + Util.ConvertToString(MapTable[MapIndex][MAP_DATA.MAP_CLEAR_GOLD.ToString()]);
            m_ClearEnergy.text = "-" + Util.ConvertToString(MapTable[MapIndex][MAP_DATA.MAP_ENERGY.ToString()]);
            
            string []ClearItem = Util.ConvertToString(MapTable[MapIndex][MAP_DATA.MAP_CLEAR_ITEM.ToString()]).Split('/'); ;
            for (int i = 0; i < ClearItem.Length; i++)
            {
                //첫 번째는 아이템 타입, 인벤토리 타입(웨폰, 스티그마), 아이템 인덱스
                string[] item = ClearItem[i].Split(';');
                GameObject Item = ResourceLoader.CreatePrefab("Prefabs/ItemSprite");
                Item.transform.SetParent(m_Grid.transform, false);
                Item.GetComponent<ItemSprite>().Setting(Util.ConvertToInt(item[2]), -1, (ITEM_TYPE)Util.ConvertToInt(item[0]), (INVENTORY_TYPE)Util.ConvertToInt(item[1]));
            }
            m_Grid.GetComponent<UIGrid>().Reposition(); //리 포지셔닝으로 그리드 재정렬
            m_Scroll.GetComponent<UIScrollView>().ResetPosition();
        }

        m_UIPanel.Refresh();
    }

    private void OnDisable()
    {
    }

    public void SelectChar(string strSelect)    //내가 선택한 패널
    {
        int iSelect = int.Parse(strSelect);
        int[] iarrList = GameManager.instance.ReturnPlayerList();
        if(iarrList[iSelect] != -1)
        {
            string strName = UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, iarrList[iSelect]) as string;
            //캐릭터 인덱스 기반 반환
            m_selectCharBT[iSelect].GetComponentInChildren<UILabel>().text = strName;
            //텍스트를 바꿔주고
            ////해당 캐릭터의 이름
            strName += m_strSprite;
            ////스프라이트 바꿈
            m_selectCharBT[iSelect].GetComponentInChildren<UISprite>().spriteName = strName;
            m_selectCharBT[iSelect].normalSprite = strName;
            //해당 스프라이트를 바꿔준다.
            m_UIPanel.Refresh();
        }
    }
    
}
