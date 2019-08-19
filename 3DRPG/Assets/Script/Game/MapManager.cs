using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public enum MAP_DATA
{
    MAP_TYPE,
    MAP_TIME,
    MAP_ENERGY,
    MAP_CLEAR_EXP,
    MAP_CLEAR_GOLD,
    MAP_CLEAR_ITEM,
}

public class MapManager
{
    //맵의 오브젝트 데이터만 설정하고 맵의 시작과 끝 지점만을 가진다.
    public List<Vector3> m_ListEventPos = new List<Vector3>();  //맵 벡터 리스트
    public List<Dictionary<MAP_DATA, object>> m_ListObjectType = new List<Dictionary<MAP_DATA, object>>();

    public MapManager(Transform Parent)
    {
        string strStage = GameManager.instance.ReturnStage(); //첫시작 
        string strFile = "Excel/StageExcel/" + strStage + "Stage_Map";

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
        }
        //맵 셋팅 

        //맵 타입 설정
        strFile = "Excel/StageExcel/Stage_Table";
        tmp = EXCEL.ExcelLoad.Read(strFile);
        int iStage = int.Parse(strStage);
        //맵의 타입, 시간
        foreach(MAP_DATA E in System.Enum.GetValues(typeof(MAP_DATA)))
        {
            Dictionary<MAP_DATA, object> Node = new Dictionary<MAP_DATA, object>();
            Node.Add(E, tmp[iStage][E.ToString()]);
            m_ListObjectType.Add(Node);
        }

        strFile = "Excel/StageExcel/" + strStage + "Stage_Event_Pos";
        tmp = EXCEL.ExcelLoad.Read(strFile);
        //맵의 특정 이벤트 지점을 저장한 백터
        //            Index LocX    LocY LocZ
        foreach(var POS in tmp)
        {
            Vector3 Node = new Vector3(float.Parse(POS["LocX"].ToString()), 
                float.Parse(POS["LocY"].ToString()), float.Parse(POS["LocZ"].ToString()));
            m_ListEventPos.Add(Node);
        }        //출발 지점등

        GameManager.instance.StageSelect(m_ListObjectType); 
        //게임 매니저에 스테이지 관련 타입들을 저장한다.
    }

    public List<Vector3> ReturnEventPos()
    {
        return m_ListEventPos;
    }
    //맵 종료시 해당 맵 클리어 결산 보상을 준다.
}
