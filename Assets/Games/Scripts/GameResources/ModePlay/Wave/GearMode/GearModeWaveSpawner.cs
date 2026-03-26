using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GearModeWaveSpawner : MonoBehaviour {
    private readonly Vector3 spawnPosition = new Vector3(100, 100, 0);
    private readonly int maxPercentSpawnTrap = 10;
    private GearModeWaveInfo waveInfo;
    private float currentDifficultMulti;
    protected GearModeController controller;

    private bool isStarted;
    private bool isPaused;
    private bool isNormal;
    private bool isInSpawn;
    private int enemyAvailableCount;
    private int enemiesDieCounter;
    private GearModeTierType gearModeTierType;
    private DifficultSpawnEnemy currentDifficultEnemy;
    private DifficultSpawnTrap currentDifficultTrap;
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

    public void SetWaveInfo(GearModeWaveInfo waveInfo, float currentDifficultMulti) {
        this.waveInfo = waveInfo;
        this.currentDifficultMulti = currentDifficultMulti;
    }
    public void SetController(GearModeController controller) {
        this.controller = controller;
    }

    public void StartSpawn() {
        SetDifficultSpawn(0);
        isStarted = true;
        isPaused = false;
        isNormal = true;
        isInSpawn = true;
        enemiesDieCounter = 0;
        enemyAvailableCount = 1;
        CalculationDelaySpawnEnemy();
        chooseEnemyDropCountdowner.StartCountdown(1);
        gearModeTierType = GearModeTierType.Enemy;
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
            EnemyBase enemyPrefab = ChooseEnemySpawn(currentDifficultEnemy, waveInfo.WaveData.EnemyIds, enemyAvailableCount);
            EnemyBase newEnemy = GameLoader.SpawnEnemy(enemyPrefab, spawnPosition);
            if (newEnemy) {
                newEnemy.ChangedStatWithMultipler(currentDifficultMulti);
                newEnemy.Initialize();
            }
        }
    }

    private EnemyBase ChooseEnemySpawn(DifficultSpawnEnemy diffcult, int[] enemyIds, int eCount) {
        EnemyType type = GameResources.Instance.GearModeData.GetEnemyTypeSpawn(diffcult);
        return GameResources.Instance.EnemyData.GetEnemyBaseRandom(enemyIds, type, controller.CurrentZoneBGIndex, eCount);
    }

    public void OnEnemyDie() {
        enemiesDieCounter++;
        controller.CurrentEnemyDied++;
        int probabilitySpawnTrap = (int)(currentDifficultMulti * enemiesDieCounter);
        if (probabilitySpawnTrap > maxPercentSpawnTrap)
            probabilitySpawnTrap = maxPercentSpawnTrap;
        if (RandomHelper.RandomWithProbability(probabilitySpawnTrap)) {
            SpawnTrap();
            enemiesDieCounter = 0;
        }
    }

    private void SpawnTrap() {
        for (int i = 0; i < currentDifficultTrap.LimitTrap; i++) {
            TrapBase trapPrefab = ChooseTrapSpawn(currentDifficultTrap, waveInfo.WaveData.TrapIds);
            TrapBase newTrap = GameLoader.SpawnTrap(trapPrefab, spawnPosition);
            if (newTrap) {
                newTrap.ChangedStatWithMultipler(currentDifficultMulti);
                newTrap.Initialize();
            }
        }
    }

    private TrapBase ChooseTrapSpawn(DifficultSpawnTrap enemyDiffcult, int[] trapIds) {
        EnemyType type = GameResources.Instance.GearModeData.GetTrapTypeSpawn(enemyDiffcult);
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
        if (gearModeTierType == GearModeTierType.Boss || gearModeTierType == GearModeTierType.Miniboss)
            return;
        gearModeTierType = GearModeTierType.Miniboss;
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
            newMiniBoss.AddOnDie(() => {
                if (IngameData.currentGameMode == GameMode.EventGear)
                    IESpawnBoss();
            });
        }
    }
    private void IESpawnBoss() {
        DG.Tweening.DOVirtual.DelayedCall(2, SpawnBoss);
    }
    public void SpawnBoss() {
        if (gearModeTierType == GearModeTierType.Boss)
            return;
        gearModeTierType = GearModeTierType.Boss;
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
        BossBase bossPrefab = GameResources.Instance.EnemyData.GetBossByIndex(bossId - 1);
        BossBase newBoss = GameLoader.SpawnEnemy(bossPrefab, spawnPosition);
        if (newBoss != null) {
            newBoss.ChangedStatWithMultipler(currentDifficultMulti);
            newBoss.Initialize();
            newBoss.RemoveAllOnDie();
            newBoss.AddOnDie(() => {
                if (IngameData.currentGameMode == GameMode.EventGear) {
                    this.DelayWait(2, () => {
                        GameManager.Instance.Win();
                    });
                }
            });
        }
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
            controller.GenNextEnemyDiedNeed();
            currentDifficultMulti = controller.GetDifficultMultiple();
            SetDifficultSpawn(enemyAvailableCount - 1);
        }
    }
    private void SetDifficultSpawn(int index) {
        var diff = waveInfo.WaveData.DifficultTier;
        if (index >= diff.EnemyPercent.Length)
            return;
        currentDifficultEnemy = diff.EnemyPercent[index];
        currentDifficultTrap = diff.TrapPercent[index];
    }
}
