using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public float m_fdist = 10.0f;  //카메라와의 거리
    public float m_fheight = 5.0f; //카메라의 높이
    public float m_fSmoothRotate = 5.0f;    //부드러운 회전을 위해서
    private bool m_bSetting = false;
    private Transform m_PlayerTR;

    private void LateUpdate()
    {
        if(m_bSetting)
        {
            float curAngle = Mathf.LerpAngle(transform.eulerAngles.y, m_PlayerTR.eulerAngles.y, m_fSmoothRotate * Time.deltaTime);

            Quaternion Qu = Quaternion.Euler(0.0f, curAngle, 0.0f);
            transform.position = m_PlayerTR.position - (Qu * Vector3.forward * m_fdist)
                + (Vector3.up * m_fheight);
            transform.LookAt(m_PlayerTR);
        }
    }

    public void CameraSetting(Transform PlayerTR)
    {
        m_PlayerTR = PlayerTR;
        m_bSetting = true;
    }

}
