using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class MapManager : MonoBehaviour
{
    //맵의 네비 매쉬를 설정하고 맵의 클리어 조건들을 설정한다.
    public NavMeshSurface m_surfaces;
    public List<Vector3> m_ListEventPos = new List<Vector3>();
    string m_strStage;
    string m_strType;
    float m_fStageTime = 0.0f;

    // Use this for initialization
    void Start()
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

            GameObject MapObject = ResourceLoader.CreatePrefab(strText, transform);

            MapObject.transform.SetPositionAndRotation(vecPos, QueRot);   //맵의 오브젝트
            MapObject.transform.localScale = vecSca;
        }
        //맵 셋팅 

        //맵 타입 설정
        strFile = "Excel/" + m_strStage + "Stage_Type";
        tmp = EXCEL.ExcelLoad.Read(strFile);
        //맵의 타입, 시간

        m_strType = tmp[0]["Type"].ToString();  //맵 타입
        if (tmp[0]["Time"].ToString() == "NULL")
            m_fStageTime = 0.0f;
        else
            m_fStageTime = float.Parse(tmp[0]["Time"].ToString());
        
        m_surfaces.BuildNavMesh();

        strFile = "Excel/" + m_strStage + "Stage_Event_Pos";
        tmp = EXCEL.ExcelLoad.Read(strFile);
        //맵의 이벤트 포스

        //            Index LocX    LocY LocZ
        for (var i =0; i < tmp.Count; i++)
        {
            Vector3 Node = new Vector3(float.Parse(tmp[i]["LocX"].ToString()), float.Parse(tmp[i]["LocY"].ToString()), float.Parse(tmp[i]["LocZ"].ToString()));

            GameObject Object = new GameObject();
            Object.name = (i.ToString() +"EventPos");
            Object.transform.SetParent(transform);
            Object.transform.position = Node;

            m_ListEventPos.Add(Node);
        }
        //특정 이벤트를 위한 포지션 벡터
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Vector3> ReturnEventPos()
    {
        return m_ListEventPos;
    }
}
