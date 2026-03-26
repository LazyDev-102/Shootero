using UnityEngine;


[CreateAssetMenu(fileName = "SuperShotModData", menuName = "Mod/ChangeBullet/SuperShot")]

public class SuperShotModData : ChangeBulletModData {
    public StatModifier bulletSizePercent;
    public StatModifier bulletDamagePercent;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipSkill.AddChangeBulletMod(new SuperShotModInfor(this));
    }
}

public class SuperShotModInfor : ChangeBulletModInfor<SuperShotModData> {
    public SuperShotModInfor(SuperShotModData mod) : base(mod) {

    }

    public SuperShotModInfor(SuperShotModInfor mod) : base(mod) {

    }

    public override void ChangeBullet(BulletBase bullet) {
        bullet.Size.AddModifier(modData.bulletSizePercent);
        bullet.HitInfor.Damage.AddModifier(modData.bulletDamagePercent);
        bullet.ChangeSize();
    }

    public override object Clone() {
        return new SuperShotModInfor(this);
    }
}

