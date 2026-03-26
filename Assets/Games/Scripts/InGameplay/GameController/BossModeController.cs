
using Gemmob;
using System.Collections;
using UnityEngine;

public class BossModeController : GameController {
    private const float startBossModeMultipler = 1;

    private float currentBossModeMultipler;
    private BossModeWaveInfo currentWave;
    private BossModeWaveSpawner waveSpawner;
    private BossModeData data;
    private BossModeInfo infoData;

    public float CurrentBossModeMultipler { get => currentBossModeMultipler; private set => currentBossModeMultipler = value; }


    public BossModeWaveInfo CurrentWave { get => currentWave; private set => currentWave = value; }



    public BossModeController(GameManager manager) : base(manager) {
    }

    public override void Initialize() {
    }

    public override void StartGame() {
        data = GameResources.Instance.BossModeData;
        infoData = data.GetInfo();
        CurrentBossModeMultipler = startBossModeMultipler;
        CurrentWave = data.GenerateWaves(CurrentWave);
        StartWave();
    }

    public override void CheckWinWave() {
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
            var p = PopupHUD.Instance.Show<BossModeResultPopup>();
            if (p) {
                p.SetWin(false);
                p.SetContent();
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
    }

    public override void Resume() {
    }

    public override void Revive() {
    }

    public override void Win() {
        GameSystem.Common.UI.HUDManager.IgnoreUserInput(true);
        DG.Tweening.DOVirtual.DelayedCall(2f, () => {
            var p = PopupHUD.Instance.Show<BossModeResultPopup>();
            if (p) {
                p.SetWin(true);
                p.SetContent();
                p.OnClose(() => {
                    Time.timeScale = 1;
                    SceneLoader.Instance.LoadHomeScene(LoadSceneType.LoadAsyn);
                });
            }
            GameSystem.Common.UI.HUDManager.IgnoreUserInput(false);
        });
    }

    public override void NextWave() {
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
            waveSpawner = gameManager.GameLoader.Instantiate<BossModeWaveSpawner>("BossMode Wave Spawner");
        }
        waveSpawner.SetWaveInfo(CurrentWave, GetDifficultMultiple());
        waveSpawner.SetController(this);
        waveSpawner.StartSpawn();
        EventDispatcher.Instance.Dispatch(new EventKey.GameStartWaveParam() {
            currentWaveIndex = 0
        });
    }

    public override int ExpShipNeed(int curLevel) {
        return (int)(5 * currentBossModeMultipler);
    }


    public override void AddGearDropPoint(Vector2 position, int point) {
        base.AddGearDropPoint(position, point);
    }
    public override void AddPassExpScore(EnemyInfo eInfo) {

    }
    public override void AddScore(int score) {
    }
    public override float GetDifficultMultiple() {
        return infoData.MultiDifficult * CurrentBossModeMultipler;
    }

}
