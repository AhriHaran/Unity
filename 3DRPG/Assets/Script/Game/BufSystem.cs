using UnityEngine;
using System.Collections;

/*버프는 일정량의 캐릭터 계수가 오르는 것
 * 디버프는 일정량의 캐릭터 계수가 떨어지는 것
 * 형식은
 * 버프 인덱스;버프 타입;, 버프 대상 데이터;버프 타입;, 버프 계수, 버프 타임
 */

public class BufSystem : MonoBehaviour
{
    private bool m_bBuf = false;
    private bool m_bDeBuf = false;
    private GameObject m_TargetObject;  //버프 타겟

    // Use this for initialization
    void Start()
    {
        m_TargetObject = transform.gameObject;  //대상 타겟
    }

    //버프
    IEnumerator BufStart(float fTime)
    {
        yield return new WaitForSeconds(fTime);
    }

    //public void StartBuf(int iSkill, ULTIMATE_SKILL SkillType)
    //{
    //    if(!m_bBuf)
    //    {
    //        int iIndex = (int)eIndex;

    //        var BufTable = EXCEL.ExcelLoad.Read("Buf_Table");
    //        BufTable[iSkill]

    //        m_bBuf = true;
    //    }
    //}

    public void StopBuf()
    {
        StopCoroutine("BufStart");
        m_bBuf = false;
    }

    //디버프
    IEnumerator DeBufStart(float fTime)
    {
        yield return new WaitForSeconds(fTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
