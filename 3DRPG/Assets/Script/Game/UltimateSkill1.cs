using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSkill1 : MonoBehaviour
{
    public float m_fMaxDemage;
    public float m_fCriDemage;
    public float m_fRadius;
    
    private void OnEnable()
    {
        RaycastHit[] Hit = Physics.SphereCastAll(transform.position, m_fRadius, transform.forward);
        for(int i = 0; i < Hit.Length; i++)
        {
            if (Hit[i].collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Hit[i].collider.gameObject.GetComponent<EnemyScript>()?.Damege(m_fMaxDemage, m_fCriDemage);
            }
        }
    }
}
