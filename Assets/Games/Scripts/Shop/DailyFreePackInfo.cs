
using UnityEngine;
using SimpleJSON;

[System.Serializable]
public class DailyFreePackInfo {
    [SerializeField] private DailyFreePackItem[] packs;
    [SerializeField] private int day;
    [SerializeField] private int year;

    public DailyFreePackItem[] Packs { get => packs; }
    public int Day { get => day; set => day = value; }
    public int Year { get => year; set => year = value; }

    private void DailyFreeInitialize() {
        Load(System.DateTime.Now.DayOfYear - 1, System.DateTime.Now.Year);
        AssignPacks();
    }
    private void Load(int day, int year) {
        this.day = day;
        this.year = year;
    }
    private void AssignPacks() {
        foreach (var item in packs) {
            item.Assign();
        }
    }
    public bool Claimable(int day, int year) {
        if (year < this.year)
            return false;
        if (year == this.year && day <= this.day)
            return false;
        return true;
    }

    public bool Claim(int day, int year, int amount) {
        if (!Claimable(day, year))
            return false;

        GameResources.Instance.DailyMission.AddPointProgress(MissionType.ClaimDailyFreePack, 1);
        Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnClaimDailyFree);
        this.day = day;
        this.year = year;
        foreach (var item in packs) {
            item.Claim(amount);
        }
        AssignPacks();
        return true;
    }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        if (saveData == null) {
            DailyFreeInitialize();
            return;
        }
        Load(saveData.DailyFreeDay, saveData.DailyFreeYear);
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.Save(day, year);
        return JsonUtility.ToJson(saveData);
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            DailyFreeInitialize();
        }
        else {
            Load(json[JsonKey.Day].AsInt, json[JsonKey.Year].AsInt);
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.Day, day);
        node.Add(JsonKey.Year, year);
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private int dfd;
        [SerializeField] private int dfy;
        public int DailyFreeDay { get => dfd; set => dfd = value; }
        public int DailyFreeYear { get => dfy; set => dfy = value; }
        public void Save(int day, int year) {
            dfd = day;
            dfy = year;
        }
    }
}
