using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public float m_fdist = 5.0f;  //카메라와의 거리
    public float m_fheight = 3.0f; //카메라의 높이
    public float m_fSmoothRotate = 5.0f;    //부드러운 회전을 위해서
    private Transform m_PlayerTR = null;

    public void CameraSet(Transform Player)
    {
        m_PlayerTR = Player;
    }

    private void LateUpdate()
    {
        if(m_PlayerTR != null)
        {
            float curAngle = Mathf.LerpAngle(transform.eulerAngles.y, m_PlayerTR.eulerAngles.y, m_fSmoothRotate * Time.deltaTime);

            Quaternion Qu = Quaternion.Euler(0.0f, curAngle, 0.0f);
            transform.position = m_PlayerTR.position - (Qu * Vector3.forward * m_fdist)
                + (Vector3.up * m_fheight);
            transform.LookAt(m_PlayerTR);
        }
    }
    //캐릭터를 바꿀 때나 캐릭터를 셋팅 완료 했을 경우 카메라를 그 캐릭터 기준으로 바꿔준다.
}
