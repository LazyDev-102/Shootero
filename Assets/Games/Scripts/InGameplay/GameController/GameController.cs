using DG.Tweening;
using UnityEngine;

public abstract class GameController : IGameControllable, ICheckGameControllable, IActionGameControllable {
    protected GameManager gameManager;
    private const int maxDropPoint = 1000;
    private int numberGain;
    private int dropPoint;
    private bool dropable;

    public int GetChipGain() {
        return numberGain;
    }
    public int GetDropPoint() {
        return dropPoint;
    }
    public void SetDropable(bool status) {
        dropable = status;
    }
    public GameController(GameManager manager) {
        this.gameManager = manager;
    }

    public abstract void Initialize();
    public abstract bool IsLose();
    public abstract bool IsWin();
    public abstract void Lose();
    public abstract void Pause();
    public abstract void PlayerDie();
    public abstract void Resume();
    public abstract void Revive();
    public abstract void StartGame();
    public abstract void Win();
    public abstract void NextWave();
    public abstract void RemoveEnemy(EnemyInfo eInfo);
    public abstract void CheckWinWave();
    public abstract int ExpShipNeed(int curLevel);
    public abstract void AddScore(int score);
    public abstract void AddPassExpScore(EnemyInfo eInfo);
    public abstract float GetDifficultMultiple();
    public virtual void OnLevelUp() {

    }
    public virtual void AddGearDropPoint(Vector2 position, int point) {
        if (!dropable)
            return;
        if (point <= 0)
            return;
        dropPoint += point;
        if (dropPoint > maxDropPoint) {
            dropPoint -= maxDropPoint;
            Gemmob.EventDispatcher.Instance.Dispatch(new EventKey.OnEnoughDropPoint() { Position = position });
        }
    }

    public virtual void EndSeasonGame() {
        ShipBase ship = gameManager.GameLoader.Ship;
        if (ship != null) {
            numberGain = Mathf.CeilToInt(ship.ChipCollection * ship.ShipStat.ChipGain.Value);
            GameResources.Instance.Inventory.Add(ConstantItemID.ChipId, numberGain);
            IngameHUD.Instance.Combat.SkillSystem.Deactive();
            gameManager.StopAllCoroutines();
            gameManager.GameLoader.DespawnEndSeason();
        }
    }

    public virtual void QuitGame() {
        ShipBase ship = gameManager.GameLoader.Ship;
        if (ship != null) {
            numberGain = Mathf.CeilToInt(ship.ChipCollection * ship.ShipStat.ChipGain.Value);
            GameResources.Instance.Inventory.Add(ConstantItemID.ChipId, numberGain);
            IngameHUD.Instance.Combat.SkillSystem.Deactive();
            gameManager.StopAllCoroutines();
            gameManager.GameLoader.DespawnEndSeason();
        }
    }
}
