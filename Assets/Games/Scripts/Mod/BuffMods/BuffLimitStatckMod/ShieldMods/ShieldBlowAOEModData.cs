using Gemmob;
using Helper;
using UnityEngine;


[CreateAssetMenu(fileName = "ShieldBlowAOEModData", menuName = "Mod/Buff/Limited/ShieldBlowAOE")]
public class ShieldBlowAOEModData : BuffLimitStackModData {
    [SerializeField] private StatModifier[] radiusModifier;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.EnergyShield.SetExplosionRadius(radiusModifier);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}
