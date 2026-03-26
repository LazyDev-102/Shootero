using UnityEngine;


[CreateAssetMenu(fileName = "ProtectSatelliteModData", menuName = "Mod/Buff/Limited/ProtectSatellite")]
public class ProtectSatelliteModData : BuffLimitStackModData {
    [SerializeField] private float rotateSpeed = -200;
    [SerializeField] private float distanceWithShip = 1;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.EnableProtectSatallite(rotateSpeed, distanceWithShip);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

