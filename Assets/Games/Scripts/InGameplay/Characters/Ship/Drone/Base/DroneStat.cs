using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneStat : CharacterStat {
    private DroneBase droneBase;
    public DroneBase DroneBase {
        get {
            if (droneBase == null) {
                droneBase = CharacterBase as DroneBase;
            }
            return droneBase;
        }
    }

    public int GetFinalDamageWeapon {
        get {
            return (int)(Atk.Value * (1+damagePercent.Value));
        }
    }
    public int GetFinalHPWeapon {
        get {
            return (int)(MaxHP.Value * (1 + hpPercent.Value));
        }
    }
    [SerializeField] private FloatStat fireRate;
    [SerializeField] private FloatStat damagePercent;
    [SerializeField] private FloatStat hpPercent;
    [SerializeField] private FloatStat rebornCooldown;
    [SerializeField] private IntStat critChance;
    [SerializeField] private FloatStat critDamage;
    [SerializeField] private FloatStat damageReduce;
    [SerializeField] private FloatStat bulletSpeed;
    [SerializeField] private FloatStat bulletSize;
    [SerializeField] private IntStat evasion;
    [SerializeField] private FloatStat burnDamagePercent;
    [SerializeField] private FloatStat burnDurationPercent;
    [SerializeField] private IntStat burnStack;
    [SerializeField] private FloatStat blastDamagePercent;
    [SerializeField] private FloatStat blastRadiusPercent;
    [SerializeField] private FloatStat blockDamage;
    [SerializeField] private FloatStat blockProbibility;

    [SerializeField] private int damageInit;
    [SerializeField] private int hpInit;
    [SerializeField] private float fireRateInit;
    private float cooldownTime;
    public FloatStat DamagePercent { get => damagePercent; }
    public FloatStat HpPercent { get => hpPercent; }
    public FloatStat RebornCooldown { get => rebornCooldown; }
    public FloatStat FireRate { get => fireRate; }
    public IntStat CritChance { get => critChance; }
    public FloatStat CritDamage { get => critDamage; }
    public FloatStat DamageReduce { get => damageReduce; }
    public FloatStat BulletSpeed { get => bulletSpeed; }
    public FloatStat BulletSize { get => bulletSize; }
    public IntStat Evasion { get => evasion; }
    public FloatStat BurnDamagePercent { get => burnDamagePercent; }
    public FloatStat BurnDurationPercent { get => burnDurationPercent; }
    public IntStat BurnStack { get => burnStack; }
    public FloatStat BlastDamagePercent { get => blastDamagePercent; }
    public FloatStat BlastRadiusPercent { get => blastRadiusPercent; }
    public FloatStat BlockDamage { get => blockDamage; }
    public FloatStat BlockProbibility { get => blockProbibility; }
    public float FireRateInit { get => fireRateInit; }

    public override void Initialize() {
        base.Initialize();
        Atk.SetBaseValue(damageInit, true);
        MaxHP.SetBaseValue(hpInit, true);
        fireRate.SetBaseValue(fireRateInit, true);
        rebornCooldown.SetBaseValue(cooldownTime, true);
        damagePercent.SetBaseValue(0, true);
        hpPercent.SetBaseValue(0, true);
        critChance.SetBaseValue(0, true);
        critDamage.SetBaseValue(0.5f, true);
        damageReduce.SetBaseValue(0, true);
        bulletSpeed.SetBaseValue(0, true);
        bulletSize.SetBaseValue(0, true);
        evasion.SetBaseValue(0, true);
        burnDamagePercent.SetBaseValue(0, true);
        burnDurationPercent.SetBaseValue(0, true);
        burnStack.SetBaseValue(0, true);
        blastDamagePercent.SetBaseValue(0, true);
        blastRadiusPercent.SetBaseValue(0, true);
        blockDamage.SetBaseValue(0, true);
        blockProbibility.SetBaseValue(0, true);
    }
    public void AddModifier(int damage, int hp, float fireRate, float cooldown) {
        this.damageInit = damage;
        this.hpInit = hp;
        this.fireRateInit = fireRate;
        this.cooldownTime = cooldown;
    }

    #region Modifier
    public void SetModifier(EventKey.StatEvent sEvent, StatModifier modifier) {
        switch (sEvent) {
            case EventKey.StatEvent.DroneAttack:
                if (Atk.Value == 0)
                    Atk.SetBaseValue((int)modifier.Value);
                Atk.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneHp:
                if (MaxHP.Value == 0)
                    MaxHP.SetBaseValue((int)modifier.Value);
                MaxHP.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneFirerate:
                if (fireRate.Value == 0)
                    fireRate.SetBaseValue(modifier.Value);
                fireRate.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneAttackPercent:
                if (damagePercent.Value == 0)
                    damagePercent.SetBaseValue(modifier.Value);
                damagePercent.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneHPPercent:
                if (hpPercent.Value == 0)
                    hpPercent.SetBaseValue(modifier.Value);
                hpPercent.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneCooldown:
                if (rebornCooldown.Value == 0)
                    rebornCooldown.SetBaseValue(modifier.Value);
                rebornCooldown.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneCritChance:
                if (critChance.Value == 0)
                    critChance.SetBaseValue((int)modifier.Value);
                critChance.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneCritDamage:
                if (critDamage.Value == 0)
                    critDamage.SetBaseValue(modifier.Value);
                critDamage.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneDamageReduce:
                if (damageReduce.Value == 0)
                    damageReduce.SetBaseValue(modifier.Value);
                damageReduce.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneBulletSpeed:
                if (bulletSpeed.Value == 0)
                    bulletSpeed.SetBaseValue(modifier.Value);
                bulletSpeed.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneBulletSize:
                if (bulletSize.Value == 0)
                    bulletSize.SetBaseValue(modifier.Value);
                bulletSize.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneEvasion:
                if (evasion.Value == 0)
                    evasion.SetBaseValue((int)modifier.Value);
                evasion.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneBurnDamagePercent:
                if (burnDamagePercent.Value == 0)
                    burnDamagePercent.SetBaseValue(modifier.Value);
                burnDamagePercent.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneBurnDurationPercent:
                if (burnDurationPercent.Value == 0)
                    burnDurationPercent.SetBaseValue(modifier.Value);
                burnDurationPercent.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneBurnStack:
                if (burnStack.Value == 0)
                    burnStack.SetBaseValue((int)modifier.Value);
                burnStack.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneBlastDamagePercent:
                if (blastDamagePercent.Value == 0)
                    blastDamagePercent.SetBaseValue(modifier.Value);
                blastDamagePercent.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneBlastRadiusPercent:
                if (blastRadiusPercent.Value == 0)
                    blastRadiusPercent.SetBaseValue(modifier.Value);
                blastRadiusPercent.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneBlockDamage:
                if (blockDamage.Value == 0)
                    blockDamage.SetBaseValue(modifier.Value);
                blockDamage.AddModifier(modifier);
                break;
            case EventKey.StatEvent.DroneBlockProbibility:
                if (blockProbibility.Value == 0)
                    blockProbibility.SetBaseValue(modifier.Value);
                blockProbibility.AddModifier(modifier);
                break;
            default:
                break;
        }
    }
    #endregion
}
