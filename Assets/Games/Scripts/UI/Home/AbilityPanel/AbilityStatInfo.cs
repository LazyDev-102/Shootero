using UnityEngine;
using System;
using Gear_Data;

[Serializable]
public class AbilityStatInfo {
    [SerializeField] private StatHardData statData;
    [SerializeField] private StatModifier[] datas;
    [SerializeField] private float defaulValue;

    public StatHardData StatData { get => statData; }
    public StatModifier[] Datas { get => datas; }

    public void AddStat(int levelIndex) {
        if (levelIndex < 0)
            return;
        if (statData != null) {
            statData.AddStat(datas[levelIndex]);
        }
    }

    public void RemoveStat(int levelIndex) {
        if (levelIndex < 0)
            return;
        if (statData != null) {
            statData.RemoveStat(datas[levelIndex]);
        }
    }

    public string GetDescription(int levelIndex) {
        if (levelIndex < 0) {
            return string.Empty;
        }
        if (statData != null) {
            return statData.GetDescription(datas[levelIndex].Value);
        }
        return "Null";
    }

    public string GetValueString(int levelIndex) {
        if (levelIndex < 0) {
            return statData.GetValueString(defaulValue);
        }
        if (statData != null) {
            return statData.GetValueString(datas[levelIndex].Value);
        }
        return "Null";
    }
}