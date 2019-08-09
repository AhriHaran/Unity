using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLoadScene : MonoBehaviour
{
    public UISlider m_LoadSlider;   //슬라이드
    public UILabel m_StartLabel;
    AsyncOperation async_operation;
    private static bool m_bUserInfo = false;   //
    private bool m_bLoadComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        UserInfo.instance.Init();
        GameManager.instance.Init();

        m_StartLabel.gameObject.SetActive(false);
        StartCoroutine(SceneLoad());
    }

    public void UserInfoComplete()
    {
        m_bUserInfo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_bLoadComplete && m_bUserInfo)  //로딩 슬라이드가 꽉차고, 유저 데이터 불러오기가 완료 되었으면
        {
            if(Input.GetMouseButtonDown(0)) //로딩 슬라이더가 다 차면 클릭만으로 넘어간다.
            {
                async_operation.allowSceneActivation = true;
            }
        }
    }

    private IEnumerator SceneLoad()
    {
        yield return null;

        async_operation = SceneManager.LoadSceneAsync("LobbyScene");   //씬 매니저로 로딩
        async_operation.allowSceneActivation = false;
        float timer = 0.0f;

        while (!async_operation.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (async_operation.progress >= 0.9f)
            {
                m_LoadSlider.value = Mathf.Lerp(m_LoadSlider.value, 1.0f, timer);

                if (m_LoadSlider.value == 1.0f)
                {
                    m_bLoadComplete = true;
                    m_LoadSlider.gameObject.SetActive(false);
                    m_StartLabel.gameObject.SetActive(true);    //라벨을 셋팅 해주고 이제 클릭만 해주면 된다.
                }
            }
            else
            {
                m_LoadSlider.value = Mathf.Lerp(m_LoadSlider.value, async_operation.progress, timer);
                //매스 공통 수학 함수, 시간에 따라서 슬라이드 밸류와, 싱크 프로그레스 사이를 보간하기 위해서
                if (m_LoadSlider.value >= async_operation.progress)
                {
                    timer = 0f;
                }
            }
        }
    }
}
