using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class MapManager
{
    //맵의 오브젝트 데이터만 설정하고 맵의 시작과 끝 지점만을 가진다.
    public List<Vector3> m_ListEventPos = new List<Vector3>();
    public List<GameObject> m_ListMapObject = new List<GameObject>();    
    string m_strStage;  //맵의 인덱스
    string m_strType;   //맵의 타입
    float m_fStageTime = 0.0f;  //맵의 제한 시간

    public MapManager(Transform Parent)
    {
        m_strStage = GameManager.instance.ReturnStage(); //첫시작 
        string strFile = "Excel/StageExcel/" + m_strStage + "Stage_Map";

        List<Dictionary<string, object>> tmp = EXCEL.ExcelLoad.Read(strFile);
        //리스트로 저장 하고 해당 리스트에 맞춰서 맵 배치
        Vector3 vecPos = new Vector3();
        Quaternion QueRot = new Quaternion();
        Vector3 vecSca = new Vector3();

        for (var i = 0; i < tmp.Count; i++)
        {
            string strText = "Prefabs/BackGround/" + tmp[i]["Prefab"].ToString();   //경로
            vecPos.Set(float.Parse(tmp[i]["LocX"].ToString()), float.Parse(tmp[i]["LocY"].ToString()), float.Parse(tmp[i]["LocZ"].ToString()));   //좌표
            QueRot.Set(float.Parse(tmp[i]["QuaX"].ToString()), float.Parse(tmp[i]["QuaY"].ToString()), float.Parse(tmp[i]["QuaZ"].ToString()), float.Parse(tmp[i]["QuaW"].ToString()));   //회전
            vecSca.Set(float.Parse(tmp[i]["ScaleX"].ToString()), float.Parse(tmp[i]["ScaleY"].ToString()), float.Parse(tmp[i]["ScaleZ"].ToString()));   //스케일

            GameObject MapObject = ResourceLoader.CreatePrefab(strText, Parent);

            MapObject.transform.SetPositionAndRotation(vecPos, QueRot);   //맵의 오브젝트
            MapObject.transform.localScale = vecSca;
            m_ListMapObject.Add(MapObject);
        }
        //맵 셋팅 

        //맵 타입 설정
        strFile = "Excel/StageExcel/Stage_Type";
        tmp = EXCEL.ExcelLoad.Read(strFile);
        int iStage = int.Parse(m_strStage);
        //맵의 타입, 시간
        m_strType = tmp[iStage]["Type"].ToString();  //맵 타입
        if (tmp[iStage]["Time"].ToString() == "NULL")
            m_fStageTime = 0.0f;
        else
            m_fStageTime = float.Parse(tmp[iStage]["Time"].ToString());

        GameManager.instance.StageSelect(m_strType, m_fStageTime);
        strFile = "Excel/StageExcel/" + m_strStage + "Stage_Event_Pos";
        tmp = EXCEL.ExcelLoad.Read(strFile);
        //맵의 특정 이벤트 지점을 저장한 백터
        //            Index LocX    LocY LocZ
        for (var i = 0; i < tmp.Count; i++)
        {
            Vector3 Node = new Vector3(float.Parse(tmp[i]["LocX"].ToString()), float.Parse(tmp[i]["LocY"].ToString()), float.Parse(tmp[i]["LocZ"].ToString()));
            m_ListEventPos.Add(Node);
        }
        //출발 지점과 끝나는 지점
    }

    public List<Vector3> ReturnEventPos()
    {
        return m_ListEventPos;
    }
}
