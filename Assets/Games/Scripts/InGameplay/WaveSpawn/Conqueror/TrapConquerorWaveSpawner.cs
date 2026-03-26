
using Gemmob;
using UnityEngine;

public class TrapConquerorWaveSpawner : ConquerorWaveSpawner {
    private readonly Vector3 spawnPosition = new Vector3(100, 100, 0);
    private TrapConquerorWaveInfo waveInfo;
    private bool isStarted;
    private bool isPaused;
    private Countdowner spawnTimeCountdowner = new Countdowner();
    private Countdowner timeSpawnTrapCD = new Countdowner();
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
        return !IsInTimeSpawn;
    }

    public void SetWaveInfo(TrapConquerorWaveInfo waveInfo) {
        this.waveInfo = waveInfo;
    }


    public override void StartSpawn() {
        isStarted = true;
        isPaused = false;
        spawnTimeCountdowner.StartCountdown(waveInfo.WaveTime);
        timeSpawnTrapCD.StartCountdown(0);
        GameManager.Instance.GameLoader.Ship.ShipAttack.ChangeStateShot(false);
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
            timeSpawnTrapCD.Countdowning(Time.deltaTime);
            if (timeSpawnTrapCD.IsTimeOut()) {
                SpawnTrap();
                timeSpawnTrapCD.StartCountdown(waveInfo.TrapWaveData.DeltaTime);
            }
        }
        else {
            EndSpawn();
            controller.CheckWinWave();
        }
    }
    private void SpawnTrap() {
        TrapBase trapPrefab = waveInfo.TrapWaveData.GetTrap();
        TrapBase newTrap = GameLoader.SpawnTrap(trapPrefab, spawnPosition);
        if (newTrap) {
            newTrap.Initialize();
            newTrap.ChangedStatWithMultipler(controller.CurrentZoneData.DifficultMultiplier * controller.CurrentWaveInfo.GetWaveMultipler());
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