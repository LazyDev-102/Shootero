using SimpleJSON;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "NewAbilityData", menuName = "Resource/HardData/Ability/NewAbilityData")]
public class NewAbilityData : ScriptableObject {
    [SerializeField] private int totalPoint;
    [SerializeField] private int point;
    [SerializeField] private int day;
    [SerializeField] private int year;
    [SerializeField] private int timeFreeReset;
    [SerializeField] private ItemStack buyPrice;
    [SerializeField] private ItemStack resetPrice;
    [SerializeField] private NewAbilityItemData[] datas;

    private void OnEnable() {
        point = 0;
        totalPoint = 0;
        day = 0;
        year = 0;
    }

    public ItemStack Price {
        get {
            buyPrice.Amount = (totalPoint + 1) * 500;
            return buyPrice;
        }
    }

    public ItemStack ResetPrice { get => resetPrice; }

    public bool Buyable => totalPoint <= GameResources.Instance.LevelProgress.GetCurrentLevel();

    public int Point { get => point; }
    public int TotalPoint { get => totalPoint; }

    public bool ResettableAds() {
        return (DateTime.Now.DayOfYear + DateTime.Now.Year * 365 >= day + year * 365);
    }

    public void BuyPoint() {
        totalPoint++;
        point++;
    }

    public void Upgrade(int pointNeed, Action onSucces, Action onFail) {
        if (point >= pointNeed) {
            point -= pointNeed;
            onSucces?.Invoke();
        }
        else {
            onFail?.Invoke();
        }
    }

    public void OldVersionRestorePoint(int point) {
        totalPoint = point;
    }

    public void ResetAll(bool isAds) {
        if (isAds) {
            day = DateTime.Now.DayOfYear + timeFreeReset;
            year = DateTime.Now.Year + day / 365;
        }
        point = totalPoint;

        foreach (var item in datas) {
            item.ResetAll();
        }
    }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        if (saveData == null) {
            point = totalPoint;
            day = DateTime.Now.DayOfYear;
            year = DateTime.Now.Year;
            foreach (var abi in datas) {
                abi.Level = 0;
            }
        }
        else {
            totalPoint = saveData.TotalPoint;
            point = saveData.Point;
            day = saveData.Day;
            year = saveData.Year;

            for (int i = 0; i < saveData.Levels.Length; i++) {
                if (i >= datas.Length)
                    continue;
                datas[i].Level = saveData.Levels[i];
            }
        }
        foreach (var item in datas) {
            item.Apply(null);
        }
    }
    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.TotalPoint = TotalPoint;
        saveData.Point = point;
        saveData.Day = day;
        saveData.Year = year;

        saveData.Levels = new int[datas.Length];
        for (int i = 0; i < datas.Length; i++) {
            saveData.Levels[i] = datas[i].Level;
        }

        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            point = totalPoint;
            day = DateTime.Now.DayOfYear;
            year = DateTime.Now.Year;
            foreach (var abi in datas) {
                abi.Level = 0;
            }
        }
        else {
            if (totalPoint == 0)
                totalPoint = json[JsonKey.Progress].AsInt;
            point = json[JsonKey.Point].AsInt;
            day = json[JsonKey.Day].AsInt;
            year = json[JsonKey.Year].AsInt;

            JSONArray lvNode = json[JsonKey.ProgressS].AsArray;
            for (int i = 0; i < lvNode.Count; i++) {
                datas[i].Level = lvNode[i].AsInt;
            }
        }
        foreach (var item in datas) {
            item.Apply(null);
        }
    }

    public JSONNode Save2Json() {
        JSONNode json = new JSONObject();
        json.Add(JsonKey.Progress, totalPoint);
        json.Add(JsonKey.Point, point);
        json.Add(JsonKey.Day, day);
        json.Add(JsonKey.Year, year);

        JSONNode lvNode = new JSONArray();
        foreach (var abi in datas) {
            lvNode.Add(abi.Level);
        }
        json.Add(JsonKey.ProgressS, lvNode);

        return json;
    }

#if CHEAT
    public void AddPoint(int value) {
        point += value;
    }
#endif

    [Serializable]
    public class SaveData {
        [SerializeField] private int tp;
        [SerializeField] private int p;
        [SerializeField] private int d;
        [SerializeField] private int y;
        [SerializeField] private int[] lv;

        public int TotalPoint { get => tp; set => tp = value; }
        public int Point { get => p; set => p = value; }
        public int Day { get => d; set => d = value; }
        public int Year { get => y; set => y = value; }
        public int[] Levels { get => lv; set => lv = value; }
    }
}
