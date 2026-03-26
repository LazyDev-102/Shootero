using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretAttackComponent : MonoBehaviour {
    [SerializeField] private Transform firePoint;
    public FloatStat TurretAtkSpeed;
    protected FloatStat TurretDMPercent;
    protected Countdowner attackCountdowner = new Countdowner();
    protected TurretAttack TurretAttack;
    protected GameLoader gameLoader;
    public float FireRate;

    public Transform FirePoint { get => firePoint; }

    public virtual void Initialize() {
        TurretAtkSpeed = new FloatStat();
        TurretDMPercent = new FloatStat();
        gameLoader = GameManager.Instance.GameLoader;
    }

    public virtual void Updating() {
        //Attack();
    }

    public virtual void SetTurretAttack(TurretAttack TurretAttack) {
        this.TurretAttack = TurretAttack;
    }

    public virtual void Attack() {

    }

    public virtual void AddFireRateModifier(FloatStat fireRate) {
        FireRate = fireRate.Value;
    }
}
