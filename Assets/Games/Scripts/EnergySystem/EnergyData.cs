using UnityEngine;
using System;
using Helper;
using SimpleJSON;

[CreateAssetMenu(fileName = "EnergyData", menuName = "Resource/HardData/Energy/EnergyData")]
public class EnergyData : ScriptableObject, ISaveLoadable {
    private readonly int energyNeedToReload = 600;

    [SerializeField] private BuyEnergyData gemBuy;
    [SerializeField] private BuyEnergyData adsBuy;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int startCountAt;
    [SerializeField] private double oldTimeQuit;
    [SerializeField] private ItemStack energyNeedToPlay;


    public bool IsEnergyRegen { get; set; }
    public BuyEnergyData GemBuy { get => gemBuy; }
    public BuyEnergyData AdsBuy { get => adsBuy; }
    public int EnergyNeedToReload { get => energyNeedToReload; }
    public ItemStack EnergyNeedToPlay { get => energyNeedToPlay; }
    public int StartCountAt { get => startCountAt; set => startCountAt = value; }
    private double cTime => DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
    public bool EnoughEnergyToPlay {
        get => GameResources.Instance.Inventory.GetItem(energyNeedToPlay.Id).Amount >= energyNeedToPlay.Amount;
    }

    public int GetMaxEnergy() {
        return maxEnergy + PlayerStatManager.Instance.MaxEnergy;
    }
    public void ResetAllRemain() {
        gemBuy.ResetRemain();
        adsBuy.ResetRemain();
    }
    public bool CanCoundown() {
        return GetMaxEnergy() > GameResources.Instance.Inventory.GetItem(ConstantItemID.EnergyId).Amount;
    }
    public void GiveEnergyOffline() {
        var inv = GameResources.Instance.Inventory;
        int delta = (int)(cTime - oldTimeQuit);
        var maxEnergy = GetMaxEnergy();
        var cValue = inv.GetItem(ConstantItemID.EnergyId).Amount;
        if (cValue >= maxEnergy)
            return;
        if (delta < 0)
            delta = 0;
        int result = startCountAt - delta;
        if (result > 0) {
            startCountAt = result;
        }
        else {
            result *= -1;
            int valueUp = (result + energyNeedToReload) / energyNeedToReload;
            startCountAt = energyNeedToReload - result % energyNeedToReload;
            if (valueUp > maxEnergy || valueUp + cValue > maxEnergy) {
                valueUp = maxEnergy - cValue;
            }
            inv.Add(ConstantItemID.EnergyId, valueUp);
        }
    }
    public void GetCoundownReward() {
        GameResources.Instance.Inventory.Add(ConstantItemID.EnergyId, 1);
    }
    public void SaveQuitTime() {
        oldTimeQuit = cTime;
    }

    #region SaveLoad
    public void LoadFromJson(string json) {
        SaveDataModel saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveDataModel>(json);
        }
        if (saveData == null) {
            saveData = new SaveDataModel();
            saveData.IsEnergyRegen = false;
            oldTimeQuit = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
            saveData.StartCountAt = energyNeedToReload - 1;
        }

        gemBuy.LoadFromJson(saveData.GemBuy);
        adsBuy.LoadFromJson(saveData.AdsBuy);
        IsEnergyRegen = saveData.IsEnergyRegen;
        oldTimeQuit = saveData.OldTimeQuit;
        startCountAt = saveData.StartCountAt;
        if (oldTimeQuit == 0)
            oldTimeQuit = cTime;
        GiveEnergyOffline();
    }
    public string SaveToJson() {
        SaveDataModel saveData = new SaveDataModel();
        oldTimeQuit = cTime;
        saveData.GemBuy = gemBuy.SaveToJson();
        saveData.AdsBuy = adsBuy.SaveToJson();
        saveData.IsEnergyRegen = IsEnergyRegen;
        saveData.OldTimeQuit = oldTimeQuit;
        saveData.StartCountAt = startCountAt;
        return JsonUtility.ToJson(saveData);
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            IsEnergyRegen = false;
            oldTimeQuit = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
            startCountAt = energyNeedToReload - 1;
        }
        else {
            gemBuy.CurrentRemain = json[JsonKey.GemBuy].AsInt;
            adsBuy.CurrentRemain = json[JsonKey.AdsBuy].AsInt;
            IsEnergyRegen = json[JsonKey.IsEnergyRegen].AsBool;
            oldTimeQuit = json[JsonKey.OldTimeQuit].AsDouble;
            startCountAt = json[JsonKey.StartCountAt].AsInt;
            if (oldTimeQuit == 0)
                oldTimeQuit = cTime;
            GiveEnergyOffline();
        }
    }
    public JSONNode Save2Json() {
        oldTimeQuit = cTime;
        JSONNode node = new JSONObject();
        node.Add(JsonKey.GemBuy, gemBuy.CurrentRemain);
        node.Add(JsonKey.AdsBuy, adsBuy.CurrentRemain);
        node.Add(JsonKey.IsEnergyRegen, IsEnergyRegen);
        node.Add(JsonKey.OldTimeQuit, oldTimeQuit);
        node.Add(JsonKey.StartCountAt, StartCountAt);
        return node;
    }

    [Serializable]
    public class SaveDataModel {
        [SerializeField] private string gb;
        [SerializeField] private string ab;
        [SerializeField] private bool ier;
        [SerializeField] private double otq;
        [SerializeField] private int sca;

        public string GemBuy { get => gb; set => gb = value; }
        public string AdsBuy { get => ab; set => ab = value; }
        public bool IsEnergyRegen { get => ier; set => ier = value; }
        public double OldTimeQuit { get => otq; set => otq = value; }
        public int StartCountAt { get => sca; set => sca = value; }
    }
    #endregion
}

[Serializable]
public class BuyEnergyData : ISaveLoadable {
    [SerializeField] private ItemStack item;
    [SerializeField] private ItemStack price;
    [SerializeField] private bool remainRequire;
    [SerializeField] private int remainADay;


    private int currentRemain;

    public int CurrentRemain { get => currentRemain; set => currentRemain = value; }

    public ItemStack Item { get => item; }
    public int RemainADay { get => remainADay; }
    public bool RemainRequire { get => remainRequire; }
    public ItemStack Price { get => price; }

    public bool HasRemain {
        get {
            return currentRemain > 0;
        }
    }

    public void LoadFromJson(string json) {
        SaveDataModel saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveDataModel>(json);
        }
        if (saveData == null) {
            saveData = new SaveDataModel();
            saveData.CurrentRemain = remainADay;
        }

        CurrentRemain = saveData.CurrentRemain;
    }

    public string SaveToJson() {
        SaveDataModel saveData = new SaveDataModel();
        saveData.CurrentRemain = CurrentRemain;
        return JsonUtility.ToJson(saveData);
    }


    public void ResetRemain() {
        currentRemain = remainADay;
    }

    [Serializable]
    public class SaveDataModel {
        [SerializeField] private int cr;

        public int CurrentRemain {
            get => cr;
            set => cr = value;
        }
    }
}
