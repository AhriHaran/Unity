using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoSingleton<UserInfo>
{
    UserData m_UserData;    //유저 데이터
    // Start is called before the first frame upda te
    void Start()
    {
        m_UserData = new UserData();    //유저 데이터 테이블
        /*
         * 내가 가진 캐릭터 테이블
         * 내가 가진 장비 테이블
         * 내가 가진 스티그마 테이블
         */

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
