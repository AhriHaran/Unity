using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WAVE_STATE
{
    WAVE_NONE,  //아직 웨이브가 남아있다.
    WAVE_CLEAR, //현재 웨이브 클리어
    WAVE_END,    //웨이브가 끝났다.
    WAVE_FAILED,    //플레이어 캐릭터가 죽었다.
}

public class EnemyManager
{
    int m_iMaxWave;
    int m_iCurWave;
    List<GameObject> m_WaveObject = new List<GameObject>(); //웨이브 오브젝트를 관리
    List<List<GameObject>> m_ListEnemyObject = new List<List<GameObject>>();

    public EnemyManager(Transform Parent, List<Dictionary<string, object>> Pos, List<Dictionary<string, object>> Info)
    {
        //엑셀을 읽어와서
        //MaxWave, CurWave,	Prefab	LocX	LocY	LocZ	QuaW	QuaX	QuaY	QuaZ	ScaleX	ScaleY	ScaleZ
        m_iMaxWave = int.Parse(Pos[0]["MaxWave"].ToString());
        //전체 웨이브 숫자
        m_iCurWave = 0;
        //현재 웨이브 숫자

        Vector3 vecPos = new Vector3();
        Quaternion QueRot = new Quaternion();
        Vector3 vecSca = new Vector3();

        for (int i = 0; i < m_iMaxWave; i++)
        {
            List<GameObject> Node = new List<GameObject>();

            GameObject WaveObject = ResourceLoader.CreatePrefab("Prefabs/Wave", Parent);
            WaveObject.name = i.ToString() + "Wave";
            for(int j = 0; j < Pos.Count; j++)
            {
                if(i == int.Parse(Pos[j]["CurWave"].ToString()))
                {
                    int Index = int.Parse(Pos[j]["Index"].ToString());   //에너미 인덱스
                    string Rote = "Enemy/" + Index.ToString() + "/Prefabs/" + Index.ToString();
                    GameObject Enemy = ResourceLoader.CreatePrefab(Rote, WaveObject.transform);//Wave 하위로 셋팅
                    vecPos.Set(float.Parse(Pos[j]["LocX"].ToString()), float.Parse(Pos[j]["LocY"].ToString()), float.Parse(Pos[j]["LocZ"].ToString()));   //좌표
                    QueRot.Set(float.Parse(Pos[j]["QuaX"].ToString()), float.Parse(Pos[j]["QuaY"].ToString()), float.Parse(Pos[j]["QuaZ"].ToString()), float.Parse(Pos[j]["QuaW"].ToString()));   //회전
                    vecSca.Set(float.Parse(Pos[j]["ScaleX"].ToString()), float.Parse(Pos[j]["ScaleY"].ToString()), float.Parse(Pos[j]["ScaleZ"].ToString()));   //스케일

                    Enemy.transform.SetPositionAndRotation(vecPos, QueRot);   //맵의 오브젝트
                    Enemy.transform.localScale = vecSca;

                    EnemyScript script = Enemy.GetComponent<EnemyScript>();
                    script.enabled = true;
                    script.Setting(Index, Info);       //내부에 캐릭터 데이터 셋팅
                    Node.Add(Enemy);
                }
            }
            WaveObject.gameObject.SetActive(false); //웨이브 오브젝트를 꺼주고 순서에 맞게 다시 켜준다.
            m_WaveObject.Add(WaveObject);
            m_ListEnemyObject.Add(Node);
        }
    }

    public void ActiveWave()
    {
        //현재 웨이브 오브젝트들을 모두 활성화
        m_WaveObject[m_iCurWave].SetActive(true);
    }

    public void TrSetting(Transform Player)
    {
        for(int i = 0; i < m_ListEnemyObject[m_iCurWave].Count; i++)
        {
            if(m_ListEnemyObject[m_iCurWave][i].activeSelf) //해당 오브젝트가 살아있는 상태인가
                m_ListEnemyObject[m_iCurWave][i].GetComponent<EnemyScript>().TrSetting(Player);
        }
    }

    public void WaveClear(ref WAVE_STATE eState)
    {
        int iCount = 0;
        for (int i = 0; i < m_ListEnemyObject[m_iCurWave].Count; i++)
        {
            if (m_ListEnemyObject[m_iCurWave][i].activeSelf) //해당 오브젝트가 살아있는 상태인가
                iCount++; //해당 오브젝트가 하나라도 살아있다면 
        }

        if (iCount > 0)
        {
            eState = WAVE_STATE.WAVE_NONE;
        }
        else
        {
            m_WaveObject[m_iCurWave].SetActive(false);
            m_iCurWave++;
            if (m_iCurWave <= m_iMaxWave - 1)
                eState = WAVE_STATE.WAVE_CLEAR;
            else
                eState = WAVE_STATE.WAVE_END;
            //살아있는 것이 없다면
        }
    }
}
