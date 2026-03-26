using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;

public class BasicConquerorWaveSpawner : ConquerorWaveSpawner {
    private readonly float chooseEnemyDropDeltaTime = 1.0f;
    private readonly Vector3 spawnPosition = new Vector3(100, 100, 0);
    private BasicConquerorWaveInfo waveInfo;
    private bool isStarted;
    private bool isPaused;
    [SerializeField] private Countdowner spawnTimeCountdowner = new Countdowner();
    [SerializeField] private Countdowner countdownSpawnEnemies = new Countdowner();
    [SerializeField] private Countdowner chooseEnemyDropCountdowner = new Countdowner();


    [SerializeField] private List<float> trapSpawnTimes = new List<float>();
    private int numberTrapSpawned;
    private bool isTutorial;
    private void Awake() {
        isTutorial = !GameResources.Instance.TutorialSytemData.FinishTutorialIntroduce;
    }

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

    public override bool IsWinWave() {
        return !IsInTimeSpawn && GameLoader.EnemyCount() <= 0;
    }

    public void SetWaveInfo(BasicConquerorWaveInfo waveInfo) {
        this.waveInfo = waveInfo;
    }


    public override void StartSpawn() {
        isStarted = true;
        isPaused = false;
        spawnTimeCountdowner.StartCountdown(waveInfo.Time);
        CalculationDelaySpawnEnemy();
        CalculateSpawnTrap();
        chooseEnemyDropCountdowner.StartCountdown(chooseEnemyDropDeltaTime);
        GameManager.Instance.GameLoader.Ship.ShipAttack.ChangeStateShot(true);
    }

    public override void EndSpawn() {
        isStarted = false;
    }

    private void Update() {
        if (!isStarted || isPaused) {
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
        if (waveInfo.BasicWaveData.TrapIds.Length != 0) {
            TrapBase trapPrefab = ChooseTrapSpawn(waveInfo.BasicWaveData.Difficult, waveInfo.BasicWaveData.TrapIds);
            TrapBase newTrap = GameLoader.SpawnTrap(trapPrefab, spawnPosition);

            if (newTrap) {
                if (isTutorial) {
                    newTrap.transform.position = Vector3.up * 30;
                    newTrap.ChangeStatTutorial(6);
                    newTrap.ChangedStatWithMultipler(20);
                }
                else {
                    newTrap.ChangeStatTutorial(1);
                    newTrap.ChangedStatWithMultipler(controller.CurrentZoneData.DifficultMultiplier * controller.CurrentWaveInfo.GetWaveMultipler());
                }
                newTrap.Initialize();
            }
        }
    }

    private TrapBase ChooseTrapSpawn(DifficultWave diffcult, int[] trapIds) {
        //int difficultIndex = (int)diffcult;
        //DifficultSpawnTrap[] difficultPercens = GameResourcesIG.Instance.ConquerorData.TrapDifficultPercents;
        DifficultSpawnTrap difficultPercens = waveInfo.BasicWaveData.TrapDifficultPercents;
        TypeEnemyPercent randomType = RandomHelper.RandomWithPercent(difficultPercens.TypePercents);
        EnemyType type = randomType.Type;
        return GameResources.Instance.EnemyData.GetTrapRandom(trapIds, type);
    }

    private void SpawnEnemies() {
        if (isTutorial && waveInfo.BasicWaveData.EnemyIds.Length == 0)
            return;
        if (SpawnEnemiesTutorial())
            return;
        SpawnEnemiesNormal();

    }
    private bool SpawnEnemiesTutorial() {
        if (isTutorial && waveInfo.BasicWaveData.EnemyIds.Length == 1 && waveInfo.BasicWaveData.EnemyIds[0] == 5) {
            spawnTimeCountdowner.StartCountdown(0);
            EnemyBase enemyPrefab = ChooseEnemySpawnTutorial(waveInfo.BasicWaveData.EnemyIds);
            EnemyBase newEnemy = GameLoader.SpawnEnemy(enemyPrefab, spawnPosition);
            if (newEnemy) {
                newEnemy.ChangedStatWithMultipler(1);
                newEnemy.ChangeStatWithEventValue(1 + waveInfo.AtkPercentEvent, 5 + waveInfo.HpPercentEvent, 1 + waveInfo.SizePercentEvent);
                newEnemy.Initialize();
            }
            return true;
        }
        return false;
    }
    private void SpawnEnemiesNormal() {
        int limitE = waveInfo.Limit;
        int currentE = GameLoader.EnemyCount();
        int numberEnemyNeedSpawn = limitE - currentE;
        for (int i = 0; i < numberEnemyNeedSpawn; ++i) {
            EnemyBase enemyPrefab = ChooseEnemySpawn(waveInfo.BasicWaveData.Difficult, waveInfo.BasicWaveData.EnemyIds);
            EnemyBase newEnemy = GameLoader.SpawnEnemy(enemyPrefab, spawnPosition);
            if (newEnemy) {
                newEnemy.ChangedStatWithMultipler(controller.CurrentZoneData.DifficultMultiplier * waveInfo.GetWaveMultipler());
                newEnemy.ChangeStatWithEventValue(1 + waveInfo.AtkPercentEvent, 1 + waveInfo.HpPercentEvent, 1 + waveInfo.SizePercentEvent);
                newEnemy.Initialize();
            }
        }
    }
    private EnemyBase ChooseEnemySpawn(DifficultWave diffcult, int[] enemyIds) {
        int difficultIndex = (int)diffcult;
        //DifficultSpawnEnemy[] difficultPercens = GameResourcesIG.Instance.ConquerorData.DifficultPercens;
        DifficultSpawnEnemy difficultPercens = waveInfo.BasicWaveData.DifficultPercens;
        TypeEnemyPercent randomType = RandomHelper.RandomWithPercent(difficultPercens.TypePercents);
        EnemyType type = randomType.Type;
        return GameResources.Instance.EnemyData.GetEnemyBaseRandom(enemyIds, type, controller.CurrentZoneIndex);
    }
    private EnemyBase ChooseEnemySpawnTutorial(int[] enemyIds) {
        return GameResources.Instance.EnemyData.GetEnemyBaseRandom(enemyIds, EnemyType.Champion, controller.CurrentZoneIndex);
    }

    public void CalculationDelaySpawnEnemy() {
        if (IsInTimeSpawn) {
            int currentE = GameLoader.EnemyCount();
            int limitE = waveInfo.Limit;
            float newDelaySpawn = (1.0f * currentE / limitE) * 3;
            //float newDelaySpawn = (1.0f - (float)(float)currentE / (float)limitE) * 3;
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
        //int difficultIndex = (int)waveInfo.WaveData.Difficult;
        //int numberTrap = GameResourcesIG.Instance.ConquerorData.TrapDifficultPercents[difficultIndex].LimitTrap;
        int numberTrap = waveInfo.BasicWaveData.TrapDifficultPercents.LimitTrap;
        for (int i = 0; i < numberTrap; ++i) {
            float timeSpawnRandom = Random.Range(2.0f, waveInfo.Time);
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
}
