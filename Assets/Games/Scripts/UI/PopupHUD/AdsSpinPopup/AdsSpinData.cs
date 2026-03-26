using Helper;
using SimpleJSON;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AdsSpinData", menuName = "Resource/HardData/Offer/AdsSpinData")]
public class AdsSpinData : ScriptableObject {
    [SerializeField] private AdsSpinInfo[] luckyData;
    [SerializeField] private BonusSpinInfo[] bonusData;
    [SerializeField] private int[] luckyPercent;
    [SerializeField] private int[] bonusPercent;
    [SerializeField] private ItemClaim oneTimeMod;
    [SerializeField] private int maxSpinTimes;
    [SerializeField] private int cSpin;
    [SerializeField] private int checkinDay;
    [SerializeField] private int checkinYear;
    public bool OneTimeModLoadable;
    public int CurrentSpin => cSpin;
    public AdsSpinInfo[] LuckyData { get => luckyData; }
    public BonusSpinInfo[] BonusData { get => bonusData; }
    public ItemClaim OneTimeMod { get => oneTimeMod; }
    public int[] LuckyPercent { get => luckyPercent; }
    public int[] BonusPercent { get => bonusPercent; }

    public bool Spinable() {
        return cSpin > 0;
    }
    public void DeSpin() {
        if (cSpin > 0)
            cSpin--;
    }
    public void Reset() {
        OneTimeModLoadable = true;
        Assign();
    }
    private void Assign() {
        for (int i = 0; i < luckyData.Length; i++) {
            luckyData[i].Assign();
            bonusData[i].Assign();
        }
    }
    private void ResetData(int day, int year) {
        if (year < checkinYear)
            return;
        if (year == checkinYear && day <= checkinDay)
            return;
        cSpin = maxSpinTimes;
        checkinDay = day;
        checkinYear = year;
    }
    #region SaveLoad Data
    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            checkinDay = DateTime.Now.DayOfYear - 1;
            checkinYear = DateTime.Now.Year;
            ResetData(checkinDay + 1, checkinYear);
            return;
        }
        cSpin = saveData.CurrentSpin;
        checkinDay = saveData.CheckinDay;
        checkinYear = saveData.CheckinYear;
        if (checkinDay > 366)// Looxi data cu
            checkinDay = DateTime.Now.DayOfYear - 1;
        ResetData(DateTime.Now.DayOfYear, DateTime.Now.Year);
    }
    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.CurrentSpin = cSpin;
        saveData.CheckinDay = checkinDay;
        saveData.CheckinYear = checkinYear;
        return JsonUtility.ToJson(saveData);
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            checkinDay = DateTime.Now.DayOfYear - 1;
            checkinYear = DateTime.Now.Year;
            ResetData(checkinDay + 1, checkinYear);
        }
        else {
            cSpin = json[JsonKey.CurrentRemain].AsInt;
            checkinDay = json[JsonKey.Day].AsInt;
            checkinYear = json[JsonKey.Year].AsInt;
            ResetData(DateTime.Now.DayOfYear, DateTime.Now.Year);
        }
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.CurrentRemain, cSpin);
        node.Add(JsonKey.Day, checkinDay);
        node.Add(JsonKey.Year, checkinYear);
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private int cs;
        [SerializeField] private int cd;
        [SerializeField] private int cy;

        public int CurrentSpin { get => cs; set => cs = value; }
        public int CheckinDay { get => cd; set => cd = value; }
        public int CheckinYear { get => cy; set => cy = value; }
    }
    #endregion
}