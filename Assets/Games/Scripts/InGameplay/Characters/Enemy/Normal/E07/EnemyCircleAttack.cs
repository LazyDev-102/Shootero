using Helper;
using UnityEngine;

public class EnemyCircleAttack : MonoBehaviour {
    [SerializeField] private EnemyBase eBase;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            IHitbox hitbox = collision.GetComponent<IHitbox>();
            if (hitbox != null) {
                hitbox.TakeHit(eBase.EnemyHitbox.GetHitboxInfor(eBase.EnemyStat.Atk.Value), transform.position);
            }
        }
    }

}
