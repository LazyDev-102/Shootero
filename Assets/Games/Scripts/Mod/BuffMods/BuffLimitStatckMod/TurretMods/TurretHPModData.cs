using Gemmob;
using Helper;
using UnityEngine;


[CreateAssetMenu(fileName = "TurretHPModData", menuName = "Mod/Buff/Limited/TurretHP")]
public class TurretHPModData : BuffLimitStackModData {
    [SerializeField] private StatModifier hp;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        TurretModInfo turretInfo = character.ShipSkill.GetTurretModInfo();
        if (turretInfo != null)
            turretInfo.ChangeHP(character, hp);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));

    }
}

