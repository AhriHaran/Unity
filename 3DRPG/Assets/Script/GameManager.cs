using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    /// <summary>
    /// 캐릭터 관련 인자들
    /// </summary>
    private int[] m_ListCharIndex;      //내가 선택한 캐릭터 인덱스들
    private int m_iCurSelectChar = -1;   //캐릭터 선택 단계에서 내가 선택한 캐릭터
    private int m_iCurGameChar = -1;     //게임 속에서 내가 현재 선택한 캐릭터
    private GameObject m_SelectCharMain = null;
    private GameObject m_SelectChar = null;    //내가 선택한 캐릭터의 프리팹

/// <summary>
/// 맵 관련 인자들
/// </summary>
    private int m_iCurStage = -1;       //현재 선택한 스테이지
    public List<Dictionary<MAP_DATA, object>> m_ListMapData = new List<Dictionary<MAP_DATA, object>>();

    /// <summary>
    /// 아이템 장착에 관련된 인자들
    /// </summary>
    private int m_iCurSelectItme = -1;
    private ITEM_TYPE m_eItemType;
    private INVENTORY_TYPE m_eInvenType;


    public void Init()
    {
        m_ListCharIndex = new int[] { -1, -1, -1 };   //3개
        m_iCurGameChar = -1;
        m_iCurSelectChar = -1;
        m_iCurSelectItme = -1;
        m_SelectCharMain = GameObject.Find("SelectCharModel");
    }

    /// <summary>
    /// 스테이지와 관련된 정보를 처리하는 구간
    /// </summary>
    /// <param name="iStage"></param>
    public void StageSelect(int iStage)
    {
        m_iCurStage = iStage;   //로비에서 스테이지 선택 시 임시 저장 변수
    }
    public string ReturnStage()
    {
        return m_iCurStage.ToString(); //현재 시작 스테이지 리턴
    }
    public void StageSelect(List<Dictionary<MAP_DATA, object>> MapData)
    {
        foreach (MAP_DATA E in System.Enum.GetValues(typeof(MAP_DATA)))
        {
            Dictionary<MAP_DATA, object> Node = new Dictionary<MAP_DATA, object>();
            Node.Add(E, MapData[(int)E][E]);
            m_ListMapData.Add(Node);
        }
        //스테이지 선택 시 
    }
    public object ReturnStageData(MAP_DATA eIndex)
    {
        return m_ListMapData[(int)eIndex][eIndex];
    }

    /// <summary>
    /// 캐릭터와 관련된 정보를 처리하는 구간
    /// </summary>
    /// <param name="iCharIndex"></param>
    public void CharSelect(int iCharIndex)
    {
        m_iCurSelectChar = iCharIndex;    //캐릭터 선택
    }
    bool CharOverLap()    //캐릭터 중복확인
    {
        for(int i = 0; i < 3; i++)
        {
            if (m_ListCharIndex[i] == m_iCurSelectChar)
                return false;
        }
        return true;
    }
    public bool CharSelectComplete(int iNum)    //몇 번째 패널인가
    {
        if (CharOverLap())
        {
            m_ListCharIndex[iNum] = m_iCurSelectChar; //인덱스 저장
            return true;
        }
        return false;
    }
    public int[] ReturnPlayerList()
    {
        return m_ListCharIndex;
    }
    public int ReturnCurSelectChar()
    {
        //내가 발키리 패널에서 선택한 캐릭터 인덱스
        return m_iCurSelectChar;
    }

    /// <summary>
    /// 아이템 장착에 관련된 함수
    /// </summary>
    /// <param name="eType"></param>
    public void ItemSelect(ITEM_TYPE eType)
    {
        m_eItemType = eType;    //내가 현재 이것의 장비 장착을 요구하였다.

        if (m_eItemType == ITEM_TYPE.ITEM_STIGMA_TOP || m_eItemType == ITEM_TYPE.ITEM_STIGMA_CENTER || m_eItemType == ITEM_TYPE.ITEM_STIGMA_BOTTOM)
            m_eInvenType = INVENTORY_TYPE.INVENTORY_STIGMA;
        else
            m_eInvenType = INVENTORY_TYPE.INVENTORY_WEAPON;

    }
    public ITEM_TYPE ReturnSelectType()
    {
        return m_eItemType;
    }
    public ITEM_TYPE ReturnSelectCharType()
    {
        //현재 선택된 캐릭터의 무기 타입을 리턴

        return (ITEM_TYPE)UserInfo.instance.GetCharData(CHAR_DATA.CHAR_WEAPON_TYPE, m_iCurSelectChar);
    }
    public INVENTORY_TYPE ReturnInvenType()
    {
        return m_eInvenType;
    }
    public int ReturnCurSelectItem()
    {
        return m_iCurSelectItme;
    }
    public void SelectCurItem(int iIndex)
    {
        m_iCurSelectItme = iIndex;
    }

    /// <summary>
    /// 캐릭터 선택 시 나오는 모델링 관련
    /// </summary>
    public void DestroyModel()
    {
        if (m_SelectChar != null)
        {
            CharPoolManager.instance.PushToPool(POOL_INDEX.POOL_USER_CHAR.ToString(), m_iCurSelectChar, m_SelectChar);
            m_SelectChar = null;
            m_SelectCharMain.transform.DetachChildren();
            //사용한 캐릭터 오브젝트 반납
        }
    }
    public void CreateModel(int iIndex)
    {
        if(m_SelectChar == null)
        {
            m_SelectChar = CharPoolManager.instance.PopFromPool(POOL_INDEX.POOL_USER_CHAR.ToString(), iIndex); //해당 인덱스를 반환해서 크리에이트
                                                                                                               //현재 지정한 캐릭터를 세팅
            m_SelectChar.SetActive(true);
            m_SelectChar.transform.SetParent(m_SelectCharMain.transform, false);
            m_SelectChar.GetComponent<Animator>().runtimeAnimatorController = UserInfo.instance.GetCharAnimator(iIndex, CHAR_ANIMATOR.CHAR_LOBBY_ANIMATOR) as RuntimeAnimatorController;
            //캐릭터 선택하면 메인 화면에 해당 캐릭터의 프리펩이 뜬다.
        }
    }
    public void ModelSetting(Vector3 vRotate, bool bSet)
    {
        if (m_SelectChar != null)
        {
            if (bSet)
            {
                ModelRotate(vRotate);
                Vector3 Local = Camera.main.transform.localPosition;
                Local.x = 1;
                Camera.main.transform.position = Local;
            }
            else
            {
                ModelRotate(new Vector3(0, 0, 0));
                Vector3 Local = Camera.main.transform.localPosition;
                Local.x = 0;
                Camera.main.transform.position = Local;
            }
        }
    }
    public void ModelRotate(Vector3 vRotate)
    {
        m_SelectChar.transform.localRotation = Quaternion.Euler(vRotate);
    }

    /// <summary>
    /// 게임 시작을 위한 정보를 처리하는 구간
    /// </summary>
    /// <returns></returns>
    public bool StageReady()
    {
        if (m_iCurStage != -1 && ifCharSelect() && ifEnergyEnough())  //스테이지 선택이 되었으며 하나 이상의 캐릭터가 선택되었다.
        {
            //플레이어의 에너지가 충분한가?
            for(int i = 0; i < 3; i++)
            {
                if (m_ListCharIndex[i] ==-1)//앞으로 정렬
                {
                    for (int j = i; j < 3 - 1; j++)
                    {
                        m_ListCharIndex[j] = m_ListCharIndex[j + 1];
                    }
                }
            }
            return true;
        }
        else
            return false;
    }
    public bool ifCharSelect()
    {
        for(int i = 0; i < m_ListCharIndex.Length; i++)
        {
            if (m_ListCharIndex[i] != -1)   //하나라도 선택된 게 있는가?
                return true;
        }
        return false;
    }
    public bool ifEnergyEnough()
    {
        int iCurEnergy = System.Convert.ToInt32(UserInfo.instance.GetUserData(USER_INFO.USER_INFO_CUR_ENERGY));
        var MapData = EXCEL.ExcelLoad.Read("Excel/Table/Stage_Table");
        int iStageEnergy = System.Convert.ToInt32(MapData[m_iCurSelectChar][MAP_DATA.MAP_ENERGY.ToString()]);
        //현재 남아있는 에너지 비교
        if (iCurEnergy >= iStageEnergy) //에너지 충분함
            return true;
        else
            return false;
    }
    public void GameStart()
    {
        LoadScene.SceneLoad("GameScene");
    }

    /// <summary>
    /// 게임 내에서 캐릭터 변경 등에 대한 함수
    /// </summary>
    public void PlayerCharChange(int iSelect)
    {
        m_iCurGameChar = iSelect;
    }
    public int ReturnCurPlayer()
    {
        return m_iCurGameChar;  //현재 선택된 캐릭터
    }
    public void ResetData()
    {
        m_iCurStage = -1;

        for(int i =0; i < 3; i++)
        {
            m_ListCharIndex[i] = -1;
        }

        m_iCurSelectChar = -1;
        m_iCurGameChar = -1;     //게임 속에서 내가 현재 선택한 캐릭터
        m_iCurSelectItme = -1;

        m_ListMapData.Clear();
        DestroyModel();
    }
}

