

using Helper;
using SimpleJSON;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ChipPackItem", menuName = "Resource/Item/Packs/ChipPackItem")]
public class ChipPackItem : Item, ISaveLoadable {
    [SerializeField] private ShopButton shopButtonKey;
    [SerializeField] private bool isFree;
    [SerializeField] private int maxTurn;
    [SerializeField] private int remainTurn;
    [SerializeField] private double timeNextFree;
    [SerializeField] private int timePerTurn;
    [SerializeField] private float chipAfkPoint;
    [SerializeField] private ItemClaim[] itemClaims;

    public ShopButton ShopButtonKey { get => shopButtonKey; }
    public bool IsFree { get => isFree; }
    public int MaxTurn { get => maxTurn; }
    public int RemainTurn { get => remainTurn; }
    public double TimeNextFree { get => timeNextFree; }
    public int TimePerTurn { get => timePerTurn; }
    public ItemClaim[] ItemClaims { get => itemClaims; }
    public bool HasReward { get => RemainTurn > 0; }
    private double cTime { get => DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds; }

    public override void Claim(int amount) {
        if (isFree) {
            if (remainTurn <= 0)
                return;
            remainTurn--;
            timeNextFree = cTime + timePerTurn;
        }
        foreach (var item in itemClaims) {
            item.Claim();
        }
        Save();
    }
    private void Save() {
        if (!isFree)
            return;
        GameResources.Instance.ShopData.Chips.Save(DateTime.Now.DayOfYear, DateTime.Now.Year);
    }
    public bool Claimable() {
        if (remainTurn <= 0)
            return false;
        return cTime > timeNextFree;
    }
    public void AssignReward() {
        if (itemClaims == null || itemClaims.Length == 0)
            return;
        GameResources.Instance.RefreshChipMaterialPerHour();
        foreach (var item in itemClaims) {
            if (item.Id == ConstantItemID.ChipId) {
                var value = (GameResources.Instance.ChipPerSecond * Constant.HourToSecond * chipAfkPoint).ConvertToInt();
                if (value < 1)
                    value = 1;
                item.Amount = value;
            }
        }
    }
    public void ResetData(bool status = true) {
        if (!status)
            return;
        remainTurn = maxTurn;
        timeNextFree = cTime;
        AssignReward();
    }

    public void LoadFromJson(string json) {
        Gemmob.EventDispatcher.Instance.AddListener(EventKey.OnLevelSystemUp, AssignReward);
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        if (saveData == null) {
            ResetData();
            return;
        }
        remainTurn = saveData.RemainTurn;
        timeNextFree = saveData.TimeNextFree;
        AssignReward();
    }

    public string SaveToJson() {
        Gemmob.EventDispatcher.Instance.RemoveListener(EventKey.OnLevelSystemUp, AssignReward);
        SaveData saveData = new SaveData();
        saveData.RemainTurn = remainTurn;
        saveData.TimeNextFree = timeNextFree;
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        Gemmob.EventDispatcher.Instance.AddListener(EventKey.OnLevelSystemUp, AssignReward);

        if (json == null || json.ToString() == "{}") {
            ResetData();
        }
        else {
            remainTurn = json[JsonKey.CurrentRemain].AsInt;
            timeNextFree = json[JsonKey.TimeNextFree].AsDouble;
            AssignReward();
        }
    }

    public JSONNode Save2Json() {
        Gemmob.EventDispatcher.Instance.RemoveListener(EventKey.OnLevelSystemUp, AssignReward);

        JSONNode node = new JSONObject();
        node.Add(JsonKey.CurrentRemain, remainTurn);
        node.Add(JsonKey.TimeNextFree, timeNextFree);
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private int rt;
        [SerializeField] private double tnd;
        public int RemainTurn { get => rt; set => rt = value; }
        public double TimeNextFree { get => tnd; set => tnd = value; }
    }
}
