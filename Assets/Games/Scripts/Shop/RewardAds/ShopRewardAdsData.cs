
using UnityEngine;
using SimpleJSON;

[System.Serializable]
public class ShopRewardAdsData {
    [SerializeField] private ShopRewardAdsPackItem[] packs;
    [SerializeField] private ShopRewardAdsProgressItem[] progressPacks;
    [SerializeField] private int day;
    [SerializeField] private int year;
    [SerializeField] private int progress;

    private int maxProgress = 15;

    public ShopRewardAdsPackItem[] Packs { get => packs; }
    public int Day { get => day; set => day = value; }
    public int Year { get => year; set => year = value; }
    public int Progress { get => progress; }

    public bool Claimable(int day, int year) {
        if (year < this.year)
            return false;
        if (year == this.year && day <= this.day)
            return false;
        return true;
    }

    public float Ratio() {
        return (float)progress / (float)maxProgress;
    }

    public void Upgrade() {
        if (progress < maxProgress)
            progress++;
    }

    public void Save(int day, int year) {
        this.day = day;
        this.year = year;
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.Day, day);
        node.Add(JsonKey.Year, year);
        node.Add(JsonKey.Progress, progress);

        JSONNode packsNode = new JSONArray();
        for (int i = 0; i < packs.Length; i++) {
            packsNode.Add(packs[i].Save2Json());
        }
        node.Add(JsonKey.Packs, packsNode);

        JSONNode progressPacksNode = new JSONArray();
        for (int i = 0; i < progressPacks.Length; i++) {
            progressPacksNode.Add(progressPacks[i].Save2Json());
        }
        node.Add(JsonKey.ProgressS, progressPacksNode);

        return node;
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            day = System.DateTime.Now.DayOfYear - 1;
            year = System.DateTime.Now.Year;
            progress = 0;
            for (int i = 0; i < packs.Length; i++) {
                packs[i].LoadFJson(null);
            }
            for (int i = 0; i < progressPacks.Length; i++) {
                progressPacks[i].LoadFJson(null);
            }
        }
        else {
            day = json[JsonKey.Day].AsInt;
            year = json[JsonKey.Year].AsInt;

            JSONArray packNode = json[JsonKey.Packs].AsArray;
            for (int i = 0; i < packs.Length; i++) {
                packs[i].LoadFJson(packNode[i]);
            }
            JSONArray progressPackNode = json[JsonKey.ProgressS].AsArray;
            for (int i = 0; i < progressPacks.Length; i++) {
                progressPacks[i].LoadFJson(progressPackNode[i]);
            }

            var canClaim = Claimable(System.DateTime.Now.DayOfYear, System.DateTime.Now.Year);
            CheckResetData();
            progress = canClaim ? 0 : json[JsonKey.Progress].AsInt;
        }
    }

    public void CheckResetData() {
        var canClaim = Claimable(System.DateTime.Now.DayOfYear, System.DateTime.Now.Year);
        for (int i = 0; i < packs.Length; i++) {
            packs[i].ResetData(canClaim);
        }
        for (int i = 0; i < progressPacks.Length; i++) {
            progressPacks[i].ResetData(canClaim);
        }
        if (canClaim) {
            progress = 0;
            Save(System.DateTime.Now.DayOfYear, System.DateTime.Now.Year);
        }
    }
    [System.Serializable]
    public class SaveData {
        [SerializeField] private int dfd;
        [SerializeField] private int dfy;
        [SerializeField] private int p;
        [SerializeField] private string[] ps;
        [SerializeField] private string[] pps;
        public int DailyFreeDay { get => dfd; set => dfd = value; }
        public int DailyFreeYear { get => dfy; set => dfy = value; }
        public int Progress { get => p; set => p = value; }
        public string[] Packs { get => ps; set => ps = value; }
        public string[] ProgressPacks { get => pps; set => pps = value; }
    }
}
