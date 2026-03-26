
using UnityEngine;
using SimpleJSON;

[System.Serializable]
public class RerollPackInfo {
    [SerializeField] private RerollPackItem[] packs;
    [SerializeField] private int day;
    [SerializeField] private int year;
    [SerializeField] private int valueInit;

    public RerollPackItem[] Packs { get => packs; }
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
            GameResources.Instance.Inventory.Add(ConstantItemID.RerollId, valueInit);
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

    public void Resetable() {
        var canClaim = Claimable(System.DateTime.Now.DayOfYear, System.DateTime.Now.Year);
        for (int i = 0; i < packs.Length; i++) {
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
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.Day, day);
        node.Add(JsonKey.Year, year);

        JSONNode packsNode = new JSONArray();
        for (int i = 0; i < packs.Length; i++) {
            packsNode.Add(packs[i].Watched);
        }
        node.Add(JsonKey.Packs, packsNode);

        return node;
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            day = System.DateTime.Now.DayOfYear - 1;
            year = System.DateTime.Now.Year;
            for (int i = 0; i < packs.Length; i++) {
                packs[i].LoadFJson(null);
            }
            GameResources.Instance.Inventory.Add(ConstantItemID.RerollId, valueInit);
        }
        else {
            day = json[JsonKey.Day].AsInt;
            year = json[JsonKey.Year].AsInt;

            JSONArray packNode = json[JsonKey.Packs].AsArray;
            var canClaim = Claimable(System.DateTime.Now.DayOfYear, System.DateTime.Now.Year);
            var maxLength = packNode.Count;
            for (int i = 0; i < packNode.Count; i++) {
                packs[i].Watched = (i >= maxLength ? false : packNode[i].AsBool);
                packs[i].ResetData(canClaim);
            }
        }
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
