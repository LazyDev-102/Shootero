
using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;

public class MaterialModeWaveSpawner : MonoBehaviour {
    [SerializeField] private Countdowner spawnTimeCountdowner = new Countdowner();
    [SerializeField] private Countdowner countdownSpawnEnemies = new Countdowner();
    [SerializeField] private Countdowner chooseEnemyDropCountdowner = new Countdowner();
    [SerializeField] private List<float> trapSpawnTimes = new List<float>();

    protected MaterialModeController controller;
    private readonly float chooseEnemyDropDeltaTime = 1.0f;
    private readonly Vector3 spawnPosition = new Vector3(100, 100, 0);
    private MaterialModeWaveInfo waveInfo;
    private bool isStarted;
    private bool isPaused;
    private int numberTrapSpawned;
    private MaterialModeTierType materialModeTierType;

    private List<int> spawnedMinibosses = new List<int>();
    private List<int> spawnedBosses = new List<int>();

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

    public void SetController(MaterialModeController controller) {
        this.controller = controller;
    }
    public bool IsWinWave() {
        bool result = (!IsInTimeSpawn || waveInfo.WaveData.IsMinibossWave || waveInfo.WaveData.IsBossWave) && GameLoader.EnemyCount() <= 0;
        return result;
    }

    public void SetWaveInfo(MaterialModeWaveInfo waveInfo) {
        this.waveInfo = waveInfo;
    }


    public void StartSpawn() {
        materialModeTierType = MaterialModeTierType.Enemy;
        if (waveInfo.WaveData.IsMinibossWave) {
            SpawnMiniBoss();
        }
        else if (waveInfo.WaveData.IsBossWave) {
            SpawnBoss();
        }
        else {
            isStarted = true;
            isPaused = false;
            spawnTimeCountdowner.StartCountdown(waveInfo.Time);
            CalculationDelaySpawnEnemy();
            CalculateSpawnTrap();
            chooseEnemyDropCountdowner.StartCountdown(chooseEnemyDropDeltaTime);
        }
        GameManager.Instance.GameLoader.Ship.ShipAttack.ChangeStateShot(true);
    }

    public void EndSpawn() {
        isStarted = false;
    }

    private void Update() {
        if (!isStarted || isPaused || waveInfo.WaveData.IsMinibossWave || waveInfo.WaveData.IsBossWave) {
            return;
        }
        countdownSpawnEnemies.Countdowning(Time.deltaTime);
        if (countdownSpawnEnemies.IsTimeOut() && IsInTimeSpawn) {
            SpawnEnemies();
        }

        if (IsInTimeSpawn) {
            spawnTimeCountdowner.Countdowning(Time.deltaTime);
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
        else {
            if (IsWinWave()) {
                StartCoroutine(controller.EndWave());
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
        if (waveInfo.WaveData.TrapIds.Length != 0) {
            TrapBase trapPrefab = ChooseTrapSpawn(waveInfo.WaveData.TrapIds);
            TrapBase newTrap = GameLoader.SpawnTrap(trapPrefab, spawnPosition);

            if (newTrap) {
                newTrap.ChangeStatTutorial(1);
                newTrap.ChangedStatWithMultipler(waveInfo.GetWaveMultipler());
                newTrap.Initialize();
            }
        }
    }

    private TrapBase ChooseTrapSpawn(int[] trapIds) {
        DifficultSpawnTrap difficultPercens = waveInfo.WaveData.TrapDifficultPercents;
        TypeEnemyPercent randomType = RandomHelper.RandomWithPercent(difficultPercens.TypePercents);
        EnemyType type = randomType.Type;
        return GameResources.Instance.EnemyData.GetTrapRandom(trapIds, type);
    }

    private void SpawnEnemies() {
        if (waveInfo.WaveData.EnemyIds.Length == 0)
            return;
        SpawnEnemiesNormal();
    }
    private void SpawnEnemiesNormal() {
        int limitE = waveInfo.Limit;
        int currentE = GameLoader.EnemyCount();
        int numberEnemyNeedSpawn = limitE - currentE;
        for (int i = 0; i < numberEnemyNeedSpawn; ++i) {
            EnemyBase enemyPrefab = ChooseEnemySpawn(waveInfo.WaveData.EnemyIds);
            EnemyBase newEnemy = GameLoader.SpawnEnemy(enemyPrefab, spawnPosition);
            if (newEnemy) {
                newEnemy.ChangedStatWithMultipler(waveInfo.GetWaveMultipler());
                newEnemy.Initialize();
            }
        }
    }
    private void SpawnMiniBoss() {
        if (materialModeTierType == MaterialModeTierType.Miniboss)
            return;
        materialModeTierType = MaterialModeTierType.Miniboss;
        int idRandom;
        int loop = 0;
        do {
            loop++;
            idRandom = waveInfo.GetMiniBossId();
        } while (spawnedMinibosses.Contains(idRandom) && loop < 15);
        spawnedMinibosses.Add(idRandom);
        MinibossBase minibossPrefab = GameResources.Instance.EnemyData.GetMiniBossByIndex(idRandom - 1);
        MinibossBase newminiBoss = GameLoader.SpawnEnemy(minibossPrefab, spawnPosition);
        if (newminiBoss) {
            newminiBoss.ChangedStatWithMultipler(controller.GetMultiDefficult());
            newminiBoss.Initialize();
            newminiBoss.CanDropChip = true;
        }
        else {
            GameLoader.SpawnEnemy(GameResources.Instance.EnemyData.GetMiniBossByIndex(0), spawnPosition);
        }
    }
    private void SpawnBoss() {
        if (materialModeTierType == MaterialModeTierType.Boss)
            return;
        materialModeTierType = MaterialModeTierType.Boss;
        int idRandom;
        int loop = 0;
        do {
            loop++;
            idRandom = waveInfo.GetBossId();
        } while (spawnedBosses.Contains(idRandom) && loop < 10);
        spawnedBosses.Add(idRandom);
        BossBase bossPrefab = GameResources.Instance.EnemyData.GetBossByIndex(idRandom - 1);
        BossBase newBoss = GameLoader.SpawnEnemy(bossPrefab, spawnPosition);
        if (newBoss != null) {
            newBoss.ChangedStatWithMultipler(controller.GetMultiDefficult());
            newBoss.Initialize();
            newBoss.CanDropChip = true;
        }
        else {
            GameLoader.SpawnEnemy(GameResources.Instance.EnemyData.GetBossByIndex(0), spawnPosition);
        }
    }
    private EnemyBase ChooseEnemySpawn(int[] enemyIds) {
        DifficultSpawnEnemy difficultPercens = waveInfo.WaveData.DifficultPercens;
        TypeEnemyPercent randomType = RandomHelper.RandomWithPercent(difficultPercens.TypePercents);
        EnemyType type = randomType.Type;
        return GameResources.Instance.EnemyData.GetEnemyBaseRandom(enemyIds, type, GameResources.Instance.ConquerorData.CurrentZoneIndex);
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
        int numberTrap = waveInfo.WaveData.TrapDifficultPercents.LimitTrap;
        for (int i = 0; i < numberTrap; ++i) {
            float timeSpawnRandom = Random.Range(2.0f, waveInfo.Time);
            trapSpawnTimes.Add(timeSpawnRandom);
        }

    }

    public void OnObjectRemove() {
        CalculationDelaySpawnEnemy();
    }

    private void OnGameStateChanged(EventKey.GameStateChangedParam param) {
        isPaused = param.gameState == GameState.Pause;
    }

    public void OnChangeTypeWave() {
        SoundManager.Instance.StopBackgroundMusic(true, 0.5f, () => {
            SoundManager.Instance.PlayBackgroundIngame(fadein: true, fadeDuration: 0.5f);
        });
    }
    public void PreStartActionPlay(System.Action onCompleted) {
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

    public void PreEndActionPlay(System.Action onCompleted) {
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
}
