using UnityEngine;


[CreateAssetMenu(fileName = "ShieldBlowModData", menuName = "Mod/Buff/Limited/ShieldBlow")]
public class ShieldBlowModData : BuffLimitStackModData {
    [SerializeField] private float percentDamage;
    [SerializeField] private float radius;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.WallShieldManager.SetActionOnDie(percentDamage, radius);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}
