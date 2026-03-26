using System.Collections.Generic;
using UnityEngine;

public class TurretShieldEffect : ShipSeflEffect {
    public static string shiledId = "turretShield";

    protected List<TurretBase> turrets;
    private FloatStat shieldDurantion;
    private FloatStat shieldCountdown;
    private bool isProtecShield;
    private IntStat hp;
    private FloatStat dodgeRate;

    private Countdowner shieldDurantionCountdowner = new Countdowner();
    private Countdowner shieldCountdownCountdowner = new Countdowner();
    public TurretShieldEffect(ShipBase ship, List<TurretBase> turrets, float shieldDurantion, float shieldCountdown, bool isProtecShield, int hp, float dodgeRate) : base(ship) {
        id = shiledId;
        this.turrets = turrets;
        this.isProtecShield = isProtecShield;
        this.shieldDurantion = new FloatStat(shieldDurantion);
        this.shieldCountdown = new FloatStat(shieldCountdown);
        this.hp = new IntStat(hp);
        this.dodgeRate = new FloatStat(dodgeRate);
    }

    public FloatStat ShieldDurantion { get => shieldDurantion; }
    public FloatStat ShieldCountdown { get => shieldCountdown; }

    public override void EffectTo() {
        foreach (var item in turrets) {
            shieldDurantionCountdowner.StartCountdown(shieldDurantion.Value);
            item.TurretHitbox.TurnOnShield(isProtecShield, hp.Value, dodgeRate.Value, shieldCountdown.Value);
        }
    }

    public override void Updating(float deltaTime) {
        if (!isProtecShield)
            return;
        if (shieldCountdownCountdowner.IsTimeOut()) {
            if (shieldDurantionCountdowner.IsCountdowning()) {
                shieldDurantionCountdowner.Countdowning(deltaTime);
                //ship.ShipHitbox.RotateShield();
                if (shieldDurantionCountdowner.IsTimeOut()) {
                    foreach (var item in turrets) {
                        item.TurretHitbox.TurnOffShield(isProtecShield);
                        shieldCountdownCountdowner.StartCountdown(shieldCountdown.Value);
                    }
                }
            }
        }
        else {
            shieldCountdownCountdowner.Countdowning(deltaTime);
            if (shieldCountdownCountdowner.IsTimeOut()) {
                foreach (var item in turrets) {
                    item.TurretHitbox.TurnOnShield(isProtecShield, hp.Value, dodgeRate.Value, shieldCountdown.Value);
                    shieldDurantionCountdowner.StartCountdown(shieldDurantion.Value);
                }
            }
        }
    }

    protected override void RemoveFrom() {
        ship.ShipSkill.RemoveSelfEffect(this);
    }
    public virtual void ResetTurret(List<TurretBase> turrets, TurretBase newTurret) {
        this.turrets = turrets;
        newTurret.TurretHitbox.TurnOnShield(isProtecShield, hp.Value, dodgeRate.Value, shieldCountdown.Value);
    }
    public void EnableReflexShield(float percentDamage) {
        foreach (var item in turrets) {
            item.EnableReflexShield(percentDamage);
        }
    }
}
