public enum POOL_INDEX
{
    POOL_USER_CHAR,
    POOL_ENEMY_CHAR,
}

public enum USER_INFO
{
    USER_INFO_NICKNAME,
    USER_INFO_LEVEL,
    USER_INFO_CUR_ENERGY,
    USER_INFO_MAX_ENERGY,
    USER_INFO_CUR_EXP,
    USER_INFO_MAX_EXP,
    USER_INFO_GOLD,
    USER_INFO_MAIN_CHAR,
}

public enum CHAR_DATA
{
    CHAR_NAME,
    CHAR_ROUTE, //캐릭터 리소스 경로
    CHAR_INDEX,
    CHAR_LEVEL, //캐릭터 레벨
    CHAR_MAX_HP,    //캐릭터 HP
    CHAR_MAX_SP,
    CHAR_MAX_EXP,
    CHAR_CUR_EXP,
    CHAR_ATK,
    CHAR_DEF,
    CHAR_CRI,
    CHAR_WEAPON_INDEX,  //캐릭터가 가지고 있는 무기 인덱스 값
    CHAR_STIGMA_TOP_INDEX,  //스티그마 상
    CHAR_STIGMA_CENTER_INDEX,   //스티그마 중
    CHAR_STIGMA_BOTTOM_INDEX,   //스티그마 하
}
//캐릭터의 데이터를 관리, 적 아군 둘다 사용가능

public enum CHAR_ANIMATOR
{
    CHAR_LOBBY_ANIMATOR,
    CHAR_BATTLE_ANIMATOR,
}

public enum MAP_DATA
{
    MAP_TYPE,
    MAP_TIME,
    MAP_CLEAR_EXP,
    MAP_CLEAR_GOLD,
    MAP_CLEAR_ITEM,
}

[System.Serializable]
public class UserInfoData
{
    public string NickName; //유저 닉네임
    public int Level;       //유저 레벨
    public int CurEnergy;   //유저 현재 에너지
    public int CurEXP;      //유저 현재 경험치
    public int Gold;        //유저 골드
    public int MainChar;    //유저 메인 캐릭터
}

[System.Serializable]
public class UserCharInfoData
{
    public string CharName; //캐릭터 이름
    public string CharRoute; //캐릭터 저장 위치
    public int CharIndex;   //캐릭터 순서
    public int CharLevel;   //캐릭터 레벨
    public int CharCurEXP;  //캐릭터 현재 EXP
    public int CharWeapon;  //캐릭터 웨폰
    public int CharStigmaTop;//캐릭터 스트그마 상
    public int CharStigmaCenter;//캐릭터 스트그마 상
    public int CharStigmaBottom;//캐릭터 스트그마 상
}
