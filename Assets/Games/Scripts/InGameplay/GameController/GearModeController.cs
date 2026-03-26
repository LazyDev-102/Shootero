
using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearModeController : GameController {
    private const float startGearModeMultipler = 1;

    private float currentGearModeMultipler;
    private int currentZoneBGIndex;
    private int enemyDiedNeed;
    private int currentEnemyDied;
    private GearModeWaveInfo currentWave;
    private Queue<int> spawnedBossIds;
    private Queue<int> spawnedMinibossIds;
    private GearModeWaveSpawner waveSpawner;
    private GearModeData data;
    private GearModeInfo infoData;
    private int eNeedIndex;

    public float CurrentGearModeMultipler { get => currentGearModeMultipler; private set => currentGearModeMultipler = value; }
    public int CurrentZoneBGIndex { get => currentZoneBGIndex; private set => currentZoneBGIndex = value; }


    public Queue<int> SpawnedBossIds { get => spawnedBossIds; private set => spawnedBossIds = value; }
    public Queue<int> SpawnedMinibossIds { get => spawnedMinibossIds; private set => spawnedMinibossIds = value; }
    public GearModeWaveInfo CurrentWave { get => currentWave; private set => currentWave = value; }
    public int EnemyDiedNeed { get => enemyDiedNeed; private set => enemyDiedNeed = value; }

    public int CurrentEnemyDied {
        get => currentEnemyDied;
        set {
            currentEnemyDied = value;
            if (currentEnemyDied >= EnemyDiedNeed) {
                waveSpawner.EndTurn();
            }
        }
    }

    public GearModeController(GameManager manager) : base(manager) {
        SpawnedBossIds = new Queue<int>();
        SpawnedMinibossIds = new Queue<int>();
    }

    public override void Initialize() {
    }

    public override void StartGame() {
        eNeedIndex = 0;
        data = GameResources.Instance.GearModeData;
        infoData = data.GetInfo();
        enemyDiedNeed = infoData.EnemyNeed[eNeedIndex];
        CurrentGearModeMultipler = startGearModeMultipler;
        CurrentWave = data.GenerateWaves(CurrentWave);
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
        GameSystem.Common.UI.HUDManager.IgnoreUserInput(true);
        if (gameManager.GameLoader.Ship != null)
            gameManager.GameLoader.Ship.ShipAttack.ChangeStateShot(false);
        DG.Tweening.DOVirtual.DelayedCall(1f, () => {
            var p = PopupHUD.Instance.Show<GearModeResultPopup>();
            if (p) {
                p.SetWin(false);
                p.SetGearContent();
                p.OnClose(() => {
                    Time.timeScale = 1;
                    SceneLoader.Instance.LoadHomeScene(LoadSceneType.LoadAsyn);
                });
            }
            GameSystem.Common.UI.HUDManager.IgnoreUserInput(false);

        });
    }
    public override void Pause() {
    }

    public override void PlayerDie() {
    }

    public override void RemoveEnemy(EnemyInfo eInfo) {
        waveSpawner.CalculationDelaySpawnEnemy();
        waveSpawner.OnEnemyDie();
#if CHEAT
        IngameHUD.Instance.Combat.EnemyLeft(gameManager.GameLoader.EnemyCount());
#endif
    }

    public override void Resume() {
    }

    public override void Revive() {
    }

    public override void Win() {
        GameSystem.Common.UI.HUDManager.IgnoreUserInput(true);
        DG.Tweening.DOVirtual.DelayedCall(2f, () => {
            var p = PopupHUD.Instance.Show<GearModeResultPopup>();
            if (p) {
                p.SetWin(true);
                p.SetGearContent();
                p.OnClose(() => {
                    Time.timeScale = 1;
                    SceneLoader.Instance.LoadHomeScene(LoadSceneType.LoadAsyn);
                });
            }
            GameSystem.Common.UI.HUDManager.IgnoreUserInput(false);
        });
    }

    public override void NextWave() {
        waveSpawner.EndSpawn();
        gameManager.StartCoroutine(INextWave());
    }

    private IEnumerator INextWave() {
        yield return Yielder.Wait(ConfigIngameData.delayNextWave);
        yield return new WaitUntil(() => gameManager.IsState(GameState.Playing));
        StartWave();
    }

    private void StartWave() {
        if (!gameManager.IsState(GameState.Playing)) {
            return;
        }
        if (waveSpawner == null) {
            waveSpawner = gameManager.GameLoader.Instantiate<GearModeWaveSpawner>("GearMode Wave Spawner");
        }
        waveSpawner.SetWaveInfo(CurrentWave, GetDifficultMultiple());
        waveSpawner.SetController(this);
        waveSpawner.StartSpawn();
        EventDispatcher.Instance.Dispatch(new EventKey.GameStartWaveParam() {
            currentWaveIndex = 0
        });
    }

    public void GenNextEnemyDiedNeed() {
        eNeedIndex++;
        enemyDiedNeed = infoData.EnemyNeed[eNeedIndex];
        currentEnemyDied = 0;
        CurrentGearModeMultipler *= 1.7f;
    }

    public override int ExpShipNeed(int curLevel) {
        return (int)(5 * (8 + 2 * currentGearModeMultipler) * curLevel);
    }


    public override void AddGearDropPoint(Vector2 position, int point) {
        base.AddGearDropPoint(position, point);
    }
    public override void AddPassExpScore(EnemyInfo eInfo) {

    }
    public override void AddScore(int score) {
    }
    public override float GetDifficultMultiple() {
        return infoData.MultiDifficult * CurrentGearModeMultipler;
    }

}
