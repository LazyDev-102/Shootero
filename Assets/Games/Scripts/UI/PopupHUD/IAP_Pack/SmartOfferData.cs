using SimpleJSON;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Purchasing;

[System.Serializable]
public class SmartOfferData {
    [SerializeField] private bool active;
    [SerializeField] private int timeLimit;
    [SerializeField] private int timeOff;
    [SerializeField] private long timeStart;
    [SerializeField] private long nextTimeAppear;
    [SerializeField] private bool bought;
    [SerializeField] private List<SmartOfferInfo> packs;
    private SmartOfferType offerType;
    private bool appearing;
    private double cTime { get => DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds; }

    public int TimeLimit { get => timeLimit; }
    public List<SmartOfferInfo> Packs { get => packs; }
    public SmartOfferType OfferType { get => offerType; }

    public void Initialize(bool status) {
        active = status;
        nextTimeAppear = status ? (long)cTime + Constant.DayToSecond * timeOff : 0;
    }
    public bool Active() {
        if (cTime < nextTimeAppear && cTime > timeStart + Constant.DayToSecond) {
            active = false;
            bought = false;
            return false;
        }
        if (cTime > nextTimeAppear) {
            bought = false;
            active = false;
        }
        if (bought)
            return false;
        if (active)
            return true;
        var item = GameResources.Instance.GearInventory.GearEquipCanCombo();
        if (item != null) {
            if (item.CurrentRank > 0) {
                offerType = (SmartOfferType)(item.CurrentRank - 1);
                var pack = GetOfferData();
                if (pack != null) {
                    if (!active)
                        pack.Reward.Id = item.Id;
                    timeStart = (long)cTime;
                    Initialize(true);
                }
                else {
                    Initialize(false);
                    return false;
                };
                return true;
            }
        }
        return false;
    }
    public bool CanSpecialTrigger() {
        if (cTime > timeStart + timeLimit * Constant.DayToSecond)
            return false;
        if (active && appearing) {
            appearing = false;
            return true;
        }
        return false;
    }
    public void SetAppearing() {
        appearing = true;
    }
    public double GetTimeRemain() {
        return timeStart + Constant.DayToSecond * timeLimit - cTime;
    }
    public void ClaimReward() {
        var pack = GetOfferData();
        pack.ClaimReward();
        bought = true;
        active = false;
        nextTimeAppear = (long)cTime + Constant.DayToSecond * timeOff;
    }
    public SmartOfferInfo GetOfferData() {
        if (packs == null)
            return null;
        return packs.Find(x => x.Type == offerType);
    }

    #region Save Load Data
    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            Initialize(false);
            return;
        }
        nextTimeAppear = saveData.TimeNextAppear;
        timeStart = saveData.TimeStart;
        active = saveData.Active;
        bought = saveData.Bought;
        offerType = (SmartOfferType)saveData.OfferType;
        if (saveData.Reward == null || saveData.Reward.Length == 0)
            return;
        for (int i = 0; i < packs.Count; i++) {
            packs[i].Reward.Id = saveData.Reward[i];
        }
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.TimeNextAppear = nextTimeAppear;
        saveData.TimeStart = timeStart;
        saveData.Active = active;
        saveData.Bought = bought;
        saveData.OfferType = (int)offerType;
        saveData.Reward = new int[3];
        for (int i = 0; i < saveData.Reward.Length; i++) {
            saveData.Reward[i] = packs[i].Reward.Id;
        }
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            Initialize(false);
        }
        else {
            nextTimeAppear = json[JsonKey.TimeNextFree].AsLong;
            timeStart = json[JsonKey.TimeStart].AsLong;
            active = json[JsonKey.Active].AsBool;
            bought = json[JsonKey.IsBoughtGems].AsBool;
            offerType = (SmartOfferType)json[JsonKey.Type].AsInt;

            JSONArray rewardNode = json[JsonKey.Reward].AsArray;
            for (int i = 0; i < rewardNode.Count; i++) {
                packs[i].Reward.Id = rewardNode[i].AsInt;
            }
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.TimeNextFree, nextTimeAppear);
        node.Add(JsonKey.TimeStart, timeStart);
        node.Add(JsonKey.Active, active);
        node.Add(JsonKey.IsBoughtGems, bought);
        node.Add(JsonKey.Type, (int)offerType);

        JSONNode rewardNode = new JSONArray();
        for (int i = 0; i < 3; i++) {
            rewardNode.Add(packs[i].Reward.Id);
        }
        node.Add(JsonKey.Reward, rewardNode);
        return node;
    }
    [System.Serializable]
    public class SaveData {
        [SerializeField] private long tna;
        [SerializeField] private long ts;
        [SerializeField] private bool at;
        [SerializeField] private bool b;
        [SerializeField] private int t;
        [SerializeField] private int[] ri;

        public long TimeNextAppear { get => tna; set => tna = value; }
        public long TimeStart { get => ts; set => ts = value; }
        public bool Active { get => at; set => at = value; }
        public bool Bought { get => b; set => b = value; }
        public int[] Reward { get => ri; set => ri = value; }
        public int OfferType { get => t; set => t = value; }
    }
    #endregion
}

[Serializable]
public class SmartOfferInfo {
    [SerializeField] private SmartOfferType type;
    [SerializeField] private string originPrice;
    [SerializeField] private float saleOffValue;
    [SerializeField] private string iapKey;
    [SerializeField] private float defaulIap;
    [SerializeField] private ProductType productType;
    [SerializeField] private ItemClaim reward;
    [SerializeField] private int rank;
    [SerializeField] private Sprite frameIcon;

    public SmartOfferType Type { get => type; }
    public string OriginPrice { get => originPrice; }
    public string IapKey { get => iapKey; }
    public float SaleOffValue { get => saleOffValue; }
    public float DefaulIap { get => defaulIap; }
    public ProductType ProductType { get => productType; }
    public ItemClaim Reward { get => reward; }
    public int Rank { get => rank; }
    public Sprite FrameIcon { get => frameIcon; }

    public void AsignReward(int id, int rank) {
        reward.Id = id;
        this.rank = rank;
    }
    public void ClaimReward() {
        GearClaimExtentions.Claim(reward.Id, rank);
    }
}