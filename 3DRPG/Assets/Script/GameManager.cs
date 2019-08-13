using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private int m_iCurStage = -1;   //현재 선택한 스테이지
    private int[] m_ListCharIndex;  //내가 선택한 캐릭터 인덱스들
    private int m_iCurSelectChar = -1;   //캐릭터 선택 단계에서 내가 선택한 캐릭터
    private int m_iCurGameChar = -1;     //게임 속에서 내가 현재 선택한 캐릭터
    private string m_strStageType;  
    private float m_fStageTime = 0.0f;

    public void Init()
    {
        m_ListCharIndex = new int[] { -1, -1, -1 };   //3개
        m_iCurGameChar = -1;
    }

    /// <summary>
    /// 스테이지와 관련된 정보를 처리하는 구간
    /// </summary>
    /// <param name="iStage"></param>
    public void StageSelect(int iStage)
    {
        m_iCurStage = iStage;
    }
    public void StageSelect(string strType, float fTime)
    {
        m_strStageType = strType;
        m_fStageTime = fTime;
    }
    public string ReturnStageType()
    {
        return m_strStageType;
    }
    public float ReturnStageTime()
    {
        return m_fStageTime;
    }
    public string ReturnStage()
    {
        return m_iCurStage.ToString(); //현재 시작 스테이지 리턴
    }


    /// <summary>
    /// 캐릭터와 관련된 정보를 처리하는 구간
    /// </summary>
    /// <param name="iCharIndex"></param>
    public void CharSelect(int iCharIndex)
    {
        if(CharOverLap(iCharIndex))
            m_iCurSelectChar = iCharIndex;    //캐릭터 선택
    }
    bool CharOverLap(int iCharIndex)    //캐릭터 중복확인
    {
        for(int i = 0; i < 3; i++)
        {
            if (m_ListCharIndex[i] == iCharIndex)
                return false;
        }
        return true;
    }
    public void CharSelectComplete(int iNum)    //몇 번째 패널인가
    {
        m_ListCharIndex[iNum] = m_iCurSelectChar; //인덱스 저장
    }
    public int GetCharIndex(int iIndex) //캐릭터 인덱스 반환
    {
        return m_ListCharIndex[iIndex];
    }
    public int[] ReturnPlayerList()
    {
        return m_ListCharIndex;
    }

    /// <summary>
    /// 게임 시작을 위한 정보를 처리하는 구간
    /// </summary>
    /// <returns></returns>
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
    public void GameStart()
    {
        LoadScene.SceneLoad("GameScene");
    }

    /// <summary>
    /// 게임 내에서 캐릭터 변경 등에 대한 함수
    /// </summary>
    public void PlayerCharChange(int iSelect)
    {
        m_iCurGameChar = iSelect;
    }
    public int ReturnCurPlayer()
    {
        return m_iCurGameChar;  //현재 선택된 캐릭터
    }
}

