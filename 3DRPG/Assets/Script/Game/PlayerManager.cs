using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    private List<GameObject> m_ListChar = new List<GameObject>();
    private List<PlayerScript> m_ScriptList = new List<PlayerScript>();

    public PlayerManager(Transform Parent, Transform Particle)
    {
        int[] iarr = GameManager.instance.ReturnPlayerList();
        GameObject GameUI = GameObject.Find("PlayerKey");
        //초기 셋팅만해놓는다.
        for(int i = 0; i < 3; i++)
        {
            if(iarr[i] != -1)
            {
                try
                {
                    string route = UserInfo.instance.GetCharData(CHAR_DATA.CHAR_INDEX, iarr[i]).ToString();
                    string name = UserInfo.instance.GetCharData(CHAR_DATA.CHAR_NAME, iarr[i]).ToString();
                    string CharRoute = "Player/" + route + "/Prefabs/" + name;

                    GameObject PlayerChar = ResourceLoader.CreatePrefab(CharRoute, Parent);
                    PlayerChar.GetComponent<Animator>().runtimeAnimatorController = UserInfo.instance.GetCharAnimator(iarr[i], CHAR_ANIMATOR.CHAR_BATTLE_ANIMATOR) as RuntimeAnimatorController;
                    //해당 캐릭터의 배틀 애니메이터 셋팅

                    int iIndex = Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_WEAPON_INDEX, iarr[i]));
                    ITEM_TYPE eType = (ITEM_TYPE)Util.ConvertToInt(UserInfo.instance.GetCharData(CHAR_DATA.CHAR_WEAPON_TYPE, iarr[i]));

                    CharRoute = "Player/" + route + "/Particle/UltimateSkill";
                    GameObject Ultimate = ResourceLoader.CreatePrefab(CharRoute);
                    Ultimate.transform.SetParent(Particle, false);
                    Ultimate.SetActive(false);

                    PlayerChar.GetComponent<WeaponPoint>().enabled = true;
                    PlayerChar.GetComponent<WeaponPoint>().ViewEffect(true);
                    PlayerScript script = PlayerChar.GetComponent<PlayerScript>();
                    script.enabled = true;
                    script.PlayerInit(Ultimate);
                    //해당 캐릭터의 플레이어 스크립트 설정

                    //플레이어 동작 스크립트
                    PlayerChar.SetActive(false);
                    m_ListChar.Add(PlayerChar);
                    m_ScriptList.Add(script);
                    //플레이어 캐릭터 셋팅
                    //해당 캐릭터의 파티클 임팩트
                }
                catch (System.NullReferenceException ex)
                {
                    Debug.Log(ex);
                }
            }
        }
    }

    public void PlayerSet(int iCount, Vector3 CharPos)  //바꾸려는 캐릭터 인덱스, 
    {
        //0,1,2 기준이며, 첫번째는 무조건 0
        int iCurChar = GameManager.instance.ReturnCurPlayer();  //현재 선택된 캐릭터를 호출
        //세 명의 캐릭터 중 가장 첫번째는 0번째 캐릭터 부터 호출된다.
        if(iCurChar != iCount) //현재 메인 캐릭터랑 다른 경우
        {
            if(iCurChar != -1)
            {
                CharPos = m_ListChar[iCurChar].transform.position;
                m_ListChar[iCurChar].SetActive(false);
                //캐릭터 변경 시에는 현재 캐릭터의 좌표를 기준으로 한다.
            }

            m_ListChar[iCount].SetActive(true);
            m_ScriptList[iCount].PlayerSet();
            m_ListChar[iCount].transform.position = CharPos; //포지션 셋팅
            GameManager.instance.PlayerCharChange(iCount);
        }
    }

    public bool PlayerDie()
    {
        for(int i = 0; i < m_ListChar.Count; i++)
        {
            if (!m_ScriptList[i].m_bDie)   //하나라도 살았으면
            {
                return false;
            }
        }
        return true;
    }

    public Transform GetCharTR()
    {
        int iCurChar = GameManager.instance.ReturnCurPlayer();
        return m_ListChar[iCurChar].transform;
    }
}
