using UnityEngine;


[CreateAssetMenu(fileName = "AssaultSatelliteModData", menuName = "Mod/Buff/Limited/AssaultSatellite")]
public class AssaultSatelliteModData : BuffLimitStackModData {
    [SerializeField] private float percentColliderDamage = 0.5f;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.EnableAssaultSatellite(percentColliderDamage);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

