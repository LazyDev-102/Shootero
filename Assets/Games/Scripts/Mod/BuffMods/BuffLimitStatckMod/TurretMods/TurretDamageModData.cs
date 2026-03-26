using Gemmob;
using Helper;
using UnityEngine;


[CreateAssetMenu(fileName = "TurretDamageModData", menuName = "Mod/Buff/Limited/TurretDamage")]
public class TurretDamageModData : BuffLimitStackModData {
    [SerializeField] private StatModifier damage;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        TurretModInfo turretInfo = character.ShipSkill.GetTurretModInfo();
        if (turretInfo != null)
            turretInfo.ChangeDamage(character, damage);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

