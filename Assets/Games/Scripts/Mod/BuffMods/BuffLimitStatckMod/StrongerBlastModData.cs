using UnityEngine;


[CreateAssetMenu(fileName = "StrongerBlastModData", menuName = "Mod/Buff/Limited/StrongerBlast")]
public class StrongerBlastModData : BuffLimitStackModData {
    [SerializeField] private BlastShotModData blastMod;
    [SerializeField] private StatModifier blastDmg;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        BlastShotModInfor burnInfor = character.ShipSkill.GetModInfor<BlastShotModInfor>(blastMod.ModId);
        burnInfor.DamagePercent.AddModifier(blastDmg);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

