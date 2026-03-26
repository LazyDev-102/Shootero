
using Gemmob;
using Helper;
using System;
using System.Collections.Generic;
using UnityEngine;

public class XmasModeWaveSpawner : ConquerorWaveSpawner {
    private readonly float chooseEnemyDropDeltaTime = 1.0f;
    private readonly Vector3 spawnPosition = new Vector3(100, 100, 0);
    private XmasModeWaveInfo waveInfo;
    private bool isStarted;
    private bool isPaused;
    private int numberTrapSpawned;
    private new XmasModeController controller;
    private XmasTierType XmasTierType;
    private XmasModeWaveData waveData;

    private List<float> trapSpawnTimes = new List<float>();
    private List<int> spawnedMinibosses = new List<int>();
    private List<int> spawnedBosses = new List<int>();
    private Countdowner spawnTimeCountdowner = new Countdowner();
    private Countdowner countdownSpawnEnemies = new Countdowner();
    private Countdowner chooseEnemyDropCountdowner = new Countdowner();
    private Countdowner timeSpawnTrapCD = new Countdowner();

    private void OnEnable() {
        EventDispatcher.Instance.AddListener<EventKey.GameStateChangedParam>(OnGameStateChanged);
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
    private bool IsInTimeSpawn {
        get {
            return !spawnTimeCountdowner.IsTimeOut();
        }
    }

    public void SetController(XmasModeController controller) {
        this.controller = controller;
    }

    public override bool IsWinWave() {
        bool result = (waveInfo.CWaveType == WaveType.Trap && !IsInTimeSpawn) || ((!IsInTimeSpawn || waveData.IsMinibossWave || waveData.IsBossWave) && GameLoader.EnemyCount() <= 0);
        return result;
    }

    public void SetWaveInfo(XmasModeWaveInfo waveInfo) {
        this.waveInfo = waveInfo;
        waveData = waveInfo.XmasWaveData;
    }

    public override void StartSpawn() {
        XmasTierType = XmasTierType.Enemy;
        if (waveData.IsMinibossWave) {
            SpawnMiniBoss();
        }
        else if (waveData.IsBossWave) {
            SpawnBoss();
        }
        else {
            isStarted = true;
            isPaused = false;
            spawnTimeCountdowner.StartCountdown(waveInfo.Time);
            if (waveData.IsEnemyWave) {
                CalculateSpawnTrap();
                CalculationDelaySpawnEnemy();
                chooseEnemyDropCountdowner.StartCountdown(chooseEnemyDropDeltaTime);
            }
        }
        GameManager.Instance.GameLoader.Ship.ShipAttack.ChangeStateShot(true);
    }

    public override void EndSpawn() {
        isStarted = false;
    }

    private void Update() {
        if (!isStarted || isPaused || waveData.IsMinibossWave || waveData.IsBossWave) {
            return;
        }
        spawnTimeCountdowner.Countdowning(Time.deltaTime);
        if (waveData.IsTrapWave) {
            UpdateTrapWave();
        } else {
            UpdateEnemyWave();
        }
    }

    private void UpdateTrapWave() {
        if (IsInTimeSpawn) {
            timeSpawnTrapCD.Countdowning(Time.deltaTime);
            if (timeSpawnTrapCD.IsTimeOut()) {
                SpawnTrap();
                timeSpawnTrapCD.StartCountdown(1);
            }
        } else {
            EndSpawn();
            StartCoroutine(controller.EndWave());
        }
    }

    private void UpdateEnemyWave() {
        countdownSpawnEnemies.Countdowning(Time.deltaTime);
        if (IsInTimeSpawn) {
            if (countdownSpawnEnemies.IsTimeOut() && IsInTimeSpawn) {
                SpawnEnemies();
            }
            chooseEnemyDropCountdowner.Countdowning(Time.deltaTime);
            if (chooseEnemyDropCountdowner.IsTimeOut()) {
                ChooseEnemyDropChip();
                chooseEnemyDropCountdowner.StartCountdown(chooseEnemyDropDeltaTime);
            }
            if (numberTrapSpawned < trapSpawnTimes.Count) {
                if (waveInfo.Time - spawnTimeCountdowner.Countdown >= trapSpawnTimes[numberTrapSpawned]) {
                    SpawnTrap();
                    numberTrapSpawned++;
                }
            }
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

    private void SpawnTrap() {
        if (waveInfo.XmasWaveData.TrapIds.Length != 0) {
            TrapBase trapPrefab = ChooseTrapSpawn(waveInfo.XmasWaveData.TrapIds);
            TrapBase newTrap = GameLoader.SpawnTrap(trapPrefab, spawnPosition);

            if (newTrap) {
                newTrap.ChangedStatWithMultipler(controller.GetDifficultMultiple());
                newTrap.Initialize();
            }
        }
    }

    private TrapBase ChooseTrapSpawn(int[] trapIds) {
        DifficultSpawnTrap difficultPercens = waveInfo.XmasWaveData.TrapDifficultPercents;
        TypeEnemyPercent randomType = RandomHelper.RandomWithPercent(difficultPercens.TypePercents);
        EnemyType type = randomType.Type;
        return GameResources.Instance.Xmas.Prefab.GetTrapRandom(trapIds, type);
    }

    private void SpawnEnemies() {
        if (waveInfo.XmasWaveData.EnemyIds.Length == 0)
            EndSpawn();
        else
            SpawnEnemiesNormal();

    }
    private void SpawnEnemiesNormal() {
        int limitE = waveInfo.Limit;
        int currentE = GameLoader.EnemyCount();
        int numberEnemyNeedSpawn = limitE - currentE;
        for (int i = 0; i < numberEnemyNeedSpawn; ++i) {
            EnemyBase enemyPrefab = ChooseEnemySpawn(waveInfo.XmasWaveData.EnemyIds);
            EnemyBase newEnemy = GameLoader.SpawnEnemy(enemyPrefab, spawnPosition);
            if (newEnemy) {
                newEnemy.ChangedStatWithMultipler(controller.GetDifficultMultiple());
                newEnemy.ChangeStatWithEventValue(1, 1, 1);
                newEnemy.Initialize();
            }
        }
    }
    private EnemyBase ChooseEnemySpawn(int[] enemyIds) {
        DifficultSpawnEnemy difficultPercens = waveInfo.XmasWaveData.DifficultPercens;
        TypeEnemyPercent randomType = RandomHelper.RandomWithPercent(difficultPercens.TypePercents);
        EnemyType type = randomType.Type;
        return GameResources.Instance.Xmas.Prefab.GetEnemyBaseRandom(enemyIds, type);
    }

    private void SpawnMiniBoss() {
        if (XmasTierType == XmasTierType.Miniboss)
            return;
        XmasTierType = XmasTierType.Miniboss;
        int idRandom;
        int loop = 0;
        do {
            loop++;
            idRandom = waveInfo.GetMiniBossId();
        } while (spawnedMinibosses.Contains(idRandom) && loop < 15);
        spawnedMinibosses.Add(idRandom);
        MinibossBase minibossPrefab = GameResources.Instance.Xmas.Prefab.GetMiniBossByIndex(idRandom - 1);
        MinibossBase newminiBoss = GameLoader.SpawnEnemy(minibossPrefab, spawnPosition);
        if (newminiBoss) {
            newminiBoss.ChangedStatWithMultipler(controller.GetDifficultMultiple());
            newminiBoss.Initialize();
            newminiBoss.CanDropChip = true;
        }
        else {
            GameLoader.SpawnEnemy(GameResources.Instance.Xmas.Prefab.GetMiniBossByIndex(0), spawnPosition);
        }
    }

    private void SpawnBoss() {
        if (XmasTierType == XmasTierType.Boss)
            return;
        XmasTierType = XmasTierType.Boss;
        int idRandom;
        int loop = 0;
        do {
            loop++;
            idRandom = waveInfo.GetBossId();
        } while (spawnedBosses.Contains(idRandom) && loop < 10);
        spawnedBosses.Add(idRandom);
        BossBase bossPrefab = GameResources.Instance.Xmas.Prefab.GetBossByIndex(idRandom - 1);
        BossBase newBoss = GameLoader.SpawnEnemy(bossPrefab, spawnPosition);
        if (newBoss != null) {
            newBoss.ChangedStatWithMultipler(controller.GetDifficultMultiple());
            newBoss.Initialize();
            newBoss.CanDropChip = true;
        }
        else {
            GameLoader.SpawnEnemy(GameResources.Instance.Xmas.Prefab.GetBossByIndex(0), spawnPosition);
        }
    }

    public void CalculationDelaySpawnEnemy() {
        if (IsInTimeSpawn) {
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

    private void CalculateSpawnTrap() {
        trapSpawnTimes = new List<float>();
        numberTrapSpawned = 0;
        int numberTrap = waveInfo.XmasWaveData.TrapDifficultPercents.LimitTrap;
        for (int i = 0; i < numberTrap; ++i) {
            float timeSpawnRandom = UnityEngine.Random.Range(1.0f, waveInfo.Time/2);
            trapSpawnTimes.Add(timeSpawnRandom);
        }

    }

    public override void OnObjectRemove() {
        CalculationDelaySpawnEnemy();
    }

    private void OnGameStateChanged(EventKey.GameStateChangedParam param) {
        isPaused = param.gameState == GameState.Pause;
    }

    public override void OnChangeTypeWave() {
        SoundManager.Instance.StopBackgroundMusic(true, 0.5f, () => {
            SoundManager.Instance.PlayBackgroundIngame(fadein: true, fadeDuration: 0.5f);
        });
    }

    public override void PreEndActionPlay(Action onCompleted) {
        WaveCondition[] preEndCondition = controller.CurrentWaveInfo.WaveData.PreEndCondition;
        if (preEndCondition == null) {
            onCompleted?.Invoke();
            return;
        }
        ShipBase ship = GameManager.Instance.GameLoader.Ship;
        if (ship == null) {
            onCompleted?.Invoke();
            return;
        }
        for (int i = 0; i < preEndCondition.Length; i++) {
            if (preEndCondition[i] != null) {
                if (preEndCondition[i].Action(ship, onCompleted)) {
                    return;
                }
            }
        }
        onCompleted?.Invoke();
    }
    public override void PreStartActionPlay(Action onCompleted) {
        WaveCondition[] preStartCondition = controller.CurrentWaveInfo.WaveData.PreStartCondition;
        if (preStartCondition == null) {
            onCompleted?.Invoke();
            return;
        }
        ShipBase ship = GameManager.Instance.GameLoader.Ship;
        for (int i = 0; i < preStartCondition.Length; i++) {
            if (preStartCondition[i] != null) {
                if (preStartCondition[i].Action(ship, onCompleted))
                    return;
            }
        }
        onCompleted?.Invoke();
    }
}
