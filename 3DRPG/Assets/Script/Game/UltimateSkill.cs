using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ULTIMATE_DATA
{
    ULTIMATE_INDEX,         //스킬 인덱스
    ULTIMATE_PROPERTY,      //스킬 속성
    ULTIMATE_TARGET_DATA,   //타겟 데이터
    ULTIMATE_TARGET_FACTOR, //타겟 데이터 팩터
}

public struct st_Skill
{
    public TARGET st_Target;
    public SKILL_TYPE st_eType;
    public CHAR_DATA st_eTargetData;
    public float st_fFactor;
}

public class UltimateSkill : MonoBehaviour
{
    public int m_iSkillIndex;
    public SKILL_PROPERTY m_eProperty;
    public List<st_Skill> m_ListSkillFactor = new List<st_Skill>();

    // Start is called before the first frame update
    void Start()
    {
        //var SkillTable = EXCEL.ExcelLoad.Read("Skill_Table");

        //string tmp = Util.ConvertToString(SkillTable[m_iSkillIndex][ULTIMATE_DATA.ULTIMATE_PROPERTY.ToString()]);
        //m_eProperty = (SKILL_PROPERTY)System.Enum.Parse(typeof(SKILL_PROPERTY), tmp);
        ////스킬의 속성
        //string [] Data = Util.ConvertToString(SkillTable[m_iSkillIndex][ULTIMATE_DATA.ULTIMATE_TARGET_DATA.ToString()]).Split('/');
        //string[] Factor = Util.ConvertToString(SkillTable[m_iSkillIndex][ULTIMATE_DATA.ULTIMATE_TARGET_FACTOR.ToString()]).Split('/');
        ///*TARGET_PLAYER;CHAR_MAX_HP;TYPE_DEBUF;
        // * ,TARGET_PLAYER;CHAR_ATK;TYPE_BUF;
        // * TARGET_ENEMY;CHAR_MAX_HP;TYPE_DEBUF;,
        // * TARGET_ENEMY;CHAR_DEF;TYPE_DEBUF;
        // * 50;,
        // * 70;
        // * 50;
        // * 70;
        //*/
        //for (int i = 0; i < Data.Length; i++)
        //{
        //    string[] smallData = Data[i].Split(',');
        //    string[] FactorData = Factor[i].Split(',');
        //    for (int j = 0; j < smallData.Length; j++ )
        //    {
        //        string[] CharData = smallData[j].Split(';');
        //        string[] FactorD = FactorData[j].Split(';');

        //        st_Skill Node = new st_Skill();
        //        Node.st_Target =(TARGET)System.Enum.Parse(typeof(TARGET), CharData[0]); //타겟
        //        Node.st_eTargetData = (CHAR_DATA)System.Enum.Parse(typeof(CHAR_DATA), CharData[1]);
        //        Node.st_eType = (SKILL_TYPE)System.Enum.Parse(typeof(SKILL_TYPE), CharData[2]);
        //        Node.st_fFactor = Util.ConvertToInt(FactorD[0]);

        //        m_ListSkillFactor.Add(Node);
        //    }
        //}
        ////노드 정리
    }

    private void OnCollisionEnter(Collision collision)
    {
        //해당 콜리더에 들어온 오브젝트 객체가 있다면?
        if (collision.gameObject.layer == 1 << LayerMask.NameToLayer("Enemy"))   //에너미 레이어인가?
        {
            collision.gameObject.GetComponent<EnemyScript>().Damege(100.0f, 50.0f);
            //foreach(var v in m_ListSkillFactor)
            //{
            //    if(v.st_Target == TARGET.TARGET_ENEMY)
            //    {
                   
            //    }
            //}
        }
        //else if (collision.gameObject.layer == 1 << LayerMask.NameToLayer("Player"))    //플레이어에 대한 데미지
        //{

        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
