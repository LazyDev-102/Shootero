

using UnityEngine;

public class T03Hitbox : TrapHitbox {

    private T03Base t03Base;
    public T03Base T03Base {
        get {
            if (t03Base == null) {
                t03Base = ObjectBase as T03Base;
            }
            return t03Base;
        }
    }

    private HitInfor hitboxInfor;

    public HitInfor GetHitboxInfor(int damage) {
        if (hitboxInfor == null) {
            hitboxInfor = new HitInfor();
        }
        hitboxInfor.SetInfor(damage, null, T03Base);
        return hitboxInfor;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        IHitbox takeHit = collider.GetComponent<IHitbox>();
        if (takeHit != null) {
            int damage = T03Base.T03Stat.Atk.Value;
            takeHit.TakeHit(GetHitboxInfor(damage), transform.position);
        }
    }
}
