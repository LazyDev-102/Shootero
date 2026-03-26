using UnityEngine;


[CreateAssetMenu(fileName = "ReflectiveShieldModData", menuName = "Mod/Buff/Limited/ReflectiveShield")]
public class ReflectiveShieldModData : BuffLimitStackModData {
    [SerializeField] private float percentDamage;
    [SerializeField] private TurretShieldModData turretShieldModData;
    [SerializeField] private DroneShieldModData droneShieldModData;

    public float PercentDamage { get => percentDamage; }

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        if (character.ShipSkill.HasMod(turretShieldModData)) {
            TurretShieldEffect turretShieldEffect = character.ShipSkill.GetSelfEffect<TurretShieldEffect>(TurretShieldEffect.shiledId);
            if (turretShieldEffect != null)
                turretShieldEffect.EnableReflexShield(percentDamage);
        }
        else if (character.ShipSkill.HasMod(droneShieldModData)) {
            DroneShieldEffect droneShieldEffect = character.ShipSkill.GetSelfEffect<DroneShieldEffect>(DroneShieldEffect.shiledId);
            if (droneShieldEffect != null)
                droneShieldEffect.EnableReflexShield(percentDamage);
        }
        else {
            character.ShipHitbox.EnableReflexShield(percentDamage);
        }

        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

