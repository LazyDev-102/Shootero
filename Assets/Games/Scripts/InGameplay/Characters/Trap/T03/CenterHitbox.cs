

using UnityEngine;

public class CenterHitbox : MonoBehaviour {

    private HitInfor hitboxInfor;
    private ObjectBase objectBase;
    private int damage;

    public void SetObjectBase(ObjectBase obj) {
        objectBase = obj;
    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }

    public HitInfor GetHitboxInfor(int damage) {
        if (hitboxInfor == null) {
            hitboxInfor = new HitInfor();
        }
        hitboxInfor.SetInfor(damage, null, objectBase);
        return hitboxInfor;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        IHitbox takeHit = collision.GetComponent<IHitbox>();
        if (takeHit != null) {
            takeHit.TakeHit(GetHitboxInfor(damage), transform.position);
        }
    }
}
