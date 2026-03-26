using Gemmob;
using Helper;
using UnityEngine;


[CreateAssetMenu(fileName = "ShieldDurationModData", menuName = "Mod/Buff/Limited/ShieldDuration")]
public class ShieldDurationModData : BuffLimitStackModData {
    [SerializeField] private StatModifier durationTime;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        ShieldEffect info = character.ShipSkill.GetSelfEffect<ShieldEffect>(ShieldEffect.shiledId);
        if (info != null)
            info.ShieldDurantion.AddModifier(durationTime);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}
