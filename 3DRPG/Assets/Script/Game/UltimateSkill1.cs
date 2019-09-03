using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSkill1 : MonoBehaviour
{
    public int m_iSkillIndex;
    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == LayerMask.NameToLayer("Enemy"))   //에너미 레이어인가?
        {
            other.GetComponent<EnemyScript>().Damege(100.0f, 50.0f);
        }
    }

}
