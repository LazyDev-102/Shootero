

using SimpleJSON;

public partial class JsonKey {
    public const string InventoryKey = "ivt";
    public const string AbilityCollectorKey = "ac";
    public const string RankInfinityDataKey = "ri";
    public const string ShipDataKey = "sd";
    public const string EnergyDataKey = "ed";
    public const string GearInventoryKey = "git";
    public const string ShopDataKey = "shop";
    public const string ConquerorData = "cd";
    public const string LevelProgression = "lp";
    public const string DailyLogin = "dl";
    public const string RookieLogin = "rl";
    public const string TutorialData = "td";
    public const string DailyPacksData = "dpd";
    public const string DailyMissionKey = "dmk";
    public const string ChallengeKey = "clk";
    public const string AfkKey = "afk";
    public const string AdsSpin = "ask";
    public const string RateUsKey = "ruk";
    public const string BattlePassKey = "bpk";
    public const string UseProfileKey = "upk";
    public const string IapPackKey = "ipk";
    public const string MaterialModeKey = "mmk";
    public const string GearModeKey = "gmk";
    public const string BossModeKey = "bossk";
    public const string ShipPackKey = "shippk";
    public const string SkillsSystemData = "skills";
    public const string AbilityData = "newabi";
    public const string HalloweenData = "hwd";
    public const string HalloweenMissionData = "hwm";
    public const string HalloweenShopData = "hws";
    public const string XmasData = "xmd";
    public const string XmasMissionData = "xmm";
    public const string XmasShopData = "xms";

    public const string UserID = "uid";
    public const string Status = "status";
    public const string Message = "message";
    public const string Data = "data";
    public const string FirstTime = "ft";
    public const string FirstLose = "fl";
    public const string CurrentZone = "cz";
    public const string UnlockZone = "uz";
    public const string Zones = "zs";
    public const string HighestWave = "hw";
    public const string FirstUnlock = "fu";
    public const string CurrentLv = "cl";
    public const string UnlockLv = "ul";
    public const string OwnedExp = "oe";
    public const string ItemSlot = "is";
    public const string ItemId = "i";
    public const string Amount = "a";
    public const string IsNew = "n";
    public const string WeaponrySlotLevel = "wsl";
    public const string ShieldSlotLevel = "ssl";
    public const string CoreSlotLevel = "csl";
    public const string EngineSlotLevel = "esl";
    public const string DroneLSlotLevel = "dll";
    public const string DroneRSlotLevel = "drl";
    public const string Level = "lv";
    public const string Rank = "r";
    public const string IsNewCheck = "inc";
    public const string IsEquiped = "ie";
    public const string GearType = "gt";
    public const string SecondStatIds = "ss";
    public const string NormalAbilitySaves = "nas";
    public const string CombineAbilitySave = "cas";
    public const string CurrentPointUpgrade = "cpu";
    public const string RankPoint = "rp";
    public const string Point = "p";
    public const string HighScore = "hs";
    public const string YourName = "yn";
    public const string CurrentShipId = "csi";
    public const string IsOpenChecked = "ioc";
    public const string IsSeeChecked = "isc";
    public const string ShipInfo = "si";
    public const string GemBuy = "gb";
    public const string AdsBuy = "ab";
    public const string IsEnergyRegen = "ier";
    public const string OldTimeQuit = "otq";
    public const string StartCountAt = "sca";
    public const string CurrentRemain = "cr";
    public const string NormalChest = "nc";
    public const string EliteChest = "ec";
    public const string DailyFree = "df";
    public const string ChipPack = "cp";
    public const string RerollPack = "rerollp";
    public const string IsBoughtGems = "ibg";
    public const string Day = "d";
    public const string Year = "y";
    public const string CurrentDay = "cday";
    public const string Packs = "ps";
    public const string TimeNextFree = "tnf";
    public const string Watched = "iw";
    public const string TimeStart = "ts";
    public const string TimeFinish = "tf";
    public const string IsCompleted = "ic";
    public const string Completed = "cpl";
    public const string Progress = "pgr";
    public const string ProgressS = "pgs";
    public const string CurrentIndex = "ci";
    public const string FinishTutorialInGame = "fti";
    public const string FinishTutorialOpenChest = "ftoc";
    public const string FinishTutorialEquipment = "fte";
    public const string FinishTutorialOpenSkill = "ftos";
    public const string FinishTutorialEquipSkills = "ftes";
    public const string FinishTutorialPlayGame = "ftpg";
    public const string FinishTutorialPlayInfinity = "ftpi";
    public const string GaveKey = "tgv";
    public const string GaveEnergy = "tge";
    public const string GaveSkill = "tgs";
    public const string IsFail = "if";
    public const string IsEpicOneShot = "ieos";
    public const string ResourcePack = "rsp";
    public const string SmartOffer = "smo";
    public const string Active = "at";
    public const string Type = "t";
    public const string Reward = "rw";
    public const string UserProfileInfo = "upi";
    public const string IdTest = "idtest";
    public const string IsPurchase = "ipc";
    public const string Items = "its";
    public const string FreeClaimd = "f";
    public const string PurchaseClaimed = "p";
    public const string Converted = "cvt";
    public const string Fixbug = "fbug";
}

public static class JK {
    public static string Get(this JSONNode json, string key, string defaultValue) {
        if (json.HasKey(key)) {
            return json[key];
        }
        else
            return defaultValue;
    }
    public static int Get(this JSONNode json, string key, int defaultValue) {
        if (json.HasKey(key)) {
            return json[key].AsInt;
        }
        else
            return defaultValue;
    }
    public static bool Get(this JSONNode json, string key, bool defaultValue) {
        if (json.HasKey(key)) {
            return json[key].AsBool;
        }
        else
            return defaultValue;
    }
}