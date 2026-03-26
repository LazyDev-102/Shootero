using Gear_Data;
using Gemmob;

public class PlayerStatManager : SingletonBind<PlayerStatManager> {
    private IntStat damage;
    private IntStat hp;
    private FloatStat fireRate;
    private FloatStat blastDamage;
    private FloatStat blastRadius;
    private FloatStat blockDamage;
    private FloatStat bulletSize;
    private FloatStat bulletSpeed;
    private FloatStat burnDamage;
    private IntStat burnStack;
    private FloatStat burnTime;
    private FloatStat chipGain;
    private FloatStat colliderDamage;
    private FloatStat critDamage;
    private FloatStat critRate;
    private FloatStat damageReduction;
    private IntStat dodgeRate;
    private FloatStat expGain;
    private FloatStat recoverHP;
    private IntStat maxEnergy;
    private StatModifier damagePassive;
    private StatModifier hpPassive;
    private IntStat damagePerLevelIngame;
    private IntStat hpPerLevelIngame;
    private IntStat pierceStack;
    private IntStat timeHoming;
    private IntStat turnHoming;
    private FloatStat bulletFadeTimeLife;
    private IntStat bounce;
    private FloatStat bulletTimeLife;
    private FloatStat lifeSteal;

    public int Damage { get => damage.Value; }
    public int HP { get => hp.Value; }
    public float FireRate { get => fireRate.Value; }
    public float BlastDamage { get => blastDamage.Value; }
    public float BlastRadius { get => blastRadius.Value; }
    public float BlockDamage { get => blockDamage.Value; }
    public float BulletSize { get => bulletSize.Value; }
    public float BulletSpeed { get => bulletSpeed.Value; }
    public float BurnDamage { get => burnDamage.Value; }
    public int BurnStack { get => burnStack.Value; }
    public float BurnTime { get => burnTime.Value; }
    public float Chip { get => chipGain.Value; }
    public float ColliderDamage { get => colliderDamage.Value; }
    public float CritDamage { get => critDamage.Value; }
    public float CritRate { get => critRate.Value; }
    public float DamageReduction { get => damageReduction.Value; }
    public int DodgeRate { get => dodgeRate.Value; }
    public float Exp { get => expGain.Value; }
    public float RecoverHP { get => recoverHP.Value; }
    public int MaxEnergy { get => maxEnergy.Value; }
    public int DamagePerLevelIngame { get => damagePerLevelIngame.Value; }
    public int HpPerLevelIngame { get => hpPerLevelIngame.Value; }
    public int PierceStack { get => pierceStack.Value; }
    public int TimeHoming { get => timeHoming.Value; }
    public int TurnHoming { get => turnHoming.Value; }
    public float BulletFadeTimeLife { get => bulletFadeTimeLife.Value; }
    public int Bounce { get => bounce.Value; }
    public float BulletTimeLife { get => bulletTimeLife.Value; }
    public float LifeSteal { get => lifeSteal.Value; }
    public float DamagePassive { get => damagePassive.Value; }
    public float HpPassive { get => hpPassive.Value; }

    private ShipInfor ship;
    private bool initialized;
    protected override void OnAwake() {
        base.OnAwake();
        AddEvent();
    }

    public void AssignData() {
        Init();
        SaveLoad.GameResourceLoaderLoad();
    }

    private void AddEvent() {
        if (!initialized) {
            initialized = true;
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.Attack, AddAttack);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.AttackSpeed, AddFireRate);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.BlastDamage, AddBlastDamage);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.BlastRadius, AddBlastRadius);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.BlockDamage, AddBlockDamage);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.BulletSize, AddBulletSize);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.BulletSpeed, AddBulletSpeed);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.BurnDamage, AddBurnDamage);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.BurnStack, AddBurnStack);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.BurnTime, AddBurnDuration);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.Chip, AddChipGain);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.ColliderDamage, AddCollideDamage);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.CritDamage, AddCritDamage);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.CritRate, AddCritRate);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.DamageReduction, AddDamageReduction);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.DodgeRate, AddDodgeRate);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.Exp, AddExpGain);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.Hp, AddHP);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.RecoverHP, AddRecovery);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.MaxEnergy, AddMaxEnergy);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.DamagePassive, AddDamagePassive);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.HpPassive, AddHpPassive);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.DamagePerLevelIngame, AddDamagePerlevelIngame);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.HpPerlevelIngame, AddHpPerLevelIngame);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.PierceStack, AddPierceStack);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.timeHoming, AddTimeHoming);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.turnHoming, AddTurnHoming);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.BulletFadeTimeLife, AddBulletFadeTimeLife);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.Bounce, AddBounce);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.BulletTimeLife, AddBulletTimeLife);
            EventDispatcher.Instance.AddListener((int)EventKey.StatEvent.LifeSteal, AddLifeSteal);
            EventDispatcher.Instance.AddListener<EventKey.OnShipChange>(LoadShipData);
            EventDispatcher.Instance.AddListener<EventKey.OnEnhanceShip>(LoadShipData);
        }
    }

    private void Init() {
        damage = new IntStat();
        hp = new IntStat();
        fireRate = new FloatStat();
        blastDamage = new FloatStat();
        blastRadius = new FloatStat();
        blockDamage = new FloatStat();
        bulletSize = new FloatStat();
        bulletSpeed = new FloatStat();
        burnDamage = new FloatStat();
        burnStack = new IntStat();
        burnTime = new FloatStat();
        chipGain = new FloatStat();
        colliderDamage = new FloatStat();
        critDamage = new FloatStat(0.5f);
        critRate = new FloatStat();
        damageReduction = new FloatStat();
        dodgeRate = new IntStat();
        expGain = new FloatStat();
        recoverHP = new FloatStat();
        maxEnergy = new IntStat();
        damagePassive = new StatModifier(0, StatModType.PercentAdd);
        hpPassive = new StatModifier(0, StatModType.PercentAdd);
        damagePerLevelIngame = new IntStat();
        hpPerLevelIngame = new IntStat();
        pierceStack = new IntStat();
        timeHoming = new IntStat();
        turnHoming = new IntStat();
        bulletFadeTimeLife = new FloatStat();
        bounce = new IntStat();
        bulletTimeLife = new FloatStat();
        lifeSteal = new FloatStat();
    }
    public void LoadData() {
        LoadShipData();
        LoadGearData();
    }
    public void LoadShipData() {
        ship = GameResources.Instance.Ship.GetCurrentShip();
        LoadPassive(ship);
        damage.SetBaseValue(ship.GetDamage());
        hp.SetBaseValue(ship.GetHP());
    }
    public void LoadGearData() {
        var weaponry = GameResources.Instance.GearInventory.WeaponrySlot;
        var shield = GameResources.Instance.GearInventory.ShieldSlot;
        var reactor = GameResources.Instance.GearInventory.CoreSlot;
        var propulsion = GameResources.Instance.GearInventory.EngineSlot;
        if (weaponry != null && weaponry.IsExist) {
            weaponry.RemoveItemStat();
        }
        if (shield != null && shield.IsExist) {
            shield.RemoveItemStat();
        }
        if (reactor != null && reactor.IsExist) {
            reactor.RemoveItemStat();
        }
        if (propulsion != null && propulsion.IsExist) {
            propulsion.RemoveItemStat();
        }
        if (weaponry != null && weaponry.IsExist) {
            weaponry.AddItemStat();
        }
        if (shield != null && shield.IsExist) {
            shield.AddItemStat();
        }
        if (reactor != null && reactor.IsExist) {
            reactor.AddItemStat();
        }
        if (propulsion != null && propulsion.IsExist) {
            propulsion.AddItemStat();
        }
    }

    public void LoadPassive(ShipInfor ship) {
        RemovePassive();
        var cSpecial = ship.GetCSpecial();
        if (cSpecial != null) {
            foreach (var item in cSpecial.SpecialValue) {
                EventDispatcher.Instance.Dispatch((int)item.statEvent, new StatValueParam() {
                    value = item.Value,
                    isAdd = true
                });
            }
        }
    }
    private void RemovePassive() {
        StatValueParam param = new StatValueParam() { value = new StatModifier(0, StatModType.Flat), isAdd = false };
        AddDamagePassive(param);
        AddHpPassive(param);
    }
    #region Add Modiffier
    private void AddAttack(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            damage.AddModifier(stat.value);
        }
        else {
            damage.RemoveModifier(stat.value);
        }

    }
    private void AddHP(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            hp.AddModifier(stat.value);
        }
        else {
            hp.RemoveModifier(stat.value);
        }
    }
    private void AddDamageReduction(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            damageReduction.AddModifier(stat.value);
        }
        else {
            damageReduction.RemoveModifier(stat.value);
        }
    }
    private void AddCritRate(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            critRate.AddModifier(stat.value);
        }
        else {
            critRate.RemoveModifier(stat.value);
        }
    }
    private void AddCritDamage(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            critDamage.AddModifier(stat.value);
        }
        else {
            critDamage.RemoveModifier(stat.value);
        }
    }
    private void AddFireRate(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            fireRate.AddModifier(stat.value);
        }
        else {
            fireRate.RemoveModifier(stat.value);
        }
    }
    private void AddBulletSpeed(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            bulletSpeed.AddModifier(stat.value);
        }
        else {
            bulletSpeed.RemoveModifier(stat.value);
        }
    }
    private void AddBulletSize(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            bulletSize.AddModifier(stat.value);
        }
        else {
            bulletSize.RemoveModifier(stat.value);
        }
    }
    private void AddDodgeRate(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            dodgeRate.AddModifier(stat.value);
        }
        else {
            dodgeRate.RemoveModifier(stat.value);
        }
    }
    private void AddBurnDamage(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            burnDamage.AddModifier(stat.value);
        }
        else {
            burnDamage.RemoveModifier(stat.value);
        }
    }
    private void AddBurnDuration(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            burnTime.AddModifier(stat.value);
        }
        else {
            burnTime.RemoveModifier(stat.value);
        }
    }
    private void AddBurnStack(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            burnStack.AddModifier(stat.value);
        }
        else {
            burnStack.RemoveModifier(stat.value);
        }
    }
    private void AddBlastDamage(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            blastDamage.AddModifier(stat.value);
        }
        else {
            blastDamage.RemoveModifier(stat.value);
        }
    }
    private void AddBlastRadius(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            blastRadius.AddModifier(stat.value);
        }
        else {
            blastRadius.RemoveModifier(stat.value);
        }
    }
    private void AddCollideDamage(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            colliderDamage.AddModifier(stat.value);
        }
        else {
            colliderDamage.RemoveModifier(stat.value);
        }
    }
    private void AddExpGain(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            expGain.AddModifier(stat.value);
        }
        else {
            expGain.RemoveModifier(stat.value);
        }
    }
    private void AddChipGain(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            chipGain.AddModifier(stat.value);
        }
        else {
            chipGain.RemoveModifier(stat.value);
        }
    }
    private void AddBlockDamage(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            blockDamage.AddModifier(stat.value);
        }
        else {
            blockDamage.RemoveModifier(stat.value);
        }
    }
    private void AddRecovery(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            recoverHP.AddModifier(stat.value);
        }
        else {
            recoverHP.RemoveModifier(stat.value);
        }
    }
    private void AddMaxEnergy(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            maxEnergy.AddModifier(stat.value);
        }
        else {
            maxEnergy.RemoveModifier(stat.value);
        }
        EventDispatcher.Instance.Dispatch<EventKey.OnMaxEnergyChange>(new EventKey.OnMaxEnergyChange());
    }
    private void AddDamagePassive(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            damagePassive.Value = stat.value.Value;
        }
        else {
            damagePassive.Value = 0;
        }
    }
    private void AddHpPassive(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            hpPassive.Value = stat.value.Value;
        }
        else {
            hpPassive.Value = 0;
        }
    }
    private void AddDamagePerlevelIngame(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            damagePerLevelIngame.SetBaseValue((int)stat.value.Value, true);
        }
        else {
            damagePerLevelIngame.SetBaseValue(0, true);
        }
    }
    private void AddHpPerLevelIngame(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            hpPerLevelIngame.SetBaseValue((int)stat.value.Value, true);
        }
        else {
            hpPerLevelIngame.SetBaseValue(0, true);
        }
    }
    private void AddPierceStack(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            pierceStack.SetBaseValue((int)stat.value.Value, true);
        }
        else {
            pierceStack.SetBaseValue(0, true);
        }
    }
    private void AddTimeHoming(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            timeHoming.SetBaseValue((int)stat.value.Value, true);
        }
        else {
            timeHoming.SetBaseValue(0, true);
        }
    }
    private void AddTurnHoming(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            turnHoming.SetBaseValue((int)stat.value.Value, true);
        }
        else {
            turnHoming.SetBaseValue(0, true);
        }
    }
    private void AddBulletFadeTimeLife(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            bulletFadeTimeLife.SetBaseValue(stat.value.Value, true);
        }
        else {
            bulletFadeTimeLife.SetBaseValue(0, true);
        }
    }
    private void AddBounce(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            bounce.SetBaseValue((int)stat.value.Value, true);
        }
        else {
            bounce.SetBaseValue(0, true);
        }
    }
    private void AddBulletTimeLife(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            bulletTimeLife.SetBaseValue(stat.value.Value, true);
        }
        else {
            bulletTimeLife.SetBaseValue(0, true);
        }
    }
    private void AddLifeSteal(object param) {
        StatValueParam stat = (StatValueParam)param;
        if (stat.isAdd) {
            lifeSteal.SetBaseValue(stat.value.Value, true);
        }
        else {
            lifeSteal.SetBaseValue(0, true);
        }
    }

    #endregion
}
