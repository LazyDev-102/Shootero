using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DroneAttackComponent : MonoBehaviour {
    [SerializeField] protected Transform firePoint;
    protected FloatStat droneAtkSpeed;
    protected FloatStat droneDMPercent;
    protected Countdowner attackCountdowner = new Countdowner();
    protected DroneAttack droneAttack;
    protected GameLoader gameLoader;
    protected bool canAttack;
    public float FireRate {
        get {
            return 1 / droneAttack.DroneBase.DroneStat.FireRate.Value;
        }
    }
    public virtual void Initialize() {
        droneAtkSpeed = new FloatStat();
        droneDMPercent = new FloatStat();
        gameLoader = GameManager.Instance.GameLoader;
    }

    public virtual void PreAttack() {

    }

    public virtual void Updating() {
        //Attack();
    }

    public virtual void SetDroneAttack(DroneAttack droneAttack) {
        this.droneAttack = droneAttack;
    }

    public virtual void Attack() {

    }
    public virtual void AddFireRateModifier(StatModifier fireRate) {
        droneAtkSpeed.AddModifier(fireRate);
    }
    public virtual void SetCanAttack(bool status) {
        canAttack = status;
    }
    protected virtual U ChangingBullet<U>(U bullet) where U : BulletBase {
        bullet.SpeedStat.SetBaseValue(droneAttack.DroneBase.DroneStat.BulletSpeed.Value);
        bullet.Size.AddModifier(new StatModifier(droneAttack.DroneBase.DroneStat.BulletSize.Value, StatModType.PercentAdd));
        bullet.SetHitInfor(droneAttack.DroneBase.DroneStat.GetFinalDamageWeapon, null, droneAttack.DroneBase, droneAttack.DroneBase.DroneStat.CritChance.Value, droneAttack.DroneBase.DroneStat.CritDamage.Value);
        bullet.ChangeSize();
        return bullet;
    }
    protected virtual U ChangingLaserBullet<U>(U bullet) where U : Laser {
        bullet.SetRadiusSize(droneAttack.DroneBase.DroneStat.BulletSize.Value, false);
        bullet.SetInfor(droneAttack.DroneBase.DroneStat.GetFinalDamageWeapon, null, droneAttack.DroneBase, droneAttack.DroneBase.DroneStat.CritChance.Value, droneAttack.DroneBase.DroneStat.CritDamage.Value);
        return bullet;
    }
    public abstract void PreloadIngame();
}
