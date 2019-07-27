using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    //맵의 네비 매쉬를 설정하고 맵의 클리어 조건들을 설정한다.
    List<Vector3> m_ListVec = new List<Vector3>();    //맵에 존재하는 모든 조건 백터
    List<string> m_ListStr = new List<string>();    //맵의 타입등의 조건

    string m_strStage;

    // Use this for initialization
    void Start()
    {
        GameObject BackGround = transform.GetChild(0).gameObject;
        m_strStage = GameManager.instance.ReturnStage(); //첫시작 
        string strFile = "Excel/" + m_strStage + "Stage_Map";

       List<Dictionary<string, object>> tmp = ExcelLoad.Read(strFile);
        //리스트로 저장 하고 해당 리스트에 맞춰서 맵 배치
        Vector3 vecPos = new Vector3();
        Vector3 vecRot = new Vector3();
        Vector3 vecSca = new Vector3();
        for (var i = 0; i < tmp.Count; i++)
        {
            string strText = "Prefabs/BackGround/" + tmp[i]["Prefab"].ToString();   //경로
            vecPos.Set(float.Parse(tmp[i]["LocX"].ToString()), float.Parse(tmp[i]["LocY"].ToString()), float.Parse(tmp[i]["LocZ"].ToString()));   //좌표
            vecRot.Set(float.Parse(tmp[i]["RotX"].ToString()), float.Parse(tmp[i]["RotY"].ToString()), float.Parse(tmp[i]["RotZ"].ToString()));   //회전
            vecSca.Set(float.Parse(tmp[i]["ScaleX"].ToString()), float.Parse(tmp[i]["ScaleY"].ToString()), float.Parse(tmp[i]["ScaleZ"].ToString()));   //스케일

            GameObject MapObject = ResourceLoader.CreatePrefab(strText, BackGround.transform);

            MapObject.transform.SetPositionAndRotation(vecPos, Quaternion.Euler(vecRot));   //맵의 오브젝트
            MapObject.transform.localScale = vecSca;
        }
        //맵 셋팅 
        strFile = "Excel/" + m_strStage + "Stage_Condition";
        tmp = ExcelLoad.Read(strFile);  //맵의 클리어 조건, 포지션에 의한 것
        /*
         * 플레이어의 첫 시작과 끝 포인트
         * 플레이어가 해당 구역에 들어오면 특정 적 리스폰
         * 등등 맵의 조건 중 포지션에 대한 조건
        */
        
        for(var i = 0; i < tmp.Count; i++)
        {
            Vector3 Vec = new Vector3();
            string strX = i + "ConLocX";
            string strY = i + "ConLocY";
            string strZ = i + "ConLocZ";
            Vec.Set(float.Parse(tmp[i][strX].ToString()), float.Parse(tmp[i][strY].ToString()), float.Parse(tmp[i][strZ].ToString()));
            m_ListVec.Add(Vec);
        }

        /*
         * 맵의 상태
         * 웨이브 형식이거나
         * 몇 마리의 몹을 제거하거나
         * 시간 제한 동안 살아남거나
         * 시간 제한 내에 모든 적을 제거하는 등의 컨셉
         */

        //
            
    }

    public List<Vector3> ReturnPos()
    {
        return m_ListVec;   //벡터 리스트를 반환 받는다.
    }

    // Update is called once per frame
    void Update()
    {

    }
}
