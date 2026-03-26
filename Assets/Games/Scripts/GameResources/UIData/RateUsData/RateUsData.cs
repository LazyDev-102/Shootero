using Gemmob.Common;
using SimpleJSON;
using UnityEngine;

[CreateAssetMenu(fileName = "RateUsData", menuName = "Resource/HardData/RateUs/RateUsData")]
public class RateUsData : ScriptableObject {
    [SerializeField] private bool rated;
    [SerializeField] private bool rateFailed;
    [SerializeField] private bool isItemEpic;
    [SerializeField] private int trigger1;//First play Zone 2
    [SerializeField] private bool trigger2;//Win any Zone
    [SerializeField] private bool isEpicOneShot;

    [Header("Remote")]
    [SerializeField] private string remoteKey;
    [SerializeField] private bool enable;

    public bool CanSpecialTrigger() {
        if (!Gemmob.Networker.IsInternetAvaiable || rated)
            return false;
        if (!enable)
            return false;
        if (trigger1 == 1) {
            trigger1 = 10;
            return true;
        }
        if (!isEpicOneShot && isItemEpic) {
            isEpicOneShot = true;
            return true;
        }
        if (trigger2)
            return true;
        return false;
    }

    public void SetFinishRated(bool status) {
        rated = status;
    }
    public void SetFinishRateFailed(bool status) {
        rateFailed = status;
        SetClaimEpicItemStatus(false);
        SetFinishZoneStatus(false);
    }

    public void SetFinishZoneStatus(bool status) {
        trigger2 = status;
    }
    public void SetClaimEpicItemStatus(bool status) {
        isItemEpic = status;
    }
    public RateUsData SetTrigger(int zoneIndex, bool isWin) {
        if (rated)
            return this;
        if (zoneIndex == 1)
            trigger1++;
        SetFinishZoneStatus(zoneIndex >= 1 && isWin);
        return this;
    }
    #region Remote
    public void ReloadDataByRemote() {
        if (remoteKey.Trim().Length == 0 || !Networks.IsInternetAvaiable) {
            return;
        }
        RemoteConfig.GetStringAsync(remoteKey, GetData);
    }
    private void GetData(string data) {
        RemoteData remoteData = null;
        if (!string.IsNullOrEmpty(data)) {
            remoteData = JsonUtility.FromJson<RemoteData>(data);
        }

        if (remoteData == null) {
            enable = false;
            return;
        }
        enable = remoteData.Enable;
    }
    public class RemoteData {
        [SerializeField] private bool enable;

        public bool Enable { get => enable; set => enable = value; }

        public RemoteData() {
            enable = false;
        }

        public RemoteData(bool enable) {
            this.enable = enable;
        }
    }
    #endregion
    #region Save Load Data
    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        trigger2 = false;
        isItemEpic = false;
        if (saveData == null) {
            rated = false;
            trigger1 = 0;
            isEpicOneShot = false;
            return;
        }
        rated = saveData.Rated;
        rateFailed = saveData.RateFailed;
        trigger1 = saveData.Trigger1;
        isEpicOneShot = saveData.IsEpicOneShot;
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.Rated = rated;
        saveData.Trigger1 = trigger1;
        saveData.RateFailed = rateFailed;
        saveData.IsEpicOneShot = isEpicOneShot;
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        trigger2 = false;
        isItemEpic = false;
        if (json == null || json.ToString() == "{}") {
            rated = false;
            trigger1 = 0;
            isEpicOneShot = false;
        }
        else {
            rated = json[JsonKey.IsCompleted].AsBool;
            rateFailed = json[JsonKey.Progress].AsBool;
            trigger1 = json[JsonKey.IsFail].AsInt;
            isEpicOneShot = json[JsonKey.IsEpicOneShot].AsBool;
        }
    }

    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.IsCompleted, rated);
        node.Add(JsonKey.Progress, trigger1);
        node.Add(JsonKey.IsFail, rateFailed);
        node.Add(JsonKey.IsEpicOneShot, isEpicOneShot);
        return node;
    }

    [System.Serializable]
    public class SaveData {
        [SerializeField] private bool rated;
        [SerializeField] private bool rf;
        [SerializeField] private int t1;
        [SerializeField] private bool ieo;

        public bool Rated { get => rated; set => rated = value; }
        public bool RateFailed { get => rf; set => rf = value; }
        public int Trigger1 { get => t1; set => t1 = value; }
        public bool IsEpicOneShot { get => ieo; set => ieo = value; }
    }
    #endregion
}
