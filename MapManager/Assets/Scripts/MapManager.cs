using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapManager : MonoSingleton<MapManager>
{
    public InputField m_Input;
    public Dropdown m_Drop;
    public InputField m_TimeInput;

    string m_strIndex;

    // Start is called before the first frame update
    void Start()
    {
        m_TimeInput.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetStageIndex()
    {
        return m_Input.text.ToString();
    }

    public string GetTimeLimit()
    {
        if (m_Drop.value != 0)
            return m_TimeInput.text.ToString();
        return "NULL";
    }

    public string GetStageType()
    {
        /*
         * 노말 방식 : 현재 방에 존재하는 모든 적을 해치우면 엔드 포인트가 열린다.
         * 필요 한 것은 현재 방의 몬스터 갯수, 몇 번 스폰 되는 가, 시작과 끝 지점
         * 타임 어택 : 시간 내로 모든 적을 제거한다.
         * 맥스 타임과 기타 등등
         * 서바이브 방식 : 시간이 끝날때까지 살아남는다.
         * 
         */
        //스테이지 종류
        return m_Drop.captionText.text.ToString();
    }

    public void ChangeValue()
    {
        if (m_Drop.value != 0)
            m_TimeInput.gameObject.SetActive(true); //노말 방식이 아니라면 타임 리미트를 걸어 줄 수 있다.
        else
            m_TimeInput.gameObject.SetActive(false);
    }
}
