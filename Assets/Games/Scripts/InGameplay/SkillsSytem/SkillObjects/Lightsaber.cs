using DG.Tweening;
using UnityEngine;

public class Lightsaber : MonoBehaviour {
    [SerializeField] private float smoothSpeed = 0.5f;
    [SerializeField] private DOTweenAnimation rotationAnim;

    private Vector3 smoothedPosition = Vector3.zero;
    private HitInfor hitboxInfor;
    private int damage = 0;
    private ShipBase ship;

    public void Initialize(ShipBase ship, float percentDamage, float rotateSpeed) {
        this.ship = ship;
        damage = (int)(ship.ShipStat.Atk.Value * percentDamage);
        if (rotationAnim != null) {
            rotationAnim.duration = rotateSpeed / 10f;
        }
    }

    public void FollowShip() {
        smoothedPosition = Vector3.Lerp(transform.position, ship.transform.position, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals(GameTag.Enemy) || collider.tag.Equals(GameTag.EnemyBlockPierce)) {
            IHitbox takeHit = collider.GetComponent<IHitbox>();
            if (takeHit != null) {
                takeHit.TakeHit(GetHitboxInfor(damage), transform.position);
                if (takeHit is EnemyHitbox eHit) {
                    eHit.EnemyBase.EnemyMove.Knockback(transform.position);
                }
            }
        }
    }

    public HitInfor GetHitboxInfor(int damage) {
        if (hitboxInfor == null) {
            hitboxInfor = new HitInfor();
        }
        hitboxInfor.SetInfor(damage, null, null);
        return hitboxInfor;
    }
}
