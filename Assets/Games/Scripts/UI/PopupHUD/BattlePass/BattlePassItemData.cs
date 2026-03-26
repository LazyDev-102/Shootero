using SimpleJSON;
using GameSystem.Common.UnityInspector;
using Gemmob;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattlePassItemData", menuName = "Resource/HardData/BattlePass/BattlePassItemData")]
public class BattlePassItemData : ScriptableObject {
    [SerializeField] private int index;
    [SerializeField] private int pointTarget;
    [SerializeField] private string description;
    [SerializeField] private bool isComplete;
    [SerializeField] private bool freeClamed;
    [SerializeField] private bool purchaseClamed;
    [SerializeField] private ItemClaim freeReward;
    [SerializeField] private ItemClaim purchaseReward;
    [SerializeField, ConstantField(typeof(EventKey))] private int[] eventRegisters;

    public int PointTarget { get => pointTarget; }
    public string Description { get => description; }
    public bool FreeClamed { get => freeClamed; }
    public bool PurchaseClamed { get => purchaseClamed; }
    public bool FreeClaimable { get => !freeClamed && isComplete; }
    public bool PurchaseClaimable { get => !purchaseClamed && GameResources.Instance.BattlePass.IsPurchase && isComplete; }
    public ItemClaim FreeReward { get => freeReward; }
    public ItemClaim PurchaseReward { get => purchaseReward; }
    public int Index { get => index; }
    public bool IsComplete { get => isComplete; }

    public void Assign() {
        foreach (int eventRegister in GetEventRegisters()) {
            EventDispatcher.Instance.RemoveListener(eventRegister, Upgrade);
            EventDispatcher.Instance.AddListener(eventRegister, Upgrade);
        }
    }
    public void Unassign() {
        foreach (int eventRegister in GetEventRegisters()) {
            EventDispatcher.Instance.RemoveListener(eventRegister, Upgrade);
        }
    }
    public IEnumerable<int> GetEventRegisters() {
        return eventRegisters;
    }
    public void LoadData(BattlePassItemSaveData data) {
        SetComplete(data.IsComplete);
        freeClamed = data.FreeClaimd;
        purchaseClamed = data.PurchaseClaimed;
    }
    public void LoadFJson(JSONNode json) {
        SetComplete(json[JsonKey.IsCompleted].AsBool);
        freeClamed = json[JsonKey.FreeClaimd].AsBool;
        purchaseClamed = json[JsonKey.PurchaseClaimed].AsBool;
    }
    public BattlePassItemSaveData SaveData() {
        return new BattlePassItemSaveData() { IsComplete = isComplete, FreeClaimd = freeClamed, PurchaseClaimed = purchaseClamed };
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.IsCompleted, isComplete);
        node.Add(JsonKey.FreeClaimd, freeClamed);
        node.Add(JsonKey.PurchaseClaimed, purchaseClamed);
        return node;
    }
    private void Upgrade(object param) {
        if (GameResources.Instance.BattlePass.Progress == index) {
            int value = (int)param;
            GameResources.Instance.Inventory.Add(ConstantItemID.BattlePassProgressId, value);
            GameResources.Instance.BattlePass.Upgrade();
        }
    }
    public BattlePassItemData ClaimFreeReward() {
        if (freeClamed || !isComplete)
            return null;
        freeReward.Claim();
        freeClamed = true;
        return this;
    }
    public BattlePassItemData ClaimPurchaseReward() {
        if (!GameResources.Instance.BattlePass.IsPurchase || purchaseClamed || !isComplete)
            return null;
        purchaseReward.Claim();
        purchaseClamed = true;
        return this;
    }
    public void ClaimReward() {
        ClaimFreeReward();
        ClaimPurchaseReward();
    }
    public void SetComplete(bool status) {
        isComplete = status;
    }
    public virtual void ResetData() {
        SetComplete(false);
        freeClamed = false;
        purchaseClamed = false;
    }
}
