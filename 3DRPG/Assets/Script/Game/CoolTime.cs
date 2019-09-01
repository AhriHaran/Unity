using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTime : MonoBehaviour
{
    public delegate void CallBack();
    private CallBack m_CallBack = null;
    private UISprite m_LockSprite;
    public float m_fCoolTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        m_LockSprite = gameObject.GetComponent<UISprite>();
        gameObject.SetActive(false);
    }

    public void CallBackSet(CallBack call)
    {
        m_CallBack = call;
    }

    public void OnClick()
    {
        //누르면 쿨타임 코루틴 호출
        gameObject.SetActive(true);
        StartCoroutine("StartCoolTime", m_fCoolTime);
    }

    IEnumerator StartCoolTime(float fCool)
    {
        float fCurTime = 0.0f;
        while (fCool > fCurTime)
        {
            // Update the fillAmount
            fCurTime += Time.deltaTime;
            m_LockSprite.fillAmount = (fCool - fCurTime) / fCool;
            yield return null;
        }

        gameObject.SetActive(false);
        m_CallBack?.Invoke();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
