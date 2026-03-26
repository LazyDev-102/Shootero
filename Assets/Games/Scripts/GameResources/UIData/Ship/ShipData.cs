using SimpleJSON;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "Resource/HardData/Ship/ShipData")]
public class ShipData : ScriptableObject, ISaveLoadable {
    [SerializeField] private int cShip; //Save (Lưu theo ID Ship)
    [SerializeField] private int cTryShip;
    [SerializeField] private int defaultShipId;
    [SerializeField] private bool trial;
    [SerializeField] private List<int> unlockedDefaultShipIds;
    [SerializeField] private List<ShipInfor> datas;

    public List<ShipInfor> Datas { get => datas; }
    public int CurrentShip { get => cShip; }
    public bool Trial { get => trial; }
    #region Editor
#if UNITY_EDITOR
    public int chipEnhance = 400;
    [ContextMenu("Load Price")]
    private void LoadPrice() {
        for (int j = 0; j < datas.Count; j++) {
            for (int i = 0; i < datas[j].Levels.Count; i++) {
                datas[j].Levels[i].Price.Amount = i * chipEnhance;
            }
        }
    }
    public int attackDefault;
    public int hpDefault;
    public int[] attackAmp;
    public int[] hpAmp;
    public int indexss;
    [ContextMenu("Load Stats")]
    private void LoadStats() {
        datas[indexss].Levels[0].Attack.Value = attackDefault;
        datas[indexss].Levels[0].HP.Value = hpDefault;
        for (int i = 1; i < datas[indexss].Levels.Count; i++) {
            datas[indexss].Levels[i].Attack.Value = i < 20 ? datas[indexss].Levels[i - 1].Attack.Value + attackAmp[0] : i < 40 ? datas[indexss].Levels[i - 1].Attack.Value + attackAmp[1] : i < 60 ? datas[indexss].Levels[i - 1].Attack.Value + attackAmp[2] : i < 80 ? datas[indexss].Levels[i - 1].Attack.Value + attackAmp[3] : datas[indexss].Levels[i - 1].Attack.Value + attackAmp[4];
            datas[indexss].Levels[i].HP.Value = i < 20 ? datas[indexss].Levels[i - 1].HP.Value + hpAmp[0] : i < 40 ? datas[indexss].Levels[i - 1].HP.Value + hpAmp[1] : i < 60 ? datas[indexss].Levels[i - 1].HP.Value + hpAmp[2] : i < 80 ? datas[indexss].Levels[i - 1].HP.Value + hpAmp[3] : datas[indexss].Levels[i - 1].HP.Value + hpAmp[4];
        }
    }
#endif
    #endregion
    public void SetTrial(bool status, int shipTrialIndex, bool fromShipPack = false) {
        trial = status;
        for (int i = 0; i < datas.Count; i++) {
            datas[i].SetShipPackTrial(fromShipPack && status);
        }
        cTryShip = shipTrialIndex;
    }
    public void SetSaw(bool saw) {
        foreach (var item in datas) {
            item.IsSeeChecked = saw;
        }
    }
    public ShipInfor GetCurrentShip() {
        if (datas == null)
            return null;
        var result = trial ? datas.Find(x => x.ID == cTryShip) : datas.Find(x => x.ID == cShip);
        if (result == null) {
            cTryShip = 1;
            cShip = 1;
            result = trial ? datas.Find(x => x.ID == cTryShip) : datas.Find(x => x.ID == cShip);
        }
        return result;
    }
    public ShipInfor GetShipInfor(int id) {
        if (datas == null)
            return null;
        return datas.Find(x => x.ID == id);
    }
    public ShipInfor GetTryShipInfor() {
        if (datas == null)
            return null;
        return datas.Find(x => x.ID == cTryShip);
    }
    public bool SetCurrentShip(int id) {
        if (datas == null)
            return false;
        if (datas.Find(x => x.ID == id) == null)
            return false;
        cShip = id;
        return true;
    }
    public bool BuyShip(int id) {
        if (datas == null)
            return false;
        var result = datas.Find(x => x.ID == id);
        if (result == null)
            return false;
        result.SetUnlock();
        return true;
    }
    public bool BuyShip(int id, int level) {
        if (datas == null)
            return false;
        var result = datas.Find(x => x.ID == id);
        if (result == null)
            return false;
        result.SetUnlock(level);
        return true;
    }
    public bool Enhance(int id) {
        if (datas == null)
            return false;
        var result = datas.Find(x => x.ID == id);
        if (result == null)
            return false;
        return result.Enhance();
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.CurrentShipId = cShip;
        saveData.Ships = new ShipSaveData[datas.Count];
        for (int i = 0; i < datas.Count; ++i) {
            saveData.Ships[i] = new ShipSaveData();
            saveData.Ships[i].CurrentLevel = datas[i].CurrentLevel;
            saveData.Ships[i].Unlocked = datas[i].Unlocked;
            saveData.Ships[i].IsOpenChecked = datas[i].IsOpenChecked;
            saveData.Ships[i].IsSeeChecked = datas[i].IsSeeChecked;
        }
        return JsonUtility.ToJson(saveData);
    }
    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            LoadNewData();
            return;
        }
        LoadAvailableData(saveData);
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.CurrentShipId, cShip);

        JSONNode shipNode = new JSONArray();
        for (int i = 0; i < datas.Count; ++i) {
            shipNode.Add(datas[i].Save2Json());
        }
        node.Add(JsonKey.ShipInfo, shipNode);

        return node;
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            LoadNewData();
        }
        else {
            LoadAvailableData(json);
        }

    }
    private void LoadNewData() {
        for (int i = 0; i < datas.Count; ++i) {
            datas[i].CurrentLevel = 0;
            datas[i].Unlocked = false;
            datas[i].IsOpenChecked = false;
            datas[i].IsSeeChecked = false;
            var length = datas[i].ShipEvolutionaries.Count;
            for (int j = 0; j < length; j++) {
                datas[i].ShipEvolutionaries[j].EvolutionState = false;
            }
        }

        foreach (var ship in datas) {
            if (unlockedDefaultShipIds.Contains(ship.ID)) {
                ship.SetUnlock();
            }
        }
        cShip = defaultShipId;
    }
    private void LoadAvailableData(SaveData saveData) {
        cShip = saveData.CurrentShipId;
        var shipSave = saveData.Ships;
        int index = 0;
        for (int i = 0; i < shipSave.Length; ++i) {
            if (i >= datas.Count)
                break;
            datas[i].CurrentLevel = shipSave[i].CurrentLevel;
            datas[i].Unlocked = shipSave[i].Unlocked;
            datas[i].IsOpenChecked = shipSave[i].IsOpenChecked;
            datas[i].IsSeeChecked = shipSave[i].IsSeeChecked;
            for (int j = 0; j < datas[i].ShipEvolutionaries.Count; j++) {
                datas[i].ShipEvolutionaries[j].EvolutionState = datas[i].CurrentLevel > (j + 1) * 20 - 2;
            }
            index++;
        }
        for (int i = index; i < datas.Count; ++i) {
            datas[i].CurrentLevel = 0;
            datas[i].Unlocked = false;
            datas[i].IsOpenChecked = false;
            datas[i].IsSeeChecked = false;
            for (int j = 0; j < datas[i].ShipEvolutionaries.Count; j++) {
                datas[i].ShipEvolutionaries[j].EvolutionState = false;
            }
        }
        datas[0].Unlocked = true;
        datas[cShip - 1].Unlocked = true;
    }
    private void LoadAvailableData(JSONNode json) {
        cShip = json[JsonKey.CurrentShipId].AsInt;
        JSONArray shipSave = json[JsonKey.ShipInfo].AsArray;
        int index = 0;
        for (int i = 0; i < shipSave.Count; ++i) {
            if (i >= datas.Count)
                break;
            datas[i].CurrentLevel = shipSave[i][JsonKey.CurrentLv].AsInt;
            datas[i].Unlocked = shipSave[i][JsonKey.UnlockLv].AsBool;
            datas[i].IsOpenChecked = shipSave[i][JsonKey.IsOpenChecked].AsBool;
            datas[i].IsSeeChecked = shipSave[i][JsonKey.IsSeeChecked].AsBool;
            for (int j = 0; j < datas[i].ShipEvolutionaries.Count; j++) {
                datas[i].ShipEvolutionaries[j].EvolutionState = datas[i].CurrentLevel > (j + 1) * 20 - 2;
            }
            index++;
        }
        for (int i = index; i < datas.Count; ++i) {
            datas[i].CurrentLevel = 0;
            datas[i].Unlocked = false;
            datas[i].IsOpenChecked = false;
            datas[i].IsSeeChecked = false;
            for (int j = 0; j < datas[i].ShipEvolutionaries.Count; j++) {
                datas[i].ShipEvolutionaries[j].EvolutionState = false;
            }
        }
        datas[0].Unlocked = true;
        datas[cShip - 1].Unlocked = true;
    }
    [Serializable]
    public class SaveData {
        [SerializeField] private int csi;
        [SerializeField] private ShipSaveData[] ss;

        public int CurrentShipId { get => csi; set => csi = value; }
        public ShipSaveData[] Ships { get => ss; set => ss = value; }
    }
}

