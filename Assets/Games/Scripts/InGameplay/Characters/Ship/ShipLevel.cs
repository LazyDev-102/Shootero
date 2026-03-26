

using DG.Tweening;
using System;
using UnityEngine;

public class ShipLevel : MonoBehaviour {
    private ShipBase shipBase;
    public ShipBase ShipBase {
        get {
            if (shipBase == null) {
                shipBase = GetComponent<ShipBase>();
            }
            return shipBase;
        }
    }
    [SerializeField] private int startLevel;
    [SerializeField] private int currentLevel;
    [SerializeField] private float currentEXP;
    [SerializeField] private int currentUpgradeLevel;
    [SerializeField] private float totalEXP;



    private Action<float> onNumberExpChanged;
    private Action<float> onPercentExpChanged;
    private Action<int> onLevelChanged;
    private Action onLeveling;

    private bool isLeveling;

    private int startPatternAbility = 0;

    public int CurrentLevel { get => currentLevel; }
    public float CurrentEXP { get => currentEXP; }
    public int CurrentUpgradeLevel { get => currentUpgradeLevel; set => currentUpgradeLevel = value; }
    public int UpgradePoint { get => (currentLevel - currentUpgradeLevel); }
    public bool HasUpgradePoint { get => UpgradePoint > 0; }

    public bool HasMustChooseAttackMod { get => CurrentUpgradeLevel == startLevel - startPatternAbility; }
    public float TotalEXP { get => totalEXP; }

    public void Initalize() {
        CurrentUpgradeLevel = startLevel;
        currentLevel = startLevel;
        startPatternAbility = 0;
    }

    public void Updating() {

    }

    public void Destroy() {

    }

    public void Revive() {

    }

    public int ExpNeedNextLevel() {
        return GameManager.Instance.GameController.ExpShipNeed(CurrentLevel);
    }

    private void StartLeveluping() {
        isLeveling = true;
        currentLevel++;
        onLeveling?.Invoke();

    }

    public void EnableAbilityStartPattern() {
        startPatternAbility = 1;
    }

    public void AddExp(float exp) {
        float curExp = currentEXP;
        float expAdd = exp * (1 + ShipBase.ShipStat.ExpGain.Value);
        curExp += expAdd;
        totalEXP += expAdd;
        float expNeedNextLevel = ExpNeedNextLevel();
        if (curExp >= expNeedNextLevel) {
            StartLeveluping();
            curExp -= expNeedNextLevel;
        }
        currentEXP = curExp;
        if (!isLeveling) {
            onNumberExpChanged?.Invoke(currentEXP);
            onPercentExpChanged?.Invoke(1.0f * currentEXP / ExpNeedNextLevel());
        }
    }

    public void LevelUp() {
        onLevelChanged?.Invoke(currentLevel);
        ShipStat shipStat = ShipBase.ShipStat;
        ModChangeOnLevelup();
        AddDamageOnLevelUpFromStat(shipStat);
        AddHpOnLevelUpFromStat(shipStat);
        ShowChooseMod();
        DispathEvent();
    }
    private void ModChangeOnLevelup() {
        foreach (var mod in ShipBase.ShipSkill.LevelupMods) {
            mod.ActionLevelup(ShipBase);
        }
    }
    private void AddHpOnLevelUpFromStat(ShipStat shipStat) {
        int hpAdd = ShipBase.ShipStat.HpPerLevel.Value;
        if (hpAdd > 0) {
            shipStat.MaxHP.AddModifier(new StatModifier(hpAdd, StatModType.Flat));
            ShipBase.ShipHealth.AddHp(hpAdd);
        }
    }
    private void AddDamageOnLevelUpFromStat(ShipStat shipStat) {
        int atkAddPerLvl = shipStat.AttackPerLevel.Value;
        if (atkAddPerLvl > 0) {
            shipStat.DamageExtend += atkAddPerLvl;
        }
    }
    private void ShowChooseMod() {
        if (!ShipBase.IsDie()) {
            SoundManager.Instance.PlayLevelup();
            IngameHUD.Instance.Combat.PlayerLevelBar.SetNumberLevelUp(onComplete: EndLeveluping);
            if (IngameData.currentGameMode == GameMode.Infinity || IngameData.currentGameMode == GameMode.EventGear || IngameData.currentGameMode == GameMode.EventBoss) {
                DOVirtual.DelayedCall(0.5f, () => PopupHUD.Instance.Show<ChooseModPopup>()).SetUpdate(true);
            }
            GameManager.Instance.GameController.OnLevelUp();
        }
    }
    private void DispathEvent() {
        Gemmob.EventDispatcher.Instance.Dispatch(new EventKey.OnShipLevelUpInGame() { Ship = ShipBase });
    }

    public void EndLeveluping() {
        if (currentLevel != 1) {
            isLeveling = false;
            onNumberExpChanged?.Invoke(currentEXP);
            onPercentExpChanged?.Invoke(1.0f * currentEXP / ExpNeedNextLevel());
        }
    }

    #region Action Listener
    public void AddOnPercentExpChanged(Action<float> onExpChanged) {
        this.onPercentExpChanged += onExpChanged;
    }

    public void RemoveOnPercentExpChanged(Action<float> onExpChanged) {
        this.onPercentExpChanged -= onExpChanged;
    }

    public void AddOnExpChanged(Action<float> onExpChanged) {
        this.onNumberExpChanged += onExpChanged;
    }

    public void RemoveOnExpChanged(Action<float> onExpChanged) {
        this.onNumberExpChanged -= onExpChanged;
    }

    public void AddOnLevelChanged(Action<int> onLevelChanged) {
        this.onLevelChanged += onLevelChanged;
    }

    public void RemoveOnLevelChanged(Action<int> onLevelChanged) {
        this.onLevelChanged -= onLevelChanged;
    }

    public void AddOnLeveling(Action onLeveling) {
        this.onLeveling += onLeveling;
    }

    public void RemoveOnLeveling(Action onLeveling) {
        this.onLeveling -= onLeveling;
    }
    #endregion

#if UNITY_EDITOR
    [SerializeField] private int addExp;

    [ContextMenu("Add EXP")]
    private void TestAddExp() {
        AddExp(addExp);
    }
#endif
}
