using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RookieLoginData", menuName = "Resource/Missions/Rookie/RookieLoginData")]
public class RookieLoginData : ScriptableObject, ISaveLoadable {
    #region Variables
    public static readonly int Count = 7;
    [SerializeField] private int dayTrigger;
    [SerializeField] private int checkinDay;
    [SerializeField] private int checkinYear;
    [SerializeField] private int currentDay;
    [SerializeField] private bool isComplete;
    [SerializeField] private List<RookieLoginInfor> rookieLoginInfor;

    public int CheckinDay { get => checkinDay; }
    public int CheckinYear { get => checkinYear; }
    public int CurrentDay { get => currentDay; }
    public bool IsComplete { get => isComplete; }
    public List<RookieLoginInfor> RookieLoginInfor { get => rookieLoginInfor; }

    #endregion

    #region Function Get, Set, Check Data
    public RookieLoginData Initialize() {
        checkinDay = System.DateTime.Now.DayOfYear - 1;
        checkinYear = System.DateTime.Now.Year;
        currentDay = 0;
        dayTrigger = checkinDay;
        isComplete = false;
        return this;
    }
    public bool OnceOfDay() {
        return dayTrigger < System.DateTime.Now.DayOfYear;
    }
    public bool CanUnlock() {
        return GameResources.Instance.TutorialSytemData.FinishTutorialPlayGame;
    }
    public bool CanSpecialTrigger() {
        if (!OnceOfDay())
            return false;
        var result = CanUnlock() && !isComplete;
        if (result)
            dayTrigger = System.DateTime.Now.DayOfYear;
        return result;
    }
    public bool Claimable(int checkinDay, int checkinYear) {
        if (isComplete)
            return false;
        if (this.CheckinYear > checkinYear)
            return false;
        if (this.CheckinYear < checkinYear)
            return true;
        return checkinDay > this.CheckinDay && CurrentDay < Count;

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
        return rookieLoginInfor[index].Rewards;
    }
    public ItemClaim[] GetCurrentReward() {
        if (isComplete)
            return null;
        return rookieLoginInfor[CurrentDay].Rewards;
    }
    private void ClaimReward(int multi) {
        if (isComplete)
            return;
        rookieLoginInfor[CurrentDay].Claim(multi);
    }
    private void ChangeCurrentDay() {
        currentDay++;
        isComplete = CurrentDay >= Count;
    }
    public int GetCurrentDay() {
        return CurrentDay;
    }

    #endregion

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
        isComplete = saveData.IsComplete;
        if (dayTrigger == 0)
            dayTrigger = checkinDay - 1;
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.CheckinDay = CheckinDay;
        saveData.CheckinYear = CheckinYear;
        saveData.CurrentDay = CurrentDay;
        saveData.DayTrigger = dayTrigger;
        saveData.IsComplete = IsComplete;
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
            isComplete = json[JsonKey.IsCompleted].AsBool;
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
        node.Add(JsonKey.IsCompleted, isComplete);
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private int d;
        [SerializeField] private int y;
        [SerializeField] private int cd;
        [SerializeField] private bool isComplete;
        [SerializeField] private int dt;

        public int CheckinDay { get => d; set => d = value; }
        public int CheckinYear { get => y; set => y = value; }
        public int CurrentDay { get => cd; set => cd = value; }
        public bool IsComplete { get => isComplete; set => isComplete = value; }
        public int DayTrigger { get => dt; set => dt = value; }
    }
    #endregion
}

[System.Serializable]
public class RookieLoginInfor {
    [SerializeField] private int day;
    [SerializeField] private string description;
    [SerializeField] private ItemClaim[] rewards;

    public int Day { get => day; }
    public string Description { get => description; }
    public ItemClaim[] Rewards { get => rewards; }

    public bool Claim(int multi) {
        foreach (var item in rewards) {
            if (GearClaimExtentions.IsGear(item.Id))
                GearClaimExtentions.Claim(item.Id, 3);
            else
                item.Claim(multi);
        }
        //foreach (var item in rewards) {
        //    if (item.Item as DroneGearHardData)
        //        GearClaimExtentions.Claim(item.Id, 3);
        //    else
        //        item.Claim(multi);
        //}
        return true;
    }
}