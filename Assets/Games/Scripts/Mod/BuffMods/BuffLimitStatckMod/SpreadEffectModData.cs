using Gemmob;
using Helper;
using UnityEngine;


[CreateAssetMenu(fileName = "SpreadEffectModData", menuName = "Mod/Buff/Limited/SpreadEffect")]
public class SpreadEffectModData : BuffLimitStackModData {
    [SerializeField] private DeadSpreadModData spreadModData;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        DeadSpreadModInfo info = character.ShipSkill.GetModInfor<DeadSpreadModInfo>(spreadModData.ModId);
        if (info != null)
            info.UpgradeSuperBullet();
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}
