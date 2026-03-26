using DG.Tweening;
using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : SingletonBind<GameManager> {

    public bool isTest;
    public bool IsTrial;

    private int reviveTime;
    private float totalEXP;
    private bool isDroping;
    private Vector2 shipSpawnPosition = new Vector2(0, -15);
    private List<ItemStack> itemClaimedCollector = new List<ItemStack>();
    private ConquerorData conquerorData;

    public List<ItemStack> ItemClaimedCollector { get => itemClaimedCollector; }
    public float TotalEXP { get => totalEXP; }
    public bool IsDroping { get => isDroping; }
    public int ReviveTime { get => reviveTime;}

    private void Start() {
        SetTrial();
        LoadResourceData();
        LoadGameLoader();
        LoadGameMode();
        LoadController();
        LoadCombatPanel();
    }
    private void SetTrial() {
        IsTrial = GameResources.Instance.Ship.Trial;
    }
    private void LoadResourceData() {
        conquerorData = GameResources.Instance.ConquerorData;
    }
    private void LoadGameLoader() {
        gameLoader.Initialize();
    }
    private void LoadGameMode() {
        currentGameMode = IngameData.currentGameMode;
    }
    private void LoadController() {
        gameController = controllerActions[(int)currentGameMode].GetController(this);
        gameController.Initialize();
    }
    private void LoadCombatPanel() {
        if (isTest) {
            DOVirtual.DelayedCall(0.5f, () => {
                var combat = IngameHUD.Instance.Combat;
                combat.Show();
                IngameHUD.Instance.AddActiveFrame(combat);
                StartCoroutine(combat.IEPlayGame(() => {
                    LoadShip();
                    StartGame();
                }));
            });
        }
        else {
            var combat1 = IngameHUD.Instance.Combat;
            combat1.Show();
            IngameHUD.Instance.AddActiveFrame(combat1);
            StartCoroutine(combat1.IEPlayGame(() => {
                LoadShip();
                StartGame();
            }));
        }
    }
    private void LoadShip() {
        ShipBase shipPrefab = GameResources.Instance.Ship.GetCurrentShip().ShipPrefab;
        if (shipPrefab != null) {
            GameLoader.SpawnShip(shipPrefab, shipSpawnPosition);
        }
    }
    public void LoadDrone() {
        (GearSoftData droneGearSoftData1, DroneBase drone1Prefab) = GameResources.Instance.GearInventory.GetDrone1();
        (GearSoftData droneGearSoftData2, DroneBase drone2Prefab) = GameResources.Instance.GearInventory.GetDrone2();

        if (drone1Prefab != null) {
            DroneBase newDrone = GameLoader.SpawnDrone1(drone1Prefab, GameLoader.Ship.DroneLeftPos.position, droneGearSoftData1);
            if (newDrone) {
                newDrone.DroneMove.SetFocus(true);
            }
        }
        if (drone2Prefab != null) {
            DroneBase newDrone = GameLoader.SpawnDrone2(drone2Prefab, GameLoader.Ship.DroneRightPos.position, droneGearSoftData2);
            if (newDrone) {
                newDrone.DroneMove.SetFocus(false);
            }
        }
    }

    public void StartGame() {
        SetState(GameState.Playing);
        EventDispatcher.Instance.Dispatch(new EventKey.OnStartGame());
        SetCanDropPoint();
        reviveTime = 0;
        if (!isTest) {
            this.DelayWait(3f, () => {
                gameController.StartGame();
            });
        }
    }
    private void SetCanDropPoint() {
        bool status = conquerorData.UnlockZone - conquerorData.CurrentZoneIndex < 1;
        if (GameController != null)
            GameController.SetDropable(status);
    }
    public void Lose(bool forceLose = false) {
        if (!IsState(GameState.Win) && !IsState(GameState.Revive) && !IsState(GameState.Playing) && !forceLose) {
            return;
        }
        Time.timeScale = 1;
        if (GameLoader.Ship != null)
            totalEXP = GameLoader.Ship.ShipLevel.TotalEXP;
        gameController.EndSeasonGame();
        SetState(GameState.Lose);
        gameController.Lose();
        if (!conquerorData.IsTut)
            Tracking.Instance.LogLose(IngameHUD.Instance.Combat.GetTime());
    }

    public void Win() {
        if (!IsState(GameState.Lose) && !IsState(GameState.Playing)) {
            return;
        }
        if (!conquerorData.IsTut) {
            Time.timeScale = 1;
            if (GameLoader.Ship != null)
                totalEXP = GameLoader.Ship.ShipLevel.TotalEXP;
            gameController.EndSeasonGame();
            SetState(GameState.Win);
            gameController.Win();
            Tracking.Instance.LogFirstWinZone(IngameHUD.Instance.Combat.GetTime());
        }
        else {
            GameSystem.Common.UI.HUDManager.IgnoreUserInput(true);
            gameLoader.Ship.ShipAttack.ChangeStateShot(false);
            Time.timeScale = 0;
            //gameLoader.Ship.ShipMove.TutorialMoveToTarget();
        }
    }

    public void Pause() {
        if (!IsState(GameState.Playing) && !isTest) {
            return;
        }
        Time.timeScale = 0;
        SetState(GameState.Pause);
        gameController.Pause();
    }

    public void PlayerDie() {
        if (!IsState(GameState.Playing)) {
            return;
        }
        SetState(GameState.Revive);
        this.DelayWait(3, () => {
            Time.timeScale = 0;
            gameController.PlayerDie();
            PopupHUD.Instance.Show<RevivePopup>()
                             .SetReviveTime(reviveTime);
        });
    }

    public void Resume() {
        if (!IsState(GameState.Pause)) {
            return;
        }
        Time.timeScale = 1;
        SetState(GameState.Playing);
        gameController.Resume();
    }

    public void QuitGame() {
        if (!IsState(GameState.Pause)) {
            return;
        }
        if (GameLoader.Ship != null)
            totalEXP = GameLoader.Ship.ShipLevel.TotalEXP;
        Time.timeScale = 1;
        gameController.QuitGame();
    }

    [ContextMenu("Revive")]

    public void Revive() {
        if (!IsState(GameState.Revive)) {
            return;
        }
        reviveTime++;
        GameLoader.Ship.StartRevive();
        Time.timeScale = 1;
        SetState(GameState.Playing);
        
        gameController.Revive();
        CheckWinWave();
    }

    public bool IsWin() {
        if (!IsState(GameState.Playing)) {
            return false;
        }
        return gameController.IsWin();
    }

    public bool IsLose() {
        return gameController.IsLose();
    }
    public void CheckWinWave() {
        if (!IsState(GameState.Playing) && !IsState(GameState.Revive) && !IsState(GameState.Pause)) {
            return;
        }
        gameController.CheckWinWave();
    }

    public void NextWave() {
        if (!IsState(GameState.Playing)) {
            return;
        }
        gameController.NextWave();
    }

    [ContextMenu("Next Game Cheat")]
    public void NextWaveCheat() {
        if (!IsState(GameState.Playing)) {
            return;
        }
        GameLoader.DespawnAllEnemy(true);
        gameController.NextWave();
    }


    public void RemoveEnemy(EnemyInfo eInfo) {
        if (isTest)
            return;

        // game loader remove
        gameController.RemoveEnemy(eInfo);
        CheckWinWave();
    }
    public void RemoveChest() {
        CheckWinWave();
    }
    public void AddClaimedItem(ItemClaim item) {
        foreach (var i in ItemClaimedCollector) {
            if (i.Id == item.Id) {
                i.Amount += item.Amount;
                return;
            }
        }
        ItemClaimedCollector.Add(new ItemStack(item.Id, item.Amount));
    }

    public void AddClaimedItem(int id, int amount) {
        foreach (var i in ItemClaimedCollector) {
            if (i.Id == id) {
                i.Amount += amount;
                return;
            }
        }
        ItemClaimedCollector.Add(new ItemStack(id, amount));
    }
    public void SetDropStatus(bool status) {
        isDroping = status;
    }
    [ContextMenu("EnemyCount")]
    public void EnemyCount() {
        Debug.LogError(GameLoader.EnemyCount());
    }
}

public class EnemyInfo {
    public int score;
}

