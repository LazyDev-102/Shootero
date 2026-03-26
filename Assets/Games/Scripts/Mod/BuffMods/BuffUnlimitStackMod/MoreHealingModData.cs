using UnityEngine;


[CreateAssetMenu(fileName = "MoreHealingModData", menuName = "Mod/Buff/Unlimited/MoreHealing")]
public class MoreHealingModData : BuffUnlimitStackModData {
    [SerializeField] private StatModifier addHealing;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipStat.HealingEffect.AddModifier(addHealing);
    }
}
