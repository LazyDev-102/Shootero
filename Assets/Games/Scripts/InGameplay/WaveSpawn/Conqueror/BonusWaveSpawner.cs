using Gemmob;
using UnityEngine;

public class BonusWaveSpawner : ConquerorWaveSpawner {
    private readonly Vector3 spawnPosition = new Vector3(100, 100, 0);
    private BonusWaveInfo waveInfo;
    private bool isStarted;
    private bool isPaused;
    private Countdowner spawnTimeCountdowner = new Countdowner();
    private Countdowner timeSpawnChestCD = new Countdowner();
    private float timeSpawnReload = 1f;
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
    private void OnEnable() {
        EventDispatcher.Instance.AddListener<EventKey.GameStateChangedParam>(OnGameStateChanged);
    }

    private void OnDisable() {
        EventDispatcher.Instance.RemoveListener<EventKey.GameStateChangedParam>(OnGameStateChanged);
    }
    public override bool IsWinWave() {
        return GameLoader.ChestCount() == 0;
    }

    public void SetWaveInfo(BonusWaveInfo waveInfo) {
        this.waveInfo = waveInfo;
    }


    public override void StartSpawn() {
        isStarted = true;
        isPaused = false;
        spawnTimeCountdowner.StartCountdown(waveInfo.WaveTime);
        timeSpawnChestCD.StartCountdown(timeSpawnReload);
        GameManager.Instance.GameLoader.Ship.ShipAttack.ChangeStateShot(true);
        SpawnChest();
    }

    public override void EndSpawn() {
        isStarted = false;
    }

    private void Update() {
        if (!isStarted || isPaused) {
            return;
        }
        if (IsInTimeSpawn) {
            spawnTimeCountdowner.Countdowning(Time.deltaTime);
            timeSpawnChestCD.Countdowning(Time.deltaTime);
            if (timeSpawnChestCD.IsTimeOut()) {
                SpawnChest();
                timeSpawnChestCD.StartCountdown(timeSpawnReload);
            }
        }
    }
    private void SpawnChest() {
        if (!waveInfo.Spawnable())
            return;
        int maxSpawn = waveInfo.MaxChestInCamera - GameLoader.ChestCount();
        for (int i = 0; i < maxSpawn; ++i) {
            ChestBase chestPrefab = waveInfo.BonusWaveData.GetChest();
            ChestBase newEnemy = GameLoader.SpawnChest(chestPrefab, spawnPosition);
            if (newEnemy) {
                newEnemy.Initialize();
                waveInfo.AddChest();
            }
        }
    }

    public override void OnChangeTypeWave() {
        SoundManager.Instance.StopBackgroundMusic(true, 0.5f, () => {
            SoundManager.Instance.PlayBackgroundIngame(fadein: true, fadeDuration: 0.5f);
        });
    }
    private void OnGameStateChanged(EventKey.GameStateChangedParam param) {
        isPaused = param.gameState == GameState.Pause;
    }
    public override void OnObjectRemove() {
    }
}
