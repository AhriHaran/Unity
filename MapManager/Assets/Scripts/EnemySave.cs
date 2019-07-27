using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySave : MonoBehaviour
{
    public GameObject m_EnemyObject;
    List<GameObject> m_ListEnemy = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        int Child = m_EnemyObject.transform.childCount;

        for (int i = 0; i < Child; i++)
        {
            GameObject Back = m_EnemyObject.transform.GetChild(i).gameObject;
            m_ListEnemy.Add(Back);
            //에너미 오브젝트에 속해 있는 모든 오브젝트를 받아온다.
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
            string strTmp = "Assets/Resources/" + strStageIndex + "Stage_Enemy.csv";    //해당 스테이지의 적들
            using (var Writer = new CsvFileWriter(strTmp))  //using을 쓰고 난 후 해당 영역을 넘어가면 삭제
            {
                List<string> columns = new List<string>() { "Wave", "Prefab", "LocX", "LocY", "LocZ", "QuaW", "QuaX", "QuaY", "QuaZ", "ScaleX", "ScaleY", "ScaleZ" };
                Writer.WriteRow(columns);
                columns.Clear();

                for (int i = 0; i < m_ListEnemy.Count; i++)
                {
                    for (int j = 0; j < m_ListEnemy[i].transform.childCount; j++)//해당 웨이브에 존재하는 적들을 저장
                    {
                        columns.Add(i.ToString());  //가장 위쪽 즉, 웨이브
                        GameObject Enemy = m_ListEnemy[i].transform.GetChild(j).gameObject; //해당 웨이브의 하위 오브젝트
                        columns.Add(Enemy.name);
                        columns.Add(Enemy.transform.position.x.ToString());
                        columns.Add(Enemy.transform.position.y.ToString());
                        columns.Add(Enemy.transform.position.z.ToString());
                        //포지션
                        columns.Add(Enemy.transform.rotation.w.ToString());
                        columns.Add(Enemy.transform.rotation.x.ToString());
                        columns.Add(Enemy.transform.rotation.y.ToString());
                        columns.Add(Enemy.transform.rotation.z.ToString());
                        //쿼터니언 회전값
                        columns.Add(Enemy.transform.localScale.x.ToString());
                        columns.Add(Enemy.transform.localScale.y.ToString());
                        columns.Add(Enemy.transform.localScale.z.ToString());
                        //스케일
                        Writer.WriteRow(columns);
                        columns.Clear();
                    }
                }
                Writer.Dispose();
                Debug.Log("Enemy Save Complete");
            }
            Debug.Log("Enemy Save Failed");
        }
        //현재 맵에 존재하는 모든 오브젝트의 정보

    }
}
