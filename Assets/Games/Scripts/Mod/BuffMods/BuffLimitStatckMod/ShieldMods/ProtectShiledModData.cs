using UnityEngine;


[CreateAssetMenu(fileName = "ProtectShiledModData", menuName = "Mod/Buff/Limited/ProtectShiled")]
public class ProtectShiledModData : BuffLimitStackModData {
    [SerializeField] private float durantion;
    [SerializeField] private float countdown;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        ShieldEffect shieldEffect = new ShieldEffect(character, durantion, countdown);
        character.ShipSkill.AddSelfEffect(shieldEffect);
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}

