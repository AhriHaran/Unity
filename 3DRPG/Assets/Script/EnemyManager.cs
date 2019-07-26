using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    List<EnemyScript> m_ListEnemy = new List<EnemyScript>();
    private int m_iMaxEnemy = 3;

    // Start is called before the first frame update
    void Start()
    {
        Transform PlayerTR = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    
        for(int i = 0; i < m_iMaxEnemy; i++)
        {
            string strIndex;
            if (i == 0)
                strIndex = "Near";
            else if (i == 1)
                strIndex = "Middle";
            else
                strIndex = "Long";
            EnemyScript Node = new EnemyScript();
            Node.Init(strIndex, PlayerTR);
            m_ListEnemy.Add(Node);
        }
    }

    public void EnterStage()
    {
        //플레이어가 스테이지에 들어왔으니 예정된 행동패턴을 보여라

        //네모는 곧장 플레이어에게로 오고, 나머지는 

        for(int i = 0; i < m_iMaxEnemy; i++)
        {
            m_ListEnemy[i].Active();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_iMaxEnemy; i++)
        {
            m_ListEnemy[i].Update();
        }
    }
}
