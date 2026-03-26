using UnityEngine;

public class LifestealFrontBullet : FrontBullet {

    private ShipBase ship;

    protected override void Hit(Collider2D collision) {
        isHitted = true;
        HitCollider.enabled = false;
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(hitInfor, transform.position);
            Healing();
        }
        DestroyWithEffect();
    }

    public void Healing() {
        if (ship == null)
            ship = GameManager.Instance.GameLoader.Ship;
        if (ship != null) {
            ship.ShipHealth.Lifesteal(hitInfor.Damage.Value);
        }
    }
}
