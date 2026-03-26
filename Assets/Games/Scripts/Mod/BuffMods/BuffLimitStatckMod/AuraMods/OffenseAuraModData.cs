using UnityEngine;


[CreateAssetMenu(fileName = "OffenseAuraModData", menuName = "Mod/Buff/Limited/OffenseAura")]
public class OffenseAuraModData : BuffLimitStackModData {
    [SerializeField] private float deltaTime;
    [SerializeField] private float percentDamage;
    [SerializeField] private float radius;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.AuraSystemManager.EnableAuraOffense(deltaTime, percentDamage, radius);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

