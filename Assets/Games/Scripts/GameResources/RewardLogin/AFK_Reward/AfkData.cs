using UnityEngine;
using System;
using Helper;
using SimpleJSON;

[CreateAssetMenu(fileName = "AfkData", menuName = "Resource/HardData/Afk/AfkData")]
public class AfkData : ScriptableObject {
    [SerializeField] private string nameEvent;
    [SerializeField] private double timeFinishAFK;
    [SerializeField] private int totalTime;
    [SerializeField] private ItemClaim[] rewards;
    [SerializeField] private bool isComplete;
    [SerializeField] private int checkinDayTrigger;
    [SerializeField] private int checkinYearTrigger;

    private double cTime { get => DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds; }
    public string NameEvent { get => nameEvent; }
    public double TimeFinishAFK { get => timeFinishAFK; }
    public int TotalTime { get => totalTime; }
    public ItemClaim[] Rewards { get => rewards; }
    public bool IsComplete { get => isComplete; }
    public bool Maxable { get => GetTimeUse() >= totalTime; }
    public bool OnceOfDay(int day, int year) {
        bool result = true;
        if (year < checkinYearTrigger)
            result = false;
        if (year == checkinYearTrigger && day <= checkinDayTrigger)
            result = false;
        if (result) {
            checkinDayTrigger = day;
            checkinYearTrigger = year;
        }
        return result;
    }
    public bool CanSpecialTrigger() {
        return OnceOfDay(DateTime.Now.DayOfYear, DateTime.Now.Year) || GetTimeUse() >= totalTime;
    }
    public void Initialize(double cTime) {
        timeFinishAFK = cTime + totalTime;
        isComplete = false;
        checkinDayTrigger = DateTime.Now.DayOfYear - 1;
        checkinYearTrigger = DateTime.Now.Year;
        RefreshReward();
    }
    public void ResetData(double cTime) {
        Initialize(cTime);

    }

    public void RefreshReward() {
        GameResources.Instance.RefreshChipMaterialPerHour();
        if (rewards == null || rewards.Length == 0)
            return;
        var timeUse = GetTimeUse();
        foreach (var item in rewards) {
            if (item.Id == ConstantItemID.ChipId) {
                int value = (GameResources.Instance.ChipPerSecond * timeUse).ConvertToInt();
                if (value < 1)
                    value = 1;
                item.Amount = value;
                continue;
            }
            item.Amount = (GameResources.Instance.MaterialPerSecond * timeUse).ConvertToInt();
        }
    }
    public double GetTimeUse() {
        var result = totalTime - (timeFinishAFK - cTime);
        if (result > totalTime)
            result = totalTime;
        if (result < 0)
            result = 0;
        return result;
    }
    public void Claim(float multi, bool max) {
        //if (Maxable) {
        if (max) {
            Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnClaimMaxAfk);
            GameResources.Instance.DailyMission.AddPointProgress(MissionType.ClaimAFKReward, 1);
        }
        foreach (var item in rewards) {
            item.Claim(multi);
        }
    }
    #region Save Load Data
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            Initialize(cTime);
        }
        else {
            timeFinishAFK = json[JsonKey.TimeFinish].AsDouble;
            isComplete = json[JsonKey.IsCompleted].AsBool;
            checkinDayTrigger = json[JsonKey.Day].AsInt;
            checkinYearTrigger = json[JsonKey.Year].AsInt;
        }
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.TimeFinish, timeFinishAFK);
        node.Add(JsonKey.IsCompleted, IsComplete);
        node.Add(JsonKey.Day, checkinDayTrigger);
        node.Add(JsonKey.Year, checkinYearTrigger);
        return node;
    }
    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            Initialize(cTime);
            return;
        }
        timeFinishAFK = saveData.TimeFinishAFK;
        isComplete = saveData.IsComplete;
        checkinDayTrigger = saveData.CheckinDayTrigger;
        checkinYearTrigger = saveData.CheckinYearTrigger;
    }
    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.TimeFinishAFK = timeFinishAFK;
        saveData.IsComplete = isComplete;
        saveData.CheckinDayTrigger = checkinDayTrigger;
        saveData.CheckinYearTrigger = checkinYearTrigger;
        return JsonUtility.ToJson(saveData);
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private double d;
        [SerializeField] private bool f;
        [SerializeField] private int cd;
        [SerializeField] private int cy;

        public double TimeFinishAFK { get => d; set => d = value; }
        public bool IsComplete { get => f; set => f = value; }
        public int CheckinDayTrigger { get => cd; set => cd = value; }
        public int CheckinYearTrigger { get => cy; set => cy = value; }
    }
    #endregion
}
