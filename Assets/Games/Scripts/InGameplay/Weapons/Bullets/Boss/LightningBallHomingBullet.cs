
using UnityEngine;

public class LightningBallHomingBullet : HomingBullet {
    [SerializeField] private float deltaAttack;

    private Countdowner deltaAttackCountdowner = new Countdowner();

    protected override void FixedUpdate() {
        base.FixedUpdate();
        deltaAttackCountdowner.Countdowning(Time.fixedDeltaTime * Time.timeScale);
    }


    protected override bool IsBlockHit() {
        return deltaAttackCountdowner.IsCountdowning();
    }

    protected override void Hit(Collider2D collision) {
        isHitted = true;
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(HitInfor, transform.position);
        }
        deltaAttackCountdowner.StartCountdown(deltaAttack);
    }

}
