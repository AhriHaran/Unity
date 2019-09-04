using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyButton : MonoBehaviour
{
    private UISprite m_KeySprite;
    private string m_KeyName;

    private void Awake()
    {
        m_KeyName = transform.name;
        m_KeySprite = transform.GetChild(1).GetComponent<UISprite>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyInfo();   
    }

    void KeyInfo()
    {
        int iIndex = GameManager.instance.ReturnCurPlayer();
        int[] iList = GameManager.instance.ReturnPlayerList();
        
        string Key = Util.ConvertToString(iList[iIndex]) + "_" + m_KeyName;
        m_KeySprite.spriteName = Key;
    }
}
