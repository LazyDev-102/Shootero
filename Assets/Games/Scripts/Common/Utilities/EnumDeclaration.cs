
public class GameTag {
    public const string Player = "Player";
    public const string Enemy = "Enemy";
    public const string EnemyBlockPierce = "EnemyBlockPierce";
    public const string EnemyBullet = "EnemyBullet";
    public const string ShieldEnemy = "ShieldEnemy";
    public const string ShieldShip = "ShieldShip";
    public const string Respawn = "Respawn";
    public const string Finish = "Finish";


}
public class GameLayer {
    public const string Default = "Default";
    public const string Ship = "Ship";
    public const string Enemy = "Enemy";
    public const string Effect = "Effect";
    public const string UI = "UI";
    public const string Tutorial = "Tutorial";
    public const int EnemyBulletIndex = 10;
}
public class GameSortingLayer {
    public const string BG = "BG";
    public const string Default = "Default";
    public const string Ship = "Ship";
    public const string ShipBullet = "ShipBullet";
    public const string Enemy = "Enemy";
    public const string EnemyBullet = "EnemyBullet";
    public const string Effect = "Effect";
    public const string UI = "UI";
    public const string Tutorial = "Tutorial";
}
public class GameDefine {
    public const string InsufficientResources = "Insufficient Resources";
    public const string LoginSuccess = "Login Success!";
    public const string LoginFail = "Login Fail!";
    public const string InternetDisconnected = "Internet disconnected!";
    public const string WaitForGPS = "Wait for Google Play Games initialize!";
    public const string UnlockPreviousAbility = "Unlock previous ability Lv.5!";
    public const string UpgradeYourGrade = "Upgrade your grade to buy more!";
    public const string ExchangeSuccess = "Exchange Success!";
    public const string Success = "Success!";
}
public enum TargetType {
    Player, Enemy, ShieldEnemy, ShieldShip, EnemyBlockPierce
}

public enum ToolBarType {
    Shop = 0,
    Gears = 1,
    Conqueror = 2,
    Ability = 3,
    Infinity = 4,
}
public enum GearMenuType {
    Weapon = 0,
    Shield = 1,
    Core = 2,
    Engine = 3,
    Drone = 4,
}
public enum GearType {
    All = 0,
    Weapon = 1,
    Shield = 2,
    Reactor = 3,
    Propulsion = 4,
    Drone1 = 5,
    Drone2 = 6,
}
public enum GearTypeSort {
    All = 0,
    Weaponry = 1,
    Shield = 2,
    Reactor = 3,
    Propulsion = 4,
}
public enum Rarety {
    Basic = 0,
    Common = 1,
    Rare = 2,
    Elite = 3,
    Legend = 4,
}
public enum CurrencyType {
    Chip = 1,
    Gem = 2,
    Energy = 3,
}
public enum ShieldType {
    ProtectShield = 1,
    EnergyShield = 2,
}
public enum LoadSceneType {
    LoadNormal = 0,
    LoadAsyn = 1,
}
public enum TrackingUserProperty {
    CurrentProgress = 0,
    CurrentUpgrade = 1,
}
public enum MissionType {
    Login, CompleteChallenge, PlayConquerorMode, BuyEnergy, ClaimAFKReward, ClaimDailyFreePack, UpgradeGearSlot, UpgradeShip, FuseGear, UpgradeAbility, PurchaseDailyPack,
    OpenNormalChest, OpenEliteChest, PurchaseGemChipPack, DefeatEnemy, DefeatBoss,
}
public enum ChallengeType {
    DefeatEnemy, DefeatBoss, PlayConquerorMode, BuyEnergy, Revive, Take4ngelOffer, DealWithSpaceMerchant, OpenNormalChest, OpenEliteChest, UpgradeAnyGearSlot, FuseAnyGear, FuseAnyShip,
    PurchaseGemChipPack, ClaimMaxAfkReward,
}
public enum WaveType {
    Boss, Miniboss, Normal, Tutorial, Bonus, Trap,
}
public enum EnemyTier {
    Tier0,
    Tier1,
    Tier2,
    Tier3,
    Tier4,
}
public enum InfinityTierType {
    Enemy,
    Miniboss,
    Boss,
}
public enum SmartOfferType {
    Common,
    Rare,
    Epic,
    Legend,
}
public enum MaterialModeTierType {
    Enemy,
    Miniboss,
    Boss,
}
public enum GearModeTierType {
    Enemy,
    Miniboss,
    Boss,
}
public enum HalloweenTierType {
    Enemy,
    Miniboss,
    Boss,
}
public enum XmasTierType {
    Enemy,
    Miniboss,
    Boss,
}
public enum ShotPatternType {
    Basic,
    Double,
    Tripple,
    Gatling,
    Gun,
    X,
    Plasma,
    Strike,
}
public enum SkillType {
    Active,
    Passive,
}
public enum HalloweenMissionType {
    PlayGame, WinGame, DefeatEnemy, DefeatMiniBoss, DefeatBoss, ReachWave,
}