using System.Collections.Generic;
using UnityEngine;

public class DroneShieldEffect : ShipSeflEffect {
    public static string shiledId = "droneShield";

    protected List<DroneBase> drones;
    private FloatStat shieldDurantion;
    private FloatStat shieldCountdown;
    private bool isProtectShield;
    private IntStat hp;
    private FloatStat dodgeRate;

    private Countdowner shieldDurantionCountdowner = new Countdowner();
    private Countdowner shieldCountdownCountdowner = new Countdowner();
    public DroneShieldEffect(ShipBase ship, List<DroneBase> drones, float shieldDurantion, float shieldCountdown, int hp, float dodgeRate, bool isProtectShield) : base(ship) {
        id = shiledId;
        this.drones = drones;
        this.isProtectShield = isProtectShield;
        this.shieldDurantion = new FloatStat(shieldDurantion);
        this.shieldCountdown = new FloatStat(shieldCountdown);
        this.hp = new IntStat(hp);
        this.dodgeRate = new FloatStat(dodgeRate);
    }

    public FloatStat ShieldDurantion { get => shieldDurantion; }
    public FloatStat ShieldCountdown { get => shieldCountdown; }

    public override void EffectTo() {
        foreach (var item in drones) {
            shieldDurantionCountdowner.StartCountdown(shieldDurantion.Value);
            item.DroneHitbox.TurnOnShield(isProtectShield, hp.Value, dodgeRate.Value, shieldCountdown.Value);
        }
    }

    public override void Updating(float deltaTime) {
        if (!isProtectShield)
            return;
        if (shieldCountdownCountdowner.IsTimeOut()) {
            if (shieldDurantionCountdowner.IsCountdowning()) {
                shieldDurantionCountdowner.Countdowning(deltaTime);
                if (shieldDurantionCountdowner.IsTimeOut()) {
                    foreach (var item in drones) {
                        item.DroneHitbox.TurnOffShield(isProtectShield);
                        shieldCountdownCountdowner.StartCountdown(shieldCountdown.Value);
                    }
                }
            }
        }
        else {
            shieldCountdownCountdowner.Countdowning(deltaTime);
            if (shieldCountdownCountdowner.IsTimeOut()) {
                foreach (var item in drones) {
                    item.DroneHitbox.TurnOnShield(isProtectShield, hp.Value, timeReborn: shieldCountdown.Value);
                    shieldDurantionCountdowner.StartCountdown(shieldDurantion.Value);
                }
            }
        }
    }

    protected override void RemoveFrom() {
        ship.ShipSkill.RemoveSelfEffect(this);
    }
    public virtual void ResetDrone(List<DroneBase> drones) {
        this.drones = drones;
    }
    public void EnableReflexShield(float percentDamage) {
        foreach (var item in drones) {
            item.EnableReflexShield(percentDamage);
        }
    }
}
