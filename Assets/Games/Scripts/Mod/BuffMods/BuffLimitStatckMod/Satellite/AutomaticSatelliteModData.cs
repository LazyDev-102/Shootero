using Gemmob;
using Helper;
using UnityEngine;


[CreateAssetMenu(fileName = "AutomaticSatelliteModData", menuName = "Mod/Buff/Limited/AutomaticSatellite")]
public class AutomaticSatelliteModData : BuffLimitStackModData {
    [SerializeField] private float timeDelay = 2f;
    [SerializeField] private float speed = 10;
    [SerializeField] private float percentDamage = 0.2f;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.EnableAutomaticSatellite(speed, timeDelay, percentDamage);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

