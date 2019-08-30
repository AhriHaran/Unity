public enum POOL_INDEX
{
    POOL_USER_CHAR,
    POOL_HP_ITEM,
    POOL_SP_ITEM,
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

public enum TARGET
{
    TARGET_PLAYER,
    TARGET_ENEMY,
}

public enum ITEM_TYPE   //아이템 테이블 이름
{
    ITEM_NONE,
    ITEM_STIGMA_TOP,
    ITEM_STIGMA_CENTER,
    ITEM_STIGMA_BOTTOM,
    ITEM_GAUNTLET,  //주먹 아이템
    ITEM_SWORD,
}

public enum SKILL_PROPERTY  //스킬 속성
{
    SKILL_FIRE,
    SKILL_ELECTRICITY,
}

public enum SKILL_TYPE
{
    TYPE_BUF,
    TYPE_DEBUF,
    TYPE_DEMAGE,
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
    public UserInfoData(string Name)
    {
        NickName = Name;
        Level = 1;
        Gold = 0;
        MainChar = 0;
        CurEXP = 0;
    }
}

[System.Serializable]
public class CharInfoData
{
    public int CharIndex;   //캐릭터 순서
    public int CharLevel;   //캐릭터 레벨
    public int CharCurEXP;  //캐릭터 현재 EXP
    public ITEM_TYPE CharWeaponType;   
    public int CharWeapon;  //캐릭터 웨폰
    public int CharStigmaTop;//캐릭터 스트그마 상
    public int CharStigmaCenter;//캐릭터 스트그마 중
    public int CharStigmaBottom;//캐릭터 스트그마 하

    public CharInfoData(int Index, ITEM_TYPE WeaponType)
    {
        CharIndex = Index;
        CharLevel = 1;
        CharCurEXP = 0;
        CharWeaponType = WeaponType;
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
    public ITEM_TYPE ItemType; //아이템 타입
    public int ItemIndex;
    public int ItemEquipChar;   //이 아이템을 장착한 이
    public int ItemLevel;
    public int ItemCurEXP;

    public ItemInfoData(ITEM_TYPE eType, int iIndex)
    {
        ItemType = eType;
        ItemIndex = iIndex;
        ItemEquipChar = -1;
        ItemLevel = 1;
        ItemCurEXP = 0;
    }
    public ItemInfoData() { }  //정보 갱신용
}