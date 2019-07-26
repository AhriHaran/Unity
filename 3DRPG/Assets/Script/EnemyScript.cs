using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript
{
    private string m_strName;
    private GameObject m_EnemyObject;
    private bool m_bActive = false;
    Transform m_PlayerTR;
    Transform m_MyTR;
    // Start is called before the first frame update

    public void Init(string strEnemy, Transform PlayerTr)
    {
        m_strName = strEnemy;
        m_EnemyObject = GameObject.Find(strEnemy);
        m_bActive = false;
        m_PlayerTR = PlayerTr;
        m_MyTR = m_EnemyObject.GetComponent<Transform>();
    }

    public void Active()
    {
        if(m_strName != "Long")
           m_bActive = true;
    }

    // Update is called once per frame
    public void Update()
    {
        if(m_bActive)   //작동 가능
        {
            //우선은 플레이어를 쫒아오게만 설정 Long의 경우에는 트리거
            Vector3 vector3 = m_PlayerTR.position - m_MyTR.position;
            vector3.Normalize();
            m_MyTR.LookAt(m_PlayerTR);
            m_MyTR.Translate(vector3 * 5.0f * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            m_bActive = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_bActive = false;  //히트 콜리더에 적용되면 더이상 쫒지 않느다
        }
    }

}
