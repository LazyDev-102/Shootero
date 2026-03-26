using UnityEngine;


[CreateAssetMenu(fileName = "SatelliteRotationModData", menuName = "Mod/Buff/Limited/SatelliteRotation")]
public class SatelliteRotationModData : BuffLimitStackModData {
    [SerializeField] private float speedRotationPercent = 0.3f;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.ChangeSatelliteRotationSpeed(speedRotationPercent);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

