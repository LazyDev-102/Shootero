using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DailyLoginData", menuName = "Resource/Missions/Daily/DailyLoginData")]
public class DailyLoginData : ScriptableObject, ISaveLoadable {
    public static readonly int Count = 28;
    private bool triggered;
    [SerializeField] private int dayTrigger;
    [SerializeField] private int checkinDay;
    [SerializeField] private int checkinYear;
    [SerializeField] private int currentDay;
    [SerializeField] private List<DailyLoginInfor> dailyLoginInfor;


    public int CheckinDay { get => checkinDay; }
    public int CheckinYear { get => checkinYear; }
    public int CurrentDay { get => currentDay; }
    public List<DailyLoginInfor> DailyLoginInfor { get => dailyLoginInfor; }
    public bool IsCompleted { get => currentDay >= Count; }

    public DailyLoginData Initialize() {
        checkinDay = System.DateTime.Now.DayOfYear - 1;
        checkinYear = System.DateTime.Now.Year;
        currentDay = 0;
        dayTrigger = checkinDay;
        return this;
    }
    public bool CanUnlock() {
        return GameResources.Instance.ConquerorData.UnlockZone > 0;
    }
    public bool IsRookieComplete() {
        return GameResources.Instance.RookieLoginData.IsComplete;
    }
    public bool OnceOfDay() {
        return dayTrigger < System.DateTime.Now.DayOfYear;
    }
    public bool CanSpecialTrigger() {
        if (!OnceOfDay())
            return false;
        var result = CanUnlock() && IsRookieComplete();
        if (result)
            dayTrigger = System.DateTime.Now.DayOfYear;
        return result;
    }
    public bool Claimable(int checkinDay, int checkinYear) {
        if (IsCompleted)
            return false;
        if (this.CheckinYear > checkinYear)
            return false;
        if (this.CheckinYear < checkinYear)
            return true;
        return checkinDay > this.CheckinDay;

    }
    public bool Claim(int checkinDay, int checkinYear, int multi = 1) {
        if (!Claimable(checkinDay, checkinYear))
            return false;
        this.checkinDay = checkinDay;
        this.checkinYear = checkinYear;
        ClaimReward(multi);
        ChangeCurrentDay();
        return true;
    }

    public ItemClaim[] GetReward(int index) {
        if (index >= Count)
            return null;
        return dailyLoginInfor[index].Rewards;
    }
    public ItemClaim[] GetCurrentReward() {
        return dailyLoginInfor[currentDay].Rewards;
    }
    private void ClaimReward(int multi) {
        dailyLoginInfor[currentDay].Claim(multi);
    }
    public int GetCurrentDay() {
        return currentDay;
    }
    private void ChangeCurrentDay() {
        //currentDay = currentDay >= Count ? 0 : currentDay + 1;
        if (IsCompleted)
            return;
        currentDay = currentDay + 1;
    }


    #region Save Load Data

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            Initialize();
            return;
        }
        checkinDay = saveData.CheckinDay;
        checkinYear = saveData.CheckinYear;
        currentDay = saveData.CurrentDay;
        dayTrigger = saveData.DayTrigger;
        if (dayTrigger == 0)
            dayTrigger = checkinDay - 1;
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.CheckinDay = CheckinDay;
        saveData.CheckinYear = CheckinYear;
        saveData.CurrentDay = currentDay;
        saveData.DayTrigger = dayTrigger;
        saveData.IsCompleted = IsCompleted;
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            Initialize();
        }
        else {
            checkinDay = json[JsonKey.Day].AsInt;
            checkinYear = json[JsonKey.Year].AsInt;
            currentDay = json[JsonKey.CurrentDay].AsInt;
            dayTrigger = json[JsonKey.Progress].AsInt;
            if (dayTrigger == 0)
                dayTrigger = checkinDay - 1;
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.Day, CheckinDay);
        node.Add(JsonKey.Year, CheckinYear);
        node.Add(JsonKey.CurrentDay, currentDay);
        node.Add(JsonKey.Progress, dayTrigger);
        node.Add(JsonKey.IsCompleted, IsCompleted);
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private int d;
        [SerializeField] private int y;
        [SerializeField] private int cd;
        [SerializeField] private int dt;
        [SerializeField] private bool f;

        public int CheckinDay { get => d; set => d = value; }
        public int CheckinYear { get => y; set => y = value; }
        public int CurrentDay { get => cd; set => cd = value; }
        public int DayTrigger { get => dt; set => dt = value; }
        public bool IsCompleted { get => f; set => f = value; }
    }
    #endregion
}


[System.Serializable]
public class DailyLoginInfor {
    [SerializeField] private int day;
    [SerializeField] private string description;
    [SerializeField] private ItemClaim[] rewards;

    public int Day { get => day; }
    public string Description { get => description; }
    public ItemClaim[] Rewards { get => rewards; }

    public bool Claim(int multi) {
        foreach (var item in rewards) {
            item.Claim(multi);
        }
        return true;
    }
}