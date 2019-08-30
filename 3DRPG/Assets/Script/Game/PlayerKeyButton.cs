using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyButton : MonoBehaviour
{
    private UISprite m_KeySprite;
    private UISprite m_KeyLockSprite;
    private string m_KeyName;

    private void Awake()
    {
        m_KeyName = transform.name;
        m_KeySprite = transform.GetChild(1).GetComponent<UISprite>();
        m_KeyLockSprite = transform.GetChild(2).GetComponent<UISprite>();
        m_KeyLockSprite.gameObject.SetActive(false);
    }

    public void KeySetting(int iIndex)
    {
        string Key = Util.ConvertToString(iIndex) + "_" + m_KeyName;
        m_KeySprite.spriteName = Key;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
