using Gear_Data;
using Gemmob;
using SimpleJSON;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GearInventory", menuName = "Resource/Gears/GearInventory")]
public class GearInventory : ScriptableObject, ISaveLoadable {
    private List<GearSoftData> gearItems = new List<GearSoftData>();
    [SerializeField] private GearSlotData weaponrySlot;
    [SerializeField] private GearSlotData shieldSlot;
    [SerializeField] private GearSlotData coreSlot;
    [SerializeField] private GearSlotData engineSlot;
    [SerializeField] private GearSlotData droneLSlot;
    [SerializeField] private GearSlotData droneRSlot;
    public List<GearSoftData> GearItems {
        get => gearItems;
    }
    public GearSlotData WeaponrySlot { get => weaponrySlot; }
    public GearSlotData ShieldSlot { get => shieldSlot; }
    public GearSlotData CoreSlot { get => coreSlot; }
    public GearSlotData EngineSlot { get => engineSlot; }
    public GearSlotData DroneLSlot { get => droneLSlot; }
    public GearSlotData DroneRSlot { get => droneRSlot; }

    public List<GearSoftData> GetAllGearInSlot(GearType type) {
        return gearItems.FindAll(x => x.GearTypeSoft == type);
    }
    public List<GearSoftData> GetWeaponries() {
        return gearItems.FindAll(x => x.GearTypeSoft == GearType.Weapon);
    }
    public List<GearSoftData> GetShields() {
        return gearItems.FindAll(x => x.GearTypeSoft == GearType.Shield);
    }
    public List<GearSoftData> GetCores() {
        return gearItems.FindAll(x => x.GearTypeSoft == GearType.Reactor);
    }
    public List<GearSoftData> GetEngines() {
        return gearItems.FindAll(x => x.GearTypeSoft == GearType.Propulsion);
    }
    public List<GearSoftData> GetDrones() {
        return gearItems.FindAll(x => x.IsDrone);
    }

    public GearSoftData GetItem(int id) {
        if (gearItems == null)
            return null;
        return gearItems.FirstOrDefault(x => x.Id == id);
    }
    public bool GearHasCombo(int id, int rank) {
        int sum = gearItems.Count(i => i.GearHardData.Id == id && i.CurrentRank == rank);
        return sum >= 3;
    }
    public bool GearCanCombo(int id, int rank) {
        int sum = gearItems.Count(i => i.GearHardData.Id == id && i.CurrentRank == rank);
        return sum >= 2;
    }
    public GearSoftData GearEquipCanCombo() {
        int sum = 0;
        int gearId = 0;
        int gearRank = 0;
        List<GearSlotData> result = new List<GearSlotData>();
        if (weaponrySlot.IsEquiped) {
            sum = 0;
            gearId = weaponrySlot.ItemEquip.Id;
            gearRank = weaponrySlot.ItemEquip.CurrentRank;
            sum = gearItems.Count(i => i.GearHardData.Id == gearId && i.CurrentRank == gearRank);
            if (sum == 2)
                result.Add(weaponrySlot);
        }
        if (shieldSlot.IsEquiped) {
            sum = 0;
            gearId = shieldSlot.ItemEquip.Id;
            gearRank = shieldSlot.ItemEquip.CurrentRank;
            sum = gearItems.Count(i => i.GearHardData.Id == gearId && i.CurrentRank == gearRank);
            if (sum == 2)
                result.Add(shieldSlot);
        }
        if (coreSlot.IsEquiped) {
            sum = 0;
            gearId = coreSlot.ItemEquip.Id;
            gearRank = coreSlot.ItemEquip.CurrentRank;
            sum = gearItems.Count(i => i.GearHardData.Id == gearId && i.CurrentRank == gearRank);
            if (sum == 2)
                result.Add(coreSlot);
        }
        if (engineSlot.IsEquiped) {
            sum = 0;
            gearId = engineSlot.ItemEquip.Id;
            gearRank = engineSlot.ItemEquip.CurrentRank;
            sum = gearItems.Count(i => i.GearHardData.Id == gearId && i.CurrentRank == gearRank);
            if (sum == 2)
                result.Add(engineSlot);
        }
        if (droneLSlot.IsEquiped) {
            sum = 0;
            gearId = droneLSlot.ItemEquip.Id;
            gearRank = droneLSlot.ItemEquip.CurrentRank;
            sum = gearItems.Count(i => i.GearHardData.Id == gearId && i.CurrentRank == gearRank);
            if (sum == 2)
                result.Add(droneLSlot);
        }
        if (droneRSlot.IsEquiped) {
            sum = 0;
            gearId = droneRSlot.ItemEquip.Id;
            gearRank = droneRSlot.ItemEquip.CurrentRank;
            sum = gearItems.Count(i => i.GearHardData.Id == gearId && i.CurrentRank == gearRank);
            if (sum == 2)
                result.Add(droneRSlot);
        }
        if (result.Count == 0)
            return null;
        else if (result.Count == 1)
            return result[0].ItemEquip;
        else {
            var resultIndex = 0;
            for (int i = 1; i < result.Count; i++) {
                if (result[i].ItemEquip.CurrentRank > result[i - 1].ItemEquip.CurrentRank)
                    resultIndex = i;
            }
            return result[resultIndex].ItemEquip;
        }
    }
    public GearSoftData GearCanComboHighest() {
        var gs = gearItems.FindAll(x => x.IsDrone);
        if (gs == null || gs.Count == 0)
            return null;
        GearSoftData result = null;
        for (int i = 0; i < gs.Count - 1; i++) {
            for (int j = i + 1; j < gs.Count; j++) {
                if (gs[j].CurrentRank > gs[i].CurrentRank)
                    result = gs[j];
            }
        }
        return result;
    }
    public void Add(GearSoftData item) {
        gearItems.Add(item);
        EventDispatcher.Instance.Dispatch(EventKey.OnGearInventoryChange);
    }

    public void Add(int id) {
        gearItems.Add(new GearSoftData(id));
        EventDispatcher.Instance.Dispatch(EventKey.OnGearInventoryChange);
    }
    public void Add(int id, int rank) {
        gearItems.Add(new GearSoftData(id, rank));
        EventDispatcher.Instance.Dispatch(EventKey.OnGearInventoryChange);
    }
    public void Remove(GearSoftData item) {
        gearItems.Remove(item);
        EventDispatcher.Instance.Dispatch(EventKey.OnGearInventoryChange);
    }
    public void UnEquipWithGearType(GearType type) {
        for(int i = 0; i < gearItems.Count; i++) {
            if(gearItems[i].GearTypeSoft == type)
                gearItems[i].SetIsEquiped(false);
        }
    }
    public void EquipUI(GearSoftData item) {
        if (gearItems == null)
            return;
        if (gearItems.Contains(item)) {
            item.SetIsEquiped(true);
            item.AddAllStat();
        }
    }
    public void UnEquip(GearType type) {
        if (gearItems == null)
            return;
        var result = gearItems.Find(x => /*x.GearHardData.GearType == type &&*/ x.GearTypeSoft == type && x.IsEquiped);
        if (result != null) {
            result.SetIsEquiped(false);
            result.RemoveAllStat();
        }
    }

    public void SortByRarety() {
        for (int i = 0; i < gearItems.Count - 1; i++) {
            for (int j = i; j < gearItems.Count; j++) {
                bool sameRank = gearItems[j].CurrentRank == gearItems[i].CurrentRank;
                bool sameGearType = gearItems[j].GearHardData.GearType == gearItems[i].GearHardData.GearType;
                bool sameOrder = gearItems[j].GearHardData.Order == gearItems[i].GearHardData.Order;
                if (gearItems[j].CurrentRank > gearItems[i].CurrentRank
                    || sameRank && gearItems[j].GearHardData.GearType > gearItems[i].GearHardData.GearType
                    || sameRank && sameGearType && gearItems[j].GearHardData.Order > gearItems[i].GearHardData.Order
                    || sameRank && sameGearType && sameOrder && gearItems[j].CurrentLevel > gearItems[i].CurrentLevel) {
                    SwapItem(i, j);
                }
            }
        }
    }
    private void SwapItem(int i, int j) {
        var temp = gearItems[i];
        gearItems[i] = gearItems[j];
        gearItems[j] = temp;
    }


    public (GearSoftData, DroneBase) GetDrone1() {
        var result = GetDroneLEquipable();
        if (result == null || result.GearTypeSoft != GearType.Drone1 || result.GearHardData == null)
            return (null, null);
        DroneGearHardData droneGear = result.GearHardData as DroneGearHardData;
        if (droneGear != null) {
            return (result, droneGear.DronePrefab);
        }
        return (null, null);
    }
    public (GearSoftData, DroneBase) GetDrone2() {
        var result = GetDroneREquipable();
        if (result == null || result.GearTypeSoft != GearType.Drone2 || result.GearHardData == null)
            return (null, null);
        DroneGearHardData droneGear = result.GearHardData as DroneGearHardData;
        if (droneGear != null) {
            return (result, droneGear.DronePrefab);
        }
        return (null, null);
    }
    public GearSoftData GetDroneLEquipable() {
        if (gearItems == null)
            return null;
        return gearItems.Find(x => x.IsEquiped /*&& x.GearHardData.GearType == GearType.Drone1 */&& x.GearTypeSoft == GearType.Drone1);
    }
    public GearSoftData GetDroneREquipable() {
        if (gearItems == null)
            return null;
        return gearItems.Find(x => x.IsEquiped /*&& x.GearHardData.GearType == GearType.Drone1 */&& x.GearTypeSoft == GearType.Drone2);
    }
    public GearSoftData GetWeaponryEquipable() {
        if (gearItems == null)
            return null;
        return gearItems.Find(x => x.IsEquiped && x.GearHardData.GearType == GearType.Weapon);
    }
    public GearSoftData GetShieldEquipable() {
        if (gearItems == null)
            return null;
        return gearItems.Find(x => x.IsEquiped && x.GearHardData.GearType == GearType.Shield);
    }
    public GearSoftData GetCoreEquipable() {
        if (gearItems == null)
            return null;
        return gearItems.Find(x => x.IsEquiped && x.GearHardData.GearType == GearType.Reactor);
    }
    public GearSoftData GetEngineEquipable() {
        if (gearItems == null)
            return null;
        return gearItems.Find(x => x.IsEquiped && x.GearHardData.GearType == GearType.Propulsion);
    }
    #region Save,Load Data
    private class SaveDataModel {
        public GearSoftData[] i;
        [SerializeField] private int wsl;
        [SerializeField] private int ssl;
        [SerializeField] private int csl;
        [SerializeField] private int esl;
        [SerializeField] private int dll;
        [SerializeField] private int drl;
        public SaveDataModel(GearSoftData[] items) {
            this.i = items;
        }

        public SaveDataModel(int capacity) {
            i = new GearSoftData[capacity];
        }

        public int WeaponrySlotLevel { get => wsl; set => wsl = value; }
        public int ShieldSlotLevel { get => ssl; set => ssl = value; }
        public int CoreSlotLevel { get => csl; set => csl = value; }
        public int EngineSlotLevel { get => esl; set => esl = value; }
        public int DroneLSlotLevel { get => dll; set => dll = value; }
        public int DroneRSlotLevel { get => drl; set => drl = value; }
    }

    private void InitSlot() {
        weaponrySlot = new GearSlotData();
        shieldSlot = new GearSlotData();
        coreSlot = new GearSlotData();
        engineSlot = new GearSlotData();
        droneLSlot = new GearSlotData();
        droneRSlot = new GearSlotData();
    }

    public void LoadFromJson(string json) {
        SaveDataModel saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveDataModel>(json);
        }
        if (weaponrySlot == null)
            InitSlot();
        if (saveData == null) {
            gearItems = new List<GearSoftData>();
            weaponrySlot.SetData(0, null);
            shieldSlot.SetData(0, null);
            coreSlot.SetData(0, null);
            engineSlot.SetData(0, null);
            droneLSlot.SetData(0, null);
            droneRSlot.SetData(0, null);
            return;
        }
        gearItems = new List<GearSoftData>();
        foreach (var item in saveData.i) {
            gearItems.Add(item);
        }

        weaponrySlot.SetData(saveData.WeaponrySlotLevel, GetWeaponryEquipable());
        shieldSlot.SetData(saveData.ShieldSlotLevel, GetShieldEquipable());
        coreSlot.SetData(saveData.CoreSlotLevel, GetCoreEquipable());
        engineSlot.SetData(saveData.EngineSlotLevel, GetEngineEquipable());
        droneLSlot.SetData(saveData.DroneLSlotLevel, GetDroneLEquipable());
        droneRSlot.SetData(saveData.DroneRSlotLevel, GetDroneREquipable());
    }
    public string SaveToJson() {
        if (gearItems == null) {
            return null;
        }

        SaveDataModel saveData = new SaveDataModel(gearItems.Count);

        int index = 0;
        foreach (var item in gearItems) {
            saveData.i[index] = item;
            index++;
        }
        saveData.WeaponrySlotLevel = weaponrySlot.CurrentLevel;
        saveData.ShieldSlotLevel = shieldSlot.CurrentLevel;
        saveData.CoreSlotLevel = coreSlot.CurrentLevel;
        saveData.EngineSlotLevel = engineSlot.CurrentLevel;
        saveData.DroneLSlotLevel = droneLSlot.CurrentLevel;
        saveData.DroneRSlotLevel = droneRSlot.CurrentLevel;
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (weaponrySlot == null)
            InitSlot();
        if (json == null || json.ToString() == "{}") {
            gearItems = new List<GearSoftData>();
            weaponrySlot.SetData(0, null);
            shieldSlot.SetData(0, null);
            coreSlot.SetData(0, null);
            engineSlot.SetData(0, null);
            droneLSlot.SetData(0, null);
            droneRSlot.SetData(0, null);
        }
        else {
            gearItems = new List<GearSoftData>();
            JSONArray items = json[JsonKey.ItemSlot].AsArray;
            foreach (var item in items.Children) {
                GearSoftData newGear = new GearSoftData(item[JsonKey.ItemId].AsInt);
                newGear.LoadFJson(item);
                gearItems.Add(newGear);
            }
            //SolveWeaponry(!json.HasKey(JsonKey.WeaponrySlotLevel), json);
            weaponrySlot.SetData(json[JsonKey.WeaponrySlotLevel] != null ? json[JsonKey.WeaponrySlotLevel].AsInt : 0, GetWeaponryEquipable());
            shieldSlot.SetData(json[JsonKey.ShieldSlotLevel] != null ? json[JsonKey.ShieldSlotLevel].AsInt : 0, GetShieldEquipable());
            coreSlot.SetData(json[JsonKey.CoreSlotLevel] != null ? json[JsonKey.CoreSlotLevel].AsInt : 0, GetCoreEquipable());
            engineSlot.SetData(json[JsonKey.EngineSlotLevel] != null ? json[JsonKey.EngineSlotLevel].AsInt : 0, GetEngineEquipable());
            droneLSlot.SetData(json[JsonKey.DroneLSlotLevel] != null ? json[JsonKey.DroneLSlotLevel].AsInt : 0, GetDroneLEquipable());
            droneRSlot.SetData(json[JsonKey.DroneRSlotLevel] != null ? json[JsonKey.DroneRSlotLevel].AsInt : 0, GetDroneREquipable());
        }
    }
    private void SolveWeaponry(bool oldData, JSONNode json) {
        if (oldData) {
            SaveDataModel saveData = null;
            string data = PlayerPrefs.GetString("git");
            if (!string.IsNullOrEmpty(data)) {
                saveData = JsonUtility.FromJson<SaveDataModel>(data);
            }
            if (saveData != null)
                weaponrySlot.SetData(saveData.WeaponrySlotLevel, GetWeaponryEquipable());
        }
        else {
            weaponrySlot.SetData(json[JsonKey.WeaponrySlotLevel] != null ? json[JsonKey.WeaponrySlotLevel].AsInt : 0, GetWeaponryEquipable());
        }

    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        int index = 0;
        JSONNode items = new JSONArray();
        foreach (var item in gearItems) {
            items.Add(item.Save2Json());
            index++;
        }
        node.Add(JsonKey.ItemSlot, items);

        if (weaponrySlot.CurrentLevel > 0)
            node.Add(JsonKey.WeaponrySlotLevel, weaponrySlot.CurrentLevel);

        if (shieldSlot.CurrentLevel > 0)
            node.Add(JsonKey.ShieldSlotLevel, shieldSlot.CurrentLevel);

        if (coreSlot.CurrentLevel > 0)
            node.Add(JsonKey.CoreSlotLevel, coreSlot.CurrentLevel);

        if (engineSlot.CurrentLevel > 0)
            node.Add(JsonKey.EngineSlotLevel, engineSlot.CurrentLevel);

        if (droneLSlot.CurrentLevel > 0)
            node.Add(JsonKey.DroneLSlotLevel, droneLSlot.CurrentLevel);

        if (droneRSlot.CurrentLevel > 0)
            node.Add(JsonKey.DroneRSlotLevel, droneRSlot.CurrentLevel);

        return node;
    }

    public void Reload() {
        gearItems.Clear();
    }

    public GearSlotData GetGearSlotByGearType(GearType type) {
        switch (type) {
            case GearType.Weapon:
                return weaponrySlot;
            case GearType.Shield:
                return shieldSlot;
            case GearType.Reactor:
                return coreSlot;
            case GearType.Propulsion:
                return engineSlot;
            case GearType.Drone1:
                return droneLSlot;
            case GearType.Drone2:
                return droneRSlot;
        }
        return weaponrySlot;
    }
    #endregion

    [ContextMenu("RemoveAll")]
    public void RemoveAll() {
        gearItems.Clear();
    }
    [ContextMenu("RemoveEquip")]
    public void RemoveEquip() {
        foreach (var item in gearItems) {
            item.SetIsEquiped(false);
        }
    }

}
