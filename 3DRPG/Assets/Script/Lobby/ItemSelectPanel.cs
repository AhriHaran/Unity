using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectPanel : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject m_ItemSelect;
    private GameObject m_ItemInfo;
    private void Awake()
    {
        m_ItemSelect = transform.GetChild(0).gameObject;
        m_ItemInfo = transform.GetChild(1).gameObject;
    }

    private void OnEnable()
    {
        m_ItemSelect.GetComponent<TweenControl>().TweenStart(TWEEN_SET.TWEEN_START);
        m_ItemInfo.GetComponent<TweenControl>().TweenStart(TWEEN_SET.TWEEN_START);
    }

    private void OnDisable()
    {
        m_ItemSelect.GetComponent<TweenControl>().TweenStart(TWEEN_SET.TWEEN_REVERSE);
        m_ItemInfo.GetComponent<TweenControl>().TweenStart(TWEEN_SET.TWEEN_REVERSE);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
