using UnityEngine;


[CreateAssetMenu(fileName = "AuraFrequencyModData", menuName = "Mod/Buff/Limited/AuraFrequency")]
public class AuraFrequencyModData : BuffLimitStackModData {
    [SerializeField] private float decreasePercentTime = 0.3f;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipHitbox.AuraSystemManager.ChangeDeltaShot(decreasePercentTime);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}