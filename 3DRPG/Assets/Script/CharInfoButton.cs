using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharInfoButton : MonoBehaviour
{
    public delegate void CallBack(int iIndex);
    private CallBack m_CallBack = null;
    private int m_iCharIndex = -1;  //해당 인포메이션 버튼이 가지는 캐릭터 인덱스
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnClick();
    }

    public void SetCallBack(CallBack call, int iIndex)
    {
        m_iCharIndex = iIndex;
        m_CallBack = call;
    }

    //콜백
    void OnClick()
    {
        //해당 인포메이션 버튼을 누르면 캐릭터 프리펩이 나오고 해당 인덱스를 저장한다.
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                m_CallBack(m_iCharIndex);
                Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 5f);
            }
        }
    }

    public void ResetCallBack()
    {
        m_iCharIndex = -1;
        m_CallBack = null;
    }

}
