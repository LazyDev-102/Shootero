using UnityEngine;


[CreateAssetMenu(fileName = "FocusModData", menuName = "Mod/ChangeBullet/FocusMod")]

public class FocusModData : ChangeBulletModData {
    public StatModifier bulletSizePercent;
    public StatModifier bulletCritChancePercent;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipAttack.Focus();
        character.ShipStat.CritChance.AddModifier(bulletCritChancePercent);
        character.ShipSkill.AddChangeBulletMod(new FocusModInfor(this));
    }
}

public class FocusModInfor : ChangeBulletModInfor<FocusModData> {
    public FocusModInfor(FocusModData mod) : base(mod) {

    }

    public FocusModInfor(FocusModInfor mod) : base(mod) {

    }

    public override void ChangeBullet(BulletBase bullet) {
        bullet.Size.AddModifier(modData.bulletSizePercent);
        bullet.ChangeSize();
    }

    public override object Clone() {
        return new FocusModInfor(this);
    }
}
