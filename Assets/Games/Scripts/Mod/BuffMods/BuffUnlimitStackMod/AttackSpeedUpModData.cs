using UnityEngine;


[CreateAssetMenu(fileName = "AttackSpeedUpModData", menuName = "Mod/Buff/Unlimited/AttackSpeedUp")]
public class AttackSpeedUpModData : BuffUnlimitStackModData {
    [SerializeField] private StatModifier attackSpeedStat;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipStat.AtkSpeed.AddModifier(attackSpeedStat);
    }
}

