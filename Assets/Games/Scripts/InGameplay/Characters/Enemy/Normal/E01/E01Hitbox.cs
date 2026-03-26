
using UnityEngine;
public class E01Hitbox : EnemyHitbox {
    protected override void OnTriggerEnter2D(Collider2D collider) {
        base.OnTriggerEnter2D(collider);
        if (collider.CompareTag(GameTag.Finish)) {
            EnemyBase.Die();
        }
    }
}
