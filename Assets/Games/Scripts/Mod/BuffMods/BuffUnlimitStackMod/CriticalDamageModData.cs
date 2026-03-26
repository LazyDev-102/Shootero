using Gemmob;
using UnityEngine;


[CreateAssetMenu(fileName = "CriticalDamageModData", menuName = "Mod/Buff/Unlimited/CriticalDamageModData")]
public class CriticalDamageModData : BuffUnlimitStackModData {
    [SerializeField] private StatModifier critDamage;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipStat.CritDamage.AddModifier(critDamage);
    }
}

