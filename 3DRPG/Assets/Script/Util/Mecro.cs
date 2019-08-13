﻿public enum POOL_INDEX
{
    POOL_USER_CHAR,
    POOL_ENEMY_CHAR,
    POOL_CHAR_INFO,
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
    CHAR_LEVEL,
    CHAR_MAX_HP,
    CHAR_CUR_HP,
    CHAR_MAX_SP,
    CHAR_CUR_SP,
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