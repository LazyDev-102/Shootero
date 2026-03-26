using Gemmob;
using Gemmob.Api.Analytics;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelProgressData", menuName = "Resource/HardData/User/LevelProgressData", order = 1)]
public class LevelProgressData : ScriptableObject {
    [SerializeField] private LevelProgressPref datas;

    public LevelProgressPref Datas { get => datas; private set { datas = value; } }

    public int GetCurrentLevel() {
        if (datas == null)
            return -1;
        return datas.CurrentLv;
    }
    public float GetRatio() {
        if (datas == null)
            return 0;
        return datas.GetRatio();
    }

    public bool CanUnlockMod(ModData mod) {
        if (datas == null)
            return false;
        return datas.UnlockFeatures.CanUnlockMod(mod);
    }
    public void AddExp(float rate) {
        if (datas == null)
            return;
        datas.AddExp(rate);
    }
    public void AddExp(int amount) {
        if (datas == null)
            return;
        datas.AddExp(amount);
    }

    #region Save Load LevelProgressData

    public void LoadFromJson(string json) {
        datas.LoadFromJson(json);

    }
    public void LoadFJson(JSONNode json) {
        datas.LoadFJson(json);

    }
    public string SaveToJson() {
        if (datas == null)
            datas = new LevelProgressPref();
        return datas.SaveToJson();
    }
    public JSONNode Save2Json() {
        return datas.Save2Json();
    }
    #endregion

#if UNITY_EDITOR
    [ContextMenu("Load XP Need")]
    private void LoadExpNeed() {
        if (datas == null)
            return;
        datas.LoadExpNeed();
    }

    [ContextMenu("Load Reward Data")]
    private void LoadRewardData() {
        if (datas == null)
            return;
        datas.LoadRewardData();
    }
#endif
}

[Serializable]
public class LevelProgressPref {
    [SerializeField] private string pID;
    [SerializeField] private string pName; //
    [SerializeField] private int currentLv; //
    [SerializeField] private int ownedExp; //
    [SerializeField] private List<ExpProgressInfor> expProgress;
    [SerializeField] private List<LevelProcessReward> rewards;
    [SerializeField] private UnlockFeature unlockFeature;
    [SerializeField] private bool newLevelUnlock;
    [SerializeField] private int pointLevelup;
    public string PlayerID { get => pID; private set { pID = value; } }
    public string PlayerName { get => pName; private set { pName = value; } }
    public int CurrentLv { get => currentLv; private set { currentLv = value; } }
    public int OwnedExp { get => ownedExp; private set { ownedExp = value; } }
    public List<LevelProcessReward> Rewards { get => rewards; private set { rewards = value; } }
    public List<ExpProgressInfor> ExpProgress { get => expProgress; }
    public UnlockFeature UnlockFeatures { get => unlockFeature; }
    public bool NewLevelUnlock { get => newLevelUnlock; }

    public bool MaxLevel { get => currentLv >= expProgress.Count - 1; }
    public int PointLevelup { get => pointLevelup; set => pointLevelup = value; }

    #region GetData
#if DEBUG_ENABLE
     public void SetCurrentLevel(int level) {
        currentLv = level;
        pointLevelup = level - 1;
    }       
#endif
    public float GetRatio() {
        return Convert.ToSingle(OwnedExp) / Convert.ToSingle(GetMaxExpInLevel());
    }
    public void SetNewLevelUnlock(bool status) {
        newLevelUnlock = status;
    }
    public bool GetLevelReward(int level) {
        if (level < 0 || level >= expProgress.Count)
            return false;
        var result = expProgress.FirstOrDefault(x => x.Level == level.ToString());
        if (result != null) {
            result.ClaimReward();
        }
        return result != null;
    }

    public ItemStack[] LevelReward(int level) {
        if (level < 0 || level >= expProgress.Count)
            return null;
        var result = expProgress.FirstOrDefault(x => x.Level == level.ToString());
        if (result == null)
            return null;
        return result.Rewards;
    }

    public int GetMaxExpInLevel(int level) {
        if (level < 0 || level >= expProgress.Count)
            return -1;
        return expProgress[level].Exp;
    }
    public int GetMaxExpInLevel() {
        if (currentLv >= expProgress.Count - 1)
            return expProgress[expProgress.Count - 1].Exp;
        else
            return expProgress[currentLv + 1].Exp;
    }

    public int GetMinLevelClaimable() {
        for (int i = 0; i < rewards.Count; i++) {
            if (!rewards[i].Claimed)
                return i;
        }
        return -1;
    }
    public (int, int) GetPreLevelClaimable() {
        for (int i = 0; i < rewards.Count; i++) {
            if (!rewards[i].Claimed)
                return i == 0 ? (-1, rewards[i].Zone) : (rewards[i - 1].Wave, rewards[i - 1].Zone);
        }
        return (rewards[rewards.Count - 2].Wave, rewards[rewards.Count - 2].Zone);
    }
    public (int, int) GetNextLevelClaimable() {
        for (int i = 0; i < rewards.Count; i++) {
            if (!rewards[i].Claimed)
                return i == rewards.Count - 1 ? (-1, rewards[i].Zone) : (rewards[i + 1].Wave, rewards[i + 1].Zone);
        }
        return (-1, rewards[rewards.Count - 1].Zone);
    }
    public (int, int) GetCurrentLevelClaimable() {
        for (int i = 0; i < rewards.Count; i++) {
            if (!rewards[i].Claimed)
                return (rewards[i].Wave, rewards[i].Zone);
        }
        return (rewards[rewards.Count - 1].Wave, rewards[rewards.Count - 1].Zone);
    }


    public LevelProgressPref SetName(string name) {
        pName = name;
        return this;
    }
    public LevelProgressPref AddExp(float rate) {
        ownedExp += (int)(1500 * rate);
        SetNewLevelUnlock(false);
        UpgradeLevel();
        EventDispatcher.Instance.Dispatch(new EventKey.OnExpChange());
        return this;
    }
    public LevelProgressPref AddExp(int amount) {
        ownedExp += amount;
        SetNewLevelUnlock(false);
        UpgradeLevel();
        EventDispatcher.Instance.Dispatch(new EventKey.OnExpChange());
        return this;
    }

    public bool UpgradeLevel() {
        if (!Upgradeable())
            return false;
        do {
            ownedExp -= GetExpNextLevel();
            currentLv++;
            GetLevelReward(currentLv);
            SetNewLevelUnlock(true);
            if (UnlockFeatures.CanUnlockAbility(currentLv + 1)) {
                GameResources.Instance.AbilityCollectorData.CurrentPointUpgrade++;
            }
        } while (Upgradeable());
        PointLevelup++;
        EventDispatcher.Instance.Dispatch(EventKey.OnLevelSystemUp);
        return true;
    }
    public bool Upgradeable() {
        if (currentLv >= expProgress.Count - 1)
            return false;
        return ownedExp >= GetExpNextLevel();
    }

    public int GetExpNextLevel() {
        if (currentLv >= expProgress.Count - 1)
            return 99999999;
        return expProgress[currentLv + 1].Exp;
    }
    #endregion

    #region Save Load LevelProgressPref


    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            currentLv = 0;
            ownedExp = 0;
            pointLevelup = 1;
            for (int i = 0; i < rewards.Count; i++) {
                rewards[i].SetClaim(false);
            }
            return;
        }
        currentLv = saveData.CurrentLv;
        ownedExp = saveData.OwnedExp;
        pointLevelup = currentLv + 1;
        try {
            for (int i = 0; i < saveData.Claimed.Length; i++) {
                if (i >= rewards.Count) {
                    break;
                }
                rewards[i].SetClaim(saveData.Claimed[i]);
            }
            for (int i = saveData.Claimed.Length; i < rewards.Count; ++i) {
                rewards[i].SetClaim(false);
            }
        }
        catch {
            for (int i = 0; i < rewards.Count; i++) {
                rewards[i].SetClaim(false);
            }
        }
    }


    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.CurrentLv = currentLv;
        saveData.OwnedExp = ownedExp;
        saveData.Claimed = new bool[rewards.Count];
        for (int i = 0; i < saveData.Claimed.Length; i++) {
            saveData.Claimed[i] = rewards[i].Claimed;
        }
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            currentLv = 0;
            ownedExp = 0;
            pointLevelup = 1;
            for (int i = 0; i < rewards.Count; i++) {
                rewards[i].SetClaim(false);
            }
        }
        else {
            currentLv = json[JsonKey.CurrentLv].AsInt;
            ownedExp = json[JsonKey.OwnedExp].AsInt;
            pointLevelup = currentLv + 1;

            int maxIndexClaimd = json[JsonKey.FreeClaimd].AsInt;
            for (int i = 0; i < rewards.Count; i++) {
                rewards[i].SetClaim(i <= maxIndexClaimd);
            }
        }
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.CurrentLv, currentLv);
        node.Add(JsonKey.OwnedExp, ownedExp);

        int maxIndexClaimed = 0;
        for (int i = 0; i < rewards.Count; i++) {
            if (rewards[i].Claimed)
                maxIndexClaimed = i;
        }
        node.Add(JsonKey.FreeClaimd, maxIndexClaimed);
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private int cl;
        [SerializeField] private int oe;
        [SerializeField] private bool[] cls;

        public int CurrentLv { get => cl; set => cl = value; }
        public int OwnedExp { get => oe; set => oe = value; }
        public bool[] Claimed { get => cls; set => cls = value; }

    }
    #endregion

    #region Class References
    [Serializable]
    public class ExpProgressInfor {
        [SerializeField] private string level;
        [SerializeField] private int exp;
        [SerializeField] private ItemStack[] rewards;
        [SerializeField] private int chipAfkPoint = 4;

        public string Level { get => level; }
        public int Exp { get => exp; set => exp = value; }
        public ItemStack[] Rewards { get => rewards; }

        public ItemStack[] GetRewards() {
            return rewards;
        }

        public void ClaimReward() {
            GameResources.Instance.RefreshChipMaterialPerHour();
            RefreshReward();
            GameResources.Instance.Inventory.Add(rewards);
        }
        private void RefreshReward() {
            for (int i = 0; i < rewards.Length; i++) {
                if (rewards[i].Id == ConstantItemID.ChipId)
                    rewards[i].Amount = (int)(chipAfkPoint * GameResources.Instance.ChipPerSecond * Constant.HourToSecond);
            }
        }
    }

    [Serializable]
    public class LevelProcessReward {
        [SerializeField] private ItemStack[] itemRewards;
        [SerializeField] private bool claimed; //
        [SerializeField] private int wave;
        [SerializeField] private int zone;
        [SerializeField] private bool comingSoon;

        public ItemStack[] ItemRewards { get => itemRewards; }
        public bool Claimed { get => claimed; }
        public int Wave { get => wave; }
        public int Zone { get => zone; }
        public bool ComingSoon { get => comingSoon; }

        public void SetClaim(bool value) {
            claimed = value;
        }
    }
    [Serializable]
    public class UnlockFeature {
        [SerializeField] private int levelUnlockAbility;
        [SerializeField] private int levelUnlockShop;
        [SerializeField] private int levelUnlockDalyLogin;
        [SerializeField] private int levelUnlockEnhanceShip;
        [SerializeField] private int levelUnlockEnhanceGear;
        [SerializeField] private int levelUnlockInfinityMode;
        [SerializeField] private int levelUnlockChest;
        [SerializeField] private int levelUnlockGearSlotItem;
        [SerializeField] private int levelUnlockGearSlotDrone1;
        [SerializeField] private int levelUnlockGearSlotDrone2;
        [SerializeField] private int levelUnlockShip2;
        [SerializeField] private int levelUnlockShip3;
        [SerializeField] private int levelUnlockShip4;
        [SerializeField] private int levelUnlockShip5;
        [SerializeField] private int levelUnlockShip6;
        [SerializeField] private int levelUnlockDrone1;
        [SerializeField] private int levelUnlockDrone2;
        [SerializeField] private List<UnlockMods> unlockMode;

        public bool CanUnlockAbility(int level) {
            return level >= levelUnlockAbility;
        }
        public int GetlevelUnlockAbility() {
            return levelUnlockAbility;
        }
        public bool CanUnlockShop(int level) {
            return level >= levelUnlockShop;
        }
        public int GetlevelUnlockShop() {
            return levelUnlockShop;
        }
        public bool CanUnlockDalyLogin(int level) {
            return level >= levelUnlockDalyLogin;
        }
        public int GetlevelUnlockDalyLogin() {
            return levelUnlockDalyLogin;
        }
        public bool CanUnlockEnhanceShip(int level) {
            return level >= levelUnlockEnhanceShip;
        }
        public bool CanUnlockEnhanceGear(int level) {
            return level >= levelUnlockEnhanceGear;
        }
        public int GetLevelUnlockEnhanceGear() {
            return levelUnlockEnhanceGear;
        }
        public bool CanUnlockInfinityMode(int level) {
            return level >= levelUnlockInfinityMode;
        }
        public int GetlevelUnlockInfinityMode() {
            return levelUnlockInfinityMode;
        }
        public bool CanUnlockChest(int level) {
            return level >= levelUnlockChest;
        }
        public bool CanUnlockGearSlotItem(int level) {
            return level >= levelUnlockGearSlotItem;
        }
        public bool CanUnlockGearSlotDrone1(int level) {
            return level >= levelUnlockGearSlotDrone1;
        }
        public bool CanUnlockGearSlotDrone2(int level) {
            return level >= levelUnlockGearSlotDrone2;
        }
        public bool CanUnlockUnlockShip2(int level) {
            return level >= levelUnlockShip2;
        }
        public int GetLevelUnlockShip2() {
            return levelUnlockShip2;
        }
        public bool CanUnlockUnlockShip3(int level) {
            return level >= levelUnlockShip3;
        }
        public int GetLevelUnlockShip3() {
            return levelUnlockShip3;
        }
        public bool CanUnlockUnlockShip4(int level) {
            return level >= levelUnlockShip4;
        }
        public int GetLevelUnlockShip4() {
            return levelUnlockShip4;
        }
        public bool CanUnlockUnlockShip5(int level) {
            return level >= levelUnlockShip5;
        }
        public int GetLevelUnlockShip5() {
            return levelUnlockShip5;
        }
        public bool CanUnlockUnlockShip6(int level) {
            return level >= levelUnlockShip6;
        }
        public int GetLevelUnlockShip6() {
            return levelUnlockShip6;
        }
        public bool CanUnlockDrone1(int level) {
            return level >= levelUnlockDrone1;
        }
        public bool CanUnlockDrone2(int level) {
            return level >= levelUnlockDrone2;
        }

        public ModData[] GetUnlockMods(int level) {
            var item = unlockMode.Find(x => x.levelUnlock == level);
            if (item != null)
                return item.modIDs;
            return null;
        }
        public bool CanUnlockMod(ModData mod) {
            var item = unlockMode.Find(x => x.modIDs.Contains(mod));
            return item != null && item.levelUnlock <= GameResources.Instance.LevelProgress.GetCurrentLevel() ? true : false;
        }
        [Serializable]
        public class UnlockMods {
            public int levelUnlock;
            public ModData[] modIDs;
        }
    }
    #endregion

#if UNITY_EDITOR
    public void LoadExpNeed() {
        int value = 0;
        for (int i = 0; i < expProgress.Count; i++) {
            value = (2 * i - 1) * 80;
            if (value < 0)
                value = 100;
            expProgress[i].Exp = value;
        }
    }
    public void LoadRewardData() {
        for (int i = 0; i < expProgress.Count; i++) {
            expProgress[i].Rewards[1].Amount = 20;
        }
    }
#endif
}
