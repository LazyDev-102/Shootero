using Gemmob;
using Helper;
using UnityEngine;


[CreateAssetMenu(fileName = "ShieldCountdownModData", menuName = "Mod/Buff/Limited/ShieldCountdown")]
public class ShieldCountdownModData : BuffLimitStackModData {
    [SerializeField] private StatModifier countdownTime;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        ShieldEffect info = character.ShipSkill.GetSelfEffect<ShieldEffect>(ShieldEffect.shiledId);
        if (info != null)
            info.ShieldCountdown.AddModifier(countdownTime);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}
