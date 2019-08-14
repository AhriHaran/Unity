using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    private List<GameObject> m_ListChar = new List<GameObject>();
    public PlayerManager(Transform Parent)
    {
        int[] iarr = GameManager.instance.ReturnPlayerList();

        foreach (int i in iarr)
        {
            if(i != -1)
            {
                try
                {
                    string route = UserInfo.instance.GetCharData(CHAR_DATA.CHAR_ROUTE, iarr[i]).ToString();
                    string name = UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, iarr[i]).ToString();
                    string CharRoute = "Player/" + route + "Prefabs/" + name;

                    GameObject PlayerChar = ResourceLoader.CreatePrefab(CharRoute, Parent);
                    PlayerChar.GetComponent<Animator>().runtimeAnimatorController = UserInfo.instance.GetCharAnimator(i, CHAR_ANIMATOR.CHAR_BATTLE_ANIMATOR) as RuntimeAnimatorController;
                    //해당 캐릭터의 배틀 애니메이터 셋팅
                    PlayerScript script = PlayerChar.GetComponent<PlayerScript>();
                    script.enabled = true;
                    script.PlayerSet();
                    //해당 캐릭터의 플레이어 스크립트 설정

                    //플레이어 동작 스크립트
                    PlayerChar.SetActive(false);
                    m_ListChar.Add(PlayerChar);

                    //플레이어 캐릭터 셋팅
                }
                catch (System.NullReferenceException ex)
                {
                    Debug.Log(ex);
                }
            }
        }
    }

    public void SetPosition(int iCount, Vector3 CharPos)
    {
        int iCurChar = GameManager.instance.ReturnCurPlayer();
        if(iCurChar != iCount) //현재 메인 캐릭터랑 다른 경우
        {
            if(iCurChar != -1)
            {
                CharPos = m_ListChar[iCurChar].transform.position;
                m_ListChar[iCurChar].SetActive(false);
                //캐릭터 변경 시에는 현재 캐릭터의 좌표를 기준으로 한다.
            }
            else
            {
                m_ListChar[iCount].SetActive(true);
                m_ListChar[iCount].transform.position = CharPos; //포지션 셋팅
                GameManager.instance.PlayerCharChange(iCount);
            }
        }
    }

    public Transform GetCharTR()
    {
        int iCurChar = GameManager.instance.ReturnCurPlayer();
        return m_ListChar[iCurChar].transform;
    }
}
