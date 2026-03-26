using Gemmob;
using UnityEngine;


[CreateAssetMenu(fileName = "MaxHpUpModData", menuName = "Mod/Buff/Unlimited/MaxHpUp")]
public class MaxHpUpModData : BuffUnlimitStackModData {
    [SerializeField] private StatModifier maxHp;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        int oldMaxHp = character.ShipStat.MaxHP.Value;
        character.ShipStat.MaxHP.AddModifier(maxHp);
        int newMaxHp = character.ShipStat.MaxHP.Value;
        character.ShipHealth.AddHp_ModHPMax(newMaxHp - oldMaxHp);
        if (character.ShipHealth.PlayerHPBar) {
            character.ShipHealth.PlayerHPBar.ChangeBars();
        }
    }
}

