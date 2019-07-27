using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSave : MonoBehaviour
{
    public GameObject m_EventObject;
    List<GameObject> m_ListEvent = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        int Child = m_EventObject.transform.childCount;

        for (int i = 0; i < Child; i++)
        {
            GameObject Event = m_EventObject.transform.GetChild(i).gameObject;
            m_ListEvent.Add(Event);
            //시작 지점과 중간 지점... 끝 지점
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        string strStageIndex = MapManager.instance.GetStageIndex();//현재 몇 스테이지 인가 확인

        if (strStageIndex != "")    //인덱스가 받아올 수 있다.
        {
            string strTmp = "Assets/Resources/" + strStageIndex + "Stage_Event_Pos.csv";    //위치에 의한 이벤트
            using (var Writer = new CsvFileWriter(strTmp))  //using을 쓰고 난 후 해당 영역을 넘어가면 삭제
            {
                List<string> columns = new List<string>() { "Index", "LocX", "LocY", "LocZ" };
                Writer.WriteRow(columns);
                columns.Clear();

                for (int i = 0; i < m_ListEvent.Count; i++)
                {
                    columns.Add(i.ToString());
                    //해당 차일드의 순서 0, 1, 2 등등
                    columns.Add(m_ListEvent[i].transform.position.x.ToString());
                    columns.Add(m_ListEvent[i].transform.position.y.ToString());
                    columns.Add(m_ListEvent[i].transform.position.z.ToString());
                    //이벤트 이므로 위치만 필요
                    Writer.WriteRow(columns);
                    columns.Clear();
                }
                Debug.Log("EventPos Save Complete");
                Writer.Dispose();
            }

            strTmp = "Assets/Resources/" + strStageIndex + "Stage_Type.csv";    //해당 스테이지의 타입, 노말, 서바이브, 타임 어택
            using (var Writer = new CsvFileWriter(strTmp))  //using을 쓰고 난 후 해당 영역을 넘어가면 삭제
            {
                List<string> columns = new List<string>() { "Type", "Time"};
                Writer.WriteRow(columns);
                columns.Clear();

                string strType = MapManager.instance.GetStageType();//현재 스테이지 타입
                columns.Add(strType);

                strType = MapManager.instance.GetTimeLimit();//타임 리미트가 걸렸나?
                
                columns.Add(strType);
                Writer.WriteRow(columns);

                Debug.Log("StageType Save Complete");
                Writer.Dispose();
            }
            
        }
        else
          Debug.Log("Event Save Failed");
        //현재 맵에 존재하는 모든 오브젝트의 정보
    }
}
