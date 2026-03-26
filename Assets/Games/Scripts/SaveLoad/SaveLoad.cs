
using SimpleJSON;
using System.Linq;
using UnityEngine;

public static class SaveLoad {
    private const string inventoryKey = "ivt";
    private const string abilityCollectorKey = "ac";
    private const string rankInfinityDataKey = "ri";
    private const string shipDataDataKey = "sd";
    private const string energyDataKey = "ed";
    private const string gearInventoryKey = "git";
    private const string shopDataKey = "shop";
    private const string conquerorData = "cd";
    private const string levelProgression = "lp";
    private const string dailyLogin = "dl";
    private const string rookieLogin = "rl";
    private const string tutorialData = "ntd";
    private const string dailyPacksData = "dpd";
    private const string dailyMissionKey = "dmk";
    private const string challengeKey = "clk";
    private const string afkKey = "afk";
    private const string adsSpin = "ask";
    private const string rateUsKey = "ruk";
    private const string battlePassKey = "bpk";
    private const string useProfileKey = "upk";
    private const string iapPackKey = "ipk";
    private const string materialModeKey = "mmk";
    private const string gearModeKey = "gmk";
    private const string bossModeKey = "bossk";
    private const string shipPackKey = "shippk";
    private const string skillsSystemData = "skills";
    private const string newAbilityKey = "nak";
    public static bool Isnitialized;

    public static void Save2Json() {
        GameResources resource = GameResources.Instance;
        JSONNode node = new JSONObject();

        //immediate save when data changed
        node.Add(JsonKey.GearInventoryKey, resource.GearInventory.Save2Json());
        node.Add(JsonKey.ShopDataKey, resource.ShopData.Save2Json());
        node.Add(JsonKey.AfkKey, resource.AFK.Save2Json());
        node.Add(JsonKey.DailyMissionKey, resource.DailyMission.Save2Json());
        node.Add(JsonKey.RookieLogin, resource.RookieLoginData.Save2Json());
        node.Add(JsonKey.ChallengeKey, resource.Challenge.Save2Json());
        node.Add(JsonKey.DailyPacksData, resource.DailyPacksData.Save2Json());
        node.Add(JsonKey.IapPackKey, resource.IapPack.Save2Json());
        node.Add(JsonKey.BattlePassKey, resource.BattlePass.Save2Json());
        node.Add(JsonKey.ShipPackKey, resource.ShipPackData.Save2Json());
        node.Add(JsonKey.SkillsSystemData, resource.SkillSystemData.Save2Json());
        node.Add(JsonKey.AbilityData, resource.AbilityData.Save2Json());

        //save when convevient
        node.Add(JsonKey.LevelProgression, resource.LevelProgress.Save2Json());
        node.Add(JsonKey.InventoryKey, resource.Inventory.Save2Json());
        node.Add(JsonKey.DailyLogin, resource.DailyLoginData.Save2Json());
        node.Add(JsonKey.AbilityCollectorKey, resource.AbilityCollectorData.Save2Json());
        node.Add(JsonKey.RankInfinityDataKey, resource.RankInfinityData.Save2Json());
        node.Add(JsonKey.EnergyDataKey, resource.EnergyData.Save2Json());
        node.Add(JsonKey.ConquerorData, resource.ConquerorData.Save2Json());//unlock zone, cur zone,..
        node.Add(JsonKey.TutorialData, resource.TutorialSytemData.Save2Json());

        //after update ship
        node.Add(JsonKey.ShipDataKey, resource.Ship.Save2Json());

        //mode 
        node.Add(JsonKey.MaterialModeKey, resource.MaterialModeData.Save2Json());
        node.Add(JsonKey.GearModeKey, resource.GearModeData.Save2Json());
        node.Add(JsonKey.BossModeKey, resource.BossModeData.Save2Json());


        //not important
        node.Add(JsonKey.AdsSpin, resource.AdsSpin.Save2Json());
        node.Add(JsonKey.RateUsKey, resource.RateUs.Save2Json());
        node.Add(JsonKey.UseProfileKey, resource.UserProfile.Save2Json());


        //holiday event
        node.Add(JsonKey.HalloweenData, resource.Halloween.Save2Json());
        node.Add(JsonKey.HalloweenMissionData, resource.HalloweenMission.Save2Json());
        node.Add(JsonKey.HalloweenShopData, resource.HalloweenShopData.Save2Json());
        node.Add(JsonKey.XmasData, resource.Xmas.Save2Json());
        node.Add(JsonKey.XmasMissionData, resource.XmasMission.Save2Json());
        node.Add(JsonKey.XmasShopData, resource.XmasShopData.Save2Json());

        SaveLocalData(node.ToString());
        PrefSaver.ConvertedData = true;
    }

    public static void LoadFJson() {
        string data = LoadLocalData();
        GameResources resource = GameResources.Instance;

        if (data.Trim() != "") {
            JSONNode node = JSONNode.Parse(data);

            resource.LevelProgress.LoadFJson(node[JsonKey.LevelProgression].AsObject);
            resource.Inventory.LoadFJson(node[JsonKey.InventoryKey].AsArray);
            resource.GearInventory.LoadFJson(node[JsonKey.GearInventoryKey].AsObject);
            resource.AbilityCollectorData.LoadFJson(node[JsonKey.AbilityCollectorKey].AsObject);
            resource.RankInfinityData.LoadFJson(node[JsonKey.RankInfinityDataKey].AsObject);
            resource.Ship.LoadFJson(node[JsonKey.ShipDataKey].AsObject);
            resource.EnergyData.LoadFJson(node[JsonKey.EnergyDataKey].AsObject);
            resource.ShopData.LoadFJson(node[JsonKey.ShopDataKey].AsObject);
            resource.ConquerorData.LoadFJson(node[JsonKey.ConquerorData].AsObject);
            resource.AFK.LoadFJson(node[JsonKey.AfkKey].AsObject);
            resource.DailyMission.LoadFJson(node[JsonKey.DailyMissionKey].AsObject);
            resource.Challenge.LoadFJson(node[JsonKey.ChallengeKey].AsObject);
            resource.DailyLoginData.LoadFJson(node[JsonKey.DailyLogin].AsObject);
            resource.RookieLoginData.LoadFJson(node[JsonKey.RookieLogin].AsObject);
            resource.TutorialSytemData.LoadFJson(node[JsonKey.TutorialData].AsObject);
            resource.DailyPacksData.LoadFJson(node[JsonKey.DailyPacksData].AsArray);
            resource.AdsSpin.LoadFJson(node[JsonKey.AdsSpin].AsObject);
            resource.RateUs.LoadFJson(node[JsonKey.RateUsKey].AsObject);
            resource.IapPack.LoadFJson(node[JsonKey.IapPackKey].AsObject);
            resource.UserProfile.LoadFJson(node[JsonKey.UseProfileKey].AsObject);
            resource.BattlePass.LoadFJson(node[JsonKey.BattlePassKey].AsObject);
            resource.MaterialModeData.LoadFJson(node[JsonKey.MaterialModeKey].AsObject);
            resource.GearModeData.LoadFJson(node[JsonKey.GearModeKey].AsObject);
            resource.BossModeData.LoadFJson(node[JsonKey.BossModeKey].AsObject);
            resource.ShipPackData.LoadFJson(node[JsonKey.ShipPackKey].AsArray);
            resource.SkillSystemData.LoadFJson(node[JsonKey.SkillsSystemData].AsObject);
            resource.AbilityData.LoadFJson(node[JsonKey.AbilityData].AsObject);
            resource.Halloween.LoadFJson(node[JsonKey.HalloweenData].AsObject);
            resource.HalloweenMission.LoadFJson(node[JsonKey.HalloweenMissionData].AsObject);
            resource.HalloweenShopData.LoadFJson(node[JsonKey.HalloweenShopData].AsObject);
            resource.Xmas.LoadFJson(node.HasKey(JsonKey.XmasData) ? node[JsonKey.XmasData].AsObject : null);
            resource.XmasMission.LoadFJson(node.HasKey(JsonKey.XmasMissionData) ? node[JsonKey.XmasMissionData].AsObject : null);
            resource.XmasShopData.LoadFJson(node.HasKey(JsonKey.XmasShopData) ? node[JsonKey.XmasShopData].AsObject : null);
        }
        else {
            resource.LevelProgress.LoadFJson(null);
            resource.Inventory.LoadFJson(null);
            resource.GearInventory.LoadFJson(null);
            resource.AbilityCollectorData.LoadFJson(null);
            resource.RankInfinityData.LoadFJson(null);
            resource.Ship.LoadFJson(null);
            resource.EnergyData.LoadFJson(null);
            resource.ShopData.LoadFJson(null);
            resource.ConquerorData.LoadFJson(null);
            resource.AFK.LoadFJson(null);
            resource.DailyMission.LoadFJson(null);
            resource.Challenge.LoadFJson(null);
            resource.DailyLoginData.LoadFJson(null);
            resource.RookieLoginData.LoadFJson(null);
            resource.TutorialSytemData.LoadFJson(null);
            resource.DailyPacksData.LoadFJson(null);
            resource.AdsSpin.LoadFJson(null);
            resource.RateUs.LoadFJson(null);
            resource.IapPack.LoadFJson(null);
            resource.UserProfile.LoadFJson(null);
            resource.BattlePass.LoadFJson(null);
            resource.MaterialModeData.LoadFJson(null);
            resource.GearModeData.LoadFJson(null);
            resource.BossModeData.LoadFJson(null);
            resource.ShipPackData.LoadFJson(null);
            resource.SkillSystemData.LoadFJson(null);
            resource.AbilityData.LoadFJson(null);
            resource.Halloween.LoadFJson(null);
            resource.HalloweenMission.LoadFJson(null);
            resource.HalloweenShopData.LoadFJson(null);
            resource.Xmas.LoadFJson(null);
            resource.XmasMission.LoadFJson(null);
            resource.XmasShopData.LoadFJson(null);
        }
    }

    public static void Save() {
        Save2Json();
        if (PrefSaver.ConvertedData && PrefSaver.PlayAsAccount) {
            GameLogin.Instance.UploadUserData(null);
            return;
        }


        SaveLevelProgressionData();
        SaveInventory();
        SaveGearInventory();
        SaveAbilityCollector();
        SaveRankInfinityData();
        SaveShipData();
        SaveEnergyData();
        SaveShopData();
        SaveConquerorData();
        SaveAfkData();
        SaveDailyMissionData();
        SaveChallengeData();
        SaveDailyLoginData();
        SaveRookieLoginData();
        SaveTutorialData();
        SaveDailyPacksData();
        SaveAdsSpinData();
        SaveRateUsData();
        SaveIapPackData();
        SaveUseProfile();
        SaveBattlePassData();
        SaveMaterialModeData();
        SaveGearModeData();
        SaveBossModeData();
        SaveShipPackData();
        SaveSkillsSystemData();
        SaveNewAbilityData();
    }

    public static void Load() {
        if (PrefSaver.ConvertedData) {
            LoadFJson();
            Isnitialized = true;
            return;
        }

        LoadLevelProgressionData();
        LoadChipMaterialPerHour();
        LoadInventory();
        LoadGearInventory();
        LoadRankInfinityData();
        LoadShipData();
        LoadEnergyData();
        LoadShopData();
        LoadConquerorData();
        LoadAfkData();
        LoadDailyMissionData();
        LoadChallengeData();
        LoadDailyLoginData();
        LoadRookieLoginData();
        LoadDailyPacksData();
        LoadAdsSpinData();
        LoadRateUsData();
        LoadIapPackData();
        LoadUseProfile();
        LoadBattlePassData();
        LoadMaterialModeData();
        LoadGearModeData();
        LoadBossModeData();
        LoadShipPackData();
        LoadSkillsSystemData();
        LoadNewAbilityData();
        GameResources.Instance.Halloween.LoadFJson(null);
        GameResources.Instance.HalloweenMission.LoadFJson(null);
        GameResources.Instance.HalloweenShopData.LoadFJson(null);
        GameResources.Instance.Xmas.LoadFJson(null);
        GameResources.Instance.XmasMission.LoadFJson(null);
        GameResources.Instance.XmasShopData.LoadFJson(null);
        Isnitialized = true;
    }

    public static void GameResourceLoaderLoad() {
        if (!PrefSaver.ConvertedData) {
            LoadTutorialData();
            LoadAbilityCollector();
        }
    }

    public static string LoadLocalData() {
        HandleTextFile.ReadString(GameURL.DataPath, out string data);
        return data;
    }

    public static void SaveLocalData(string data) {
        HandleTextFile.WriteString(GameURL.DataPath, data);
        if (data != "")
            PrefSaver.ConvertedData = true;
    }

    #region Inventory
    private static void SaveInventory() {
        PlayerPrefs.SetString(inventoryKey, GameResources.Instance.Inventory.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadInventory() {
        string json = PlayerPrefs.GetString(inventoryKey);
        GameResources.Instance.Inventory.LoadFromJson(json);
    }
    #endregion

    #region AbilityCollector
    private static void SaveAbilityCollector() {
        PlayerPrefs.SetString(abilityCollectorKey, GameResources.Instance.AbilityCollectorData.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadAbilityCollector() {
        string json = PlayerPrefs.GetString(abilityCollectorKey);
        GameResources.Instance.AbilityCollectorData.LoadFromJson(json);
    }
    #endregion

    #region RankInfinityData
    private static void SaveRankInfinityData() {
        PlayerPrefs.SetString(rankInfinityDataKey, GameResources.Instance.RankInfinityData.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadRankInfinityData() {
        string json = PlayerPrefs.GetString(rankInfinityDataKey);
        GameResources.Instance.RankInfinityData.LoadFromJson(json);
    }
    #endregion

    #region ShipData
    private static void SaveShipData() {
        PlayerPrefs.SetString(shipDataDataKey, GameResources.Instance.Ship.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadShipData() {
        string json = PlayerPrefs.GetString(shipDataDataKey);
        GameResources.Instance.Ship.LoadFromJson(json);
    }
    #endregion

    #region EnergyData
    public static void SaveEnergyData() {
        PlayerPrefs.SetString(energyDataKey, GameResources.Instance.EnergyData.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadEnergyData() {
        string json = PlayerPrefs.GetString(energyDataKey);
        GameResources.Instance.EnergyData.LoadFromJson(json);
    }
    #endregion

    #region GearInventory
    private static void SaveGearInventory() {
        PlayerPrefs.SetString(gearInventoryKey, GameResources.Instance.GearInventory.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadGearInventory() {
        string json = PlayerPrefs.GetString(gearInventoryKey);
        GameResources.Instance.GearInventory.LoadFromJson(json);
    }
    #endregion

    #region ShopData
    private static void SaveShopData() {
        PlayerPrefs.SetString(shopDataKey, GameResources.Instance.ShopData.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadShopData() {
        string json = PlayerPrefs.GetString(shopDataKey);
        GameResources.Instance.ShopData.LoadFromJson(json);
    }
    #endregion

    #region ConquerorData
    private static void SaveConquerorData() {
        PlayerPrefs.SetString(conquerorData, GameResources.Instance.ConquerorData.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadConquerorData() {
        string json = PlayerPrefs.GetString(conquerorData);
        GameResources.Instance.ConquerorData.LoadFromJson(json);
    }
    #endregion

    #region LevelProgression
    private static void SaveLevelProgressionData() {
        PlayerPrefs.SetString(levelProgression, GameResources.Instance.LevelProgress.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadLevelProgressionData() {
        string json = PlayerPrefs.GetString(levelProgression);
        GameResources.Instance.LevelProgress.LoadFromJson(json);
    }
    #endregion

    #region Daily Login
    private static void SaveDailyLoginData() {
        PlayerPrefs.SetString(dailyLogin, GameResources.Instance.DailyLoginData.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadDailyLoginData() {
        string json = PlayerPrefs.GetString(dailyLogin);
        GameResources.Instance.DailyLoginData.LoadFromJson(json);
    }
    #endregion

    #region Rookie Login
    private static void SaveRookieLoginData() {
        PlayerPrefs.SetString(rookieLogin, GameResources.Instance.RookieLoginData.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadRookieLoginData() {
        string json = PlayerPrefs.GetString(rookieLogin);
        GameResources.Instance.RookieLoginData.LoadFromJson(json);
    }
    #endregion

    #region Tutorial
    private static void SaveTutorialData() {
        PlayerPrefs.SetString(tutorialData, GameResources.Instance.TutorialSytemData.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadTutorialData() {
        string json = PlayerPrefs.GetString(tutorialData);
        GameResources.Instance.TutorialSytemData.LoadFromJson(json);
    }
    #endregion

    #region DailyMission
    private static void SaveDailyMissionData() {
        PlayerPrefs.SetString(dailyMissionKey, GameResources.Instance.DailyMission.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadDailyMissionData() {
        string json = PlayerPrefs.GetString(dailyMissionKey);
        GameResources.Instance.DailyMission.LoadFromJson(json);
    }
    #endregion

    #region ChallengeData
    private static void SaveChallengeData() {
        PlayerPrefs.SetString(challengeKey, GameResources.Instance.Challenge.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadChallengeData() {
        string json = PlayerPrefs.GetString(challengeKey);
        GameResources.Instance.Challenge.LoadFromJson(json);
    }
    #endregion

    #region DailyPacksData
    private static void SaveDailyPacksData() {
        PlayerPrefs.SetString(dailyPacksData, GameResources.Instance.DailyPacksData.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadDailyPacksData() {
        string json = PlayerPrefs.GetString(dailyPacksData);
        GameResources.Instance.DailyPacksData.LoadFromJson(json);
    }
    #endregion

    #region AFK Reward Data
    private static void SaveAfkData() {
        PlayerPrefs.SetString(afkKey, GameResources.Instance.AFK.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadAfkData() {
        string json = PlayerPrefs.GetString(afkKey);
        GameResources.Instance.AFK.LoadFromJson(json);
    }
    #endregion

    #region Ads Spin Data
    private static void SaveAdsSpinData() {
        PlayerPrefs.SetString(adsSpin, GameResources.Instance.AdsSpin.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadAdsSpinData() {
        string json = PlayerPrefs.GetString(adsSpin);
        GameResources.Instance.AdsSpin.LoadFromJson(json);
    }
    #endregion
    #region Ads Rate Us
    private static void SaveRateUsData() {
        PlayerPrefs.SetString(rateUsKey, GameResources.Instance.RateUs.SaveToJson());
        PlayerPrefs.Save();
    }

    private static void LoadRateUsData() {
        string json = PlayerPrefs.GetString(rateUsKey);
        GameResources.Instance.RateUs.LoadFromJson(json);
    }
    #endregion

    #region Iap Pack
    private static void SaveIapPackData() {
        PlayerPrefs.SetString(iapPackKey, GameResources.Instance.IapPack.SaveToJson());
        PlayerPrefs.Save();
    }
    private static void LoadIapPackData() {
        string json = PlayerPrefs.GetString(iapPackKey);
        GameResources.Instance.IapPack.LoadFromJson(json);
    }
    #endregion

    #region BattlePass
    private static void SaveBattlePassData() {
        PlayerPrefs.SetString(battlePassKey, GameResources.Instance.BattlePass.SaveToJson());
        PlayerPrefs.Save();
    }
    private static void LoadBattlePassData() {
        string json = PlayerPrefs.GetString(battlePassKey);
        GameResources.Instance.BattlePass.LoadFromJson(json);
    }
    #endregion

    #region MaterialModeData
    private static void SaveMaterialModeData() {
        PlayerPrefs.SetString(materialModeKey, GameResources.Instance.MaterialModeData.SaveToJson());
        PlayerPrefs.Save();
    }
    private static void LoadMaterialModeData() {
        string json = PlayerPrefs.GetString(materialModeKey);
        GameResources.Instance.MaterialModeData.LoadFromJson(json);
    }
    #endregion

    #region GearModeData
    private static void SaveGearModeData() {
        PlayerPrefs.SetString(gearModeKey, GameResources.Instance.GearModeData.SaveToJson());
        PlayerPrefs.Save();
    }
    private static void LoadGearModeData() {
        string json = PlayerPrefs.GetString(gearModeKey);
        GameResources.Instance.GearModeData.LoadFromJson(json);
    }
    #endregion
    #region BossModeData
    private static void SaveBossModeData() {
        PlayerPrefs.SetString(bossModeKey, GameResources.Instance.BossModeData.SaveToJson());
        PlayerPrefs.Save();
    }
    private static void LoadBossModeData() {
        string json = PlayerPrefs.GetString(bossModeKey);
        GameResources.Instance.BossModeData.LoadFromJson(json);
    }
    #endregion
    #region SkillsSystemData
    private static void SaveSkillsSystemData() {
        PlayerPrefs.SetString(skillsSystemData, GameResources.Instance.SkillSystemData.SaveToJson());
        PlayerPrefs.Save();
    }
    private static void LoadSkillsSystemData() {
        string json = PlayerPrefs.GetString(skillsSystemData);
        GameResources.Instance.SkillSystemData.LoadFromJson(json);
    }
    #endregion

    #region NewAbility
    private static void SaveNewAbilityData() {
        PlayerPrefs.SetString(newAbilityKey, GameResources.Instance.AbilityData.SaveToJson());
        PlayerPrefs.Save();
    }
    private static void LoadNewAbilityData() {
        string json = PlayerPrefs.GetString(newAbilityKey);
        GameResources.Instance.AbilityData.LoadFromJson(json);
    }
    #endregion
    #region Ship Pack Data
    private static void SaveShipPackData() {
        PlayerPrefs.SetString(shipPackKey, GameResources.Instance.ShipPackData.SaveToJson());
        PlayerPrefs.Save();
    }
    private static void LoadShipPackData() {
        string json = PlayerPrefs.GetString(shipPackKey);
        GameResources.Instance.ShipPackData.LoadFromJson(json);
    }
    #endregion
    #region UseProfile
    private static void SaveUseProfile() {
        PlayerPrefs.SetString(useProfileKey, GameResources.Instance.UserProfile.SaveToJson());
        PlayerPrefs.Save();
    }
    private static void LoadUseProfile() {
        string json = PlayerPrefs.GetString(useProfileKey);
        GameResources.Instance.UserProfile.LoadData(json);
    }
    #endregion

    #region Halloween
    //private static void SaveHalloweenData() {
    //    PlayerPrefs.SetString(halloweenData, GameResources.Instance.Halloween.SaveToJson());
    //    PlayerPrefs.Save();
    //}
    //private static void LoadHalloweenData() {
    //    string json = PlayerPrefs.GetString(halloweenData);
    //    GameResources.Instance.Halloween.LoadFromJson(json);
    //}
    //private static void SaveHalloweenMissionData() {
    //    PlayerPrefs.SetString(halloweenMissionData, GameResources.Instance.HalloweenMission.SaveToJson());
    //    PlayerPrefs.Save();
    //}
    //private static void LoadHalloweenMissionData() {
    //    string json = PlayerPrefs.GetString(halloweenMissionData);
    //    GameResources.Instance.HalloweenMission.LoadFromJson(json);
    //}
    //private static void SaveHalloweenShopData() {
    //    PlayerPrefs.SetString(halloweenShopData, GameResources.Instance.HalloweenShopData.SaveToJson());
    //    PlayerPrefs.Save();
    //}
    //private static void LoadHalloweenShopData() {
    //    string json = PlayerPrefs.GetString(halloweenShopData);
    //    GameResources.Instance.HalloweenShopData.LoadFromJson(json);
    //}
    #endregion

    #region Init Chip, Material Per Hour
    private static void LoadChipMaterialPerHour() {
        GameResources.Instance.RefreshChipMaterialPerHour();
    }
    #endregion
}
