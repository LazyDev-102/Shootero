using System;
using Gemmob;
using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

[Serializable]
public class ShipInfor : IEventParams {
    [SerializeField] private int id;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private string extDescription;
    [SerializeField] private Sprite icon;
    [SerializeField] private Sprite[] icons;
    [SerializeField] private Sprite extIcon;
    [SerializeField] private int canUnlockLevel;
    [SerializeField] private int currentLevel; //
    [SerializeField] private bool unlocked; //
    [SerializeField] private bool comingSoon; //
    [SerializeField] private List<ShipLevelInfor> levels; //
    [SerializeField] private ShipBase shipPrefab;

    [SerializeField] private bool isOpenChecked;
    [SerializeField] private bool isSeeChecked;
    [SerializeField] private List<ShipSpecialInfo> shipSpecial;
    [SerializeField] private List<ShipEvolutionary> shipEvolutionaries;
    [SerializeField] private Color trailColor;
    [HideInInspector]
    private bool shipPackTrial;
#if UNITY_EDITOR
    public string spreadSheetName;
    public string workSheetName;

#endif
    public int ID { get => id; }
    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public string ExtDescription { get => extDescription; set => extDescription = value; }
    public Sprite[] Icons { get => icons; }
    public Sprite ExtIcon { get => extIcon; }
    public int CanUnlockLevel { get => canUnlockLevel; set => canUnlockLevel = value; }
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public bool Unlocked { get => unlocked; set => unlocked = value; }
    public bool ComingSoon { get => comingSoon; }
    public List<ShipLevelInfor> Levels { get => levels; }
    public ShipBase ShipPrefab { get => shipPrefab; }
    public bool IsOpenChecked { get => isOpenChecked; set => isOpenChecked = value; }
    public bool IsSeeChecked { get => isSeeChecked; set => isSeeChecked = value; }
    public bool IsMax => currentLevel == levels.Count - 1;
    public List<ShipSpecialInfo> ShipSpecial { get => shipSpecial; }
    public List<ShipEvolutionary> ShipEvolutionaries { get => shipEvolutionaries; }
    public Color TrailColor { get => trailColor; }

    public bool CanUnlock(int curLevelProgress) {
        if (unlocked)
            return true;
        return CanUnlockLevel <= curLevelProgress;
    }

    public ShipInfor() {
        currentLevel = 0;
        unlocked = true;
    }

    public void SetUnlock() {
        unlocked = true;
    }
    public void SetUnlock(int level) {
        unlocked = true;
        currentLevel = level;
    }
    public void SetShipPackTrial(bool status) {
        shipPackTrial = status;
    }
    public int GetTrialDamage() {
        return shipPackTrial ? (int)levels[79].Attack.Value : (int)levels[0].Attack.Value;
    }
    public int GetTrialHp() {
        return shipPackTrial ? (int)levels[79].HP.Value : (int)levels[0].HP.Value;

    }
    public bool Enhance() {
        if (IsMax)
            return false;
        currentLevel++;
        return true;
    }
    public Sprite GetIcon() {
        if (icons == null || icons.Length == 0)
            return icon;
        return shipPackTrial ? icons[icons.Length - 1] : icons[(currentLevel + 1) / 20];
    }
    public Sprite GetOldIcon() {
        if (icons == null || icons.Length == 0)
            return icon;
        var index = (currentLevel + 1) / 20 - 1;
        if (index < 0)
            index = 0;
        return icons[index];
    }
    public float GetCurrentAttack() {
        return levels[currentLevel].Attack.Value;
    }
    public float GetCurrentHP() {
        return levels[currentLevel].HP.Value;
    }
    public float GetNextAttackInc(int level) {
        if (level >= levels.Count - 1)
            return 0;
        return levels[level + 1].Attack.Value - levels[level].Attack.Value;
    }
    public float GetNextHPInc(int level) {
        if (level >= levels.Count - 1)
            return 0;
        return levels[level + 1].HP.Value - levels[level].HP.Value;
    }
    public float GetPrice(int level) {
        if (level >= levels.Count)
            return levels[levels.Count - 1].Price.Amount;
        return levels[level].Price.Amount;
    }
    public int GetCurrency(int level) {
        if (level >= levels.Count)
            return levels[levels.Count - 1].Price.Id;
        return levels[level].Price.Id;
    }
    public Sprite GetPriceIcon(int level) {
        if (level >= levels.Count)
            return null;
        return levels[level].Price.Icon;
    }
    public Sprite GetUnlockIcon() {
        if (levels == null || levels.Count == 0)
            return null;
        return levels[0].Price.Icon;
    }
    public int GetDamage() {
        var attackBase = levels[currentLevel].Attack.Value;
        return (int)(attackBase + attackBase * PlayerStatManager.Instance.DamagePassive);
    }
    public int GetHP() {
        var hpBase = levels[currentLevel].HP.Value;
        return (int)(hpBase + hpBase * PlayerStatManager.Instance.HpPassive);
    }
    public string GetCurrentSpecialText() {
        for (int i = shipSpecial.Count - 1; i >= 0; i--) {
            if (shipSpecial[i].Level <= currentLevel + 1)
                return shipSpecial[i].GetDescription();
        }
        return "";
    }
    public ShipSpecialInfo GetCSpecial() {
        for (int i = shipSpecial.Count - 1; i >= 0; i--) {
            if (shipSpecial[i].Level <= currentLevel + 1)
                return shipSpecial[i];
        }
        return null;
    }
    public bool CheckEnvoluration() {
        if (shipEvolutionaries == null || shipEvolutionaries.Count == 0)
            return false;
        foreach (var item in shipEvolutionaries) {
            if (item.EvolutionState)
                continue;
            return item.Level == currentLevel + 2;
        }
        return false;
    }
    public ShipEvolutionary GetShipEvolution() {
        if (shipEvolutionaries == null || shipEvolutionaries.Count == 0)
            return null;
        foreach (var item in shipEvolutionaries) {
            if (!item.EvolutionState)
                return item;
        }
        return null;
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.CurrentLv, CurrentLevel);
        if (Unlocked)
            node.Add(JsonKey.UnlockLv, Unlocked);

        if (!IsOpenChecked)
            node.Add(JsonKey.IsOpenChecked, IsOpenChecked);

        if (!IsSeeChecked)
            node.Add(JsonKey.IsSeeChecked, IsSeeChecked);

        return node;
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            currentLevel = 0;
            unlocked = false;
            isOpenChecked = false;
            isSeeChecked = false;
        }
        else {
            currentLevel = json[JsonKey.CurrentLv].AsInt;
            unlocked = json[JsonKey.UnlockLv] != null ? json[JsonKey.UnlockLv].AsBool : false;
            isOpenChecked = json[JsonKey.IsOpenChecked] != null ? json[JsonKey.IsOpenChecked].AsBool : true;
            isSeeChecked = json[JsonKey.IsSeeChecked] != null ? json[JsonKey.IsSeeChecked].AsBool : true;
        }
    }

    [Serializable]
    public class ShipLevelInfor {
        public StatModifier Attack;
        public StatModifier HP;
        public ItemStack Price;
    }
}

[Serializable]
public class ShipSpecialInfo {
    public int Level;
    public string Prefix;
    public string Suffix;
    public string Description;
    public ShipSpecialValue[] SpecialValue;

    [Serializable]
    public class ShipSpecialValue {
        public EventKey.StatEvent statEvent;
        public StatModifier Value;
    }
    public string GetValue() {
        return Suffix.Equals("%") ? $"{SpecialValue[0].Value.Value * 100}" : $"{SpecialValue[0].Value.Value}";
    }
    public string GetDescription() {
        return $"{Prefix}{GetValue()}{Suffix} {Description}";
    }
}
[Serializable]
public class ShipSaveData {
    [SerializeField] private int cl;
    [SerializeField] private bool ul;
    [SerializeField] private bool ioc;
    [SerializeField] private bool isc;

    public int CurrentLevel { get => cl; set => cl = value; }
    public bool Unlocked { get => ul; set => ul = value; }
    public bool IsOpenChecked { get => ioc; set => ioc = value; }
    public bool IsSeeChecked { get => isc; set => isc = value; }
}
[Serializable]
public class ShipEvolutionary {
    public int Level;
    public bool EvolutionState;
}