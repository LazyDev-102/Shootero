using UnityEngine;
using SimpleJSON;

[System.Serializable]
public class ChipPackInfo {
    [SerializeField] private ChipPackItem[] packs;
    [SerializeField] private int day;
    [SerializeField] private int year;

    public ChipPackItem[] Packs { get => packs; }
    public int Day { get => day; set => day = value; }
    public int Year { get => year; set => year = value; }

    public bool Claimable(int day, int year) {
        if (year < this.year)
            return false;
        if (year == this.year && day <= this.day)
            return false;
        return true;
    }
    public void Save(int day, int year) {
        this.day = day;
        this.year = year;
    }
    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            day = System.DateTime.Now.DayOfYear - 1;
            year = System.DateTime.Now.Year;
            for (int i = 0; i < packs.Length; i++) {
                packs[i].LoadFromJson(null);
            }
            return;
        }
        day = saveData.DailyFreeDay;
        year = saveData.DailyFreeYear;

        if (saveData.Packs == null)
            saveData.Packs = new string[packs.Length];
        var canClaim = Claimable(System.DateTime.Now.DayOfYear, System.DateTime.Now.Year);
        var maxLength = saveData.Packs.Length;
        for (int i = 0; i < packs.Length; i++) {
            packs[i].LoadFromJson(i >= maxLength ? null : saveData.Packs[i]);
            packs[i].ResetData(canClaim);
        }
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.DailyFreeDay = day;
        saveData.DailyFreeYear = year;
        saveData.Packs = new string[packs.Length];
        for (int i = 0; i < packs.Length; i++) {
            saveData.Packs[i] = packs[i].SaveToJson();
        }
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            day = System.DateTime.Now.DayOfYear - 1;
            year = System.DateTime.Now.Year;
            for (int i = 0; i < packs.Length; i++) {
                packs[i].LoadFJson(null);
            }
        }
        else {
            day = json[JsonKey.Day].AsInt;
            year = json[JsonKey.Year].AsInt;
            JSONArray packsNode = json[JsonKey.Packs].AsArray;
            var canClaim = Claimable(System.DateTime.Now.DayOfYear, System.DateTime.Now.Year);
            var maxLength = packsNode.Count;
            for (int i = 0; i < maxLength; i++) {
                packs[i].LoadFJson(packsNode[i]);
                packs[i].ResetData(canClaim);
            }
            for (int j = maxLength; j < packs.Length; j++) {
                packs[j].LoadFJson(null);
                packs[j].ResetData(canClaim);
            }
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.Day, day);
        node.Add(JsonKey.Year, year);

        JSONNode packsNode = new JSONArray();
        for (int i = 0; i < packs.Length; i++) {
            packsNode.Add(packs[i].Save2Json());
        }
        node.Add(JsonKey.Packs, packsNode);

        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private int dfd;
        [SerializeField] private int dfy;
        [SerializeField] private string[] ps;
        public int DailyFreeDay { get => dfd; set => dfd = value; }
        public int DailyFreeYear { get => dfy; set => dfy = value; }
        public string[] Packs { get => ps; set => ps = value; }
    }

}
