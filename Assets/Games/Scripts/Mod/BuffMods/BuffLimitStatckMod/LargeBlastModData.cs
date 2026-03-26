using Gemmob;
using Helper;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "LargeBlastModData", menuName = "Mod/Buff/Limited/LargeBlast")]
public class LargeBlastModData : BuffLimitStackModData {
    [SerializeField] private StatModifier radius;
    [SerializeField] private BlastShotModData blastShotMod;
    [SerializeField] private FireballSatelliteModData fireballSatelliteMod;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        BlastShotModInfor info = character.ShipSkill.GetModInfor<BlastShotModInfor>(blastShotMod.ModId);
        FireballSatelliteModInfor fireInfo = character.ShipSkill.GetModInfor<FireballSatelliteModInfor>(fireballSatelliteMod.ModId);
        if (info != null)
            info.ChangeRadius(radius);
        if (fireInfo != null)
            fireInfo.ChangeRadius(radius);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}
