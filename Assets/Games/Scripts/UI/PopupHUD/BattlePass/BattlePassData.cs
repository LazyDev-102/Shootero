using SimpleJSON;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattlePassData", menuName = "Resource/HardData/BattlePass/BattlePassData")]
public class BattlePassData : ScriptableObject {
    [SerializeField] private string nameEvent;
    [SerializeField] private int timePerSeason;
    [SerializeField] private int dayEndSeason;
    [SerializeField] private int yearEndSeason;
    [SerializeField] private bool isComplete;
    [SerializeField] private bool isPurchase;
    [SerializeField] private int progress;
    [SerializeField] private int seasonIndex;
    [SerializeField] private List<BattlePassItemData> datas;
    public string NameEvent { get => nameEvent; }
    public int DayEndSeason { get => dayEndSeason; }
    public int YearEndSeason { get => yearEndSeason; }
    public bool IsComplete { get => isComplete; }
    public int Count { get => datas.Count; }
    public bool IsPurchase { get => isPurchase; }
    public int Progress { get => progress; }
    public int SeasonIndex { get => seasonIndex; }
    public List<BattlePassItemData> ItemData { get => datas; }

    public string PurchaseKey;
    public string OriginIapKey;
    public float DefaulIap;
    public UnityEngine.Purchasing.ProductType ProductType;
    public int TimeLeft {
        get {
            return yearEndSeason > DateTime.Now.Year ?
                (dayEndSeason + 365 - DateTime.Now.DayOfYear) * Constant.DayToSecond
                : (dayEndSeason - DateTime.Now.DayOfYear) * Constant.DayToSecond;
        }
    }


    private void Initialize(int day, int year) {
        bool over = dayEndSeason > 365;
        dayEndSeason = over ? day + timePerSeason - 365 : day + timePerSeason;
        yearEndSeason = over ? year + 1 : year;
        isComplete = false;
        progress = 0;
        for (int i = 0; i < datas.Count; i++) {
            datas[i].ResetData();
        }
        datas[0].Assign();
    }
    public void SetPurchase(bool status) {
        isPurchase = status;
    }
    public bool IsReset(int day, int year) {
        if (!CanReset(day, year))
            return false;
        ClaimAvailable();//Show Popup
        for (int i = 0; i < datas.Count; i++) {
            datas[i].Unassign();
        }
        Initialize(day, year);
        ResetData();
        return true;
    }
    private bool CanReset(int day, int year) {
        if (year < yearEndSeason)
            return false;
        if (year == yearEndSeason && day < dayEndSeason)
            return false;
        return true;
    }
    public void ResetData() {
        progress = 0;
        foreach (var item in datas) {
            if (item != null) {
                item.ResetData();
            }
        }
    }

    public bool Claimable() {
        foreach (var item in datas) {
            if (item.FreeClaimable || item.PurchaseClaimable)
                return true;
        }
        return false;
    }
    public bool Claimable(int index, bool isFree) {
        if (progress <= index)
            return false;
        return isFree ? datas[index].FreeClaimable : datas[index].PurchaseClaimable;
    }
    public void ClaimAvailable() {
        List<ItemClaim> reward = new List<ItemClaim>();
        foreach (var item in datas) {
            var free = item.ClaimFreeReward();
            var purchase = item.ClaimPurchaseReward();
            if (free != null)
                reward.Add(free.FreeReward);
            if (purchase != null)
                reward.Add(purchase.PurchaseReward);
        }
        if (reward.Count != 0)
            if (PopupHUD.HasInstance)
                PopupHUD.Instance.Show<RewardPopup>().UpdateClaimUI(reward);
            else
                SpecialTriggerSystem.Instance.AddOnEnd(() => {
                    PopupHUD.Instance.Show<RewardPopup>().UpdateClaimUI(reward);
                });
    }
    public void Upgrade() {
        if (progress > Count - 1)
            return;
        if (Ratio() <= 1)
            return;
        if (progress < Count) {
            datas[progress].Unassign();
            datas[progress].SetComplete(true);
        }
        progress++;
        if (progress < Count)
            datas[progress].Assign();
        GameResources.Instance.Inventory.GetItem(ConstantItemID.BattlePassProgressId).Amount = 0;
    }
    public string GetDescription() {
        if (progress >= datas.Count)
            return "Done";
        return $"{datas[progress].Description} {GameResources.Instance.Inventory.GetItem(ConstantItemID.BattlePassProgressId).Amount}/{datas[progress].PointTarget}";
    }
    public float Ratio() {
        if (progress >= datas.Count)
            return 1;
        return (float)GameResources.Instance.Inventory.GetItem(ConstantItemID.BattlePassProgressId).Amount / (float)datas[progress].PointTarget;
    }
    #region Save Load Data
    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            Initialize(DateTime.Now.DayOfYear, DateTime.Now.Year);
            return;
        }
        dayEndSeason = saveData.DayEndSeason;
        yearEndSeason = saveData.YearEndSeason;
        isComplete = saveData.IsComplete;
        isPurchase = saveData.IsPurchase;
        progress = saveData.Progress;
        var itemDatas = saveData.BattlePassItemData;
        if (itemDatas == null || itemDatas.Length == 0) {
            BattlePassItemSaveData temp = new BattlePassItemSaveData() { IsComplete = false, FreeClaimd = false, PurchaseClaimed = false };
            for (int i = 0; i < datas.Count; i++) {
                datas[i].LoadData(temp);
            }
        }
        else {
            for (int i = 0; i < datas.Count; i++) {
                datas[i].LoadData(itemDatas[i]);
            }
        }
        if (progress < Count)
            datas[progress].Assign();
        IsReset(DateTime.Now.DayOfYear, DateTime.Now.Year);
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.DayEndSeason = dayEndSeason;
        saveData.YearEndSeason = yearEndSeason;
        saveData.IsComplete = isComplete;
        saveData.IsPurchase = isPurchase;
        saveData.Progress = progress;
        if (saveData.BattlePassItemData == null || saveData.BattlePassItemData.Length == 0)
            saveData.BattlePassItemData = new BattlePassItemSaveData[datas.Count];
        for (int i = 0; i < datas.Count; i++) {
            saveData.BattlePassItemData[i] = datas[i].SaveData();
        }
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            Initialize(DateTime.Now.DayOfYear, DateTime.Now.Year);
        }
        else {
            dayEndSeason = json[JsonKey.Day].AsInt;
            yearEndSeason = json[JsonKey.Year].AsInt;
            isComplete = json[JsonKey.IsCompleted].AsBool;
            isPurchase = json[JsonKey.IsPurchase].AsBool;
            progress = json[JsonKey.Progress].AsInt;

            JSONArray itemSave = json[JsonKey.Items].AsArray;
            if (itemSave == null || itemSave.Count == 0) {
                JSONNode node = new JSONObject();
                node.Add(JsonKey.IsCompleted, false);
                node.Add(JsonKey.FreeClaimd, false);
                node.Add(JsonKey.PurchaseClaimed, false);
                for (int i = 0; i < datas.Count; i++) {
                    datas[i].LoadFJson(node);
                }
            }
            else {
                for (int i = 0; i < datas.Count; i++) {
                    datas[i].LoadFJson(itemSave[i]);
                }
            }

            if (progress < Count)
                datas[progress].Assign();
            IsReset(DateTime.Now.DayOfYear, DateTime.Now.Year);
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.Day, dayEndSeason);
        node.Add(JsonKey.Year, yearEndSeason);
        node.Add(JsonKey.IsCompleted, isComplete);
        node.Add(JsonKey.IsPurchase, isPurchase);
        node.Add(JsonKey.Progress, progress);

        JSONNode itemSaveNode = new JSONArray();
        for (int i = 0; i < datas.Count; i++) {
            itemSaveNode.Add(datas[i].Save2Json());
        }
        node.Add(JsonKey.Items, itemSaveNode);

        return node;
    }

    [Serializable]
    public class SaveData {
        [SerializeField] private int des;
        [SerializeField] private int yes;
        [SerializeField] private bool ic;
        [SerializeField] private bool ip;
        [SerializeField] private int p;
        [SerializeField] private BattlePassItemSaveData[] bpis;

        public int DayEndSeason { get => des; set => des = value; }
        public int YearEndSeason { get => yes; set => yes = value; }
        public int Progress { get => p; set => p = value; }
        public bool IsComplete { get => ic; set => ic = value; }
        public bool IsPurchase { get => ip; set => ip = value; }
        public BattlePassItemSaveData[] BattlePassItemData { get => bpis; set => bpis = value; }
    }
    #endregion
}

[Serializable]
public class BattlePassItemSaveData {
    [SerializeField] private bool ic;
    [SerializeField] private bool fc;
    [SerializeField] private bool pc;

    public bool IsComplete { get => ic; set => ic = value; }
    public bool FreeClaimd { get => fc; set => fc = value; }
    public bool PurchaseClaimed { get => pc; set => pc = value; }
}