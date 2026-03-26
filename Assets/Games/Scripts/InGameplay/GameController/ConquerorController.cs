using DG.Tweening;
using Gemmob;
using System.Collections;
using UnityEngine;

public class ConquerorController : GameController {
    private ConquerorWaveInfo[] waveInfoes;
    private int currentWaveIndex;
    private int currentZoneIndex;

    private ConquerorWaveSpawner waveSpawner;
    private ConquerorData conquerorData;
    private TutorialSytemData tutData;

    public ConquerorWaveInfo CurrentWaveInfo {
        get {
            return waveInfoes[currentWaveIndex];
        }
    }
    public ConquerorZoneData CurrentZoneData {
        get {
            return GameResources.Instance.ConquerorData.ZoneDatas[CurrentZoneIndex];
        }
    }

    public int CurrentZoneIndex { get => currentZoneIndex; }
    public bool IsCompleted => currentWaveIndex + 1 >= waveInfoes.Length;

    public ConquerorController(GameManager manager) : base(manager) {

    }

    public override void Initialize() {
        tutData = GameResources.Instance.TutorialSytemData;
        conquerorData = GameResources.Instance.ConquerorData;
        bool trial = GameResources.Instance.Ship.Trial;
        var finishTutorial = tutData.FinishTutorialIntroduce;
        var finishTutorialPlayGame = tutData.FinishTutorialPlayGame;
        conquerorData.IsTut = !finishTutorial;
        conquerorData.IsTutPlayGame = !finishTutorialPlayGame;
        currentWaveIndex = 0;
        currentZoneIndex = IngameData.currentZoneIndex;
        waveInfoes = conquerorData.GenerateWaves(CurrentZoneIndex, !finishTutorial || trial);
        conquerorData.SetCurrentZone(currentZoneIndex);
        BackgroundManager.Instance.SetBackground(CurrentZoneData.Background);
        if (finishTutorial)
            tutData.SetFinishTutorialPlayGame(true);
    }

    #region Tracking
    private void TrackingStartGame() {
        //var gearInv = GameResources.Instance.GearInventory;
        //var drone1Equipable = gearInv.GetDroneLEquipable();
        //var drone2Equipable = gearInv.GetDroneREquipable();
        //var weaponryEquipable = gearInv.GetWeaponryEquipable();
        //var shieldEquipable = gearInv.GetShieldEquipable();
        //var reactorEquipable = gearInv.GetCoreEquipable();
        //var propulsionEquipable = gearInv.GetEngineEquipable();
        //string zone = $"{currentZoneIndex}";
        //string drone1 = drone1Equipable == null ? "-1" : $"{drone1Equipable.GearHardData.Id}";
        //string drone2 = drone2Equipable == null ? "-1" : $"{drone2Equipable.GearHardData.Id}";
        //string weaponry = weaponryEquipable == null ? "-1" : $"{weaponryEquipable.GearHardData.Id}";
        //string shield = shieldEquipable == null ? "-1" : $"{shieldEquipable.GearHardData.Id}";
        //string reactor = reactorEquipable == null ? "-1" : $"{reactorEquipable.GearHardData.Id}";
        //string propulsion = propulsionEquipable == null ? "-1" : $"{propulsionEquipable.GearHardData.Id}";
        //Tracking.Instance.LogStartLevel(zone, drone1, drone2, weaponry, shield, reactor, propulsion);

        conquerorData.CurrentZone.IncNumberPlayBeforeWin();
    }
    private void TrackingFinishGame(bool isWin) {
        //string zone = $"{currentZoneIndex}";
        //string level = $"{currentWaveIndex}";
        //string result = isWin ? "1" : "0";
        //if (!tutData.FinishTutorialEquipment) {
        //    Tracking.Instance.LogTutorialEndLevel(level, result);
        //    return;
        //}
        //var modGen = GameResources.Instance.ModGenerator;
        //var gearInventory = GameResources.Instance.GearInventory;
        //var drone1Equipable = gearInventory.GetDroneLEquipable();
        //var drone2Equipable = gearInventory.GetDroneREquipable();
        //var weaponryEquipable = gearInventory.GetWeaponryEquipable();
        //var shieldEquipable = gearInventory.GetShieldEquipable();
        //var reactorEquipable = gearInventory.GetCoreEquipable();
        //var propulsionEquipable = gearInventory.GetEngineEquipable();
        //string bullet = modGen.CurentPatternMod == null ? "0" : $"{modGen.CurentPatternMod.ModId}";
        //string mods = TrackingModUsing();
        //string drone1 = drone1Equipable == null ? "-1" : $"{drone1Equipable.GearHardData.Id}";
        //string drone2 = drone2Equipable == null ? "-1" : $"{drone2Equipable.GearHardData.Id}";
        //string weaponry = weaponryEquipable == null ? "-1" : $"{weaponryEquipable.GearHardData.Id}";
        //string shield = shieldEquipable == null ? "-1" : $"{shieldEquipable.GearHardData.Id}";
        //string reactor = reactorEquipable == null ? "-1" : $"{reactorEquipable.GearHardData.Id}";
        //string propulsion = propulsionEquipable == null ? "-1" : $"{propulsionEquipable.GearHardData.Id}";
        //string firstTime = conquerorData.FirstTime ? "1" : "0";
        //SetFirstLoseStatus(!isWin);
        //string firstLose = !isWin && conquerorData.FirstLose ? "1" : "0";
        //Tracking.Instance.LogEndLevel(level, result, bullet, mods, zone, drone1, drone2, weaponry, shield, reactor, propulsion, firstTime, firstLose);

    }
    private string TrackingModUsing() {
        var result = "";
        var hardData = GameResources.Instance.ModGenerator.AllMods;
        var shipSkill = GameManager.Instance.GameLoader.Ship.ShipSkill;
        var softData = shipSkill.Mods;
        foreach (var item in hardData) {
            if (!softData.Contains(item)) {
                result += "0,";
            }
            else {
                result += $"{shipSkill.GetCountMod(item)},";
            }
        }
        result.Remove(result.Length - 1, 1);
        return result;
    }
    #endregion

    private void SetFirstLoseStatus(bool status) {
        if (status)
            GameResources.Instance.ConquerorData.SetFirstLoseStatus();
    }
    public override void Lose() {
        conquerorData.CurrentZone.SetHighestWave(currentWaveIndex, conquerorData.IsTut);
        GameResources.Instance.RateUs.SetTrigger(currentZoneIndex, false);
        GameResources.Instance.IapPack.SmartOffer.SetAppearing();
        PopupHUD.Instance.Show<ResultPopup>()
            .SetWin(false)
            .SetWave(currentZoneIndex, currentWaveIndex)
            .OnClose(() => {
                Time.timeScale = 1;
                SceneLoader.Instance.LoadHomeScene(LoadSceneType.LoadAsyn);
            });
        //TrackingFinishGame(false);
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
        if (!gameManager.IsTrial && !conquerorData.IsCurrentZoneHasPass(currentZoneIndex)) {
            conquerorData.SetNextUnlockZone();
        }
        else {
            conquerorData.SetCurrentZone(currentZoneIndex);
        }
        GameResources.Instance.RateUs.SetTrigger(currentZoneIndex, true);
        tutData.SetFirstConditionPlayInfinity(currentZoneIndex >= 4);
        PopupHUD.Instance.Show<ResultPopup>()
            .SetWin(true)
            .SetWave(currentZoneIndex, currentWaveIndex)
            .OnClose(() => {
                Time.timeScale = 1;
                SceneLoader.Instance.LoadHomeScene(LoadSceneType.LoadAsyn);
            });
        //TrackingFinishGame(true);
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
        if (tutData.FinishTutorialIntroduce) {
            conquerorData.SetFirstTimePlay();
            TrackingStartGame();
        }
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
        //if (!GameResources.Instance.TutorialSytemData.FinishTutorialEquipment)
        //    Tracking.Instance.LogTutorialStartWave($"{currentWaveIndex}");
        //else
        //    Tracking.Instance.LogStartWave($"{currentZoneIndex}", $"{currentWaveIndex}");
    }
    private void CreatWave() {
        if (!gameManager.IsState(GameState.Playing) || currentWaveIndex >= waveInfoes.Length) {
            return;
        }
        ConquerorWaveSpawner newSpawner = CurrentWaveInfo.SetupSpawner(waveSpawner);
        newSpawner.enabled = true;
        if (newSpawner != waveSpawner) {
            newSpawner.OnChangeTypeWave();
        }
        waveSpawner = newSpawner;
        waveSpawner.SetController(this);
        waveSpawner.PreStartActionPlay(() => StartWave());

    }
    private IEnumerator EndWave() {
        waveSpawner.EndSpawn();
        waveSpawner.enabled = false;
        var combat = IngameHUD.Instance.Combat;
        var ship = GameManager.Instance.GameLoader.Ship;
        combat.ShowClearWaveText();
        if (gameManager.GameLoader.Ship != null) {
            gameManager.GameLoader.Ship.ShipAttack.ChangeStateShot(false);
        }
        if (ship != null && conquerorData.IsTut && (currentWaveIndex == 1 || currentWaveIndex == 3)) {
            ship.ShipLevel.AddExp(ExpShipNeed(ship.ShipLevel.CurrentLevel));
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
    //public void AfterPause() {
    //    if (IngameHUD.Instance.Combat.PlayerLevelBar.CanChooseMod) {
    //        PopupHUD.Instance.Show<ChooseModPopup>().AddOnComplete(PreNextWave);
    //    }
    //    else
    //        PreNextWave();
    //}
    public override void NextWave() {
        gameManager.StartCoroutine(INextWave(0.5f));
    }


    private IEnumerator INextWave(float timeDelay) {
        yield return Yielder.Wait(timeDelay);
        if (gameManager.IsWin()) {
            gameManager.Win();
        }
        else {
            currentWaveIndex++;
            //if (currentWaveIndex < 24 && !GameResourcesIG.Instance.ConquerorData.IsTut)
            //    currentWaveIndex = 24;
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
    public override void AddPassExpScore(EnemyInfo eInfo) {
        int score = 0;
        try {
            score = (int)(eInfo.score /** GetDifficulty(eInfo.EType)*/ * CurrentZoneData.DifficultMultiplier / 2);
            GameResources.Instance.Inventory.Add(ConstantItemID.BattlePassProgressId, score);
            EventDispatcher.Instance.Dispatch(EventKey.OnPassExpChanged, score);
        }
        catch {
            return;
        }
    }
    private void ActionOnWin() {

    }

    public override float GetDifficultMultiple() {
        return CurrentZoneData.DifficultMultiplier * CurrentWaveInfo.GetWaveMultipler();
    }
}
