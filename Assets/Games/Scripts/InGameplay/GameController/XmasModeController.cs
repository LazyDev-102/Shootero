
using Gemmob;
using System.Collections;
using UnityEngine;

public class XmasModeController : GameController {
    private XmasModeWaveInfo[] waveInfoes;
    private int currentWaveIndex;
    private int currentZoneIndex;

    private XmasModeWaveSpawner waveSpawner;
    private XmasModeData XmasData;

    public XmasModeWaveInfo CurrentWaveInfo => waveInfoes[currentWaveIndex];
    public XmasModeWaveData CurrentWaveData => CurrentWaveInfo.XmasWaveData;

    public bool IsCompleted => currentWaveIndex + 1 >= waveInfoes.Length;

    public XmasModeController(GameManager manager) : base(manager) {
    }

    public override void Initialize() {
        XmasData = GameResources.Instance.Xmas;
        currentWaveIndex = 0;
        waveInfoes = XmasData.GenerateWaves();
        BackgroundManager.Instance.SetBackground(XmasData.Background);
    }


    public override void Pause() {
    }

    public override void PlayerDie() {
    }

    public override void Resume() {
    }

    public override void Revive() {
    }

    public override void Lose() {
        EventDispatcher.Instance.Dispatch(EventKey.XmasComplete);
        PopupHUD.Instance.Show<XmasResultPopup>()
            .SetWin(false)
            //.SetWave(currentZoneIndex, currentWaveIndex)
            .OnClose(() => {
                Time.timeScale = 1;
                SceneLoader.Instance.LoadHomeScene(LoadSceneType.LoadAsyn, onFadeOut: ()=> {
                    ToolbarScaler.Instance.ShowModesPanel();
                    PanelHUD.Instance.Show<XmasPanel>();
                    });
            });
    }

    public override void Win() {
        EventDispatcher.Instance.Dispatch(EventKey.XmasComplete);
        EventDispatcher.Instance.Dispatch(EventKey.XmasCompleteWin);
        GameResources.Instance.RateUs.SetTrigger(currentZoneIndex, true);
        PopupHUD.Instance.Show<XmasResultPopup>()
            .SetWin(true)
            //.SetWave(currentZoneIndex, currentWaveIndex)
            .OnClose(() => {
                Time.timeScale = 1;
                SceneLoader.Instance.LoadHomeScene(LoadSceneType.LoadAsyn, onFadeOut: () => {
                    ToolbarScaler.Instance.ShowModesPanel();
                    PanelHUD.Instance.Show<XmasPanel>();
                });
            });
    }

    public override bool IsLose() {
        return false;
    }

    public override bool IsWin() {
        return IsCompleted;
    }

    public override void RemoveEnemy(EnemyInfo eInfo) {
        waveSpawner.OnObjectRemove();
        if (!gameManager.IsTrial)
            AddPassExpScore(eInfo);
#if CHEAT
        IngameHUD.Instance.Combat.EnemyLeft(gameManager.GameLoader.EnemyCount());
#endif
    }

    public override void CheckWinWave() {
        if (waveSpawner.IsWinWave()) {
            gameManager.StartCoroutine(EndWave());
        }
    }

    public override void StartGame() {
        gameManager.StartCoroutine(IDelayStartGame());
    }

    private IEnumerator IDelayStartGame() {
        IngameHUD.Instance.Combat.ShowNextWave();
        yield return Yielder.Wait(ConfigIngameData.delayShowNextWaveText);
        CreatWave();
    }

    private void StartWave() {
        waveSpawner.StartSpawn();
        EventDispatcher.Instance.Dispatch(new EventKey.GameStartWaveParam() {
            currentWaveIndex = currentWaveIndex
        });
    }

    private void CreatWave() {
        if (!gameManager.IsState(GameState.Playing) || currentWaveIndex >= waveInfoes.Length) {
            return;
        }
        XmasModeWaveSpawner newSpawner = CurrentWaveInfo.SetupSpawner(waveSpawner) as XmasModeWaveSpawner;
        newSpawner.enabled = true;
        if (newSpawner != waveSpawner) {
            newSpawner.OnChangeTypeWave();
        }
        waveSpawner = newSpawner;
        waveSpawner.SetController(this);
        waveSpawner.PreStartActionPlay(() => StartWave());

    }

    public IEnumerator EndWave() {
        waveSpawner.EndSpawn();
        waveSpawner.enabled = false;
        var combat = IngameHUD.Instance.Combat;
        var ship = GameManager.Instance.GameLoader.Ship;
        combat.ShowClearWaveText();
        if (gameManager.GameLoader.Ship != null) {
            gameManager.GameLoader.Ship.ShipAttack.ChangeStateShot(false);
        }
        float timeDelay = GameManager.Instance.IsDroping ? ConfigIngameData.delayNextWave * 2 : ConfigIngameData.delayNextWave;
        yield return Yielder.Wait(timeDelay);
        if (combat != null && combat.PlayerLevelBar.CanChooseMod) {
            PopupHUD.Instance.Show<ChooseModPopup>().AddOnComplete(PreNextWave);
        }
        else
            PreNextWave();
    }

    private void PreNextWave() {
        if (!waveSpawner.IsWinWave())
            return;
        waveSpawner.PreEndActionPlay(NextWave);
    }

    public override void NextWave() {
        gameManager.StartCoroutine(INextWave(0.5f));
    }

    private IEnumerator INextWave(float timeDelay) {
        yield return Yielder.Wait(timeDelay);
        if (gameManager.IsWin()) {
            yield return Yielder.Wait(2);
            gameManager.Win();
        }
        else {
            currentWaveIndex++;
            IngameHUD.Instance.Combat.ShowNextWave();
            yield return Yielder.Wait(ConfigIngameData.delayShowNextWaveText);
            yield return new WaitUntil(() => gameManager.IsState(GameState.Playing));
            CreatWave();
        }
    }

    public override int ExpShipNeed(int curLevel) {
        return (curLevel + 1) * 5 + (curLevel - 1) * (currentZoneIndex + 1) * curLevel;
    }

    public override void AddScore(int score) {
    }
    public override void AddGearDropPoint(Vector2 position, int point) {
        base.AddGearDropPoint(position, point);
    }

    public override float GetDifficultMultiple() {
        return CurrentWaveInfo.GetWaveMultipler() * XmasData.MultiDifficult;
    }

    public override void AddPassExpScore(EnemyInfo eInfo) {

    }
}
