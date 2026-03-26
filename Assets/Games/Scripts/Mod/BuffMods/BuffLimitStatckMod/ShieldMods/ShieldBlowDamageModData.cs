using Gemmob;
using Helper;
using UnityEngine;


[CreateAssetMenu(fileName = "ShieldBlowDamageModData", menuName = "Mod/Buff/Limited/ShieldBlowDamage")]
public class ShieldBlowDamageModData : BuffLimitStackModData {
    [SerializeField] private StatModifier[] damageModifier;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.EnergyShield.SetExplosionDamage(damageModifier);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}
