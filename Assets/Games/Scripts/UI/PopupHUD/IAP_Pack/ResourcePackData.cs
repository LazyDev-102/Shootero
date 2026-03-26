using Gear_Data;
using SimpleJSON;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

[System.Serializable]
public class ResourcePackData {
    [SerializeField] private int dayCheckin;
    [SerializeField] private int yearCheckin;
    [SerializeField] private string originPrice;
    [SerializeField] private float saleOffValue;
    [SerializeField] private string iapKey;
    [SerializeField] private float defaulIap;
    [SerializeField] private ProductType productType;
    [SerializeField] private List<ItemClaim> rewards;
    private bool appearable;
    private double cTime { get => DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds; }

    public List<ItemClaim> Rewards { get => rewards; }
    public string OriginPrice { get => originPrice; }
    public string IapKey { get => iapKey; }
    public float SaleOffValue { get => saleOffValue; }
    public float DefaulIap { get => defaulIap; }
    public ProductType ProductType { get => productType; }
    public void Initialize() {
        dayCheckin = 0;
        yearCheckin = 0;
    }
    public bool CanSpecialTrigger() {
        var year = DateTime.Now.Year;
        var day = DateTime.Now.DayOfYear;
        if (yearCheckin > year)
            return false;
        if (yearCheckin == year && dayCheckin >= day)
            return false;
        if (!appearable)
            return false;
        dayCheckin = day;
        yearCheckin = year;
        appearable = false;
        return true;
    }
    public void SetAppear(GearSlotData gearSlot) {
        appearable = true;
        AssignReward(gearSlot);
    }
    public void AssignReward(GearSlotData gearSelect = null) {
        if (gearSelect != null) {
            rewards[2].Id = gearSelect.MaterialID;
            return;
        }
        var gearslot = GetGearSlotTopLevel();
        rewards[2].Id = gearslot.MaterialID;
    }
    public void ClaimReward() {
        for (int i = 0; i < rewards.Count; i++) {
            rewards[i].Claim();
        }
    }
    private GearSlotData GetGearSlotTopLevel() {
        var inv = GameResources.Instance.GearInventory;
        var max = Mathf.Max(inv.WeaponrySlot.CurrentLevel, inv.ShieldSlot.CurrentLevel, inv.CoreSlot.CurrentLevel, inv.EngineSlot.CurrentLevel, inv.DroneLSlot.CurrentLevel, inv.DroneRSlot.CurrentLevel);
        if (inv.WeaponrySlot.CurrentLevel == max)
            return inv.WeaponrySlot;
        if (inv.ShieldSlot.CurrentLevel == max)
            return inv.WeaponrySlot;
        if (inv.CoreSlot.CurrentLevel == max)
            return inv.WeaponrySlot;
        if (inv.EngineSlot.CurrentLevel == max)
            return inv.WeaponrySlot;
        if (inv.DroneLSlot.CurrentLevel == max)
            return inv.WeaponrySlot;
        if (inv.DroneRSlot.CurrentLevel == max)
            return inv.WeaponrySlot;
        return inv.WeaponrySlot;
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
        dayCheckin = saveData.CheckinDay;
        yearCheckin = saveData.CheckinYear;
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.CheckinDay = dayCheckin;
        saveData.CheckinYear = yearCheckin;
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            Initialize();
        }
        else {
            dayCheckin = json[JsonKey.Day].AsInt;
            yearCheckin = json[JsonKey.Year].AsInt;
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.Day, dayCheckin);
        node.Add(JsonKey.Year, yearCheckin);
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private int d;
        [SerializeField] private int y;
        [SerializeField] private bool ap;

        public bool Appeared { get => ap; set => ap = value; }
        public int CheckinDay { get => d; set => d = value; }
        public int CheckinYear { get => y; set => y = value; }
    }
    #endregion
}
