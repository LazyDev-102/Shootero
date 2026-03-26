using UnityEngine;


[CreateAssetMenu(fileName = "AuraRadiusModData", menuName = "Mod/Buff/Limited/AuraRadius")]
public class AuraRadiusModData : BuffLimitStackModData {
    [SerializeField] private float percentRadiusModifier;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.AuraSystemManager.ChangeRadius(percentRadiusModifier);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}