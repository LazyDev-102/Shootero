
using UnityEngine;

public class B14HitBox : BossHitbox {
    private B14Base b14Base;
    public B14Base B14Base {
        get {
            if (b14Base == null) {
                b14Base = CharacterBase as B14Base;
            }
            return b14Base;
        }
    }
    public override void TakeHitDamage(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal) {
        if (B14Base.CanHitDamage())
            base.TakeHitDamage(hit, positionCollider, type);
    }
    protected override void TakeHitDamage(int damage, Vector2 positionCollider, ObjectBase causer, HitType type = HitType.Normal) {
        if (B14Base.CanHitDamage())
            base.TakeHitDamage(damage, positionCollider, causer, type);
    }
}
