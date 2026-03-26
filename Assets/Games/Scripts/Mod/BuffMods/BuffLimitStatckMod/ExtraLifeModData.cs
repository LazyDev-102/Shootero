using UnityEngine;


[CreateAssetMenu(fileName = "ExtraLifeModData", menuName = "Mod/Buff/Limited/ExtraLife")]
public class ExtraLifeModData : BuffLimitStackModData {
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.AddLives();
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

