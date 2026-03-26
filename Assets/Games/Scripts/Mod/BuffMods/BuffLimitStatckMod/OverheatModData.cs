using UnityEngine;


[CreateAssetMenu(fileName = "OverheatModData", menuName = "Mod/Buff/Limited/Overheat")]
public class OverheatModData : BuffLimitStackModData {
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipStat.SetOverHeatStatus(true);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

