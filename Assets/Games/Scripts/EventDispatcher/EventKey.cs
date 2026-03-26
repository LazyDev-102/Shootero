
using Gemmob;
using UnityEngine;

public static class EventKey {

    #region GameEvents
    public struct GameStateChangedParam : IEventParams {
        public GameState gameState;
    }

    public struct GameStartParam : IEventParams {

    }

    public struct GameStartWaveParam : IEventParams {
        public int currentWaveIndex;
    }

    public struct ScoreChangedParam : IEventParams {
        public int score;
        public int scoreNeed;
    }

    public struct OnMinibossSpawnParam : IEventParams {
        public MinibossBase minibossBase;
        public bool isSpawn;
    }


    public struct OnBossSpawnParam : IEventParams {
        public BossBase bossBase;
        public bool isSpawn;
    }

    public struct OnBossRage : IEventParams {
        public BossBase bossBase;
        public bool isStart;
    }

    public struct OnShipInitilized : IEventParams {
        public ShipBase ship;
    }
    #endregion

    public struct ExplosionObject : IEventParams {
        public Vector3 Position;
        public float radius;
        public int Damage;
        public ObjectBase Causer;
    }
    public struct OnSelectShip : IEventParams {
        public int ID;
    }
    public struct OnBuyShip : IEventParams {
        public int ID;
    }
    public struct OnEnhanceShip : IEventParams {
        public int ID;
    }
    public struct OnEquipGear : IEventParams {

    }
    public struct OnExpChange : IEventParams {

    }
    public struct OnCoinChange : IEventParams {

    }
    public struct OnGemChange : IEventParams {

    }
    public struct OnInventoryChange : IEventParams {
        public int ID;
    }

    public struct OnMaxEnergyChange : IEventParams {
    }

    public struct OnShipChange : IEventParams {
        public int shipID;
    }
    public struct MaxHpUpModData : IEventParams {

    }
    public struct OnShipHitDamage : IEventParams {
        public ObjectBase Causer;
    }
    public struct OnShieldHitDamage : IEventParams {
        public ObjectBase Causer;
        public Transform Target;
        //public ShieldType shieldType;
    }
    public struct OnEnergyShieldHitDamage : IEventParams {
        public ObjectBase Causer;
        //public ShieldType shieldType;
        public int CurrentHP;
        public Transform Target;
    }
    public struct OnAuraHitDamage : IEventParams {
        public HitInfor Hit;
        public float PercentDamage;
    }
    public struct OnShipLevelUpInGame : IEventParams {
        public ShipBase Ship;
    }
    public struct OnStartGame : IEventParams {

    }
    public struct OnEnhanceGear : IEventParams {
        public int ID;
    }
    public struct OnEnoughDropPoint : IEventParams {
        public Vector2 Position;
    }
    public struct OnLoadScene : IEventParams {
        public SceneDefined.Index Index;
    }
    public struct OnBossHpChanged : IEventParams {
        public float Percent;
    }
    public struct OnClaimHalloweenMission : IEventParams {
        public Vector2 Position;
    }
    public struct OnClaimXmasMission : IEventParams {
        public Vector2 Position;
    }
#if CHEAT
    public struct OnEnemyDied : IEventParams {
    }
#endif
    #region UI Event

    public struct OnOpenChest : IEventParams {
        public GearSoftData newGear;
    }

    #endregion

    public enum StatEvent {
        Attack = 9000, Hp = 9001, DamageReduction = 9002, CritRate = 9003, CritDamage = 9004, AttackSpeed = 9005, BulletSpeed = 9006, BulletSize = 9007, DodgeRate = 9008,
        BurnDamage = 9009, BurnTime = 9010, BurnStack = 9011, BlastDamage = 9012, BlastRadius = 9013, Healing = 9014, ColliderDamage = 9015, Exp = 9016, Chip = 9017,
        BlockDamage = 9018, RecoverHP = 9019, MaxEnergy = 9020, DroneAttack = 9021, DroneHp = 9022, DroneFirerate = 9023, DroneAttackPercent = 9024, DroneHPPercent = 9025,
        DroneCooldown = 9026, DroneCritChance = 9027, DroneCritDamage = 9028, DroneDamageReduce = 9029, DroneBulletSpeed = 9030, DroneBulletSize = 9031, DroneEvasion = 9032,
        DroneBurnDamagePercent = 9033, DroneBurnDurationPercent = 9034, DroneBurnStack = 9035, DroneBlastDamagePercent = 9036, DroneBlastRadiusPercent = 9037, DroneBlockDamage = 9038,
        DroneBlockProbibility = 9039, DroneLaserDuration = 9040, None = 9041, DamagePerLevelIngame = 9042, HpPerlevelIngame = 9043, PierceStack = 9044, timeHoming = 9045, turnHoming = 9046,
        DamagePassive = 9047, HpPassive = 9048, BulletFadeTimeLife = 9049, Bounce = 9050, BulletTimeLife = 9051, LifeSteal = 9052,
    }
    public struct OnSelectGearUpgrade : IEventParams {
        public int ID;
        public int Rank;
    }


    #region Interger EventKey
    public const int OnGearInventoryChange = 10000;
    public const int OnAllNewGearChecked = 10001;
    public const int OnInventoryChanged = 10002;
    public const int OnLoadHomeScene = 10003;
    public const int OnBuyMoreEnergy = 10004;
    public const int OnPlayConquerorMode = 10005;
    public const int OnUpgradeGear = 10006;
    public const int OnUpgradeShip = 10007;
    public const int OnFuseGear = 10008;
    public const int OnDefeatEnemy = 10009;
    public const int OnDefeatBoss = 10010;
    public const int OnRevive = 10011;
    public const int OnTake4ngel = 10012;
    public const int OnDealWithSpaceMerchant = 10013;
    public const int OnOpenNormalChest = 10014;
    public const int OnOpenEliteChest = 10015;
    public const int OnPurchaseGemChipPack = 10016;
    public const int OnClaimMaxAfk = 10017;
    public const int OnCompleteChallenge = 10018;
    public const int OnLogin = 10019;
    public const int OnClaimDailyFree = 10020;
    public const int OnUpgradeAbility = 10021;
    public const int OnPurchaseDailyPacks = 10022;
    public const int OnLevelSystemUp = 10023;
    public const int OnChipChanged = 10024;
    public const int OnGemChanged = 10025;
    public const int OnEnergyChanged = 10026;
    public const int OnMaterialChanged = 10027;
    public const int OnPassExpChanged = 10028;
    public const int OnLoadLogoDone = 10029;
    public const int OnLoginGPSFinish = 11000;
    public const int OnWatchRewardAdSuccess = 11001;

    #endregion

    #region Halloween
    public const int HalloweenDefeatEnemy = 10031;
    public const int HalloweenDefeatMiniBoss = 10032;
    public const int HalloweenDefeatBoss = 10033;
    public const int HalloweenPlayGame = 10034;
    public const int HalloweenComplete = 10035;
    public const int HalloweenCompleteWin = 10036;
    public const int OnDropHalloweenCandy = 12010;
    public const int OnExchangeHalloweenCandy = 12011;
    public const int OnHTicketChanged = 12012;
    public const int OnHCandyChanged = 12013;
    #endregion

    #region Xmas
    public const int OnDropXmasCandy = 12100;
    public const int OnExchangeXmasCandy = 12101;
    public const int OnXTicketChanged = 12102;
    public const int OnXCandyChanged = 12103;
    public const int XmasDefeatEnemy = 12104;
    public const int XmasDefeatMiniBoss = 12105;
    public const int XmasDefeatBoss = 12106;
    public const int XmasPlayGame = 12107;
    public const int XmasComplete = 12108;
    public const int XmasCompleteWin = 12109;
    #endregion
}

