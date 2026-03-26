

using UnityEngine;

public class T01PartHitbox : ObjectHitbox {
    private T01PartBase t01PartBase;
    public T01PartBase T01PartBase {
        get {
            if (t01PartBase == null) {
                t01PartBase = GetComponent<T01PartBase>();
            }
            return t01PartBase;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (true) { // is player
            IHitbox hitbox = collision.GetComponent<IHitbox>();
            if (hitbox != null) {
                T01PartBase.T01PartAttack.CollisionAttack(hitbox);
                if (hitbox is EnemyHitbox eHit) {
                    eHit.EnemyBase.EnemyMove.Knockback(transform.position);
                }
                T01PartBase.MyParent.ReloadChildDamage(T01PartBase);
            }
        }
    }
}
