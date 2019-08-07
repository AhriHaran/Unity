using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private int m_iCurStage = -1;   //현재 선택한 스테이지
    private int[] m_ListCharIndex;  //내가 선택한 캐릭터 인덱스들
    private int m_iCurChar;

    public void Init()
    {
        m_ListCharIndex = new int[] { -1, -1, -1 };   //3개
    }

    //StageSelect
    public void StageSelect(int iStage)
    {
        m_iCurStage = iStage;
    }

    public void GameStart()
    {
        LoadScene.SceneLoad("GameScene");
    }

    public void CharSelect(int iCharIndex)
    {
        if(CharOverLap(iCharIndex))
            m_iCurChar = iCharIndex;    //캐릭터 선택
    }

    bool CharOverLap(int iCharIndex)
    {
        for(int i = 0; i < 3; i++)
        {
            if (m_ListCharIndex[i] == iCharIndex)
                return false;   //중복체크
        }
        return true;
    }

    public void CharSelectComplete(int iNum)    //몇 번째 패널인가
    {
        m_ListCharIndex[iNum] = m_iCurChar; //인덱스 저장
    }

    public int GetCharIndex(int iIndex)
    {
        return m_ListCharIndex[iIndex];
    }

    public bool StageReady()
    {
        if (m_iCurStage != -1 && ifCharSelect())  //스테이지 선택이 되었으며 하나 이상의 캐릭터가 선택되었다.
        {
            return true;
        }
        else
            return false;
    }

    public bool ifCharSelect()
    {
        for(int i = 0; i < m_ListCharIndex.Length; i++)
        {
            if (m_ListCharIndex[i] != -1)   //하나라도 선택된 게 있는가?
                return true;
        }
        return false;
    }
    
    public string ReturnStage()
    {
        return m_iCurStage.ToString(); //현재 시작 스테이지 리턴
    }

    public int[] ReturnPlayerList()
    {
        return m_ListCharIndex;
    }
}

