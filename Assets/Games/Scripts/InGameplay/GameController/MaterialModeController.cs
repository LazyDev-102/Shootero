
using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialModeController : GameController {
    private MaterialModeWaveInfo[] waveInfoes;
    private int currentWaveIndex;

    private MaterialModeWaveSpawner waveSpawner;

    public MaterialModeWaveInfo CurrentWaveInfo {
        get {
            return waveInfoes[currentWaveIndex];
        }
    }

    public MaterialModeWaveSpawner WaveSpawner { get => waveSpawner; }
    public bool IsCompleted => currentWaveIndex + 1 >= waveInfoes.Length;

    public MaterialModeController(GameManager manager) : base(manager) {

    }

    public override void Initialize() {
        currentWaveIndex = 0;
        waveInfoes = GameResources.Instance.MaterialModeData.GenerateWaves();
        BackgroundManager.Instance.SetBackground(GameResources.Instance.ConquerorData.CurrentZone.Background);
    }

    public override void StartGame() {
        GameResources.Instance.MaterialModeData.OnStartGame();
        gameManager.StartCoroutine(IDelayStartGame());
    }

    private IEnumerator IDelayStartGame() {
        IngameHUD.Instance.Combat.ShowNextWave();
        yield return Yielder.Wait(ConfigIngameData.delayShowNextWaveText);
        StartWave();
    }
    public override void Lose() {
        GameSystem.Common.UI.HUDManager.IgnoreUserInput(true);
        DG.Tweening.DOVirtual.DelayedCall(2f, () => {
            var p = PopupHUD.Instance.Show<MaterialResultPopup>();
            if (p) {
                p.SetWin(false);
                p.SetMaterialContent(currentWaveIndex);
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

    public override void Resume() {
    }

    public override void Revive() {
    }

    public override void Win() {
        GameSystem.Common.UI.HUDManager.IgnoreUserInput(true);
        DG.Tweening.DOVirtual.DelayedCall(2f, () => {
            var p = PopupHUD.Instance.Show<MaterialResultPopup>();
            if (p) {
                p.SetWin(true);
                p.SetMaterialContent(currentWaveIndex);
                p.OnClose(() => {
                    Time.timeScale = 1;
                    SceneLoader.Instance.LoadHomeScene(LoadSceneType.LoadAsyn);
                });
            }
            GameSystem.Common.UI.HUDManager.IgnoreUserInput(false);
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
#if CHEAT
        IngameHUD.Instance.Combat.EnemyLeft(gameManager.GameLoader.EnemyCount());
#endif
        //AddPassExpScore(eInfo);
    }
    private bool checkingEndwave;
    public override void CheckWinWave() {
        if (waveSpawner.IsWinWave() && !checkingEndwave) {
            checkingEndwave = true;
            gameManager.StartCoroutine(EndWave());
        }
    }
    public IEnumerator EndWave() {
        waveSpawner.EndSpawn();
        waveSpawner.enabled = false;
        var combat = IngameHUD.Instance.Combat;
        IngameHUD.Instance.Combat.ShowClearWaveText();
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
        waveSpawner.PreEndActionPlay(NextWave);
    }
    public override void NextWave() {
        gameManager.StartCoroutine(INextWave(0.5f));
        IngameHUD.Instance.GetCombat<MaterialModeCombatPanel>().ShowMaterialModeRewardPerWave();
    }
    private void StartWave() {
        checkingEndwave = false;
        if (!gameManager.IsState(GameState.Playing)) {
            return;
        }
        MaterialModeWaveSpawner newSpawner = CurrentWaveInfo.SetupSpawner(waveSpawner);
        newSpawner.enabled = true;
        if (newSpawner != waveSpawner) {
            newSpawner.OnChangeTypeWave();
        }
        waveSpawner = newSpawner;
        waveSpawner.SetController(this);
        waveSpawner.StartSpawn();
        EventDispatcher.Instance.Dispatch(new EventKey.GameStartWaveParam() {
            currentWaveIndex = currentWaveIndex
        });
    }

    private IEnumerator INextWave(float timeDelay) {
        yield return Yielder.Wait(timeDelay);
        if (gameManager.IsWin()) {
            gameManager.Win();
        }
        else {
            currentWaveIndex++;
            IngameHUD.Instance.Combat.ShowNextWave();
            yield return Yielder.Wait(ConfigIngameData.delayShowNextWaveText);
            yield return new WaitUntil(() => gameManager.IsState(GameState.Playing));
            waveSpawner.PreStartActionPlay(() => StartWave());
        }
    }

    public override int ExpShipNeed(int curLevel) {
        return (curLevel + 1) * 5 + (curLevel - 1) * curLevel;
    }

    public override void AddScore(int score) {
    }
    public override void AddGearDropPoint(Vector2 position, int point) {
        base.AddGearDropPoint(position, point);
    }
    public override void AddPassExpScore(EnemyInfo eInfo) {
        int score = 0;
        try {
            //score = (int)(eInfo.score * CurrentZoneData.DifficultMultiplier / 2);
            GameResources.Instance.Inventory.Add(ConstantItemID.BattlePassProgressId, score);
            EventDispatcher.Instance.Dispatch(EventKey.OnPassExpChanged, score);
        }
        catch {
            return;
        }
    }
    private void ActionOnWin() {

    }
    public float GetMultiDefficult() {
        return GameResources.Instance.MaterialModeData.GetInfo().MultiDifficult * CurrentWaveInfo.WaveData.WaveMultipler;
    }

    public override float GetDifficultMultiple() {
        return CurrentWaveInfo.GetWaveMultipler();
    }
}
