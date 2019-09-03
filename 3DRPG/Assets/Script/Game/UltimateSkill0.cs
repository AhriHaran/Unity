using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSkill0 : MonoBehaviour
{
    public int m_iSkillIndex;

    private void OnCollisionEnter(Collision collision)
    {
        //해당 콜리더에 들어온 오브젝트 객체가 있다면?
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))   //에너미 레이어인가?
        {
            collision.gameObject.GetComponent<EnemyScript>().Damege(100.0f, 50.0f);
        }
    }

}
