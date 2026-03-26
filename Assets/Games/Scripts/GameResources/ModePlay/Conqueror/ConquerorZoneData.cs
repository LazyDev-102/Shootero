
using SimpleJSON;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ConquerorZoneData", menuName = "Resource/Modes/Conqueror/ConquerorZoneData")]
public class ConquerorZoneData : ScriptableObject {
    [SerializeField] private string nameZone;
    [SerializeField] Sprite icon;
    [SerializeField] private float difficultMultiplier;
    [SerializeField] private ConquerorWaveData[] waveDatas;
    [SerializeField] private ZoneBackground background;

    [Space, Header("Preload")]
    [SerializeField] private int[] enemyIds;
    [SerializeField] private int[] minibossIds;
    [SerializeField] private int[] bossIds;
    [SerializeField] private int[] trapIds;
    [SerializeField] private int[] chestIds;
    [SerializeField] private int minibossNumberPreload;
    [SerializeField] private int bossNumberPreload;
    [SerializeField] private int trapNumberPreload;
    [SerializeField] private int chestNumberPreload;
    [SerializeField] private bool firstUnlock;
    private int highestWave;
    private int currentWave;
    private int numberPlayBeforeFirstWin;
    private bool isTracked;

    public Sprite Icon { get => icon; }
    public ZoneBackground Background { get => background; }
    public ConquerorWaveData[] WaveDatas { get => waveDatas; }
    public string NameZone { get => nameZone; }
    public int MaxWave { get => waveDatas.Length; }
    public int CurrentWave { get => currentWave; }
    public bool FirstUnlock { get => firstUnlock; }
    public int HighestWave { get => highestWave; }
    public int NumberPlayBeforeFirstWin { get => numberPlayBeforeFirstWin;}
    public bool IsTracked { get => isTracked; }
    public float DifficultMultiplier { get => difficultMultiplier; }

    public void PreloadIngame(int curZoneIndex) {
        GameResources.Instance.EnemyData.PreloadEnemies(curZoneIndex, false)
                                        .PreloadBoss(bossIds, bossNumberPreload)
                                        .PreloadMiniboss(minibossIds, minibossNumberPreload)
                                        .PreloadTrap(trapIds, trapNumberPreload)
                                        .PreloadChest(chestIds, chestNumberPreload);
    }
    public float GetRate(bool win) {
        if (win)
            return 1;
        return (float)currentWave / (float)MaxWave;
    }
    public void SetCurrentWave(int value) {
        currentWave = value;
    }
    public void SetFirstUnlock(bool status) {
        firstUnlock = status;
    }
    public void IncNumberPlayBeforeWin() {
        if(highestWave != MaxWave)
            numberPlayBeforeFirstWin++;
    }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            highestWave = 0;
            firstUnlock = false;
            return;
        }
        highestWave = saveData.HighestWave;
        firstUnlock = saveData.FirstUnlock;
    }

    public string SaveToJson() {
        SaveData saveData = new SaveData();
        saveData.HighestWave = HighestWave;
        saveData.FirstUnlock = firstUnlock;
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.AsObject == "") {
            highestWave = 0;
            numberPlayBeforeFirstWin = 0;
            firstUnlock = false;
        }
        else {
            highestWave = json[JsonKey.HighestWave].AsInt;
            firstUnlock = json.HasKey(JsonKey.FirstUnlock) ? json[JsonKey.FirstUnlock].AsBool : false;
            numberPlayBeforeFirstWin = json.HasKey(JsonKey.Progress) ? json[JsonKey.Progress].AsInt : 0;
            isTracked = json.HasKey(JsonKey.UnlockZone) ? json[JsonKey.UnlockZone].AsBool : false;
        }
    }
    public JSONNode Save2Json() {
        JSONNode node = new JSONObject();
        node.Add(JsonKey.HighestWave, HighestWave);
        node.Add(JsonKey.Progress, numberPlayBeforeFirstWin);
        node.Add(JsonKey.UnlockZone, isTracked);

        if (FirstUnlock)
            node.Add(JsonKey.FirstUnlock, FirstUnlock);

        return node;
    }

    public ConquerorZoneData SetHighestWave(int wave, bool isMaxWave = false, bool isTutorial = false) {
        if (isTutorial) {
            var newWave = wave - 1;
            if (newWave > HighestWave)
                highestWave = newWave;
            if (isMaxWave)
                highestWave = MaxWave - 1;
        }
        else {

            if (wave > HighestWave)
                highestWave = wave;
            if (isMaxWave)
                highestWave = MaxWave;
        }
        return this;
    }

    public ConquerorZoneData SetHighestWave(int wave, bool isTutorial) {
        if (isTutorial) {
            var newWave = wave - 1;
            if (newWave > HighestWave)
                highestWave = newWave;
        }
        else {
            if (wave > HighestWave)
                highestWave = wave;
        }
        return this;
    }


    [Serializable]
    public class SaveData {
        [SerializeField] int hw;
        [SerializeField] bool fu;
        public int HighestWave { get => hw; set => hw = value; }
        public bool FirstUnlock { get => fu; set => fu = value; }
    }
}