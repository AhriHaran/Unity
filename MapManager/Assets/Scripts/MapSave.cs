using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSave: MonoBehaviour
{
    public GameObject m_BackGround;
    List<GameObject> m_ListMapObject = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        int Child = m_BackGround.transform.childCount;

        for(int i = 0; i < Child; i++)
        {
            GameObject Back = m_BackGround.transform.GetChild(i).gameObject;
            m_ListMapObject.Add(Back);
            //백그라운드 소속 오브젝트를 모조리 넣어준다.
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void OnClick()
    {
        string strStageIndex = MapManager.instance.GetStageIndex();//현재 몇 스테이지 인가 확인

        if(strStageIndex != "")    //인덱스가 받아올 수 있다.
        {
            string strTmp = "Assets/Resources/" + strStageIndex + "Stage_Map.csv";
            using (var Writer = new CsvFileWriter(strTmp))  //using을 쓰고 난 후 해당 영역을 넘어가면 삭제
            {
                List<string> columns = new List<string>() { "Prefab", "LocX", "LocY", "LocZ", "QuaW", "QuaX", "QuaY", "QuaZ", "ScaleX", "ScaleY", "ScaleZ" };
                Writer.WriteRow(columns);
                columns.Clear();

                for(int i = 0;  i < m_ListMapObject.Count; i++)
                {
                    columns.Add(m_ListMapObject[i].name);   
                    //해당 프리펩의 이름
                    columns.Add(m_ListMapObject[i].transform.position.x.ToString());
                    columns.Add(m_ListMapObject[i].transform.position.y.ToString());
                    columns.Add(m_ListMapObject[i].transform.position.z.ToString());
                    //포지션
                    columns.Add(m_ListMapObject[i].transform.rotation.w.ToString());
                    columns.Add(m_ListMapObject[i].transform.rotation.x.ToString());
                    columns.Add(m_ListMapObject[i].transform.rotation.y.ToString());
                    columns.Add(m_ListMapObject[i].transform.rotation.z.ToString());
                    //쿼터니언 회전값
                    columns.Add(m_ListMapObject[i].transform.localScale.x.ToString());
                    columns.Add(m_ListMapObject[i].transform.localScale.y.ToString());
                    columns.Add(m_ListMapObject[i].transform.localScale.z.ToString());
                    //스케일
                    Writer.WriteRow(columns);
                    columns.Clear();
                }
                Writer.Dispose();
                Debug.Log("Map Save Complete");
            }
        }
        else Debug.Log("Map Save Failed");
        //현재 맵에 존재하는 모든 오브젝트의 정보
    }
}
