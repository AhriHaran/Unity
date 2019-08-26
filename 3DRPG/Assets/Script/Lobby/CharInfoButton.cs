using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInfoButton : MonoBehaviour
{
    public delegate void CallBack(int iIndex);
    private CallBack m_CallBack = null;
    private int m_iCharIndex = -1;  //해당 인포메이션 버튼이 가지는 캐릭터 인덱스
    private string m_strSprite = "Icon";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_CallBack != null)
            OnClick();
    }

    public void Setting(int iIndex)
    {
        m_iCharIndex = iIndex;
        string strIcon = Util.ConvertToString(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, m_iCharIndex));
        transform.GetChild(0).GetComponent<UILabel>().text = strIcon;
        strIcon += m_strSprite;
        transform.GetChild(1).GetComponent<UISprite>().spriteName = strIcon;
        //INGUIAtlas atlas = transform.GetChild(1).GetComponent<UISprite>().atlas;
    }

    public void SetCallBack(CallBack call)
    {
        m_CallBack = call;
    }

    //콜백
    void OnClick()
    {
        //해당 인포메이션 버튼을 누르면 캐릭터 프리펩이 나오고 해당 인덱스를 저장한다.
        if(Input.GetMouseButtonDown(0))
        {
            GameObject Object = null; 
            if(Util.RayCastHitObject(ref Object))
            {
                m_CallBack(m_iCharIndex);
            }
        }
    }
}
