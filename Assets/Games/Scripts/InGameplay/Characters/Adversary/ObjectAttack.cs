using System.Collections.Generic;
using UnityEngine;



public abstract class ObjectAttack : MonoBehaviour {
    private ObjectBase objectBase;
    public ObjectBase ObjectBase {
        get {
            if (objectBase == null) {
                objectBase = GetComponent<ObjectBase>();
            }
            return objectBase;
        }
    }

    private HitInfor collisionHitInfor;
    public HitInfor CollisionHitInfor {
        get {
            if (collisionHitInfor == null) {
                collisionHitInfor = new HitInfor();
            }
            return collisionHitInfor;
        }
    }

    public virtual void PreloadIngame() {

    }

    public virtual void Initialize() {

    }

    public virtual void Destroy() {
        StopAllCoroutines();
    }

    public virtual void Updating() {

    }

    public virtual void CollisionAttack(IHitbox victim) {
        if (victim != null) {
            CollisionHitInfor.SetInfor(ObjectBase.ObjectStat.Atk.Value, new List<IEffectAttackModable>(), ObjectBase);
            victim.TakeHit(CollisionHitInfor, transform.position, HitType.Normal);
        }
    }
}
