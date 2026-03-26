

using UnityEngine;

public class T04Hitbox : TrapHitbox {

    private T04Base t04Base;
    public T04Base T04Base {
        get {
            if (t04Base == null) {
                t04Base = ObjectBase as T04Base;
            }
            return t04Base;
        }
    }

    private HitInfor hitboxInfor;

    public HitInfor GetHitboxInfor(int damage) {
        if (hitboxInfor == null) {
            hitboxInfor = new HitInfor();
        }
        hitboxInfor.SetInfor(damage, null, T04Base);
        return hitboxInfor;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        IHitbox takeHit = collider.GetComponent<IHitbox>();
        if (takeHit != null) {
            int damage = T04Base.T04Stat.Atk.Value;
            takeHit.TakeHit(GetHitboxInfor(damage), transform.position);
        }
    }
}
