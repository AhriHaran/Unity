﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    int m_iMaxWave;
    int m_iCurWave;
    List<GameObject> m_WaveObject = new List<GameObject>(); //웨이브 오브젝트를 관리
    List<List<GameObject>> m_ListEnemyObject = new List<List<GameObject>>();

    public EnemyManager(Transform Parent, Transform Player)
    {
        //엑셀을 읽어와서
        string iStage = GameManager.instance.ReturnStage();
        iStage = "Excel/StageExcel/" + iStage + "Stage_Enemy";
        
        var EnemyExcel = EXCEL.ExcelLoad.Read(iStage);
        var EnemyInfo = EXCEL.ExcelLoad.Read("Excel/CharacterExcel/Enemy_Char_Info");   //에너미 캐릭터 정보를 담을 그릇

        //MaxWave, CurWave,	Prefab	LocX	LocY	LocZ	QuaW	QuaX	QuaY	QuaZ	ScaleX	ScaleY	ScaleZ
        m_iMaxWave = int.Parse(EnemyExcel[0]["MaxWave"].ToString());
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
            for(int j = 0; j < EnemyExcel.Count; j++)
            {
                if(i == int.Parse(EnemyExcel[j]["CurWave"].ToString()))
                {
                    int Index = int.Parse(EnemyExcel[j]["Index"].ToString());   //에너미 인덱스
                    string Rote = "Enemy/" + EnemyInfo[Index][CharacterData.CHAR_ENUM.CHAR_ROUTE.ToString()].ToString() + "Prefabs/"
                        + EnemyInfo[Index][CharacterData.CHAR_ENUM.CHAR_NAME.ToString()].ToString();

                    GameObject Enemy = ResourceLoader.CreatePrefab(Rote, WaveObject.transform);//Wave 하위로 셋팅
                    vecPos.Set(float.Parse(EnemyExcel[j]["LocX"].ToString()), float.Parse(EnemyExcel[j]["LocY"].ToString()), float.Parse(EnemyExcel[j]["LocZ"].ToString()));   //좌표
                    QueRot.Set(float.Parse(EnemyExcel[j]["QuaX"].ToString()), float.Parse(EnemyExcel[j]["QuaY"].ToString()), float.Parse(EnemyExcel[j]["QuaZ"].ToString()), float.Parse(EnemyExcel[j]["QuaW"].ToString()));   //회전
                    vecSca.Set(float.Parse(EnemyExcel[j]["ScaleX"].ToString()), float.Parse(EnemyExcel[j]["ScaleY"].ToString()), float.Parse(EnemyExcel[j]["ScaleZ"].ToString()));   //스케일

                    Enemy.transform.SetPositionAndRotation(vecPos, QueRot);   //맵의 오브젝트
                    Enemy.transform.localScale = vecSca;

                    EnemyScript script = Enemy.GetComponent<EnemyScript>();
                    script.enabled = true;
                    script.Setting(Index, EnemyInfo, Player);       //내부에 캐릭터 데이터 셋팅
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

    }

    public void Hit()
    {

    }
}