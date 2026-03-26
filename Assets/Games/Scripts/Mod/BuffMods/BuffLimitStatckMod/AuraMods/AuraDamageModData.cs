using UnityEngine;


[CreateAssetMenu(fileName = "AuraDamageModData", menuName = "Mod/Buff/Limited/AuraDamageModData")]
public class AuraDamageModData : BuffLimitStackModData {
    [SerializeField] private float percentDamage;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.AuraSystemManager.ChangeDamage(percentDamage);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}