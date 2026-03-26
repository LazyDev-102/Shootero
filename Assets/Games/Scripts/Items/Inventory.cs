using DG.Tweening;
using GameSystem.Common.UnityInspector;
using Gemmob;
using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Resource/Item/Inventory", order = 1)]
public class Inventory : ScriptableObject, ISaveLoadable {

    [System.Serializable]
    private class SaveDataModel {
        public ItemSlot[] i;

        public SaveDataModel(ItemSlot[] items) {
            this.i = items;
        }

        public SaveDataModel(int capacity) {
            i = new ItemSlot[capacity];
        }
    }

    private readonly Dictionary<int, ItemSlot> itemDictionary = new Dictionary<int, ItemSlot>();
    [ItemField] private List<int> infiniteItemIds = new List<int>();

    public IEnumerable<ItemStack> GetAllItem() {
        return itemDictionary.Values;
    }

    public void AddInitialize(params ItemStack[] items) {
        foreach (ItemStack item in items) {
            if (ItemDatabase.Constains(item.Id)) {
                int amount = item.Amount;
                if (itemDictionary.ContainsKey(item.Id)) {
                    itemDictionary[item.Id].Stack(amount);
                }
                else {
                    itemDictionary.Add(item.Id, new ItemSlot(item.Id, amount));
                }
            }
        }
    }

    public virtual void Add(params ItemStack[] items) {
        foreach (ItemStack item in items) {
            Add(item.Id, item.Amount);
        }
    }

    public virtual void Add(int id, int amount) {
        if (ItemDatabase.Constains(id)) {
            if (itemDictionary.ContainsKey(id)) {
                itemDictionary[id].Stack(amount);
            }
            else {
                itemDictionary.Add(id, new ItemSlot(id, amount));
            }
            DispatcherEvent(id, amount);
        }
    }

    public virtual void Remove(params ItemStack[] items) {
        foreach (ItemStack item in items) {
            Remove(item.Id, item.Amount);
        }
    }

    public virtual void Remove(int id, int amount) {
        if (itemDictionary.ContainsKey(id)) {
            itemDictionary[id].Destack(amount);
            DispatcherEvent(id, amount);
        }
    }
    private void DispatcherEvent(int id, int amount) {
        EventDispatcher.Instance.Dispatch(EventKey.OnInventoryChanged);
        if (id == ConstantItemID.ChipId)
            EventDispatcher.Instance.Dispatch(EventKey.OnChipChanged);
        else if (id == ConstantItemID.GemId)
            EventDispatcher.Instance.Dispatch(EventKey.OnGemChanged);
        else if (id == ConstantItemID.EnergyId)
            EventDispatcher.Instance.Dispatch(EventKey.OnEnergyChanged);
        else if (id == ConstantItemID.HTicket)
            EventDispatcher.Instance.Dispatch(EventKey.OnHTicketChanged);
        else if (id == ConstantItemID.HCandy)
            EventDispatcher.Instance.Dispatch(EventKey.OnHCandyChanged);
        else if (id == ConstantItemID.XTicket)
            EventDispatcher.Instance.Dispatch(EventKey.OnXTicketChanged);
        else if (id == ConstantItemID.XCandy)
            EventDispatcher.Instance.Dispatch(EventKey.OnXCandyChanged);
        else if (id <= ConstantItemID.RandomMatId && id >= ConstantItemID.WeaponryMatId)
            EventDispatcher.Instance.Dispatch(EventKey.OnMaterialChanged);
        if (id != ConstantItemID.ChipId && id != ConstantItemID.GemId)
            return;
        Tracking.Instance.TrackingCurrency(id, amount);
    }
    public virtual ItemStack GetItem(int id) {
        if (IsInfinite(id)) {
            return new ItemStack(id, int.MaxValue);
        }

        if (itemDictionary.TryGetValue(id, out ItemSlot item)) {
            return item;
        }

        return new ItemStack(id, 0);
    }

    public void AddHCandy(int amount) {
        Add(ConstantItemID.HCandy, amount);
        EventDispatcher.Instance.Dispatch(EventKey.OnHCandyChanged);
    }
    public ItemStack GetHCandy() {
        return GetItem(ConstantItemID.HCandy);
    }
    public void AddHTicket(int amount) {
        Add(ConstantItemID.HTicket, amount);
        EventDispatcher.Instance.Dispatch(EventKey.OnHTicketChanged);
    }
    public ItemStack GetHTicket() {
        return GetItem(ConstantItemID.HTicket);
    }
    public void AddXCandy(int amount) {
        Add(ConstantItemID.XCandy, amount);
        EventDispatcher.Instance.Dispatch(EventKey.OnXCandyChanged);
    }
    public ItemStack GetXCandy() {
        return GetItem(ConstantItemID.XCandy);
    }
    public void AddXTicket(int amount) {
        Add(ConstantItemID.XTicket, amount);
        EventDispatcher.Instance.Dispatch(EventKey.OnXTicketChanged);
    }
    public ItemStack GetXTicket() {
        return GetItem(ConstantItemID.XTicket);
    }

    public bool EnoughPrice(ItemStack price) {
        ItemStack ownItem = GetItem(price.Id);
        return ownItem.Amount >= price.Amount;
    }
    public void EnoughPrice(ItemStack price, System.Action onEnough, System.Action onFail) {
        ItemStack ownItem = GetItem(price.Id);
        if (ownItem.Amount >= price.Amount) {
            Remove(price.Id, price.Amount);
            onEnough?.Invoke();
        }
        else {
            onFail?.Invoke();
        }
    }
    public string SaveToJson() {
        if (itemDictionary == null) {
            return null;
        }

        SaveDataModel saveData = new SaveDataModel(itemDictionary.Count);

        int index = 0;
        foreach (int id in itemDictionary.Keys) {
            saveData.i[index] = itemDictionary[id];
            index++;
        }
        return JsonUtility.ToJson(saveData);
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONArray();
        foreach (var item in itemDictionary.Values) {
            node.Add(item.Save2Json());
        }
        return node;
    }

    public void LoadFromJson(string json) {
        SaveDataModel saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveDataModel>(json);
        }

        if (saveData == null) {
#if DEBUG_ENABLE
            OnDebugEnable();
            GameResourcesIG.Instance.LevelProgress.Datas.SetCurrentLevel(70);
#endif
            return;
        }

        foreach (ItemSlot item in saveData.i) {
            AddInitialize(item);
        }
    }
    public void LoadFJson(JSONArray json) {
        if (json == null || json.Count <= 0) {
#if DEBUG_ENABLE
            OnDebugEnable();
            GameResourcesIG.Instance.LevelProgress.Datas.SetCurrentLevel(70);
#endif
        }
        else {
            foreach (JSONNode itemNode in json.Children) {
                ItemSlot newItem = new ItemSlot(itemNode[JsonKey.ItemId].AsInt, itemNode[JsonKey.Amount].AsInt);
                AddInitialize(newItem);
            }
        }
    }
    public void Reload() {
        itemDictionary.Clear();
    }
    private void OnDebugEnable() {
        DOVirtual.DelayedCall(5, () => {
            for (int i = 0; i < 5; i++) {
                for (int j = 2001; j < 2006; j++) {
                    GameResources.Instance.GearInventory.Add(j, i);
                }
                for (int k = 2101; k < 2106; k++) {
                    GameResources.Instance.GearInventory.Add(k, i);
                }
                for (int g = 2201; g < 2206; g++) {
                    GameResources.Instance.GearInventory.Add(g, i);
                }
                for (int h = 2301; h < 2306; h++) {
                    GameResources.Instance.GearInventory.Add(h, i);
                }
                for (int f = 2401; f < 2405; f++) {
                    GameResources.Instance.GearInventory.Add(f, i);
                }
            }
        });

        GameResources.Instance.Inventory.Add(ConstantItemID.ChipId, 9700000);
        GameResources.Instance.Inventory.Add(ConstantItemID.GemId, 970000);
        GameResources.Instance.Inventory.Add(ConstantItemID.EnergyId, 1000);
    }
    private bool IsInfinite(int id) {
        if (infiniteItemIds != null) {
            return infiniteItemIds.Contains(id);
        }
        return false;
    }

    public void AddInfiniteItem(int id) {
        if (infiniteItemIds != null) {
            if (!infiniteItemIds.Contains(id)) {
                infiniteItemIds.Add(id);
            }
        }
    }

    public void RemoveInfiniteItem(int id) {
        if (infiniteItemIds != null) {
            if (infiniteItemIds.Contains(id)) {
                infiniteItemIds.Remove(id);
            }
        }
    }
}
