using UnityEngine;


[CreateAssetMenu(fileName = "CriticalStrikeModData", menuName = "Mod/Buff/Unlimited/CriticalStrike")]
public class CriticalStrikeModData : BuffLimitStackModData {
    [SerializeField] private StatModifier addCritChance;
    //[SerializeField] private StatModifier addCritDamage;

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipStat.CritChance.AddModifier(addCritChance);
        //character.ShipStat.CritDamage.AddModifier(addCritDamage);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}