using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageReadyPanel : MonoBehaviour
{
    private UIButton [] m_selectCharBT = new UIButton[3];
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            m_selectCharBT[i] = transform.GetChild(i + 1).GetComponent<UIButton>();
        }
    }

    private void OnDisable()
    {
        //해당 버튼의 라벨과 스프라이트를 리셋

        for(int i = 0; i < 3; i++)
        {
            m_selectCharBT[i].transform.GetChild(0).GetComponent<UILabel>().text = "Name";  //이름
            m_selectCharBT[i].transform.GetChild(0).GetComponent<UISprite>().spriteName = "";   //바꿔 줄 스프라이트
            m_selectCharBT[i].transform.GetChild(0).GetComponent<UISprite>().onRender(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SelectChar(string strSelect)
    {
        m_selectCharBT[int.Parse(strSelect)].transform.GetChild(0).GetComponent<UILabel>().text =
            UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, GameManager.instance.GetCharIndex(int.Parse(strSelect))) as string;
        //텍스트를 바꿔주고

        string spriteName = 
        m_selectCharBT[int.Parse(strSelect)].transform.GetChild(1).GetComponent<UISprite>().spriteName =
            UserInfo.instance.GetCharData(CHAR_DATA.CHAR, GameManager.instance.GetCharIndex(int.Parse(strSelect))) as string;
        //해당 스프라이트를 바꿔준다.
    }
    
}
