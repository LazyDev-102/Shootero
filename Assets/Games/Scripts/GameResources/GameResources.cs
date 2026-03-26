using Gear_Data;
using UnityEngine;
using Gemmob;

[CreateAssetMenu(menuName = "GameResources", fileName = "Resource/GameResources", order = 10)]
public class GameResources : ScriptableObject {
    #region Singleton
    private static GameResources instance;
    public static GameResources Instance {
        get {
            if (instance == null) {
                instance = GameResourceLoader.Instance.GameResources;
                if (instance == null) {
                    string path = typeof(GameResources).Name;
                    instance = Resources.Load<GameResources>(path);
                }
            }
            return instance;
        }
    }
    #endregion

    private float chipPerSecond;
    private float materialPerSecond;
    private CacheLoader<PreloadGame> preloadGame = new CacheLoader<PreloadGame>();
    private CacheLoader<ConquerorData> conquerorData = new CacheLoader<ConquerorData>();
    private CacheLoader<InfinityModeData> infinityModeData = new CacheLoader<InfinityModeData>();
    private CacheLoader<EnemyData> enemyData = new CacheLoader<EnemyData>();
    private CacheLoader<DropData> dropData = new CacheLoader<DropData>();
    private CacheLoader<ModGenerator> modGenerator = new CacheLoader<ModGenerator>();
    private CacheLoader<Inventory> inventory = new CacheLoader<Inventory>();
    private CacheLoader<EnergyData> energyData = new CacheLoader<EnergyData>();
    private CacheLoader<LevelProgressData> levelProgress = new CacheLoader<LevelProgressData>();
    private CacheLoader<GearInventory> gearInventory = new CacheLoader<GearInventory>();
    private CacheLoader<ShipData> shipData = new CacheLoader<ShipData>();
    private CacheLoader<GearData> gearData = new CacheLoader<GearData>();
    private CacheLoader<RankInfinityData> rankInfinityData = new CacheLoader<RankInfinityData>();
    private CacheLoader<ShopData> shopData = new CacheLoader<ShopData>();
    private CacheLoader<RookieLoginData> rookieLoginData = new CacheLoader<RookieLoginData>();
    private CacheLoader<DailyLoginData> dailyLoginData = new CacheLoader<DailyLoginData>();
    private CacheLoader<ItemDatabase> itemDatabase = new CacheLoader<ItemDatabase>();
    private CacheLoader<DailyPacksData> dailyPacksData = new CacheLoader<DailyPacksData>();
    private CacheLoader<AfkData> afkData = new CacheLoader<AfkData>();
    private CacheLoader<DailyMissionData> dailyMission = new CacheLoader<DailyMissionData>();
    private CacheLoader<ChallengeData> challenge = new CacheLoader<ChallengeData>();
    private CacheLoader<SpaceMerchantData> spaceMerchant = new CacheLoader<SpaceMerchantData>();
    private CacheLoader<MysteryStationData> mysteryStation = new CacheLoader<MysteryStationData>();
    private CacheLoader<FullHealData> fullHeal = new CacheLoader<FullHealData>();
    private CacheLoader<AdsSpinData> adsSpin = new CacheLoader<AdsSpinData>();
    private CacheLoader<RateUsData> rateUs = new CacheLoader<RateUsData>();
    private CacheLoader<BattlePassData> battlePass = new CacheLoader<BattlePassData>();
    private CacheLoader<IapPackData> iapPack = new CacheLoader<IapPackData>();
    private CacheLoader<MaterialModeData> materialModeData = new CacheLoader<MaterialModeData>();
    private CacheLoader<GearModeData> gearModeData = new CacheLoader<GearModeData>();
    private CacheLoader<BossModeData> bossModeData = new CacheLoader<BossModeData>();
    private CacheLoader<ShipPackData> shipPackData = new CacheLoader<ShipPackData>();
    private CacheLoader<UserProfile> userProfile = new CacheLoader<UserProfile>();
    private CacheLoader<TutorialSytemData> tutorialSytemData = new CacheLoader<TutorialSytemData>();
    private CacheLoader<AbilityCollectorData> abilityCollectorData = new CacheLoader<AbilityCollectorData>();
    private CacheLoader<SkillSystemData> skillSystemData = new CacheLoader<SkillSystemData>();
    private CacheLoader<NewAbilityData> abilityData = new CacheLoader<NewAbilityData>();
    private CacheLoader<HalloweenModeData> halloweenModeData = new CacheLoader<HalloweenModeData>();
    private CacheLoader<HalloweenMissionData> halloweenMissionData = new CacheLoader<HalloweenMissionData>();
    private CacheLoader<HalloweenShopData> halloweenShopData = new CacheLoader<HalloweenShopData>();
    private CacheLoader<XmasModeData> xmasModeData = new CacheLoader<XmasModeData>();
    private CacheLoader<XmasMissionData> xmasMissionData = new CacheLoader<XmasMissionData>();
    private CacheLoader<XmasShopData> xmasShopData = new CacheLoader<XmasShopData>();


    public float ChipPerSecond { get => chipPerSecond; }
    public float MaterialPerSecond { get => materialPerSecond; }
    public PreloadGame PreloadGame => preloadGame.GetRef();
    public ConquerorData ConquerorData => conquerorData.GetRef();
    public InfinityModeData InfinityModeData => infinityModeData.GetRef();
    public Inventory Inventory => inventory.GetRef();
    public EnemyData EnemyData => enemyData.GetRef();
    public ConquerorDropSystem Drop => dropData.GetRef().DropSystem[(int)IngameData.currentGameMode];
    public LevelProgressData LevelProgress => levelProgress.GetRef();
    public EnergyData EnergyData => energyData.GetRef();
    public ModGenerator ModGenerator => modGenerator.GetRef();
    public GearInventory GearInventory => gearInventory.GetRef();
    public ShipData Ship => shipData.GetRef();
    public GearData GearData => gearData.GetRef();
    public RankInfinityData RankInfinityData => rankInfinityData.GetRef();
    public ShopData ShopData => shopData.GetRef();
    public RookieLoginData RookieLoginData => rookieLoginData.GetRef();
    public DailyLoginData DailyLoginData => dailyLoginData.GetRef();
    public ItemDatabase ItemDatabase => itemDatabase.GetRef();
    public DailyPacksData DailyPacksData => dailyPacksData.GetRef();
    public AfkData AFK => afkData.GetRef();
    public DailyMissionData DailyMission => dailyMission.GetRef();
    public ChallengeData Challenge => challenge.GetRef();
    public SpaceMerchantData SpaceMerchant => spaceMerchant.GetRef();
    public MysteryStationData MysteryStation => mysteryStation.GetRef();
    public FullHealData FullHeal => fullHeal.GetRef();
    public AdsSpinData AdsSpin => adsSpin.GetRef();
    public RateUsData RateUs => rateUs.GetRef();
    public BattlePassData BattlePass => battlePass.GetRef();
    public IapPackData IapPack => iapPack.GetRef();
    public MaterialModeData MaterialModeData => materialModeData.GetRef();
    public GearModeData GearModeData => gearModeData.GetRef();
    public BossModeData BossModeData => bossModeData.GetRef();
    public ShipPackData ShipPackData => shipPackData.GetRef();
    public UserProfile UserProfile => userProfile.GetRef();
    public TutorialSytemData TutorialSytemData => tutorialSytemData.GetRef();
    public AbilityCollectorData AbilityCollectorData => abilityCollectorData.GetRef();
    public SkillSystemData SkillSystemData => skillSystemData.GetRef();
    public NewAbilityData AbilityData => abilityData.GetRef();
    public HalloweenModeData Halloween => halloweenModeData.GetRef();
    public HalloweenMissionData HalloweenMission => halloweenMissionData.GetRef();
    public HalloweenShopData HalloweenShopData => halloweenShopData.GetRef();
    public XmasModeData Xmas => xmasModeData.GetRef();
    public XmasMissionData XmasMission => xmasMissionData.GetRef();
    public XmasShopData XmasShopData => xmasShopData.GetRef();

    public bool AutoPlay;

    public void Assign() {
        CheckDayOpenGame();
        OnStartGame();
        EventDispatcher.Instance.AddListener(EventKey.OnLevelSystemUp, RefreshChipMaterialPerHour);
        EventDispatcher.Instance.AddListener<EventKey.OnStartGame>(OnStartGame);
    }
    public void Unassign() {
        EventDispatcher.Instance.RemoveListener(EventKey.OnLevelSystemUp, RefreshChipMaterialPerHour);
        EventDispatcher.Instance.RemoveListener<EventKey.OnStartGame>(OnStartGame);
    }
    private void CheckDayOpenGame() {
        if (PrefSaver.Instance.IsNewDay()) {
            EnergyData.ResetAllRemain();
        }
    }
    private void OnStartGame() {
        MysteryStation.Reset();
        FullHeal.Reset();
        AdsSpin.Reset();
    }
    public void RefreshChipMaterialPerHour() {
        chipPerSecond = 20 * (LevelProgress.GetCurrentLevel() + 1) / (float)Constant.HourToSecond;
        materialPerSecond = chipPerSecond / 200f;
    }
    public void Reload() {
        Inventory.Reload();
        GearInventory.Reload();
        ConquerorData.Reload();
        SkillSystemData.Reload();
        AbilityCollectorData.Reload();
        PlayerStatManager.Instance.AssignData();
        IngameData.currentGameMode = GameMode.Conqueror;
        SpecialTriggerSystem.Instance.AddOnEnd(null);
    }
}
