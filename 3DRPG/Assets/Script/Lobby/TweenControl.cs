using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TWEEN_SET
{
    TWEEN_START,
    TWEEN_REVERSE,
}

public class TweenControl : MonoBehaviour
{
    // Start is called before the first frame update
    private TweenPosition m_Tween;
    public Vector3 m_vec3 = new Vector3();
    private void Awake()
    {
        m_Tween = transform.GetComponent<TweenPosition>();
        m_Tween.from = transform.localPosition  + m_vec3;
        m_Tween.to = transform.localPosition;
        transform.localPosition = transform.localPosition + m_vec3;
        m_Tween.duration = 0.2f;
        m_Tween.enabled = false;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TweenStart(TWEEN_SET eTween)
    {
        switch (eTween)
        {
            case TWEEN_SET.TWEEN_START:
                m_Tween.PlayForward();
                break;
            case TWEEN_SET.TWEEN_REVERSE:
                m_Tween.PlayReverse();
                break;
        }
    }
}
