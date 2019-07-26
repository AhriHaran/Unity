using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private int m_iCurStage;

    // Start is called before the first frame update
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
    }

    //StageSelect
    public void StageSelect(int iStage)
    {
        m_iCurStage = iStage;
        //현재 스테이지를 저장하고 다음 씬으로 넘어간다.LoadScene
        LoadScene.SceneLoad("GameScene");
    }

    public string ReturnStage()
    {
        return m_iCurStage.ToString(); //현재 시작 스테이지 리턴
    }
}
