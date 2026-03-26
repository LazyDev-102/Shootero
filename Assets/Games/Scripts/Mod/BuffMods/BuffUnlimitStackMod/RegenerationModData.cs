using UnityEngine;


[CreateAssetMenu(fileName = "RegenerationModData", menuName = "Mod/Buff/Unlimited/Regeneration")]
public class RegenerationModData : BuffUnlimitStackModData {
    [SerializeField] private float percentHeal = 0.1f;
    [SerializeField] private float timeCountdown = 3f;
    [SerializeField] private float timeDuration = 1f;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHealth.StartHealHPByPercentLoop(timeDuration, timeCountdown, percentHeal);
    }
}
