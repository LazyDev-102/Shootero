using UnityEngine;


[CreateAssetMenu(fileName = "SpeedUpModData", menuName = "Mod/Buff/Limited/SpeedUp")]
public class SpeedUpModData : BuffLimitStackModData {
    [SerializeField] private StatModifier speedupValue;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipStat.SpeedUp(speedupValue);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}
