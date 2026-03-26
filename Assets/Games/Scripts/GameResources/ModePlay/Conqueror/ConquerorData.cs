
using UnityEngine;
using System;
using Helper;
using SimpleJSON;

[CreateAssetMenu(fileName = "ConquerorData", menuName = "Resource/Modes/Conqueror/ConquerorData")]
public class ConquerorData : ScriptableObject, ISaveLoadable {
    private readonly string path = "EnemyData/ConquerorZoneData";

    [SerializeField] private ConquerorZoneData[] zoneDatas;
    [SerializeField] private ConquerorZoneData tutorialconqueZone;
    [SerializeField] private ConquerorZoneData trialconqueZone;
    [Space]

    [SerializeField] private bool firstTime;
    [SerializeField] private bool firstLose;

    private int currentZoneIndex;
    private int unlockZoneIndex;


    public ConquerorZoneData[] ZoneDatas { get => zoneDatas; }
    public ConquerorZoneData TrialZone { get => trialconqueZone; }
    public ConquerorZoneData TutorialZone { get => tutorialconqueZone; }
    public ConquerorZoneData CurrentZone { get => ZoneDatas[currentZoneIndex]; }
    public int UnlockZone { get => unlockZoneIndex; }
    public int CurrentZoneIndex { get => currentZoneIndex; }
    public bool FirstTime { get => firstTime; }
    public bool FirstLose { get => firstLose; }

    public bool IsTut;
    public bool IsTutPlayGame;

    public void Reload() {
        currentZoneIndex = 0;
    }

    public void LoadZoneData(int zoneIndex) {
        if (zoneDatas == null || zoneDatas.Length == 0)
            zoneDatas = new ConquerorZoneData[Constant.ZoneCount];
        if (zoneDatas[zoneIndex] == null) {
            zoneDatas[zoneIndex] = Resources.Load<ConquerorZoneData>(path + zoneIndex);
        }
    }

    public void SetFirstTimePlay() {
        if (firstTime) {
            firstTime = false;
            return;
        }
        firstTime = true;
    }
    public void SetFirstLoseStatus() {
        if (firstLose) {
            firstLose = false;
            return;
        }
        firstLose = true;
    }
    public ConquerorWaveInfo[] GenerateWaves(int currentZoneIndex, bool isTutorial = false) {
        ConquerorWaveData[] waveDatas = GameManager.Instance.IsTrial ? TrialZone.WaveDatas : isTutorial ? TutorialZone.WaveDatas : ZoneDatas[currentZoneIndex].WaveDatas;
        ConquerorWaveInfo[] waveInfoes = new ConquerorWaveInfo[waveDatas.Length];
        for (int i = 0; i < waveInfoes.Length; ++i) {
            ConquerorWaveInfo waveInfo = waveDatas[i].CreateInfo(currentZoneIndex, i);
            waveInfoes[i] = waveInfo;
        }
        return waveInfoes;
    }

    public bool IsCurrentZoneHasPass(int currentZoneIndex) {
        return currentZoneIndex < unlockZoneIndex;
    }

    public bool SetNextUnlockZone() {
        CurrentZone.SetHighestWave(CurrentZone.MaxWave, true, IsTut);
        if (unlockZoneIndex >= ZoneDatas.Length - 1)
            return false;
        unlockZoneIndex++;
        SetCurrentZone(unlockZoneIndex);
        CurrentZone.SetFirstUnlock(true);
        return true;
    }
    public bool SetCurrentZone(int zone) {
        if (zone >= ZoneDatas.Length)
            return false;
        currentZoneIndex = zone;
        return true;
    }
    public void CheckAutoUnlockZone() {
        if (unlockZoneIndex < ZoneDatas.Length - 1) {
            if (ZoneDatas[unlockZoneIndex].HighestWave == ZoneDatas[unlockZoneIndex].MaxWave) {
                SetNextUnlockZone();
            }
        }
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.FirstTime = firstTime;
        saveData.UnlockZone = unlockZoneIndex;
        saveData.CurrentZone = currentZoneIndex;
        saveData.Zones = new string[ZoneDatas.Length];
        for (int i = 0; i < ZoneDatas.Length; ++i) {
            saveData.Zones[i] = ZoneDatas[i].SaveToJson();
        }
        return JsonUtility.ToJson(saveData);
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.FirstTime, firstTime);
        node.Add(JsonKey.UnlockZone, unlockZoneIndex);
        node.Add(JsonKey.CurrentZone, currentZoneIndex);

        JSONNode zoneNodes = new JSONArray();

        foreach (var item in ZoneDatas) {
            JSONNode itemNode = item.Save2Json();
            zoneNodes.Add(itemNode);
        }
        node.Add(JsonKey.Zones, zoneNodes);
        return node;
    }
    public void LoadFJson(JSONNode json) {
        if (json == null || json.AsObject == "") {
            NewInitialize();
        }
        else {
            firstTime = json[JsonKey.FirstTime].AsBool;
            firstLose = json[JsonKey.FirstLose].AsBool;
            unlockZoneIndex = json[JsonKey.UnlockZone].AsInt;
            currentZoneIndex = json[JsonKey.CurrentZone].AsInt;

            JSONArray zones = json[JsonKey.Zones].AsArray;
            for (int i = 0; i < zones.Count; ++i) {
                ZoneDatas[i].LoadFJson(zones[i]);
            }
            int index = zones.Count;
            for (int i = index; i < ZoneDatas.Length; ++i) {
                ZoneDatas[i].LoadFJson(null);
            }
        }

    }
    private void NewInitialize() {
        firstTime = false;
        firstLose = false;
        unlockZoneIndex = 0;
        currentZoneIndex = 0;
        foreach (var z in ZoneDatas) {
            z.LoadFJson(null);
        }
    }
    private void Initialize() {
        firstTime = false;
        firstLose = false;
        unlockZoneIndex = 0;
        currentZoneIndex = 0;
        foreach (var z in ZoneDatas) {
            z.LoadFromJson(null);
        }
    }
    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            Initialize();
            return;
        }
        IsTutPlayGame = false;
        firstTime = saveData.FirstTime;
        firstLose = saveData.FirstLose;
        unlockZoneIndex = saveData.UnlockZone;
        currentZoneIndex = saveData.CurrentZone;
        int index = 0;
        for (int i = 0; i < saveData.Zones.Length; ++i) {
            if (i >= ZoneDatas.Length) {
                return;
            }
            ZoneDatas[i].LoadFromJson(saveData.Zones[i]);
            index++;
        }
        for (int i = index; i < ZoneDatas.Length; ++i) {
            ZoneDatas[i].LoadFromJson(null);
        }
    }

    [Serializable]
    public class SaveData {
        [SerializeField] private int cz;
        [SerializeField] private int uz;
        [SerializeField] private string[] zs;
        [SerializeField] private bool ft;
        [SerializeField] private bool fl;

        public int CurrentZone { get => cz; set => cz = value; }
        public int UnlockZone { get => uz; set => uz = value; }
        public string[] Zones { get => zs; set => zs = value; }
        public bool FirstTime { get => ft; set => ft = value; }
        public bool FirstLose { get => fl; set => fl = value; }
    }
    [Serializable]
    public class ConfigModeWave {
        [SerializeField] private RangeIntValue limitRange;
        [SerializeField] private RangeIntValue timeRange;

        public RangeIntValue LimitRange { get => limitRange; set => limitRange = value; }
        public RangeIntValue TimeRange { get => timeRange; set => timeRange = value; }
    }

}

[Serializable]
public class DifficultSpawnEnemy {
    [SerializeField] private TypeEnemyPercent[] typePercents;

    public TypeEnemyPercent[] TypePercents { get => typePercents; set => typePercents = value; }
}

[Serializable]
public class TypeEnemyPercent : IPercentable {
    [SerializeField] private EnemyType type;
    [SerializeField] private int percent;

    public EnemyType Type { get => type; set => type = value; }
    public int Percent { get => percent; set => percent = value; }

    public int GetPercent() {
        return percent;
    }
}


[Serializable]
public class DifficultSpawnTrap {
    [SerializeField] private TypeEnemyPercent[] typePercents;
    [SerializeField] private int limitTrap;

    public TypeEnemyPercent[] TypePercents { get => typePercents; set => typePercents = value; }
    public int LimitTrap { get => limitTrap; }
}

//public static partial class JSONKey {

//}