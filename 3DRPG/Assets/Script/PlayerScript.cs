using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float m_fSpeed = 10.0f;
    private Rigidbody m_Rigidbody;
    private Vector3 m_Vec3;


    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        m_Vec3.Set(h, 0, v);
        m_Vec3 = m_Vec3.normalized * m_fSpeed * Time.deltaTime;
        m_Rigidbody.MovePosition(transform.position + m_Vec3);
    }
}
