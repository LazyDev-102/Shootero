using UnityEngine;
using SimpleJSON;
using UnityEngine.Purchasing;
using Helper;

[System.Serializable]
public class DailyPacksInfo {
    [SerializeField] private string packName;
    [SerializeField] private bool isFree;
    [SerializeField] private int checkinDay;
    [SerializeField] private int checkinYear;
    [SerializeField] private int multiChipAfk;
    [SerializeField] private int multiMaterialAfk;
    [SerializeField] private string iapKey;
    [SerializeField] private float defaulIap;
    [SerializeField] private ProductType productType;
    [SerializeField] private ItemClaim[] rewards;

    public string PackName { get => packName; }
    public bool IsFree { get => isFree; }
    public int CheckinDay { get => checkinDay; }
    public int CheckinYear { get => checkinYear; }
    public string IAPKey { get => iapKey; }
    public float DefaulIap { get => defaulIap; }
    public ProductType ProductType { get => productType; }
    public ItemClaim[] Rewards { get => rewards; }

    public void Initialize() {
        checkinDay = System.DateTime.Now.DayOfYear - 1;
        checkinYear = System.DateTime.Now.Year;
        AssignRewards();
    }
    private void AssignRewards() {
        if (rewards == null || rewards.Length == 0)
            return;
        foreach (var item in rewards) {
            if (item.Id == ConstantItemID.ChipId) {
                item.Amount = (GameResources.Instance.ChipPerSecond * Constant.HourToSecond * multiChipAfk).ConvertToInt();
            }
            if (item.Id == ConstantItemID.RandomMatId) {
                var value = (GameResources.Instance.MaterialPerSecond * Constant.HourToSecond * multiMaterialAfk).ConvertToInt();
                if (value < 1)
                    value = 1;
                item.Amount = value;
            }
        }
    }
    public bool Claimable(int checkinDay, int checkinYear) {
        if (checkinYear < this.checkinYear)
            return false;
        if (checkinYear == this.checkinYear && checkinDay <= this.checkinDay)
            return false;
        return true;
    }

    public bool Claim(int day, int year, int multi) {
        if (!Claimable(day, year))
            return false;

        this.checkinDay = day;
        this.checkinYear = year;
        foreach (var item in rewards) {
            item.Claim(multi);
        }
        AssignRewards();
        Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnPurchaseDailyPacks);
        return true;
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
        if (Claimable(System.DateTime.Now.DayOfYear, System.DateTime.Now.Year))
            AssignRewards();
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.CheckinDay = CheckinDay;
        saveData.CheckinYear = CheckinYear;
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            Initialize();
        }
        else {
            checkinDay = json[JsonKey.Day].AsInt;
            checkinYear = json[JsonKey.Year].AsInt;
            if (Claimable(System.DateTime.Now.DayOfYear, System.DateTime.Now.Year))
                AssignRewards();
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.Day, CheckinDay);
        node.Add(JsonKey.Year, CheckinYear);
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private int d;
        [SerializeField] private int y;

        public int CheckinDay { get => d; set => d = value; }
        public int CheckinYear { get => y; set => y = value; }
    }
    #endregion
}
