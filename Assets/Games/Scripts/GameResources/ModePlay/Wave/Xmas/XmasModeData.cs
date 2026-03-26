using SimpleJSON;
using System;
using UnityEngine;
using System.Collections.Generic;
using Gemmob.Common;
using Gemmob;

[CreateAssetMenu(fileName = "XmasModeData", menuName = "Resource/Modes/Xmas/XmasModeData")]
public class XmasModeData : ScriptableObject {
    [SerializeField] private int day;
    [SerializeField] private int year;
    [SerializeField] private int maxTurn;
    [SerializeField] private ItemStack moreTicketPrice;
    [SerializeField] private ItemStack moreTicketReward;
    [SerializeField] private ZoneBackground background;
    [SerializeField] private XmasModeWaveData[] zoneData;
    [SerializeField] private XmasZoneData enemyData;
    [SerializeField] private int buyablePerDay;
    [SerializeField] private int buyableRemain;
    [SerializeField] private int cSession;

    [Header("Remote")]
    [SerializeField] private bool remoteEnable;
    [SerializeField] private string remoteKey;
    [SerializeField] private int startDay;
    [SerializeField] private int startMonth;
    [SerializeField] private int startYear;
    [SerializeField] private int endDay;
    [SerializeField] private int endMonth;
    [SerializeField] private int endYear;
    [SerializeField] private int remoteSession;

    private List<int> gearIds;
    private List<int> gearRanks;
    private float multiDifficult;
    private int currentWave;


    public int MaxTurn { get => maxTurn; }
    public float MultiDifficult { get => multiDifficult; }
    public List<int> GearIds { get => gearIds; }
    public List<int> GearRanks { get => gearRanks; }
    public XmasModeWaveData[] ZoneData { get => zoneData; }
    public ZoneBackground Background { get => background; }
    public XmasZoneData Prefab { get => enemyData; }
    public ItemStack MoreTicketPrice { get => moreTicketPrice; }
    public ItemStack MoreTicketReward { get => moreTicketReward; }
    public int CurrentWave { get => currentWave; }
    public int BuyablePerDay { get => buyablePerDay; }
    public int BuyableRemain { get => buyableRemain; }
    public bool Buyable => buyableRemain > 0;

    public int Session { get => cSession; }
    public XmasZoneData EnemyData { get => enemyData; }

    private void OnEnable() {
        EventDispatcher.Instance.AddListener(EventKey.OnLoadHomeScene, GetDataFromRemote);
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener(EventKey.OnLoadHomeScene, GetDataFromRemote);
    }
    public void SetCurrentWave(int value) {
        currentWave = value;
    }

    public void Reload() {
        // No reward
    }

    public void ChangeTurnRemain(int value) {
        GameResources.Instance.Inventory.Remove(moreTicketReward.Id, value);
    }
    public void BuyMoreTicket() {
        buyableRemain--;
        GameResources.Instance.Inventory.AddXTicket(moreTicketReward.Amount);
    }

    public void ClaimReward() {

    }

    public void Preload() {
        if (enemyData != null) {
            enemyData.PreloadIngame();
        }
    }


    public XmasModeWaveInfo[] GenerateWaves() {
        XmasModeWaveInfo[] waveInfoes = new XmasModeWaveInfo[zoneData.Length];
        for (int i = 0; i < waveInfoes.Length; ++i) {
            XmasModeWaveInfo waveInfo = zoneData[i].CreateInfo(0, i) as XmasModeWaveInfo;
            waveInfoes[i] = waveInfo;
        }
        currentWave = 0;
        gearIds = new List<int>();
        gearRanks = new List<int>();
        multiDifficult = (GameResources.Instance.LevelProgress.GetCurrentLevel() + 1) / 5;
        return waveInfoes;
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            day = DateTime.Now.DayOfYear - 1;
            year = DateTime.Now.Year;
            cSession = 0;
            buyableRemain = buyablePerDay;
            GameResources.Instance.Inventory.AddXTicket(5);
            ResetTurn(true);
        }
        else {
            day = JK.Get(json, JsonKey.Day, DateTime.Now.DayOfYear - 1);
            year = JK.Get(json, JsonKey.Year, DateTime.Now.Year);
            cSession = json[JsonKey.Progress].AsInt;
            buyableRemain = json[JsonKey.CurrentRemain].AsInt;
        }
    }

    private void ResetTurn(bool isNewSession) {
        if (isNewSession || (DateTime.Now.Year * 365 + DateTime.Now.DayOfYear > year * 365 + day)) {
            buyableRemain = buyablePerDay;
            year = DateTime.Now.Year;
            day = DateTime.Now.DayOfYear;
            var inv = GameResources.Instance.Inventory;
            var xTicket = inv.GetXTicket();
            if(isNewSession) {
                var xShock = inv.GetXCandy();
                inv.Remove(xShock.Id, xShock.Amount);
            }
            if (xTicket.Amount < 5) {
                inv.Add(xTicket.Id, 5 - xTicket.Amount);
            }
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();

        node.Add(JsonKey.Day, day);
        node.Add(JsonKey.Year, year);
        node.Add(JsonKey.Progress, cSession);
        node.Add(JsonKey.CurrentRemain, buyableRemain);

        return node;
    }
    public bool Status() {
        if (remoteEnable) {
            var cYear = DateTime.Now.Year;
            var cMonth = DateTime.Now.Month;
            var cDay = DateTime.Now.Day;
            var cAllDay = cDay + cMonth * 30 + cYear * 365;
            var startAllDay = startDay + startMonth * 30 + startYear * 365;
            var endAllDay = endDay + endMonth * 30 + endYear * 365;
            if (cAllDay < startAllDay || cAllDay > endAllDay)
                return false;
            return true;
        }
        else {
            return false;
        }
    }

    #region Remote
    public void GetDataFromRemote() {
        if (remoteKey.Trim().Length == 0 || !Networks.IsInternetAvaiable) {
            return;
        }
        RemoteConfig.GetStringAsync(remoteKey, GetData);

    }
    private void GetData(string data) {
        RemoteData remoteData = null;
        if (!string.IsNullOrEmpty(data)) {
            remoteData = JsonUtility.FromJson<RemoteData>(data);
        }

        if (remoteData == null) {
            startDay = endDay = DateTime.Now.Day - 1;
            startMonth = endMonth = DateTime.Now.Month;
            startYear = endYear = DateTime.Now.Year;
            remoteEnable = false;
            remoteSession = 0;
            return;
        }
        remoteEnable = remoteData.Enable;
        startDay = remoteData.StartDay;
        startMonth = remoteData.StartMonth;
        startYear = remoteData.StartYear;
        endDay = remoteData.EndDay;
        endMonth = remoteData.EndMonth;
        endYear = remoteData.EndYear;
        remoteSession = remoteData.Session;
        ResetTurn(remoteSession > cSession);
        if (remoteSession > cSession) {
            GameResources.Instance.XmasMission.ResetData();
            GameResources.Instance.XmasShopData.ResetData();
            cSession = remoteSession;
        }
    }
    public class RemoteData {
        [SerializeField] private bool enable;
        [SerializeField] private int startDay;
        [SerializeField] private int startMonth;
        [SerializeField] private int startYear;
        [SerializeField] private int endDay;
        [SerializeField] private int endMonth;
        [SerializeField] private int endYear;
        [SerializeField] private int session;

        public bool Enable { get => enable; set => enable = value; }
        public int StartDay { get => startDay; set => startDay = value; }
        public int StartMonth { get => startMonth; set => startMonth = value; }
        public int StartYear { get => startYear; set => startYear = value; }
        public int EndDay { get => endDay; set => endDay = value; }
        public int EndMonth { get => endMonth; set => endMonth = value; }
        public int EndYear { get => endYear; set => endYear = value; }
        public int Session { get => session; set => session = value; }

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
}