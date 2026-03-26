

using DG.Tweening;
using Gemmob;
using Helper;
using System;
using UnityEngine;

public class ShipHealth : CharacterHealth {
    [UnityEngine.SerializeField] private PlayerHealthBar playerHealthBar;
    private PlayerHealthBar playerHPBar;
    private ShipBase shipBase;
    protected Action<int> onHpUp;
    protected Action<int> onBloodSucking;
    public ShipBase ShipBase {
        get {
            if (shipBase == null) {
                shipBase = CharacterBase as ShipBase;
            }
            return shipBase;
        }
    }

    public PlayerHealthBar PlayerHPBar { get => playerHPBar; }

    public override void Initalize() {
        base.Initalize();
        if (playerHPBar == null)
            LoadHealthBar();
    }

    public void Revive() {
        ForceChangeCurrentHp(CharacterBase.CharacterStat.MaxHP.Value);
        DOVirtual.DelayedCall(2f, () => playerHPBar.ForceFillBar(1)).SetUpdate(true);
    }

    public void AddHpWithHealingEffect(int hp, bool hasBloodSucking = false, bool hasHpUp = false) {
        int hpExtend = Mathf.CeilToInt(hp * ShipBase.ShipStat.HealingEffect.Value);
        AddHp(hp + hpExtend);
        if (hasBloodSucking)
            onBloodSucking?.Invoke(hp + hpExtend);
        if (hasHpUp)
            onHpUp?.Invoke(hp + hpExtend);

    }
    public virtual void AddHpByPercentWithHealing(float percent) {
        int hp = Mathf.CeilToInt(CharacterBase.CharacterStat.MaxHP.Value * percent);
        int hpExtend = Mathf.CeilToInt(hp * ShipBase.ShipStat.HealingEffect.Value);
        CurrentHp += hpExtend + hp;
        if (percent >= 0)
            TextShowupManager.Instance.ShowHealingText($"+ {hpExtend + hp}", CharacterBase.CharacterMove.MyRigi.position);
        else
            TextShowupManager.Instance.ShowHitText(HitType.Burn, $" {hpExtend + hp}", CharacterBase.CharacterMove.MyRigi.position);
    }
    public override void AddFullHP() {
        int maxHp = ShipBase.ShipStat.MaxHP.Value;
        int hp = maxHp - CurrentHp;
        CurrentHp = maxHp;
        TextShowupManager.Instance.ShowHealingText($"+ {hp}", ShipBase.ShipMove.MyRigi.position);
        onBloodSucking?.Invoke(hp);
    }
    public void AddHp_ModHPMax(int hp) {
        AddHp(hp);
        onHpUp?.Invoke(hp);
    }

    public void LoadHealthBar() {
        if (playerHealthBar) {
            playerHPBar = playerHealthBar.Spawn(CommonHUD.Instance.transform);
            playerHPBar.ForceFillBar(1);
            playerHPBar.SetContentShipHealText($"{ShipBase.ShipHealth.CurrentHp}", true);
            playerHPBar.AddListenerHealthChanged(ShipBase);
            playerHPBar.gameObject.SetActive(false);
            playerHPBar.SetTarget(ShipBase.transform);
        }
    }
    public void AddPlayerTakeHit(PlayerTakeHitEffect playerTakeHitEffect) {
        if (playerHPBar != null)
            playerHPBar.Assign(playerTakeHitEffect);
    }

    public void AddOnHpUp(Action<int> onHpUp) {
        this.onHpUp += onHpUp;
    }

    public void RemoveOnHpUP(Action<int> onHpUp) {
        this.onHpUp -= onHpUp;
    }

    public void AddOnBloodSucking(Action<int> onBloodSucking) {
        this.onBloodSucking += onBloodSucking;
    }

    public void RemoveOnBloodSucking(Action<int> onBloodSucking) {
        this.onBloodSucking -= onBloodSucking;
    }

    public void SeflDestroy() {
        playerHPBar.SelfDestroy();
    }
    private bool isHealHPByPercentLoop;
    private Countdowner healingCowndowner = new Countdowner();
    private float timeDurationLoop;
    private float timeCountdown;
    private float percentHealingLoop;
    private int count = 0;
    protected bool canHeal = true;
    public void StartHealHPByPercentLoop(float timeDuration, float timeCountdown, float percent) {
        count++;
        this.timeDurationLoop = timeDuration;
        this.timeCountdown = timeCountdown;
        this.percentHealingLoop = percent * count;
        healingCowndowner.StartCountdown(timeDuration);
        isHealHPByPercentLoop = true;
        playerHPBar.TurnOnRegenerationMod(true);
        AddHpWithHealingEffect(Mathf.CeilToInt(CharacterBase.CharacterStat.MaxHP.Value * percentHealingLoop));
    }
    public void ResetHealHPByPercentLoop() {
        if (!isHealHPByPercentLoop)
            return;
        isHealHPByPercentLoop = false;
        healingCowndowner.StartCountdown(timeCountdown);
        isHealHPByPercentLoop = true;
    }
    public void ResetHpAttachMaxHp() {
        var maxHp = shipBase.ShipStat.MaxHP.Value;
        if (CurrentHp > maxHp)
            CurrentHp = maxHp;
    }
    public void HealHPByPercentLoopStatus(bool status) {
        canHeal = status;
    }
    public void Lifesteal(int damage) {
        AddHpWithHealingEffect((int)(ShipBase.ShipStat.LifeSteal.Value * damage / 100));
    }
    public override void Updating() {
        base.Updating();
        if (canHeal) {
            if (isHealHPByPercentLoop) {
                if (healingCowndowner.IsTimeOut()) {
                    AddHpWithHealingEffect(Mathf.CeilToInt(CharacterBase.CharacterStat.MaxHP.Value * percentHealingLoop));
                    healingCowndowner.StartCountdown(timeDurationLoop);
                }
                else {
                    healingCowndowner.Countdowning(UnityEngine.Time.deltaTime);
                }
            }
        }
    }
}
