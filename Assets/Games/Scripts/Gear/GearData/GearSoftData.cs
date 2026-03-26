
using Gear_Data;
using Gemmob;
using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GearSoftData : IEventParams {
    [SerializeField] private int i;
    [SerializeField] private int lv;
    [SerializeField] private int r;
    [SerializeField] private List<int> ss;
    [SerializeField] private bool ie;
    [SerializeField] private GearType gt;
    [SerializeField] private bool inc;
    public int Id => i;
    public int CurrentLevel { get => lv; }
    public int CurrentRank { get => r; }
    public List<int> SecondStatIds { get => ss; }
    public bool IsEquiped { get => ie; }
    public GearType GearTypeSoft { get => gt; }
    public bool IsMaxLevel { get => GearHardData != null ? CurrentLevel >= GearHardData.Levels.Count : true; }
    public bool IsMaxRank { get => CurrentRank >= GameResources.Instance.GearData.GearRaretyData.RaretyData.Length - 1; }
    public bool IsNewChecked { get => inc; private set => inc = value; }
    public bool IsDrone { get => GearHardData != null && (GearTypeSoft == GearType.Drone1 || GearTypeSoft == GearType.Drone2); }
    public bool IsDroneL { get => GearHardData != null && GearTypeSoft == GearType.Drone1; }

    private GearHardData gearDataCache;

    public GearHardData GearHardData {
        get {
            if (gearDataCache != null) {
                return gearDataCache;
            }
            IItem item;
            ItemDatabase.TryGetItem(Id, out item);
            if (item is GearHardData gear) {
                gearDataCache = gear;
                return gear;
            }
            return null;
        }
    }

    public RaretyData CurrentRaretyData {
        get {
            return GameResources.Instance.GearData.GearRaretyData.RaretyData[CurrentRank];
        }
    }

    #region Notification
    public void CheckNew() {
        IsNewChecked = true;
    }

    public void UnCheckNew() {
        IsNewChecked = false;
    }

    #endregion

    public void Levelup() {
        if (IsMaxLevel)
            return;
        if (IsEquiped) {
            RemoveAllStat();
            lv++;
            AddAllStat();
        }
        else {
            lv++;
        }
    }

    public void Rankup() {
        if (IsMaxRank) {
            return;
        }
        if (IsDrone) {
            DroneRankUp();
        }
        else {
            if (IsEquiped) {
                RemoveAllStat();
                r++;
                AddNewSecondStat();
                AddAllStat();
            }
            else {
                r++;
                AddNewSecondStat();
            }
        }
    }
    public void DroneRankUp() {
        r++;
    }
    public void AddAllStat() {
        //GearHardData.Equip(CurrentRank, CurrentLevel - 1);

        RankStatData rankStatData = GameResources.Instance.GearData.RankStatData;
        foreach (var id in ss) {
            rankStatData.AddStat(id, CurrentRank);
        }
    }

    public void RemoveAllStat() {
        //GearHardData.Unequip(CurrentRank, CurrentLevel - 1);

        RankStatData rankStatData = GameResources.Instance.GearData.RankStatData;
        foreach (var id in ss) {
            rankStatData.RemoveStat(id, CurrentRank);
        }
    }

    private void AddNewSecondStat() {
        int newId = GameResources.Instance.GearData.RankStatData.RandomRankStat();
        ss.Add(newId);
    }

    public void SetIsEquiped(bool equip) {
        ie = equip;
    }
    public void SetGearTypeSoft(GearType type) {
        gt = type;
    }
    public GearSoftData(int id) {
        i = id;
        lv = 1;
        r = 0;
        ss = new List<int>();
        ss.Add(GearHardData.FirstRankStatData.Id);
        gt = GearHardData.GearType;
        IsNewChecked = false;
    }

    public GearSoftData(int id, int rank) {
        i = id;
        lv = 1;
        r = rank;
        gt = GearHardData.GearType;
        IsNewChecked = false;
        if (gt == GearType.Drone1 || gt == GearType.Drone2)
            return;
        ss = new List<int>();
        ss.Add(GearHardData.FirstRankStatData.Id);
        for (int i = 0; i < rank; ++i) {
            AddNewSecondStat();
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.ItemId, i);
        node.Add(JsonKey.GearType, (int)gt);

        if (lv > 1)
            node.Add(JsonKey.Level, lv);

        if (r > 0)
            node.Add(JsonKey.Rank, r);

        if (!inc)
            node.Add(JsonKey.IsNewCheck, inc);

        if (ie)
            node.Add(JsonKey.IsEquiped, ie);

        if (ss != null && ss.Count > 0) {
            JSONNode ssNode = new JSONArray();
            foreach (var item in ss) {
                ssNode.Add(item);
            }
            node.Add(JsonKey.SecondStatIds, ssNode);
        }

        return node;
    }
    public void LoadFJson(JSONNode json) {
        i = json[JsonKey.ItemId].AsInt;
        gt = (GearType)json[JsonKey.GearType].AsInt;
        lv = json[JsonKey.Level] != null ? json[JsonKey.Level].AsInt : 1;
        r = json[JsonKey.Rank] != null ? json[JsonKey.Rank].AsInt : 0;
        inc = json[JsonKey.IsNewCheck] != null ? json[JsonKey.IsNewCheck].AsBool : true;
        ie = json[JsonKey.IsEquiped] != null ? json[JsonKey.IsEquiped].AsBool : false;

        JSONArray ssNode = json[JsonKey.SecondStatIds].AsArray;
        ss = new List<int>();
        foreach (var item in ssNode.Children) {
            ss.Add(item.AsInt);
        }
    }
}
