using Gemmob;
using Helper;
using UnityEngine;


[CreateAssetMenu(fileName = "TurretFireRateModData", menuName = "Mod/Buff/Limited/TurretFireRate")]
public class TurretFireRateModData : BuffUnlimitStackModData {
    [SerializeField] private StatModifier fireRate;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        TurretModInfo turretInfo = character.ShipSkill.GetTurretModInfo();
        if (turretInfo != null)
            turretInfo.ChangeFireRate(fireRate);
        //character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

