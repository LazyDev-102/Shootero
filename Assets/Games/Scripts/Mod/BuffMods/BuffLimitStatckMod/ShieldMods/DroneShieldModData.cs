using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DroneShieldModData", menuName = "Mod/Buff/Limited/DroneShield")]
public class DroneShieldModData : BuffLimitStackModData {
    [SerializeField] private ReflectiveShieldModData reflectiveShieldMod;
    private int hpUpCount;
    private int damageUpCount;
    private int fireRateUpCount;

    private bool isProtectShield;
    private List<DroneBase> droneBases = new List<DroneBase>();

    public int HpUpCount { get => hpUpCount; }
    public int DamageUpCount { get => damageUpCount; }
    public int FireRateUpCount { get => fireRateUpCount; }
    public List<DroneBase> DroneBases { get => droneBases; }

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        ShieldEffect shipSeftEffect = character.ShipSkill.GetSelfEffect<ShieldEffect>(ShieldEffect.shiledId);
        isProtectShield = shipSeftEffect != null;

        #region Load Current Drone 
        var drone1 = GameManager.Instance.GameLoader.Drone1;
        var drone2 = GameManager.Instance.GameLoader.Drone2;
        var hasDrone1 = drone1 != null;
        var hasDrone2 = drone2 != null;
        droneBases.Clear();
        if (hasDrone1)
            droneBases.Add(drone1);
        if (hasDrone2)
            droneBases.Add(drone2);
        #endregion

        #region Transfer Shield
        if (isProtectShield) {
            DroneShieldEffect droneShieldEffect = new DroneShieldEffect(character, droneBases, shipSeftEffect.ShieldDurantion.Value, shipSeftEffect.ShieldCountdown.Value, 0, 0, isProtectShield);
            character.ShipSkill.AddSelfEffect(droneShieldEffect);
            //character.ShipSkill.RemoveSelfEffect(shipSeftEffect);
            shipSeftEffect.PauseEffect(true);
        }
        else {
            EnergyShieldEffect energyShieldEffect = character.ShipSkill.GetSelfEffect<EnergyShieldEffect>(EnergyShieldEffect.shieldId);
            DroneShieldEffect droneShieldEffect = new DroneShieldEffect(character, droneBases, energyShieldEffect.TimeReborn.Value, energyShieldEffect.TimeReborn.Value, energyShieldEffect.HP.Value, energyShieldEffect.DodgeRate.Value, isProtectShield);
            character.ShipSkill.AddSelfEffect(droneShieldEffect);
            //character.ShipSkill.RemoveSelfEffect(energyShieldEffect);
            energyShieldEffect.PauseEffect(true);
        }
        #endregion

        #region Transfer Reflective Shield
        bool hasReflectiveShield = character.ShipSkill.HasMod(reflectiveShieldMod);
        if (hasReflectiveShield) {
            character.ShipHitbox.DisableReflectiveShield();
            if (hasDrone1)
                drone1.EnableReflexShield(reflectiveShieldMod.PercentDamage);
            if (hasDrone2)
                drone2.EnableReflexShield(reflectiveShieldMod.PercentDamage);
        }
        #endregion

        character.ShipHitbox.TurnOffShield(isProtectShield);

        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));

        if (character.ShipSkill.GetDroneModInfo() == null) {
            character.ShipSkill.AddDroneModInfo(new DroneShieldInfo(this));
        }
    }

    public override bool FirstCondition(ShipBase character) {
        var drone1 = GameResources.Instance.GearInventory.GetDroneLEquipable();
        var drone2 = GameResources.Instance.GearInventory.GetDroneREquipable();
        return drone1 != null || drone2 != null;
    }
    public void CreateDroneBases() {
        var drone1 = GameManager.Instance.GameLoader.Drone1;
        var drone2 = GameManager.Instance.GameLoader.Drone2;
        droneBases.Clear();
        if (drone1 != null)
            droneBases.Add(drone1);
        if (drone2 != null)
            droneBases.Add(drone2);
    }
    public void AddDroneModInfo() {
        CreateDroneBases();
        GameManager.Instance.GameLoader.Ship.ShipSkill.AddDroneModInfo(new DroneShieldInfo(this));
    }
}


public class DroneShieldInfo : ModInfor<DroneShieldModData>, IModable {
    private bool hasInit;
    private int hpUpCount = 0;
    private int damageUpCount = 0;
    private int fireRateUpCount = 0;
    private List<DroneBase> droneBases = new List<DroneBase>();

    public DroneShieldInfo(DroneShieldModData mod) : base(mod) {
        if (hasInit)
            return;
        hasInit = true;
        hpUpCount = mod.HpUpCount;
        damageUpCount = mod.DamageUpCount;
        fireRateUpCount = mod.FireRateUpCount;
        droneBases = mod.DroneBases;
    }

    public DroneShieldInfo(DroneShieldInfo mod) : base(mod) {

    }

    public void ChangeHP(StatModifier[] value) {
        if (hpUpCount >= value.Length)
            return;
        var modifier = value[hpUpCount];
        foreach (var t in droneBases) {
            t.DroneStat.MaxHP.AddModifier(modifier);
            t.DroneHealth.AddHp(Mathf.CeilToInt(modifier.Value));
            //TextShowupManager.Instance.ShowHealingText($"+ {modifier.Value}", t.DroneMove.MyRigi.position);
        }
        hpUpCount++;
    }

    public void ChangeTimeRespawn(StatModifier value) {
        foreach (var t in droneBases) {
            t.DroneStat.RebornCooldown.AddModifier(value);
        }
    }
    public void ChangeDamage(StatModifier[] value) {
        if (damageUpCount >= value.Length)
            return;
        var modifier = value[damageUpCount];
        foreach (var t in droneBases) {
            t.DroneStat.Atk.AddModifier(modifier);
        }
        damageUpCount++;
    }
    public void ChangeFireRate(StatModifier[] value) {
        if (fireRateUpCount >= value.Length)
            return;
        var fireRate = value[fireRateUpCount];
        foreach (var t in droneBases) {
            //t.DroneAttack.AddFireModifier(fireRate);
            t.DroneStat.FireRate.AddModifier(fireRate);
        }
        fireRateUpCount++;
    }
    public ModInfor GetModInfor() {
        return this;
    }

    public object Clone() {
        return new DroneShieldInfo(this);
    }
}

