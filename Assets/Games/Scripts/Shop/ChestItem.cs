using UnityEngine;
using Helper;
using System;
using Gear_Data;
using Gemmob;
using SimpleJSON;

[CreateAssetMenu(fileName = "ChestItem", menuName = "Resource/Item/Obstacles/ChestItem")]
public class ChestItem : Item, ISaveLoadable {
    [SerializeField] private ItemStack nextPrice;
    [SerializeField] private int numberSpecialOpen;
    [SerializeField] private string specialDescription;
    [SerializeField] private string specialTypeDescription;
    [SerializeField] private RankGearProbability[] normalData;
    [SerializeField] private RankGearProbability[] specialData;
    [SerializeField] private ItemStack keyOpen;
    [SerializeField] private int timeGetFree; //hour
    [SerializeField] private bool isOpenFreeWithAds;
    [SerializeField] private ItemCollector gearCollector;
    [SerializeField] private string skinName;
    [SerializeField] private bool showEffect;

    public int SpecialOpenCountdown { get; set; }
    public ItemStack NextPrice { get => nextPrice; }
    public RankGearProbability[] NormalData { get => normalData; }
    public RankGearProbability[] SpecialData { get => specialData; }
    public ItemStack KeyOpen { get => keyOpen; }
    public int TimeGetFree { get => timeGetFree; }
    public bool IsOpenFreeWithAds { get => isOpenFreeWithAds; }
    public string SkinName { get => skinName; }
    public bool ShowEffect { get => showEffect; }



    //
    public int CurrentNumberSpectialOpen { get; set; }
    public DateTime GetFreeTimeReady { get; set; }
    public string GetSpecialDescription {
        get {
            return $"{specialDescription} <b><size=30><color=yellow>{CurrentNumberSpectialOpen}</color></size></b> times";
        }
    }
    public string GetSpecialTypeDescription {
        get {
            return $"{specialTypeDescription} ";
        }
    }

    public bool IsGetFreeReady() {
        return DateTime.Now.CompareTo(GetFreeTimeReady) >= 0;
    }

    public void AddKey(int number) {
        GameResources.Instance.Inventory.Add(keyOpen.Id, number);
    }

    public GearSoftData OpenChest() {
        var isTut = !GameResources.Instance.TutorialSytemData.FinishTutorialEquipment;
        RankGearProbability[] probability = normalData;
        if (CurrentNumberSpectialOpen == 1) {
            probability = specialData;
            CurrentNumberSpectialOpen = numberSpecialOpen;
        }
        else {
            CurrentNumberSpectialOpen--;
        }
        int rankRandom = isTut ? 1 : RandomHelper.RandomWithPercent(probability).Rank;
        Item itemRandom = isTut ? RandomHelper.RandomInCollection(20, gearCollector.Items) : RandomHelper.RandomInCollection(gearCollector.Items);
        GearSoftData newSoftGear = null;
        if (itemRandom is GearHardData gear) {
            newSoftGear = gear.AddNewGear(rankRandom);
        }
        EventDispatcher.Instance.Dispatch<EventKey.OnOpenChest>(new EventKey.OnOpenChest() {
            newGear = newSoftGear
        });
        return newSoftGear;
    }

    public void ResetFreeOpen() {
        GetFreeTimeReady = DateTime.Now.AddHours(TimeGetFree);
    }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            CurrentNumberSpectialOpen = numberSpecialOpen;
            GetFreeTimeReady = DateTime.Now.AddHours(TimeGetFree);
            return;
        }
        CurrentNumberSpectialOpen = saveData.CurrentNumberSpecial;
        GetFreeTimeReady = saveData.GetFreeTimeReady;
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.CurrentNumberSpecial = CurrentNumberSpectialOpen;
        saveData.GetFreeTimeReady = GetFreeTimeReady;
        return JsonUtility.ToJson(saveData);
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            CurrentNumberSpectialOpen = numberSpecialOpen;
            GetFreeTimeReady = DateTime.Now.AddHours(TimeGetFree);
        }
        else {
            CurrentNumberSpectialOpen = json[JsonKey.CurrentRemain].AsInt;
            GetFreeTimeReady = json[JsonKey.TimeNextFree].AsDateTime;
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.CurrentRemain, CurrentNumberSpectialOpen);
        node.Add(JsonKey.TimeNextFree, GetFreeTimeReady);
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private int cns;
        [SerializeField] private JsonDateTime ftr;
        public int CurrentNumberSpecial { get => cns; set => cns = value; }
        public DateTime GetFreeTimeReady { get => ftr; set => ftr = value; }
    }

    [System.Serializable]
    public class RankGearProbability : IPercentable {
        [SerializeField] private int rank;
        [SerializeField] private int probability;

        public int Rank { get => rank; }

        public int GetPercent() {
            return probability;
        }
    }
}
