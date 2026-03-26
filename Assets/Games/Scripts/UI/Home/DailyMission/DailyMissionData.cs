using SimpleJSON;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyMissionData", menuName = "Resource/HardData/DailyMission/DailyMissionData")]
public class DailyMissionData : ScriptableObject {
    [SerializeField] private string nameEvent;
    [SerializeField] private int checkinDay;
    [SerializeField] private int checkinYear;
    [SerializeField] private int pointTarget;
    [SerializeField] private int pointProgress;
    [SerializeField] private DailyMissionItemData[] datas;
    [SerializeField] private DailyMissionProgressItemData[] progressDatas;
    [SerializeField] private bool isComplete;
    [SerializeField] private bool[] missionItemComplete;
    [SerializeField] private int[] missionItemProgress;
    public string NameEvent { get => nameEvent; }
    public int CheckinDay { get => checkinDay; }
    public int CheckinYear { get => checkinYear; }
    public int PointTarget { get => pointTarget; }
    public int PointProgress { get => pointProgress; }
    public DailyMissionItemData[] Datas { get => datas; }
    public DailyMissionProgressItemData[] ProgressData { get => progressDatas; }
    public bool IsComplete { get => isComplete; }
    public float Progress { get => pointProgress / pointTarget; }
    public bool[] MissionItemComplete { get => missionItemComplete; }

    public void Initialize(int day, int year, int progressInit) {
        checkinDay = day;
        checkinYear = year;
        pointProgress = progressInit;
        isComplete = false;
        missionItemComplete = new bool[datas.Length];
        missionItemProgress = new int[datas.Length];
        ResetData();
        AssignProgressRewards();
    }
    private void AssignProgressRewards() {
        foreach (var item in progressDatas) {
            item.Assign();
        }
    }
    public void ResetData() {
        foreach (var item in datas) {
            if (item != null)
                item.ResetData();
        }
        foreach (var item in progressDatas) {
            if (item != null)
                item.ResetData();
        }
    }
    public bool Claimable() {
        for (int i = 0; i < datas.Length; i++) {
            if (datas[i].CanApply())
                return true;
        }
        return false;
    }
    public bool IsReset(int day, int year) {
        if (year < checkinYear)
            return false;
        if (year == checkinYear && day <= checkinDay)
            return false;
        Initialize(day, year, 0);
        return true;
    }
    public void AddPointProgress(int value) {
        if (isComplete)
            return;
        pointProgress += value;
        if (pointProgress >= pointTarget) {
            pointProgress = pointTarget;
            isComplete = true;
        }
        CheckClaimProgressReward();
    }
    public void AddPointProgress(MissionType type, int value) {
        var item = datas.FirstOrDefault(x => x.Type == type);
        if (item != null) {
            item.Upgrade(value);
        }
    }
    private void CheckClaimProgressReward() {
        List<ItemClaim> itemClaim = new List<ItemClaim>();
        foreach (var item in progressDatas) {
            if (item != null && item.Claimable(pointProgress)) {
                item.Claim();
                foreach (var i in item.Rewards) {
                    itemClaim.Add(i);
                }
            }
        }
        if (itemClaim.Count != 0)
            PopupHUD.Instance.Show<RewardPopup>(hideCurrent: false).UpdateClaimUI(itemClaim);
    }
    public void SetMissionItemComplete(int index, bool status) {
        if (index >= datas.Length)
            return;
        missionItemComplete[index] = status;
    }
    public void SetMissionItemProgress(int index, int value) {
        if (index >= datas.Length)
            return;
        missionItemProgress[index] = value;
    }
    #region Save Load Data

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            Initialize(System.DateTime.Now.DayOfYear - 1, System.DateTime.Now.Year, 0);
            return;
        }
        checkinDay = saveData.CheckinDay;
        checkinYear = saveData.CheckinYear;
        pointProgress = saveData.PointProgress;
        isComplete = saveData.IsComplete;
        missionItemComplete = saveData.MissionItemComplete;
        missionItemProgress = saveData.MissionItemProgress;
        if (missionItemComplete == null || missionItemComplete.Length == 0)
            missionItemComplete = new bool[datas.Length];
        if (missionItemProgress == null || missionItemProgress.Length == 0)
            missionItemProgress = new int[datas.Length];
        for (int i = 0; i < datas.Length; i++) {
            if (i >= missionItemComplete.Length)
                continue;
            datas[i].SetOnComplete(missionItemComplete[i]);
            datas[i].SetProgress(missionItemProgress[i]);
        }
        SetProgressing();
        IsReset(System.DateTime.Now.DayOfYear - 1, System.DateTime.Now.Year);
    }
    public void SetProgressing() {
        for (int i = 0; i < progressDatas.Length; i++) {
            progressDatas[i].SetIsComplete(pointProgress);
        }
    }
    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.CheckinDay = CheckinDay;
        saveData.CheckinYear = CheckinYear;
        saveData.PointProgress = pointProgress;
        saveData.IsComplete = isComplete;
        saveData.MissionItemComplete = missionItemComplete;
        saveData.MissionItemProgress = missionItemProgress;
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            Initialize(System.DateTime.Now.DayOfYear - 1, System.DateTime.Now.Year, 0);
        }
        else {
            checkinDay = json[JsonKey.Day].AsInt;
            checkinYear = json[JsonKey.Year].AsInt;
            pointProgress = json[JsonKey.Progress].AsInt;
            isComplete = json[JsonKey.IsCompleted].AsBool;

            missionItemComplete = new bool[datas.Length];
            missionItemProgress = new int[datas.Length];

            JSONArray completeNode = json[JsonKey.Completed].AsArray;
            for (int i = 0; i < completeNode.Count; i++) {
                missionItemComplete[i] = completeNode[i].AsBool;
            }

            JSONArray progressNode = json[JsonKey.ProgressS].AsArray;
            for (int i = 0; i < progressNode.Count; i++) {
                missionItemProgress[i] = progressNode[i].AsInt;
            }
        }
        for (int i = 0; i < datas.Length; i++) {
            datas[i].SetOnComplete(missionItemComplete[i]);
            datas[i].SetProgress(missionItemProgress[i]);
        }
        for (int i = 0; i < progressDatas.Length; i++) {
            progressDatas[i].SetIsComplete(pointProgress);
        }
        IsReset(System.DateTime.Now.DayOfYear - 1, System.DateTime.Now.Year);
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.Day, checkinDay);
        node.Add(JsonKey.Year, CheckinYear);
        node.Add(JsonKey.Progress, pointProgress);
        node.Add(JsonKey.IsCompleted, isComplete);

        JSONNode completeNode = new JSONArray();
        foreach (var item in MissionItemComplete) {
            completeNode.Add(item);
        }
        node.Add(JsonKey.Completed, completeNode);

        JSONNode progressNode = new JSONArray();
        foreach (var item in missionItemProgress) {
            progressNode.Add(item);
        }
        node.Add(JsonKey.ProgressS, progressNode);

        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private int d;
        [SerializeField] private int y;
        [SerializeField] private int p;
        [SerializeField] private bool f;
        [SerializeField] private bool[] ic;
        [SerializeField] private int[] ip;

        public int CheckinDay { get => d; set => d = value; }
        public int CheckinYear { get => y; set => y = value; }
        public int PointProgress { get => p; set => p = value; }
        public bool IsComplete { get => f; set => f = value; }
        public bool[] MissionItemComplete { get => ic; set => ic = value; }
        public int[] MissionItemProgress { get => ip; set => ip = value; }
    }
    #endregion
}
