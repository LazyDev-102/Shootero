using UnityEngine;


[CreateAssetMenu(fileName = "BurnLongerModData", menuName = "Mod/Buff/Limited/BurnLonger")]
public class BurnLongerModData : BuffLimitStackModData {
    [SerializeField] private BurnShotModData burnMod;
    [SerializeField] private StatModifier burnDuration;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        BurnShotModInfor burnInfor = character.ShipSkill.GetModInfor<BurnShotModInfor>(burnMod.ModId);
        if (burnInfor != null) {
            burnInfor.Duration.AddModifier(burnDuration);
        }
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

