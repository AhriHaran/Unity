using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    enum OBJECT_INDEX
    {
        OBJECT_BACKGROUND,
        OBJECT_ENEMY,
        OBJECT_PLAYER,
    }

    public FollowCam m_MainCamera;
    private GameObject[] m_arrObject;
    //private GameObject m_EnemyObject;
    //private GameObject m_PlayerObject;
    // Start is called before the first frame update
    void Start()
    {
        int ChildCount = transform.childCount;
        m_arrObject = new GameObject[ChildCount];
        for (int i = 0; i < ChildCount; i++)
        {
            m_arrObject[i] = transform.GetChild(i).gameObject;
        }

        //하위 오브젝트를 모두 받아온다.
        string strIndex = GameManager.instance.ReturnStage();
        string strFile = "Excel/" + strIndex + "Stage_Map";

        List<Dictionary<string, object>> Map = ExcelLoad.Read(strFile);
        //리스트로 저장 하고 해당 리스트에 맞춰서 맵 배치
        Vector3 vecPos = new Vector3();
        Vector3 vecRot = new Vector3();
        Vector3 vecSca = new Vector3();
        for (var i = 0; i < Map.Count; i++)
        {
            string strText = "Prefabs/BackGround/" + Map[i]["Prefab"].ToString();   //경로
            vecPos.Set(float.Parse(Map[i]["LocX"].ToString()), float.Parse(Map[i]["LocY"].ToString()), float.Parse(Map[i]["LocZ"].ToString()));   //좌표
            vecRot.Set(float.Parse(Map[i]["RotX"].ToString()), float.Parse(Map[i]["RotY"].ToString()), float.Parse(Map[i]["RotZ"].ToString()));   //회전
            vecSca.Set(float.Parse(Map[i]["ScaleX"].ToString()), float.Parse(Map[i]["ScaleY"].ToString()), float.Parse(Map[i]["ScaleZ"].ToString()));   //스케일
            GameObject MapObject= ResourceLoader.CreatePrefab(strText, m_arrObject[(int)OBJECT_INDEX.OBJECT_BACKGROUND].transform);

            MapObject.transform.SetPositionAndRotation(vecPos, Quaternion.Euler(vecRot));   //맵의 오브젝트
            MapObject.transform.localScale = vecSca;
        }
        //맵 셋팅 
        strFile = "Excel/" + strIndex + "Stage_Condition";
        Map = ExcelLoad.Read(strFile);  //맵의 클리어 조건 등

        vecPos.Set(float.Parse(Map[0]["StartLocX"].ToString()), float.Parse(Map[0]["StartLocY"].ToString()), float.Parse(Map[0]["StartLocZ"].ToString()));  //플레이어의 시작 위치

        GameObject Player = ResourceLoader.CreatePrefab("Prefabs/Player/Player", m_arrObject[(int)OBJECT_INDEX.OBJECT_PLAYER].transform);
        Player.transform.position = vecPos;
        //플레이어 캐릭터 셋팅
        m_MainCamera.CameraSetting(Player.transform);

        //스테이지를 관리하는 스테이지 매니저가 필요
        //에너미 오브젝트 풀


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
