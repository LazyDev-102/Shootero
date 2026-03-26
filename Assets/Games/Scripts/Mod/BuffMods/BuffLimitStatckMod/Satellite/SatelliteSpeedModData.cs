using UnityEngine;


[CreateAssetMenu(fileName = "SatelliteSpeedModData", menuName = "Mod/Buff/Limited/SatelliteSpeed")]
public class SatelliteSpeedModData : BuffLimitStackModData {
    [SerializeField] private float delayTimePercent = 0.3f;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.ChangeSatelliteDelayAttackTime(delayTimePercent);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

