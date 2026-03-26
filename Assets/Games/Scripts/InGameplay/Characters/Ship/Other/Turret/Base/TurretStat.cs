using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStat : CharacterStat {
    private TurretBase turretBase;
    public TurretBase TurretBase {
        get {
            if (turretBase == null) {
                turretBase = CharacterBase as TurretBase;
            }
            return turretBase;
        }
    }

    [SerializeField]

    private int damageInit;
    private int hpInit;
    private float fireRateInit;



    public int GetFinalDamageWeapon {
        get {
            return Atk.Value;
        }
    }

    public override void Initialize() {
        base.Initialize();
        Atk.SetBaseValue(damageInit, true);
        MaxHP.SetBaseValue(hpInit, true);
    }
    public void AddModifier(int damage, int hp, float fireRate) {
        this.damageInit = damage;
        this.hpInit = hp;
        this.fireRateInit = fireRate;
    }
    public void AddMoreModifier(StatModifier damage, StatModifier hp, float fireRate) {
        Atk.AddModifier(damage);
        MaxHP.AddModifier(hp);
        fireRateInit = fireRate;
    }
    public void AddHPModifier(StatModifier hp) {
        MaxHP.AddModifier(hp);
    }
    public void AddMoreModifier(float fireRate, bool reset = false) {
        fireRateInit = reset ? fireRate : fireRate + fireRateInit;
    }
    public void AddAtkModifier(StatModifier damage) {
        Atk.AddModifier(damage);
    }
}
