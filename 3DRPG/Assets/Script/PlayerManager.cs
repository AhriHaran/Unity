using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    private List<GameObject> m_ListChar = new List<GameObject>();
    private int m_iCurChar = -1;
    public PlayerManager(Transform Parent)
    {
        int[] iarr = GameManager.instance.ReturnPlayerList();

        foreach (int i in iarr)
        {
            if(i != -1)
            {
                try
                {
                    string route = UserInfo.instance.GetCharData(CharacterData.CHAR_ENUM.CHAR_ROUTE, iarr[i]).ToString();
                    string name = UserInfo.instance.GetCharData(CharacterData.CHAR_ENUM.CHAR_NAME, iarr[i]).ToString();
                    string CharRoute = route + "Prefabs/" + name;

                    GameObject PlayerChar = ResourceLoader.CreatePrefab(CharRoute, Parent);
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

    public void SetPosition(Vector3 CharPos, int iCout)
    {
        if(m_iCurChar != iCout) //현재 캐릭터랑 다른 경우
        {
            m_ListChar[iCout].SetActive(true);
            m_ListChar[iCout].transform.position = CharPos; //포지션 셋팅
            m_iCurChar = iCout;
        }
    }
}
