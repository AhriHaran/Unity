using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailedPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //실패하면 에너지만 깍인다.
        int ClearEnergy = Util.ConvertToInt(GameManager.instance.ReturnStageData(MAP_DATA.MAP_ENERGY));
        UserInfo.instance.UserUpdate(USER_INFO.USER_INFO_CUR_ENERGY, Util.SumData(UserInfo.instance.GetUserData(USER_INFO.USER_INFO_CUR_ENERGY), ClearEnergy, false));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
