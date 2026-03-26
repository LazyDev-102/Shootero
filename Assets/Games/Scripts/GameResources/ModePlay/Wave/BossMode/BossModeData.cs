using SimpleJSON;
using Helper;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossModeData", menuName = "Resource/Modes/Boss/BossModeData")]
public class BossModeData : ScriptableObject {
    [SerializeField] private int maxTurn;
    [SerializeField] private ItemCollector gearCollector;
    [SerializeField] private BossModeWaveData gearWaveData;
    [SerializeField] private BossModeInfo[] BossModeInfos;

    public int TurnRemain { get => turnRemain; }
    public int MaxTurn { get => maxTurn; }
    public List<int> GearIds { get => gearIds; }
    public List<int> GearRanks { get => gearRanks; }

    private int turnRemain;
    private int checkinDay;
    private int checkinYear;
    private BossModeInfo currentGearInfo;
    private List<int> gearIds;
    private List<int> gearRanks;
    public BossModeWaveInfo GenerateWaves(BossModeWaveInfo preWave) {
        if (preWave == null) {
            preWave = new BossModeWaveInfo();
        }
        preWave.CreateData(gearWaveData);
        gearIds = new List<int>();
        gearRanks = new List<int>();
        return preWave;
    }
    public void AddGearClaimUI(int id, int rank) {
        gearIds.Add(id);
        gearRanks.Add(rank);
    }
    public void ChangeTurnRemain(int amplitude = -1) {
        turnRemain += amplitude;
    }
    public void ClaimReward() {
        var info = GetInfo();
        if (info != null) {
            info.ClaimReward(gearCollector);
        }
    }
    public BossModeInfo GetInfo() {
        if (currentGearInfo != null && currentGearInfo.TimeLimit != 0)
            return currentGearInfo;
        var currentLevel = GameResources.Instance.LevelProgress.GetCurrentLevel() + 1;
        for (int i = BossModeInfos.Length - 1; i >= 0; i--) {
            if (BossModeInfos[i].SytemLevel <= currentLevel)
                return BossModeInfos[i];
        }
        return BossModeInfos[BossModeInfos.Length - 1];
    }

    public void Preload() {
        gearWaveData.Preload();
    }

    #region SaveData
    public string SaveToJson() {
        SaveData saveData = new SaveData();
        if (turnRemain > 10)
            turnRemain = 10;
        saveData.TurnRemain = turnRemain;
        saveData.CheckinDay = checkinDay;
        saveData.CheckinYear = checkinYear;
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            turnRemain = maxTurn;
            checkinDay = DateTime.Now.DayOfYear;
            checkinYear = DateTime.Now.Year;
            return;
        }
        turnRemain = saveData.TurnRemain;
        checkinDay = saveData.CheckinDay;
        checkinYear = saveData.CheckinYear;
        ResetDay();
    }
    public JSONNode Save2Json() {
        if (turnRemain > 10)
            turnRemain = 10;

        JSONNode node = new JSONObject();
        node.Add(JsonKey.CurrentRemain, turnRemain);
        node.Add(JsonKey.Day, checkinDay);
        node.Add(JsonKey.Year, checkinYear);

        return node;
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            turnRemain = maxTurn;
            checkinDay = DateTime.Now.DayOfYear;
            checkinYear = DateTime.Now.Year;
        }
        else {
            turnRemain = json[JsonKey.CurrentRemain].AsInt;
            checkinDay = json[JsonKey.Day].AsInt;
            checkinYear = json[JsonKey.Year].AsInt;
            ResetDay();
        }
    }
    public void ResetDay() {
        if (DateTime.Now.Year < checkinYear)
            return;
        if (DateTime.Now.Year == checkinYear && DateTime.Now.DayOfYear <= checkinDay)
            return;
        turnRemain = maxTurn;
        checkinDay = DateTime.Now.DayOfYear;
        checkinYear = DateTime.Now.Year;
    }
    [Serializable]
    public class SaveData {
        [SerializeField] private int tr;
        [SerializeField] private int d;
        [SerializeField] private int y;
        public int TurnRemain { get => tr; set => tr = value; }
        public int CheckinDay { get => d; set => d = value; }
        public int CheckinYear { get => y; set => y = value; }
    }
    #endregion

}

[Serializable]
public class BossModeInfo {
    [SerializeField] private int sytemLevel;
    [SerializeField] private int timeLimit;
    [SerializeField] private float multiDifficult;
    [SerializeField] private int[] gearCount;
    [SerializeField] private ItemClaim[] otherReward;

    public int SytemLevel { get => sytemLevel; }
    public int TimeLimit { get => timeLimit; }
    public float MultiDifficult { get => multiDifficult; }

    public void ClaimReward(ItemCollector gearCollector) {
        ClaimMainReward(gearCollector);
        ClaimOtherReward();
    }
    private void ClaimMainReward(ItemCollector itemCollector) {
        for (int i = 0; i < gearCount.Length; i++) {
            if (gearCount[i] <= 0)
                continue;
            for (int j = 0; j < gearCount[i]; j++) {
                Item item = RandomHelper.RandomInCollection(itemCollector.Items);
                GearClaimExtentions.Claim(item.Id, i);
                GameManager.Instance.AddClaimedItem(item.Id, 1);
                GameResources.Instance.BossModeData.AddGearClaimUI(item.Id, i);
            }
        }
    }
    private void ClaimOtherReward() {
        foreach (var item in otherReward) {
            item.Claim();
            GameManager.Instance.AddClaimedItem(item.Id, item.Amount);
        }
    }
}
