using SimpleJSON;
using Gemmob.Common;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipPackData", menuName = "Resource/HardData/Ship/ShipPackData")]
public class ShipPackData : ScriptableObject {
    [SerializeField] private ShipPackInfo[] packs;
    [SerializeField] private int levelUnlock;
    [SerializeField] private string remoteKey;
    [SerializeField] private int startDay;
    [SerializeField] private int startMonth;
    [SerializeField] private int startYear;
    [SerializeField] private int endDay;
    [SerializeField] private int endMonth;
    [SerializeField] private int endYear;
    [SerializeField] private bool enable;
    public ShipPackInfo[] Packs { get => packs; }
    [System.NonSerialized]
    public bool LoadRemoteDone;
    private bool isReloadRemoteData;

    public void Initialize() {
        for (int i = 0; i < packs.Length; i++) {
            packs[i].SetBought(false);
        }
    }
    public void ActionOnLoad() {
#if UNITY_ANDROID
        if (PrefSaver.FirstOpenGame) {
            RestorePurchased();
            PrefSaver.FirstOpenGame = false;
        }
#endif
    }
    public bool FirstCondition() {
        return GameResources.Instance.LevelProgress.GetCurrentLevel() >= levelUnlock;
    }
    public bool OfflineStatus() {
        for (int i = 0; i < packs.Length; i++) {
            if (packs[i].Status())
                return true;
        }
        return false;
    }
    public void RestorePurchased() {
        for (int i = 0; i < packs.Length; i++) {
            packs[i].RestorePurchase();
        }
    }
    public void ReloadDataByRemote() {
        LoadRemoteDone = false;
        if (remoteKey.Trim().Length == 0 || !Networks.IsInternetAvaiable) {
            LoadRemoteDone = true;
            return;
        }
        if (!isReloadRemoteData)
            RemoteConfig.GetStringAsync(remoteKey, GetData);
        else
            RemoteConfig.Instance.ReloadGetStringAsyncs(remoteKey, GetData);
        isReloadRemoteData = true;

    }
    private void GetData(string data) {
        RemoteData remoteData = null;
        if (!string.IsNullOrEmpty(data)) {
            remoteData = JsonUtility.FromJson<RemoteData>(data);
        }

        if (remoteData == null) {
            startDay = endDay = System.DateTime.Now.Day - 1;
            startMonth = endMonth = System.DateTime.Now.Month;
            startYear = endYear = System.DateTime.Now.Year;
            enable = false;
            return;
        }
        enable = remoteData.Enable;
        startDay = remoteData.StartDay;
        startMonth = remoteData.StartMonth;
        startYear = remoteData.StartYear;
        endDay = remoteData.EndDay;
        endMonth = remoteData.EndMonth;
        endYear = remoteData.EndYear;
        LoadRemoteDone = true;
    }
    public string GetTimeStart() {
        return string.Format("{0:D2}/{1:D2}/{2}", startDay, startMonth, startYear);
    }
    public string GetTimeEnd() {
        return string.Format("{0:D2}/{1:D2}/{2}", endDay, endMonth, endYear);
    }
    public string GetTimeHappen() {
        if (!enable || startYear <= 0 || startMonth <= 0 || startDay <= 0 || endDay <= 0 || endMonth <= 0 || endYear <= 0)
            return "";
        System.DateTime startTime = new System.DateTime(startYear, startMonth, startDay);
        System.DateTime endTime = new System.DateTime(endYear, endMonth, endDay);
        return startTime.ToString("MMM dd") + " - " + endTime.ToString("MMM dd");
    }
    public bool Status() {
        if (enable) {
            var cYear = System.DateTime.Now.Year;
            var cMonth = System.DateTime.Now.Month;
            var cDay = System.DateTime.Now.Day;
            var cAllDay = cDay + cMonth * 30 + cYear * 365;
            var startAllDay = startDay + startMonth * 30 + startYear * 365;
            var endAllDay = endDay + endMonth * 30 + endYear * 365;
            if (cAllDay < startAllDay || cAllDay > endAllDay)
                return false;
            return true;
        }
        else {
            return OfflineStatus();
        }
    }
    #region Remote
    public class RemoteData {
        [SerializeField] private bool enable;
        [SerializeField] private int startDay;
        [SerializeField] private int startMonth;
        [SerializeField] private int startYear;
        [SerializeField] private int endDay;
        [SerializeField] private int endMonth;
        [SerializeField] private int endYear;

        public bool Enable { get => enable; set => enable = value; }
        public int StartDay { get => startDay; set => startDay = value; }
        public int StartMonth { get => startMonth; set => startMonth = value; }
        public int StartYear { get => startYear; set => startYear = value; }
        public int EndDay { get => endDay; set => endDay = value; }
        public int EndMonth { get => endMonth; set => endMonth = value; }
        public int EndYear { get => endYear; set => endYear = value; }

        public RemoteData() {
            enable = false;
            startDay = 0;
            startMonth = 0;
            startYear = 0;
            endDay = 0;
            endMonth = 0;
            endYear = 0;
        }

        public RemoteData(bool enable, int sDay, int sMonth, int sYear, int eDay, int eMonth, int eYear) {
            this.enable = enable;
            startDay = sDay;
            startMonth = sMonth;
            startYear = sYear;
            endDay = eDay;
            endMonth = eMonth;
            endYear = eYear;
        }
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
        if (saveData.Bought == null || saveData.Bought.Length == 0)
            saveData.Bought = new bool[packs.Length];
        for (int i = 0; i < saveData.Bought.Length; i++) {
            packs[i].SetBought(saveData.Bought[i]);
        }
        isReloadRemoteData = false;
    }
    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.Bought = new bool[packs.Length];
        for (int i = 0; i < packs.Length; i++) {
            saveData.Bought[i] = packs[i].Bought;
        }
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONArray json) {
        if (json == null || json.Count <= 0) {
            Initialize();
        }
        else {
            for (int i = 0; i < json.Count; i++) {
                if (i >= packs.Length)
                    continue;
                packs[i].SetBought(json[i].AsBool);
            }
            isReloadRemoteData = false;
        }
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONArray();
        for (int i = 0; i < packs.Length; i++) {
            node.Add(packs[i].Bought);
        }
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private bool[] b;

        public bool[] Bought { get => b; set => b = value; }
    }
    #endregion
}