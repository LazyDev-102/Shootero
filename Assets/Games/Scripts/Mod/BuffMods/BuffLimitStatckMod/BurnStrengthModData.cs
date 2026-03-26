using UnityEngine;


[CreateAssetMenu(fileName = "BurnStrengthModData", menuName = "Mod/Buff/Limited/BurnStrength")]
public class BurnStrengthModData : BuffLimitStackModData {
    [SerializeField] private BurnShotModData burnMod;
    [SerializeField] private StatModifier burnDmg;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        BurnShotModInfor burnInfor = character.ShipSkill.GetModInfor<BurnShotModInfor>(burnMod.ModId);
        burnInfor.DamagePercent.AddModifier(burnDmg);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

