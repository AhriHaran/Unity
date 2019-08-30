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
    private bool m_bLoadComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        //우선 처음 파일이 존재하는 가를 확인한다.

        if(JSON.JsonUtil.FileCheck("UserInfoData")) //해당 파일이 존재하는가?
        {
            //존재한다면 그것을 기반으로 Init하여라
            //처음 로딩 할 때, 유저 정보가 저장된 제이슨 파일을 불러오게 한다.
            UserInfo.instance.Init();   //여기서 json을 깐다.
        }
        else
        {
            //존재하지 않다면 닉네임을 생성하고 만들어줘라
            m_UserCreate.gameObject.SetActive(true);
            //없을 경우 생성후 INit
        }
        
        //처음 로딩 할 때, 유저 정보가 저장된 제이슨 파일을 불러오게 한다.

        m_StartLabel.gameObject.SetActive(false);
        StartCoroutine(SceneLoad());
    }

    public void OnClick()
    {
        UIInput  Input = m_UserCreate.transform.GetChild(2).GetComponent<UIInput>();

        if(Input.value != string.Empty)
        {
            //적힌 것이 있다면
            //유저 데이터를 기반으로 저장
            UserInfoData Data = new UserInfoData(Input.value);
            var Table = EXCEL.ExcelLoad.Read("Excel/Table/UserTable");
            Data.CurEnergy = int.Parse(Table[0][USER_INFO.USER_INFO_MAX_ENERGY.ToString()].ToString());
            string jsonData = JSON.JsonUtil.ToJson(Data);
            Debug.Log(jsonData);
            JSON.JsonUtil.CreateJson("UserInfoData", jsonData);
            //유저 데이터 JSON(초기 유저 데이터)

            CharInfoData[] Char = new CharInfoData[2];
            Char[0] = new CharInfoData(0, ITEM_TYPE.ITEM_GAUNTLET);
            Char[0].CharWeapon = 0;
            Char[1] = new CharInfoData(1, ITEM_TYPE.ITEM_SWORD);
            Char[1].CharWeapon = 1;
            string CharData = JSON.JsonUtil.ToJson<CharInfoData>(Char);
            Debug.Log(CharData);
            JSON.JsonUtil.CreateJson("UserCharInfoData", CharData);
            //초기 캐릭터 데이터 JSON

            ItemInfoData[] Item = new ItemInfoData[2];
            Item[0] = new ItemInfoData(ITEM_TYPE.ITEM_GAUNTLET, 0);//아이템 타입, 아이템 인덱스
            Item[0].ItemEquipChar = 0;
            Item[1] = new ItemInfoData(ITEM_TYPE.ITEM_SWORD, 0);
            Item[1].ItemEquipChar = 1;
            string WeaponData = JSON.JsonUtil.ToJson<ItemInfoData>(Item);
            Debug.Log(WeaponData);
            JSON.JsonUtil.CreateJson(INVENTORY_TYPE.INVENTORY_WEAPON.ToString(), WeaponData);


            //ItemInfoData[] StigmaList = new ItemInfoData[3];
            //for (int i = 0; i < 3; i++)
            //{
            //    try
            //    {
            //        ITEM_TYPE type = ITEM_TYPE.ITEM_NONE;
            //        if (i == 0)
            //        {
            //            type = ITEM_TYPE.ITEM_STIGMA_TOP;
            //        }
            //        else if (i == 1)
            //        {
            //            type = ITEM_TYPE.ITEM_STIGMA_CENTER;
            //        }
            //        else if (i == 2)
            //        {
            //            type = ITEM_TYPE.ITEM_STIGMA_BOTTOM;

            //        }
            //        StigmaList[i] = new ItemInfoData(type, 0);
            //    }
            //    catch (System.NullReferenceException ex)
            //    {
            //        Debug.Log(ex);
            //    }
            //}
            //string Stigma = JSON.JsonUtil.ToJson<ItemInfoData>(StigmaList);
            //Debug.Log(Stigma);
            //JSON.JsonUtil.CreateJson(INVENTORY_TYPE.INVENTORY_STIGMA.ToString(), Stigma);
            ////유저의 초기 데이터들

            UserInfo.instance.Init();
            m_UserCreate.gameObject.SetActive(false);
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
