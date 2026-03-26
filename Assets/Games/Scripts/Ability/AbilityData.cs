using Gear_Data;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Resource/HardData/Ability/AbilityData")]
public class AbilityData : ScriptableObject {
    [SerializeField] private int id;
    [SerializeField] private string abilityName;
    [SerializeField] private string abilityDescription;
    [SerializeField] private string prefix;
    [SerializeField] private Sprite icon;
    [SerializeField] private AbilityStatInfo abilityStat;
    [SerializeField] private AbilityRequireInfo[] requires;
    [SerializeField] private bool converted;
    public bool IsUnlocked => CurrentLevel >= 0;

    public int CurrentLevel { get; set; }

    public bool IsMaxLevel => CurrentLevel == abilityStat.Datas.Length - 1;

    public int NumberLevel => abilityStat.Datas.Length;

    public string StatName {
        get {
            if (abilityStat.StatData != null) {
                return abilityStat.StatData.Description;
            }
            return "Fake Null";
        }
    }

    public int Id { get => id; }
    public string AbilityName { get => abilityName; }
    public Sprite Icon { get => icon; }
    public string Description { get => abilityDescription; }
    public string AbilityDescription { get => abilityDescription + prefix; }

    public bool CanUnlock() {
        if (requires == null || requires.Length == 0)
            return true;
        foreach (var item in requires) {
            if (!item.EnoughCondition())
                return false;
        }
        return true;
    }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            CurrentLevel = -1;
            return;
        }
        CurrentLevel = saveData.CurrentLevel;
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.CurrentLevel = CurrentLevel;
        return JsonUtility.ToJson(saveData);
    }

    public void Levelup() {
        Remove();
        CurrentLevel++;
        Apply();
    }

    public void Apply() {
        abilityStat.AddStat(CurrentLevel);
    }

    public void Remove() {
        abilityStat.RemoveStat(CurrentLevel);
    }

    public string GetDescription() {
        return abilityStat.GetDescription(CurrentLevel);
    }

    public float GetValueStat(int levelIndex) {
        if (levelIndex >= 0) {
            return abilityStat.Datas[levelIndex].Value;
        }
        return 0;
    }

    public string GetValueString(int levelIndex) {
        return abilityStat.GetValueString(levelIndex);
    }

    [Serializable]
    public class SaveData {
        [SerializeField] private int cl;

        public int CurrentLevel { get => cl; set => cl = value; }
    }
}
