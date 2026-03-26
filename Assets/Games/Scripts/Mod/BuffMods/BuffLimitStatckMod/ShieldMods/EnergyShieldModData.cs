using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnergyShieldModData", menuName = "Mod/Buff/Limited/EnergyShield")]
public class EnergyShieldModData : BuffLimitStackModData {
    [SerializeField] private int hp = 1000;
    [SerializeField] private float dodgeRate = 0;
    [SerializeField] private float timeReborn = 10;

    public int Hp { get => hp; }
    public float DodgeRate { get => dodgeRate; }
    public float TimeReborn { get => timeReborn; }
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        EnergyShieldEffect energyShieldEffect = new EnergyShieldEffect(character, hp, dodgeRate, timeReborn);
        character.ShipSkill.AddSelfEffect(energyShieldEffect);
        character.ShipHitbox.TurnOnShield(false, hp, dodgeRate, timeReborn);
        character.ShipSkill.AddEnergyShieldModInfo(new EnergyShieldModInfo(this));
    }
}

public class EnergyShieldModInfo : ModInfor<EnergyShieldModData>, IModable {
    private IntStat hp;
    private FloatStat dodgeRate;
    private FloatStat timeReborn;

    private ShipBase character;
    public EnergyShieldModInfo(EnergyShieldModData mod) : base(mod) {
        hp = new IntStat(mod.Hp);
        dodgeRate = new FloatStat(mod.DodgeRate);
        timeReborn = new FloatStat(mod.TimeReborn);
    }

    public EnergyShieldModInfo(EnergyShieldModInfo mod) : base(mod) {

    }

    public void Updating() {

    }

    public ModInfor GetModInfor() {
        return this;
    }

    public object Clone() {
        return new EnergyShieldModInfo(this);
    }
}

