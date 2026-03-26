using System.Linq;
using UnityEngine;
using System;
using SimpleJSON;

[CreateAssetMenu(fileName = "ChallengeData", menuName = "Resource/HardData/Challenge/ChallengeData")]
public class ChallengeData : ScriptableObject {
    [SerializeField] private string nameEvent;
    [SerializeField] private int checkinDay;
    [SerializeField] private int checkinYear;
    [SerializeField] private int pointTarget;
    [SerializeField] private int pointProgress;
    [SerializeField] private int maxChallenge;
    [SerializeField] private int[] cChallengeIndex;
    [SerializeField] private long[] timeReady;
    [SerializeField] private long delayTime;
    [SerializeField] private int totalTimeToDay;
    [SerializeField] private ChallengeDataInfo[] datas;
    [SerializeField] private ItemClaim[] specialReward;
    [SerializeField] private int[] percentCreateItemNormal;
    [SerializeField] private int[] percentCreateItemOnSkip;
    [SerializeField] private bool isComplete;
    [SerializeField] private bool[] challengeItemComplete;
    [SerializeField] private int[] challengeItemProgress;
    public string NameEvent { get => nameEvent; }
    public int CheckinDay { get => checkinDay; }
    public int CheckinYear { get => checkinYear; }
    public int PointTarget { get => pointTarget; }
    public int PointProgress { get => pointProgress; }
    public ChallengeDataInfo[] Datas { get => datas; }
    public ItemClaim[] SpecialReward { get => specialReward; }
    public bool IsComplete { get => isComplete; }
    public float Progress { get => pointProgress / pointTarget; }
    public int[] ChallengeIndex { get => cChallengeIndex; }
    public long[] TimeReady { get => timeReady; }
    private bool firstOpen;
    public bool[] ChallengeItemComplete { get => challengeItemComplete; }
    public int[] ChallengeItemProgress { get => challengeItemProgress; }

    public void Initialize(int day, int year, int progressInit) {
        checkinDay = day + totalTimeToDay;
        checkinYear = year;
        pointProgress = progressInit;
        isComplete = false;
        firstOpen = true;
        cChallengeIndex = new int[] { -1, 0, 0 };
        timeReady = new long[] { 0, 0, 0 };
        challengeItemComplete = new bool[] { false, false, false };
        challengeItemProgress = new int[] { 0, 0, 0 };
        GetNewFullChallenge(3);
    }
    public bool IsSkip(int index) {
        if (index >= timeReady.Length)
            return false;
        return timeReady[index] > DateTimeOffset.Now.ToUnixTimeSeconds();
    }
    public void ResetData() {
        foreach (var item in datas) {
            if (item != null) {
                foreach (var i in item.Datas) {
                    if (i != null) {
                        i.ResetData();
                    }
                }
            }
        }
    }
    private bool CanReset(int day, int year) {
        if (year < checkinYear)
            return false;
        if (year == checkinYear && day <= checkinDay)
            return false;
        return true;
    }
    public bool IsReset(int day, int year) {
        if (!CanReset(day, year))
            return false;
        Initialize(day - 1, year, 0);
        ResetData();
        return true;
    }
    public void SetTimeReady(int index) {
        if (index >= timeReady.Length)
            return;
        timeReady[index] = DateTimeOffset.Now.ToUnixTimeSeconds() + delayTime;
    }
    public float GetProgress() {
        return (float)pointProgress / (float)pointTarget;
    }
    public void AddPointProgress(int value) {
        if (isComplete)
            return;
        pointProgress += value;
        if (pointProgress >= pointTarget) {
            pointProgress = pointTarget;
        }
    }
    public bool Claimable() {
        if (cChallengeIndex[0] == -1)
            return false;

        ChallengeItemData[] temp = GetAlreadyFullChallenge(maxChallenge);
        for (int i = 0; i < temp.Length; i++) {
            if (temp[i].Claimable)
                return true;
        }
        return false;
    }
    public ChallengeItemData GetItemWithID(int id) {
        ChallengeItemData result = null;
        for (int i = 0; i < datas.Length; i++) {
            var item1 = datas[i].Datas.FirstOrDefault(x => x.ChallengeID == id);
            if (item1 != null) {
                result = item1;
                break;
            }
        }
        return result;
    }
    public bool CheckClaimSpecialReward() {
        if (isComplete)
            return false;
        if (pointProgress < pointTarget)
            return false;
        PopupHUD.Instance.Show<RewardPopup>(hideCurrent: false).UpdateClaimUI(specialReward);
        foreach (var item in specialReward) {
            item.Claim();
        }
        Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnCompleteChallenge);
        isComplete = true;
        return true;
    }
    public ChallengeItemData[] GetFullChallenge(int length) {
        return cChallengeIndex[0] == -1 ? GetNewFullChallenge(length) : GetAlreadyFullChallenge(length);
    }
    private ChallengeItemData[] GetAlreadyFullChallenge(int length) {
        ChallengeItemData[] result = new ChallengeItemData[length];
        for (int i = 0; i < datas.Length; i++) {
            if (result[0] == null) {
                var item1 = datas[i].Datas.FirstOrDefault(x => x.ChallengeID == cChallengeIndex[0]);
                if (item1 != null) {
                    result[0] = item1;
                    result[0].Assign();
                    if (timeReady[0] != 0 && timeReady[0] < DateTimeOffset.Now.ToUnixTimeSeconds())
                        result[0] = GetOneChallenge(0, false);
                }
            }
            if (result[1] == null) {
                var item2 = datas[i].Datas.FirstOrDefault(x => x.ChallengeID == cChallengeIndex[1]);
                if (item2 != null) {
                    result[1] = item2;
                    result[1].Assign();
                    if (timeReady[1] != 0 && timeReady[1] < DateTimeOffset.Now.ToUnixTimeSeconds())
                        result[1] = GetOneChallenge(1, false);
                }
            }
            if (result[2] == null) {
                var item3 = datas[i].Datas.FirstOrDefault(x => x.ChallengeID == cChallengeIndex[2]);
                if (item3 != null) {
                    result[2] = item3;
                    result[2].Assign();
                    if (timeReady[2] != 0 && timeReady[2] < DateTimeOffset.Now.ToUnixTimeSeconds())
                        result[2] = GetOneChallenge(2, false);
                }
            }
        }
        return result;
    }
    private ChallengeItemData[] GetNewFullChallenge(int length) {
        ChallengeItemData[] result = new ChallengeItemData[length];
        for (int i = 0; i < length; i++) {
            ChallengeItemData newChallenge = null;
            do {
                newChallenge = GetOneChallenge(i, false);
            } while (result.Contains(newChallenge));
            result[i] = newChallenge;
            cChallengeIndex[i] = newChallenge.ChallengeID;
        }
        return result;
    }
    public ChallengeItemData GetOneChallenge(int index, bool isAds) {
        timeReady[index] = 0;
        int group = Helper.RandomHelper.RandomWithPercent(isAds ? percentCreateItemOnSkip : percentCreateItemNormal);
        ChallengeItemData result = null;
        int loop = 0;
        do {
            result = Helper.RandomHelper.RandomInCollection(datas[group].Datas);
            loop++;
            if (loop > 10)
                break;
        } while (ChallengeIndex.Contains(result.ChallengeID));
        result.Assign();
        result.ResetData();
        challengeItemProgress[index] = 0;
        challengeItemComplete[index] = false;
        cChallengeIndex[index] = result.ChallengeID;
        return result;
    }

    public bool CanShowNotification() {
        for (int i = 0; i < timeReady.Length; i++) {
            if (timeReady[i] != 0 && DateTimeOffset.Now.ToUnixTimeSeconds() >= timeReady[i])
                return true;
        }
        if (CanReset(DateTime.Now.DayOfYear, DateTime.Now.Year))
            return true;
        return false;
    }
    public bool IsFirstOpen() {
        if (firstOpen) {
            firstOpen = false;
            return true;
        }
        return firstOpen;
    }
    public void SetMissionItemComplete(int index, bool status) {
        for (int i = 0; i < cChallengeIndex.Length; i++) {
            if (index == cChallengeIndex[i]) {
                challengeItemComplete[i] = status;
                break;
            }
        }
    }

    public void SetMissionItemProgress(int index, int value) {
        for (int i = 0; i < cChallengeIndex.Length; i++) {
            if (index == cChallengeIndex[i]) {
                challengeItemProgress[i] = value;
            }
        }
    }

    [Serializable]
    public class ChallengeDataInfo {
        [SerializeField] private ChallengeItemData[] datas;

        public ChallengeItemData[] Datas { get => datas; }
    }

    #region Save Load Data
    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null || json == "") {
            Initialize(DateTime.Now.DayOfYear - 1, DateTime.Now.Year, 0);
            return;
        }
        cChallengeIndex = saveData.CurrentChallengeIndex;
        timeReady = saveData.TimeReady;
        checkinDay = saveData.CheckinDay;
        checkinYear = saveData.CheckinYear;
        firstOpen = true;
        pointProgress = saveData.PointProgress;
        isComplete = saveData.IsComplete;
        challengeItemComplete = saveData.ChallengeItemComplete;
        challengeItemProgress = saveData.ChallengeItemProgress;
        if (challengeItemComplete == null || challengeItemComplete.Length == 0)
            challengeItemComplete = new bool[cChallengeIndex.Length];
        if (challengeItemProgress == null || challengeItemProgress.Length == 0)
            challengeItemProgress = new int[cChallengeIndex.Length];
        for (int i = 0; i < cChallengeIndex.Length; i++) {
            var index = i;
            var item = GetItemWithID(cChallengeIndex[index]);
            if (item != null) {
                item.SetProgress(challengeItemProgress[index]);
                item.SetOnComplete(challengeItemComplete[index]);
            }
        }
        IsReset(DateTime.Now.DayOfYear, DateTime.Now.Year);
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.CurrentChallengeIndex = cChallengeIndex;
        saveData.TimeReady = timeReady;
        saveData.CheckinDay = CheckinDay;
        saveData.CheckinYear = CheckinYear;
        saveData.PointProgress = pointProgress;
        saveData.IsComplete = isComplete;
        saveData.ChallengeItemComplete = challengeItemComplete;
        saveData.ChallengeItemProgress = challengeItemProgress;
        return JsonUtility.ToJson(saveData);
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            Initialize(DateTime.Now.DayOfYear - 1, DateTime.Now.Year, 0);
        }
        else {
            firstOpen = true;

            checkinDay = json[JsonKey.Day].AsInt;
            checkinYear = json[JsonKey.Year].AsInt;
            pointProgress = json[JsonKey.Progress].AsInt;
            isComplete = json[JsonKey.IsCompleted].AsBool;

            int length = cChallengeIndex.Length;
            cChallengeIndex = new int[length];
            timeReady = new long[length];
            challengeItemComplete = new bool[length];
            challengeItemProgress = new int[length];


            JSONArray cChallengeNode = json[JsonKey.CurrentIndex].AsArray;
            for (int i = 0; i < cChallengeIndex.Length; i++) {
                cChallengeIndex[i] = cChallengeNode[i].AsInt;
            }

            JSONArray timeReadyNode = json[JsonKey.TimeFinish].AsArray;
            for (int i = 0; i < timeReady.Length; i++) {
                timeReady[i] = timeReadyNode[i].AsLong;
            }

            JSONArray completeNode = json[JsonKey.Completed].AsArray;
            for (int i = 0; i < challengeItemComplete.Length; i++) {
                challengeItemComplete[i] = completeNode[i].AsBool;
            }

            JSONArray progressNode = json[JsonKey.ProgressS].AsArray;
            for (int i = 0; i < challengeItemProgress.Length; i++) {
                challengeItemProgress[i] = progressNode[i].AsInt;
            }

            for (int i = 0; i < cChallengeIndex.Length; i++) {
                var index = i;
                var item = GetItemWithID(cChallengeIndex[index]);
                if (item != null) {
                    item.SetProgress(challengeItemProgress[index]);
                    item.SetOnComplete(challengeItemComplete[index]);
                }
            }
            IsReset(DateTime.Now.DayOfYear, DateTime.Now.Year);
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();


        node.Add(JsonKey.Day, CheckinDay);
        node.Add(JsonKey.Year, CheckinYear);
        node.Add(JsonKey.Progress, pointProgress);
        node.Add(JsonKey.IsCompleted, isComplete);

        JSONNode cChallengeNode = new JSONArray();
        for (int i = 0; i < cChallengeIndex.Length; i++) {
            cChallengeNode.Add(cChallengeIndex[i]);
        }
        node.Add(JsonKey.CurrentIndex, cChallengeNode);

        JSONNode timeReadyNode = new JSONArray();
        for (int i = 0; i < timeReady.Length; i++) {
            timeReadyNode.Add(timeReady[i]);
        }
        node.Add(JsonKey.TimeFinish, timeReadyNode);

        JSONNode completeNode = new JSONArray();
        for (int i = 0; i < challengeItemComplete.Length; i++) {
            completeNode.Add(challengeItemComplete[i]);
        }
        node.Add(JsonKey.Completed, completeNode);

        JSONNode progressNode = new JSONArray();
        for (int i = 0; i < challengeItemProgress.Length; i++) {
            progressNode.Add(challengeItemProgress[i]);
        }
        node.Add(JsonKey.ProgressS, progressNode);

        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private int d;
        [SerializeField] private int y;
        [SerializeField] private int pp;
        [SerializeField] private bool f;
        [SerializeField] private int[] i;
        [SerializeField] private long[] t;
        [SerializeField] private bool[] cic;
        [SerializeField] private int[] cip;

        public int CheckinDay { get => d; set => d = value; }
        public int CheckinYear { get => y; set => y = value; }
        public int PointProgress { get => pp; set => pp = value; }
        public bool IsComplete { get => f; set => f = value; }
        public int[] CurrentChallengeIndex { get => i; set => i = value; }
        public long[] TimeReady { get => t; set => t = value; }
        public bool[] ChallengeItemComplete { get => cic; set => cic = value; }
        public int[] ChallengeItemProgress { get => cip; set => cip = value; }
    }
    #endregion
}