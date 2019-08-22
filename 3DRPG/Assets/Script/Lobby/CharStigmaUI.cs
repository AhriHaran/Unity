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
        m_StigmaT = transform.GetChild(0).GetComponent<UISprite>();
        m_StigmaC = transform.GetChild(1).GetComponent<UISprite>();
        m_StigmaB = transform.GetChild(2).GetComponent<UISprite>();

        m_StigmaTName = m_StigmaT.GetComponentInChildren<UILabel>();
        m_StigmaCName = m_StigmaC.GetComponentInChildren<UILabel>();
        m_StigmaBName = m_StigmaB.GetComponentInChildren<UILabel>();
    }

    public void OnFinished()
    {
        int iIndex = GameManager.instance.ReturnCurSelectChar();
        
    }

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
