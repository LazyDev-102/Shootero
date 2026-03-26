using Gemmob;
using Helper;
using UnityEngine;


[CreateAssetMenu(fileName = "ShieldHPModData", menuName = "Mod/Buff/Limited/ShieldHP")]
public class ShieldHPModData : BuffLimitStackModData {
    [SerializeField] private StatModifier[] hpBuff;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.WallShieldManager.AddHP(hpBuff);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}
