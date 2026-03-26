using Gemmob;
using Helper;
using UnityEngine;


[CreateAssetMenu(fileName = "SatelliteRangeModData", menuName = "Mod/Buff/Limited/SatelliteRange")]
public class SatelliteRangeModData : BuffLimitStackModData {
    [SerializeField] private float[] range;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.EnableSatelliteRange(range);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

