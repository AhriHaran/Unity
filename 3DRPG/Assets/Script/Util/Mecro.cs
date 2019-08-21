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

public enum CHAR_ANIMATOR
{
    CHAR_LOBBY_ANIMATOR,
    CHAR_BATTLE_ANIMATOR,
}

public enum ITEM_TYPE   //아이템 테이블 이름
{
    ITEM_NONE,
    ITEM_GAUNTLET,  //주먹 아이템
    ITEM_SWORD,
    ITEM_STIGMA_TOP,
    ITEM_STIGMA_CENTER,
    ITEM_STIGMA_BOTTOM,
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
public class CharInfoData
{
    public int CharIndex;   //캐릭터 순서
    public int CharLevel;   //캐릭터 레벨
    public int CharCurEXP;  //캐릭터 현재 EXP
    public int CharWeapon;  //캐릭터 웨폰
    public int CharStigmaTop;//캐릭터 스트그마 상
    public int CharStigmaCenter;//캐릭터 스트그마 중
    public int CharStigmaBottom;//캐릭터 스트그마 하

    public CharInfoData(int Index)
    {
        CharIndex = Index;
        CharLevel = 1;
        CharCurEXP = 0;
        CharWeapon = -1;
        CharStigmaTop = -1;
        CharStigmaCenter = -1;
        CharStigmaBottom = -1;
    }
    public CharInfoData() { } //정보 갱신용
}

[System.Serializable]
public class ItemInfoData
{
    public string ItemType; //아이템 타입
    public int ItemIndex;
    public int ItemLevel;
    public int ItemCurEXP;

    public ItemInfoData(string strType, int iIndex)
    {
        ItemType = strType;
        ItemIndex = iIndex;
        ItemLevel = 1;
        ItemCurEXP = 0;
    }
    public ItemInfoData() { }  //정보 갱신용
}