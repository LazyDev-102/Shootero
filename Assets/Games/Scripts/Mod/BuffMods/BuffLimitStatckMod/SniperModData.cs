using UnityEngine;


[CreateAssetMenu(fileName = "SniperModData", menuName = "Mod/Buff/Limited/Sniper")]
public class SniperModData : BuffLimitStackModData {
    [SerializeField] private float percentOneShot = 0.05f;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipStat.SetSuperCriticalStatus(true, percentOneShot);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

