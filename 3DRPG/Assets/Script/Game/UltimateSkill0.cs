using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSkill0 : MonoBehaviour
{
    public float m_fMaxDemage;
    public float m_fCriDemage;
    public float m_fRadius;

    private void OnEnable()
    {
        //레이캐스트로 충돌 처리
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_fRadius, 1 << LayerMask.NameToLayer("Enemy"));
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<EnemyScript>()?.Damege(m_fMaxDemage, m_fCriDemage);
        }
    }
}
