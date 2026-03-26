

using UnityEngine;

public class MiniShieldHitbox : CharacterHitbox {


    private MiniShieldBase miniShieldBase;
    public MiniShieldBase MiniShieldBase {
        get {
            if (miniShieldBase == null) {
                miniShieldBase = CharacterBase as MiniShieldBase;
            }
            return miniShieldBase;
        }
    }

    private HitInfor hitboxInfor;

    public HitInfor GetHitboxInfor(int damage) {
        if (hitboxInfor == null) {
            hitboxInfor = new HitInfor();
        }
        hitboxInfor.SetInfor(damage, null, MiniShieldBase);
        return hitboxInfor;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Player")) {
            IHitbox takeHit = collider.GetComponent<IHitbox>();
            if (takeHit != null) {
                takeHit.TakeHit(GetHitboxInfor(MiniShieldBase.MiniShieldStat.Atk.Value), transform.position);
            }
        }
    }

    protected override void TakeHitDamage(int damage, Vector2 positionCollider, ObjectBase causer, HitType type = HitType.Normal) {
        base.TakeHitDamage(damage, positionCollider, causer, type);
        if (!MiniShieldBase.IsDie()) {
            MiniShieldBase.MiniShieldEffect.StartEnemyHitEffect();
        }
    }
}
