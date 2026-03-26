using UnityEngine;


[CreateAssetMenu(fileName = "AttackUpModData", menuName = "Mod/Buff/Unlimited/AttackUp")]
public class AttackUpModData : BuffUnlimitStackModData {
    [SerializeField] private StatModifier attackStat;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipStat.Atk.AddModifier(attackStat);
    }
}
