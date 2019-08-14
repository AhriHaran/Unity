using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLoadScene : MonoBehaviour
{
    public UISlider m_LoadSlider;   //슬라이드
    public UILabel m_StartLabel;
    public UIPanel m_UserCreate;
    AsyncOperation async_operation;
    private static bool m_bUserInfo = false;   //
    private bool m_bCreate = true;
    private string m_strNickName;
    private bool m_bLoadComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        //우선 처음 파일이 존재하는 가를 확인한다.

        string FilePath = Application.dataPath + "/Resources/Json/UserInfoData.json";
        if(System.IO.File.Exists(FilePath)) //해당 파일이 존재하는가?
        {
            //존재한다면 그것을 기반으로 Init하여라

            //처음 로딩 할 때, 유저 정보가 저장된 제이슨 파일을 불러오게 한다.

            UserInfo.instance.Init();   //여기서 json을 깐다.
        }
        else
        {
            //존재하지 않다면 닉네임을 생성하고 만들어줘라
            m_UserCreate.gameObject.SetActive(true);
            m_bCreate = true;
            //없을 경우 생성후 INit
        }
        
        //처음 로딩 할 때, 유저 정보가 저장된 제이슨 파일을 불러오게 한다.

        UserInfo.instance.Init();   //여기서 json을 깐다.
        GameManager.instance.Init();

        m_StartLabel.gameObject.SetActive(false);
        StartCoroutine(SceneLoad());
    }

    public void OnClick()
    {
        UIInput  Input = m_UserCreate.transform.GetChild(2).GetComponent<UIInput>();

        if(Input.value != string.Empty)
        {
            //적힌 것이 있다면
            m_strNickName = Input.value;
        }
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
