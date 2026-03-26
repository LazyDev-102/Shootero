using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityController : GameController {
    private const float startInfinityMultipler = 1;
    private const int startScoreNeed = 100;
    private const int fakeHightesScore = 10;

    private float currentInfinityMultipler;
    private float offsetInfinityMultiple;
    private int currentZoneBGIndex;

    private int scoreNeed;
    private int currentScore;
    private InfinityWavaInfo currentWave;
    private int enemyDefeatForTrapCounter;
    private int currentWaveIndex;
    private int highesScore;
    private int enemyDefeatCounter;
    private Queue<int> spawnedBossIds;
    private Queue<int> spawnedMinibossIds;
    private InfinityWaveSpawner waveSpawner;

    public bool CanAddScore;

    public float CurrentInfinityMultipler { get => currentInfinityMultipler; private set => currentInfinityMultipler = value; }
    public int ScoreNeed { get => scoreNeed; private set => scoreNeed = value; }
    public int CurrentZoneBGIndex { get => currentZoneBGIndex; private set => currentZoneBGIndex = value; }

    public int CurrentScore {
        get => currentScore;
        private set {
            currentScore = value;
            EventDispatcher.Instance.Dispatch<EventKey.ScoreChangedParam>(new EventKey.ScoreChangedParam() {
                score = currentScore,
                scoreNeed = ScoreNeed
            });
            if (CurrentScore >= ScoreNeed) {
                waveSpawner.EndTurn();
            }
        }
    }
    public int CurrentWaveIndex { get => currentWaveIndex; private set => currentWaveIndex = value; }
    public int HighesScore { get => highesScore; private set => highesScore = value; }
    public int EnemyDefeatCounter { get => enemyDefeatCounter; private set => enemyDefeatCounter = value; }
    public Queue<int> SpawnedBossIds { get => spawnedBossIds; private set => spawnedBossIds = value; }
    public Queue<int> SpawnedMinibossIds { get => spawnedMinibossIds; private set => spawnedMinibossIds = value; }
    public InfinityWavaInfo CurrentWave { get => currentWave; private set => currentWave = value; }
    public int EnemyDefeatForTrapCounter { get => enemyDefeatForTrapCounter; private set => enemyDefeatForTrapCounter = value; }

    public InfinityController(GameManager manager) : base(manager) {
        SpawnedBossIds = new Queue<int>();
        SpawnedMinibossIds = new Queue<int>();
    }

    public override void Initialize() {
    }
    public override void OnLevelUp() {
        offsetInfinityMultiple = currentInfinityMultipler;
    }
    public override void StartGame() {
        CanAddScore = true;
        CurrentInfinityMultipler = startInfinityMultipler;
        offsetInfinityMultiple = startInfinityMultipler;
        ScoreNeed = startScoreNeed;
        HighesScore = fakeHightesScore;
        CurrentWave = GameResources.Instance.InfinityModeData.GenerateWave(CurrentWave);
        StartWave();
    }

    public override void CheckWinWave() {
        if (waveSpawner.IsWinNormal()) {
            waveSpawner.SpawnMiniBoss();
        }
    }

    public override bool IsLose() {
        return false;
    }

    public override bool IsWin() {
        return false;
    }

    public override void Lose() {
        GameResources.Instance.RankInfinityData.RankPoint += CalculateRankPoint();
        Logs.Log("Infinity Lose");
        SendData();
        PopupHUD.Instance.Show<ResultPopup>()
            .SetWin(false)
            .InfinitySetContent()
            .OnClose(() => {
                Time.timeScale = 1;
                SceneLoader.Instance.LoadHomeScene(LoadSceneType.LoadAsyn);
            });
    }
    private void SendData() {
        var point = GameResources.Instance.UserProfile.GetHighScore() / 100 > currentScore ? currentScore * 50 : currentScore * 100;
        GameResources.Instance.UserProfile.SetPoint(point);
#if CHEAT || SAVEDATA
        GameLogin.Instance.UploadLeaderBoardData();
#endif
    }
    public override void Pause() {
    }

    public override void PlayerDie() {
    }

    public override void RemoveEnemy(EnemyInfo eInfo) {
        AddScore((int)(eInfo.score * currentInfinityMultipler));
        AddPassExpScore(eInfo);
        waveSpawner.CalculationDelaySpawnEnemy();
        waveSpawner.OnEnemyDie();
    }

    public override void Resume() {
    }

    public override void Revive() {
    }

    public override void Win() {
        GameResources.Instance.RankInfinityData.RankPoint += CalculateRankPoint();
        SendData();
        PopupHUD.Instance.Show<ResultPopup>()
            .SetWin(false)
            .InfinitySetContent()
            .OnClose(() => {
                Time.timeScale = 1;
                SceneLoader.Instance.LoadHomeScene(LoadSceneType.LoadAsyn);
            });
    }

    public override void NextWave() {
        waveSpawner.EndSpawn();
        gameManager.StartCoroutine(INextWave());
    }

    private IEnumerator INextWave() {
        yield return Yielder.Wait(ConfigIngameData.delayNextWave);
        yield return new WaitUntil(() => gameManager.IsState(GameState.Playing));
        CurrentWaveIndex++;
        CurrentInfinityMultipler *= 2f;
        StartWave();
    }
    public void GenNextScoreNeed() {
        ScoreNeed = ScoreNeed + (int)(100 + 500 * (CurrentInfinityMultipler - 1));
    }
    public void AddScoreNeedBeforeDefeatBoss(int value) {
        ScoreNeed += value;
    }
    private void StartWave() {
        if (!gameManager.IsState(GameState.Playing)) {
            return;
        }
        if (waveSpawner == null) {
            waveSpawner = gameManager.GameLoader.Instantiate<InfinityWaveSpawner>("Infinity Wave Spawner");
        }
        waveSpawner.SetWaveInfo(CurrentWave, CurrentInfinityMultipler);
        waveSpawner.SetController(this);
        waveSpawner.StartSpawn();
        EventDispatcher.Instance.Dispatch(new EventKey.GameStartWaveParam() {
            currentWaveIndex = currentWaveIndex
        });
    }

    private int CalculateRankPoint() {
        float n = 0;
        if (CurrentScore >= HighesScore) { // over highest
            n = 1;
        }
        else {
            n = 0.5f;
        }
        int rankPoint = (int)(n * CurrentScore);
        Logs.Log($"get {rankPoint} rank point");
        return rankPoint;
    }

    public override int ExpShipNeed(int curLevel) {
        return (int)(5 * (8 + 2 * offsetInfinityMultiple) * curLevel);
    }

    public override void AddScore(int score) {
        if (CanAddScore)
            CurrentScore += score;
    }
    public override void AddGearDropPoint(Vector2 position, int point) {
        base.AddGearDropPoint(position, point);
    }
    public override void AddPassExpScore(EnemyInfo eInfo) {
        if (eInfo == null)
            return;
        try {
            int score = (int)(eInfo.score * CurrentInfinityMultipler / 20);
            GameResources.Instance.Inventory.Add(ConstantItemID.BattlePassProgressId, score);
            EventDispatcher.Instance.Dispatch(EventKey.OnPassExpChanged, score);
        }
        catch {

        }
    }

    public override float GetDifficultMultiple() {
        return CurrentInfinityMultipler;
    }
}
