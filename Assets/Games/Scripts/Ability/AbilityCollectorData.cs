using GameSystem.Common.UnityInspector;
using Helper;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SimpleJSON;

[CreateAssetMenu(fileName = "AbilityCollectorData", menuName = "Resource/HardData/Ability/AbilityCollectorData")]
public class AbilityCollectorData : ScriptableObject, ISaveLoadable {
    [SerializeField] private AbilityData[] normalAbilityDatas;
    [SerializeField, ItemField] private int priceId;
    [SerializeField] private int currentPointUpgrade;
    [SerializeField] private bool converted; // Convert to new AbilityData

    private int currentLevel;
    private Queue<AbilityData> upgradedAbilities = new Queue<AbilityData>();
    public int CurrentPointUpgrade { get => currentPointUpgrade; set => currentPointUpgrade = value; }
    public AbilityData[] NormalAbilityDatas { get => normalAbilityDatas; }

    public bool HasPointUpgrade => CurrentPointUpgrade > 0;
    public int PriceAmount => (currentLevel + 1) * 500;
    public int PriceId { get => priceId; }

    public bool CanUpgrade {
        get {
            int priceHave = GameResources.Instance.Inventory.GetItem(priceId).Amount;
            int priceNeed = PriceAmount;
            AbilityData[] canUpgradeAbilities = GetAllAbilityCanUpgrade().ToArray();
            List<AbilityData> upgradedNoMaxAbilities = new List<AbilityData>();
            foreach (var a in upgradedAbilities) {
                if (!a.IsMaxLevel) {
                    upgradedNoMaxAbilities.Add(a);
                }
            }
            return currentPointUpgrade > 0 && priceNeed <= priceHave && ((canUpgradeAbilities.Length > 0) || (upgradedNoMaxAbilities.Count > 0));
        }
    }

    public void Reload() {
        foreach (var item in GetAllAbility()) {
            item.Remove();
            item.CurrentLevel = -1;
        }
    }

    public AbilityData Upgrade() {
        currentPointUpgrade--;
        currentLevel++;
        List<AbilityData> upgradeableAbilities = new List<AbilityData>();

        do {
            upgradeableAbilities = GetAllAbilityCanUpgrade().ToList();
            if (upgradeableAbilities == null || upgradeableAbilities.Count == 0) {
                upgradedAbilities.Clear();
            }
        } while (upgradeableAbilities == null || upgradeableAbilities.Count == 0);

        AbilityData abilityChoose = RandomHelper.RandomInCollection(upgradeableAbilities);
        upgradedAbilities.Enqueue(abilityChoose);
        if (upgradedAbilities.Count > 2) {
            upgradedAbilities.Dequeue();
        }
        abilityChoose.Levelup();
        return abilityChoose;
    }

    public IEnumerable<AbilityData> GetAllAbilityCanUpgrade() {
        foreach (var ability in normalAbilityDatas) {
            if (!ability.IsMaxLevel && !upgradedAbilities.Contains(ability)) {
                yield return ability;
            }
        }
    }

    public IEnumerable<AbilityData> GetAllAbility() {
        foreach (var ability in normalAbilityDatas) {
            yield return ability;
        }
    }

    private void ApplyData() {
        foreach (var a in GetAllAbility()) {
            a.Apply();
        }
    }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            foreach (var a in NormalAbilityDatas) {
                a.LoadFromJson(null);
            }
            currentPointUpgrade = 1;
            currentLevel = 0;
            return;
        }
        converted = saveData.Converted;
        if (converted)
            return;

        int index = 0;
        for (int i = 0; i < saveData.NormalAbilitySaves.Length; ++i) {
            NormalAbilityDatas[i].LoadFromJson(saveData.NormalAbilitySaves[i]);
            index++;
        }
        for (int j = index; j < NormalAbilityDatas.Length; ++j) {
            NormalAbilityDatas[j].LoadFromJson(null);
        }
        currentPointUpgrade = saveData.CurrentPointUpgrade;
        currentLevel = saveData.CurrentLevel;
        if (!converted) {
            converted = true;
            GameResources.Instance.AbilityData.OldVersionRestorePoint(currentLevel);
        }
        //ApplyData();
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.NormalAbilitySaves = new string[NormalAbilityDatas.Length];
        for (int i = 0; i < NormalAbilityDatas.Length; ++i) {
            saveData.NormalAbilitySaves[i] = NormalAbilityDatas[i].SaveToJson();
        }
        saveData.CurrentPointUpgrade = currentPointUpgrade;
        saveData.CurrentLevel = currentLevel;
        saveData.Converted = converted;
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            foreach (var a in NormalAbilityDatas) {
                a.CurrentLevel = -1;
            }
            currentPointUpgrade = 1;
            currentLevel = 0;
        }
        else {
            converted = json.HasKey(JsonKey.Converted) ? json[JsonKey.Converted].AsBool : false;
            if (converted)
                return;
            int index = 0;
            JSONArray normalAbilityNode = json[JsonKey.NormalAbilitySaves].AsArray;
            for (int i = 0; i < normalAbilityNode.Count; i++) {
                NormalAbilityDatas[i].CurrentLevel = normalAbilityNode[i].AsInt;
                index++;
            }
            for (int j = index; j < NormalAbilityDatas.Length; ++j) {
                NormalAbilityDatas[j].CurrentLevel = -1;
            }
            currentPointUpgrade = json[JsonKey.CurrentPointUpgrade].AsInt;
            currentLevel = json[JsonKey.CurrentLv].AsInt;
        }
        if (!converted) {
            converted = true;
            GameResources.Instance.AbilityData.OldVersionRestorePoint(currentLevel);
        }
        //ApplyData();
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();

        JSONNode normalAbilityNode = new JSONArray();
        foreach (var item in NormalAbilityDatas) {
            normalAbilityNode.Add(item.CurrentLevel);
        }
        node.Add(JsonKey.NormalAbilitySaves, normalAbilityNode);

        node.Add(JsonKey.CurrentPointUpgrade, currentPointUpgrade);
        node.Add(JsonKey.CurrentLv, currentLevel);
        node.Add(JsonKey.Converted, converted);
        return node;
    }

    [Serializable]
    public class SaveData {
        [SerializeField] private string[] nas;
        [SerializeField] private string cas;
        [SerializeField] private int cpu;
        [SerializeField] private int cl;
        [SerializeField] private bool converted;

        public string[] NormalAbilitySaves { get => nas; set => nas = value; }
        public string CombineAbilitySave { get => cas; set => cas = value; }
        public int CurrentPointUpgrade { get => cpu; set => cpu = value; }
        public int CurrentLevel { get => cl; set => cl = value; }
        public bool Converted { get => converted; set => converted = value; }
    }
}
