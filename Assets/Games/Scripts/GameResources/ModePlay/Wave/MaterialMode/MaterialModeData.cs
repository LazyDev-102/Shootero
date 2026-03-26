using SimpleJSON;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialModeData", menuName = "Resource/Modes/Material/MaterialModeData")]
public class MaterialModeData : ScriptableObject {
    [SerializeField] private ItemStack energyNeed;
    [SerializeField] private int maxTurn;
    [SerializeField] private MaterialModeWeekData[] weekDatas;
    [SerializeField] private MaterialModeInfo[] materialModeInfos;
    [SerializeField] private MaterialWaveObstacle[] obstacles;
    [SerializeField] private Helper.Area obstacleSpawnPos;

    public ItemStack EnergyNeed { get => energyNeed; }
    public MaterialModeWeekData[] WeekDatas { get => weekDatas; }
    public int TurnRemain { get => turnRemain; }
    public int MaxTurn { get => maxTurn; }

    private int turnRemain;
    private int checkinDay;
    private int checkinYear;
    private MaterialModeInfo currentMaterialInfo;
    public MaterialModeWaveInfo[] GenerateWaves() {
        int cDay = (int)DateTime.Now.DayOfWeek;
        int length = GetInfo().MaxWave;// weekDatas[cDay].Data.Length;
        MaterialModeWaveInfo[] waveInfoes = new MaterialModeWaveInfo[length];
        for (int i = 0; i < waveInfoes.Length; ++i) {
            MaterialModeWaveInfo waveInfo = weekDatas[cDay].Data[i].CreateInfo();
            waveInfoes[i] = waveInfo;
        }
        return waveInfoes;
    }
    public void OnStartGame() {
        ChangeTurnRemain();
        SpawnObstacles();
    }
    private void ChangeTurnRemain(int amplitude = -1) {
        turnRemain += amplitude;
    }
#if CHEAT
    public void AddTurn(int amplitude) {
        turnRemain += amplitude;
    }
#endif
    public void ClaimReward(int currentWave, bool isWin) {
        var info = GetInfo();
        if (info != null) {
            info.Reward[(int)DateTime.Now.DayOfWeek].ClaimReward(currentWave, isWin);
        }
    }
    public MaterialModeInfo GetInfo() {
        if (currentMaterialInfo != null && currentMaterialInfo.Reward.Length != 0)
            return currentMaterialInfo;
        var currentLevel = GameResources.Instance.LevelProgress.GetCurrentLevel() + 1;
        for (int i = materialModeInfos.Length - 1; i >= 0; i--) {
            if (materialModeInfos[i].SytemLevel <= currentLevel)
                return materialModeInfos[i];
        }
        return materialModeInfos[materialModeInfos.Length - 1];
    }
    private void SpawnObstacles() {
        var data = Helper.RandomHelper.RandomInCollection(obstacles);
        int count = data.Count.GetRandomValue();
        for (int i = 0; i < count; i++) {
            Vector2 pos = Helper.BorderHelper.GetWorldPointInsideArea(data.SpawnArea[i]);
            ObstacleBase obsPrefab = ChooseObstacleSpawn(obstacles, data.BuffShape);
            ObstacleBase newObstacle = GameManager.Instance.GameLoader.SpawnObstacle(obsPrefab, pos);
            newObstacle.ChangeRange(((int)data.BuffSize + 1) * 0.75f);
            newObstacle.SetData(data);
        }
    }
    private ObstacleBase ChooseObstacleSpawn(MaterialWaveObstacle[] obstacleIds, MaterialModeBuffShape shape) {
        return GameResources.Instance.EnemyData.GetObstaclesRandom(obstacleIds, shape);
    }

    public void Preload() {
        int cDay = (int)DateTime.Now.DayOfWeek;
        int cZone = GameResources.Instance.ConquerorData.CurrentZoneIndex;
        GameResources.Instance.EnemyData.PreloadEnemies(cZone, weekDatas[cDay].EIds)
                                        .PreloadBoss(weekDatas[cDay].BossIds, 1)
                                        .PreloadMiniboss(weekDatas[cDay].MbIds, 1)
                                        .PreloadTrap(weekDatas[cDay].TrapIds, 3);
    }
    #region SaveData
    public string SaveToJson() {
        SaveData saveData = new SaveData();
        if (turnRemain > 10)
            turnRemain = 10;
        saveData.TurnRemain = turnRemain;
        saveData.CheckinDay = checkinDay;
        saveData.CheckinYear = checkinYear;
        return JsonUtility.ToJson(saveData);
    }

    public void LoadFromJson(string json) {
        SaveData saveData = null;
        if (!string.IsNullOrEmpty(json)) {
            saveData = JsonUtility.FromJson<SaveData>(json);
        }

        if (saveData == null) {
            turnRemain = maxTurn;
            checkinDay = DateTime.Now.DayOfYear;
            checkinYear = DateTime.Now.Year;
            return;
        }
        turnRemain = saveData.TurnRemain;
        checkinDay = saveData.CheckinDay;
        checkinYear = saveData.CheckinYear;
        ResetDay();
    }
    public JSONNode Save2Json() {
        if (turnRemain > 10)
            turnRemain = 10;

        JSONNode node = new JSONObject();
        node.Add(JsonKey.CurrentRemain, turnRemain);
        node.Add(JsonKey.Day, checkinDay);
        node.Add(JsonKey.Year, checkinYear);

        return node;
    }

    public void LoadFJson(JSONNode json) {
        if (json == null || json.ToString() == "{}") {
            turnRemain = maxTurn;
            checkinDay = DateTime.Now.DayOfYear;
            checkinYear = DateTime.Now.Year;
        }
        else {
            turnRemain = json[JsonKey.CurrentRemain].AsInt;
            checkinDay = json[JsonKey.Day].AsInt;
            checkinYear = json[JsonKey.Year].AsInt;
            ResetDay();
        }
    }
    public void ResetDay() {
        if (DateTime.Now.Year < checkinYear)
            return;
        if (DateTime.Now.Year == checkinYear && DateTime.Now.DayOfYear <= checkinDay)
            return;
        turnRemain = maxTurn;
        checkinDay = DateTime.Now.DayOfYear;
        checkinYear = DateTime.Now.Year;
    }
    [Serializable]
    public class SaveData {
        [SerializeField] private int tr;
        [SerializeField] private int d;
        [SerializeField] private int y;
        public int TurnRemain { get => tr; set => tr = value; }
        public int CheckinDay { get => d; set => d = value; }
        public int CheckinYear { get => y; set => y = value; }
    }
    #endregion

}
[Serializable]
public class MaterialModeInfo {
    [SerializeField] private int sytemLevel;
    [SerializeField] private int timeLimit;
    [SerializeField] private int maxWave;
    [SerializeField] private float multiDifficult;
    [SerializeField] private MaterialModeRewardInfo[] reward;

    public int SytemLevel { get => sytemLevel; }
    public int TimeLimit { get => timeLimit; }
    public int MaxWave { get => maxWave; }
    public float MultiDifficult { get => multiDifficult; }
    public MaterialModeRewardInfo[] Reward { get => reward; }


    [Serializable]
    public class MaterialModeRewardInfo {
        [SerializeField] private ItemClaim[] rewardPerWave;
        [SerializeField] private ItemClaim[] fullReward;
        public ItemClaim[] RewardPerWave { get => rewardPerWave; }
        public ItemClaim[] FullReward { get => fullReward; }
        public void ClaimReward(int wave, bool isWin) {
            if (isWin) {
                for (int i = 0; i < fullReward.Length; i++) {
                    fullReward[i].Claim();
                    GameManager.Instance.AddClaimedItem(fullReward[i].Id, fullReward[i].Amount);
                }
            }
            else {
                for (int i = 0; i < rewardPerWave.Length; i++) {
                    rewardPerWave[i].Claim(wave);
                    GameManager.Instance.AddClaimedItem(rewardPerWave[i].Id, rewardPerWave[i].Amount * wave);
                }
            }
        }
    }
}
[Serializable]
public class MaterialModeWeekData {
    [SerializeField] private MaterialModeWaveData[] data;
    [Header("Preload")]
    [SerializeField] private int[] eIds;
    [SerializeField] private int[] mbIds;
    [SerializeField] private int[] bossIds;
    [SerializeField] private int[] trapIds;

    public MaterialModeWaveData[] Data { get => data; }
    public int[] EIds { get => eIds; }
    public int[] MbIds { get => mbIds; }
    public int[] BossIds { get => bossIds; }
    public int[] TrapIds { get => trapIds; }
}
