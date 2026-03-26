

using UnityEngine;

public class ShipStat : CharacterStat {
    private ShipBase shipBase;
    public ShipBase ShipBase {
        get {
            if (shipBase == null) {
                shipBase = CharacterBase as ShipBase;
            }
            return shipBase;
        }
    }


    [SerializeField] private FloatStat atkSpeed;
    [SerializeField] private FloatStat dMPercent;
    [SerializeField] private IntStat critChance;
    [SerializeField] private FloatStat critDamage;
    [SerializeField] private FloatStat healingEffect;
    [SerializeField] private FloatStat damageReduce;
    [SerializeField] private FloatStat bulletSpeed;
    [SerializeField] private FloatStat bulletSize;
    [SerializeField] private IntStat evasion;
    [SerializeField] private FloatStat burnDamagePercent;
    [SerializeField] private FloatStat burnDurationPercent;
    [SerializeField] private IntStat burnStack;
    [SerializeField] private FloatStat blastRadiusPercent;
    [SerializeField] private FloatStat blockDamage;
    [SerializeField] private FloatStat blockProbibility;


    [SerializeField] private FloatStat chipGain;
    [SerializeField] private FloatStat expGain;
    [SerializeField] private IntStat attackPerLevel;
    [SerializeField] private IntStat hpPerLevel;
    [SerializeField] private IntStat pierceStack;
    [SerializeField] private IntStat timeHoming;
    [SerializeField] private IntStat turnHoming;
    [SerializeField] private FloatStat bulletFadeTimeLife;
    [SerializeField] private IntStat bounce;
    [SerializeField] private FloatStat bulletTimeLife;
    [SerializeField] private FloatStat lifeSteal;

    public FloatStat AtkSpeed { get => atkSpeed; }
    public FloatStat DMPercent { get => dMPercent; }
    public IntStat CritChance { get => critChance; }
    public FloatStat CritDamage { get => critDamage; }
    public FloatStat HealingEffect { get => healingEffect; }
    public FloatStat DamageReduce { get => damageReduce; }
    public FloatStat BulletSpeed { get => bulletSpeed; }
    public FloatStat BulletSize { get => bulletSize; }
    public IntStat Evasion { get => evasion; }
    public FloatStat BurnDamagePercent { get => burnDamagePercent; }
    public FloatStat BurnDurationPercent { get => burnDurationPercent; }
    public IntStat BurnStack { get => burnStack; }
    public FloatStat BlastRadiusPercent { get => blastRadiusPercent; }
    public FloatStat BlockDamage { get => blockDamage; }
    public FloatStat BlockProbibility { get => blockProbibility; }

    public FloatStat ChipGain { get => chipGain; }
    public FloatStat ExpGain { get => expGain; }

    public IntStat AttackPerLevel { get => attackPerLevel; }

    public IntStat HpPerLevel { get => hpPerLevel; }

    public IntStat PierceStack { get => pierceStack; }
    public IntStat TimeHoming { get => timeHoming; }
    public IntStat TurnHoming { get => turnHoming; }
    public FloatStat BulletFadeTimeLife { get => bulletFadeTimeLife; }
    public IntStat Bounce { get => bounce; }
    public FloatStat BulletTimeLife { get => bulletTimeLife; }
    public FloatStat LifeSteal { get => lifeSteal; }

    private int damageExtend;
    public int DamageExtend {
        get => damageExtend;
        set => damageExtend = value;
    }
    private bool isSuperCritical;
    private float percentSuperCritical;
    private bool isOverHeat;

    public bool IsOverHeat { get => isOverHeat; }
    public void SetSuperCriticalStatus(bool status, float percent) {
        isSuperCritical = status;
        percentSuperCritical += percent;
    }
    public void SetOverHeatStatus(bool status) {
        isOverHeat = status;
    }
    public override void Initialize() {
        base.Initialize();
        PlayerStatManager initStat = PlayerStatManager.Instance;
        var ship = GameResources.Instance.Ship.GetTryShipInfor();
        Atk.SetBaseValue(GameManager.Instance.IsTrial ? ship.GetTrialDamage() : initStat.Damage);
        MaxHP.SetBaseValue(GameManager.Instance.IsTrial ? ship.GetTrialHp() : initStat.HP);
        ColliderDamage.SetBaseValue(1);
        AtkSpeed.AddModifier(new StatModifier(initStat.FireRate, StatModType.PercentAdd));
        // DMPercent
        CritChance.AddModifier(new StatModifier(initStat.CritRate, StatModType.Flat));
        CritDamage.SetBaseValue(1f, true);
        CritDamage.AddModifier(new StatModifier(initStat.CritDamage, StatModType.Flat));
        HealingEffect.AddModifier(new StatModifier(initStat.RecoverHP, StatModType.Flat));
        DamageReduce.SetBaseValue(initStat.DamageReduction, true);
        BulletSpeed.SetBaseValue(initStat.BulletSpeed, true);
        BulletSize.SetBaseValue(initStat.BulletSize, true);
        Evasion.SetBaseValue(initStat.DodgeRate, true);
        BurnDamagePercent.SetBaseValue(initStat.BurnDamage, true);
        BurnDurationPercent.SetBaseValue(initStat.BurnTime, true);
        BurnStack.SetBaseValue(initStat.BurnStack, true);
        BlastRadiusPercent.SetBaseValue(initStat.BlastDamage, true);
        BlockDamage.SetBaseValue(initStat.BlockDamage, true);
        //BlockProbility
        ChipGain.SetBaseValue(initStat.Chip, true);
        ExpGain.SetBaseValue(initStat.Exp, true);
        AttackPerLevel.SetBaseValue(initStat.DamagePerLevelIngame, true);
        HpPerLevel.SetBaseValue(initStat.HpPerLevelIngame, true);
        PierceStack.SetBaseValue(initStat.PierceStack, true);
        TimeHoming.SetBaseValue(initStat.TimeHoming, true);
        TurnHoming.SetBaseValue(initStat.TurnHoming, true);
        BulletFadeTimeLife.SetBaseValue(initStat.BulletFadeTimeLife, true);
        Bounce.SetBaseValue(initStat.Bounce, true);
        BulletTimeLife.SetBaseValue(initStat.BulletTimeLife, true);
        LifeSteal.SetBaseValue(initStat.LifeSteal, true);
        damageExtend = 0;
    }

    public void Revive() {

    }
    public void SpeedUp(StatModifier modifier) {
        BulletSpeed.AddModifier(modifier);
    }

    public int GetFinalDamageWeapon {
        get {
            return CaculateFinalDamage(Mathf.CeilToInt(Atk.Value * (DMPercent.Value) + damageExtend));
        }
    }

    public bool CanSuperCritical() {
        return isSuperCritical && Helper.RandomHelper.RandomWithPercent(percentSuperCritical * 100);
    }

    protected int CaculateFinalDamage(int damage) {
        if (isOverHeat) {
            damage += (int)(damage * (1 - ShipBase.ShipHealth.GetPercentHPRemain()));
        }
        return damage;
    }
}
