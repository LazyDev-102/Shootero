using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;

public class InfinityWaveSpawner : MonoBehaviour {
    private readonly Vector3 spawnPosition = new Vector3(100, 100, 0);
    private readonly int maxPercentSpawnTrap = 20;
    private InfinityWavaInfo waveInfo;
    private float currentDifficultMulti;
    protected InfinityController controller;

    private bool isStarted;
    private bool isPaused;
    private bool isNormal;
    private bool isInSpawn;
    private int enemyAvailableCount;
    private int enemiesDieCounter;
    private InfinityTierType infinityTierType;
    [SerializeField] private Countdowner countdownSpawnEnemies = new Countdowner();
    [SerializeField] private Countdowner chooseEnemyDropCountdowner = new Countdowner();

    private void OnEnable() {
        this.AddListener<EventKey.GameStateChangedParam>(OnGameStateChanged);
    }

    private void OnDisable() {
        EventDispatcher.Instance.RemoveListener<EventKey.GameStateChangedParam>(OnGameStateChanged);
    }

    private GameLoader gameLoader;
    public GameLoader GameLoader {
        get {
            if (gameLoader == null) {
                gameLoader = GameManager.Instance.GameLoader;
            }
            return gameLoader;
        }
    }

    public bool IsWinWave {
        get {
            return !isNormal && GameLoader.EnemyCount() == 0;
        }
    }

    public bool IsWinNormal() {
        return isNormal && !isInSpawn && GameLoader.EnemyCount() == 0;
    }

    public void SetWaveInfo(InfinityWavaInfo waveInfo, float currentDifficultMulti) {
        this.waveInfo = waveInfo;
        this.currentDifficultMulti = currentDifficultMulti;
    }
    public void SetController(InfinityController controller) {
        this.controller = controller;
    }

    public void StartSpawn() {
        isStarted = true;
        isPaused = false;
        isNormal = true;
        isInSpawn = true;
        enemiesDieCounter = 0;
        enemyAvailableCount = 1;
        CalculationDelaySpawnEnemy();
        chooseEnemyDropCountdowner.StartCountdown(1);
        infinityTierType = InfinityTierType.Enemy;
    }

    public void EndSpawn() {
        isStarted = false;
    }

    private void Update() {
        if (!isStarted || isPaused) {
            return;
        }

        if (!isInSpawn) {
            return;
        }
        countdownSpawnEnemies.Countdowning(Time.deltaTime);
        if (countdownSpawnEnemies.IsTimeOut()) {
            //ChooseEnemyDropChip();
            SpawnEnemies();
        }
        chooseEnemyDropCountdowner.Countdowning(Time.deltaTime);
        if (chooseEnemyDropCountdowner.IsTimeOut()) {
            ChooseEnemyDropChip();
            chooseEnemyDropCountdowner.StartCountdown(1);
        }
    }

    private void SpawnEnemies() {
        int limitE = waveInfo.Limit;
        int currentE = GameLoader.EnemyCount();
        int numberEnemyNeedSpawn = limitE - currentE;
        for (int i = 0; i < numberEnemyNeedSpawn; ++i) {
            EnemyBase enemyPrefab = ChooseEnemySpawn(enemyAvailableCount - 1, waveInfo.WaveData.EnemyIds, enemyAvailableCount);
            EnemyBase newEnemy = GameLoader.SpawnEnemy(enemyPrefab, spawnPosition);
            if (newEnemy) {
                newEnemy.ChangedStatWithMultipler(currentDifficultMulti);
                newEnemy.Initialize();
            }
        }
    }

    private EnemyBase ChooseEnemySpawn(int diffcultIndex, int[] enemyIds, int eCount) {
        EnemyType type = GameResources.Instance.InfinityModeData.GetEnemyTypeSpawn(diffcultIndex);
        return GameResources.Instance.EnemyData.GetEnemyBaseRandom(enemyIds, type, controller.CurrentZoneBGIndex, eCount);
    }

    public void OnEnemyDie() {
        enemiesDieCounter++;
        int probabilitySpawnTrap = (int)(currentDifficultMulti * enemiesDieCounter);
        if (probabilitySpawnTrap > maxPercentSpawnTrap)
            probabilitySpawnTrap = maxPercentSpawnTrap;
        if (RandomHelper.RandomWithProbability(probabilitySpawnTrap)) {
            SpawnTrap();
            enemiesDieCounter = 0;
        }
    }

    private void SpawnTrap() {
        TrapBase trapPrefab = ChooseTrapSpawn(enemyAvailableCount - 1, waveInfo.WaveData.TrapIds);
        TrapBase newTrap = GameLoader.SpawnTrap(trapPrefab, spawnPosition);
        if (newTrap) {
            newTrap.ChangedStatWithMultipler(currentDifficultMulti);
            newTrap.Initialize();
        }
    }

    private TrapBase ChooseTrapSpawn(int diffcultIndex, int[] trapIds) {
        EnemyType type = GameResources.Instance.InfinityModeData.GetTrapTypeSpawn(diffcultIndex);
        return GameResources.Instance.EnemyData.GetTrapRandom(trapIds, type);
    }

    public void CalculationDelaySpawnEnemy() {
        if (isInSpawn) {
            int currentE = GameLoader.EnemyCount();
            int limitE = waveInfo.Limit;
            float newDelaySpawn = (1.0f * currentE / limitE) * 3;
            if (!countdownSpawnEnemies.IsTimeOut() && newDelaySpawn < countdownSpawnEnemies.Countdown) {
                countdownSpawnEnemies.StartCountdown(newDelaySpawn);
            }
            else if (countdownSpawnEnemies.IsTimeOut()) {
                countdownSpawnEnemies.StartCountdown(newDelaySpawn);
            }
        }
    }


    private void OnGameStateChanged(EventKey.GameStateChangedParam param) {
        isPaused = param.gameState == GameState.Pause;
    }
    public void SpawnMiniBoss() {
        if (infinityTierType == InfinityTierType.Boss || infinityTierType == InfinityTierType.Miniboss)
            return;
        infinityTierType = InfinityTierType.Miniboss;
        controller.CanAddScore = false;
        int mbId;
        Queue<int> spawnedMbIds = controller.SpawnedMinibossIds;
        do {
            mbId = RandomHelper.RandomInCollection(waveInfo.WaveData.MbIds);
        } while (spawnedMbIds.Contains(mbId));
        if (spawnedMbIds.Count == 3) {
            spawnedMbIds.Dequeue();
        }
        spawnedMbIds.Enqueue(mbId);
        MinibossBase bossPrefab = GameResources.Instance.EnemyData.GetMiniBossByIndex(mbId - 1);
        MinibossBase newMiniBoss = GameLoader.SpawnEnemy(bossPrefab, spawnPosition);
        if (newMiniBoss) {
            newMiniBoss.ChangedStatWithMultipler(currentDifficultMulti);
            newMiniBoss.Initialize();
            newMiniBoss.RemoveAllOnDie();
            newMiniBoss.CanDropChip = true;
            newMiniBoss.AddOnDie(() => {
                if (IngameData.currentGameMode == GameMode.Infinity) {
                    controller.CanAddScore = true;
                    controller.AddScore((int)(100 * currentDifficultMulti));
                    controller.CanAddScore = false;
                    IESpawnBoss();
                }
            });
        }
    }
    private void IESpawnBoss() {
        DG.Tweening.DOVirtual.DelayedCall(2, SpawnBoss);
    }
    public void SpawnBoss() {
        if (infinityTierType == InfinityTierType.Boss)
            return;
        infinityTierType = InfinityTierType.Boss;
        isNormal = false;
        int bossId;
        Queue<int> spawnedBossIds = controller.SpawnedBossIds;
        do {
            bossId = RandomHelper.RandomInCollection(waveInfo.WaveData.BossIds);
        } while (spawnedBossIds.Contains(bossId));
        if (spawnedBossIds.Count == 3) {
            spawnedBossIds.Dequeue();
        }
        spawnedBossIds.Enqueue(bossId);
        //int bossId = RandomHelper.RandomInCollection(waveInfo.WaveData.BossIds);
        BossBase bossPrefab = GameResources.Instance.EnemyData.GetBossByIndex(bossId - 1);
        BossBase newBoss = GameLoader.SpawnEnemy(bossPrefab, spawnPosition);
        if (newBoss != null) {
            newBoss.ChangedStatWithMultipler(currentDifficultMulti);
            newBoss.Initialize();
            newBoss.RemoveAllOnDie();
            newBoss.CanDropChip = true;
            newBoss.AddOnDie(() => {
                if (IngameData.currentGameMode == GameMode.Infinity) {
                    controller.CanAddScore = true;
                    controller.NextWave();
                }
            });
        }
        enemyAvailableCount = 1;
        waveInfo.WaveData.ChooseEnemy();
    }
    private void ChooseEnemyDropChip() {
        List<EnemyBase> enemies = GameLoader.Enemies;
        RandomHelper.Shuffle(enemies);
        for (int i = 0; i < enemies.Count; ++i) {
            EnemyBase e = enemies[i];
            if (e != null && !e.IsDie() && e.EnableDropChip && !e.CanDropChip) {
                e.CanDropChip = true;
                return;
            }
        }
    }
    public void StopSpawn() {

        isInSpawn = false;
    }
    public void EndTurn() {
        enemyAvailableCount++;
        if (enemyAvailableCount > 5) {
            StopSpawn();
        }
        else {
            controller.GenNextScoreNeed();
        }
    }
}
