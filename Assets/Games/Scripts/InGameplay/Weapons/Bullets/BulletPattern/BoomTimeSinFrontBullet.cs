
using DG.Tweening;
using UnityEngine;

public class BoomTimeSinFrontBullet : SinFrontBullet {
    [SerializeField] protected Rigidbody2D myRigi;
    [SerializeField] private Explosioner explosioner;
    [SerializeField] private float rotateAccelerSpeed;
    [SerializeField] private BurningEffect burnEffect;
    private float delayAttack = 0.5f;
    private float radius = 2;
    private bool canAttack;
    private Countdowner delayAttackCd = new Countdowner();

    public override void Initalize() {
        base.Initalize();
        canAttack = false;
    }

    public override void Shoot(float speed, Vector2 direction, float acceleration = 0, float minSpeed = float.MinValue) {
        base.Shoot(speed, direction, acceleration, minSpeed);
    }
    public BoomTimeSinFrontBullet SetTimeAttackBoom(float time) {
        delayAttackCd.StartCountdown(time);
        canAttack = true;
        return this;
    }
    public BoomTimeSinFrontBullet SetBoomRadius(float radius) {
        this.radius = radius;
        return this;
    }
    protected override void FixedUpdate() {
        base.FixedUpdate();
        if (canAttack) {
            float deltaTime = Time.fixedDeltaTime * Time.timeScale;
            delayAttackCd.Countdowning(Time.fixedDeltaTime);

            if (sprite) {
                sprite.transform.Rotate(Vector3.back, rotateSpeed * deltaTime);
                rotateSpeed += rotateAccelerSpeed * deltaTime;
            }

            if (delayAttackCd.IsTimeOut()) {
                canAttack = false;
                AttackBoom(true);
            }
        }
    }


    protected override void Hit(Collider2D collision) {
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(hitInfor, transform.position);
            AttackBoom(false);
        }
        else
            Destroy();
    }
    private void AttackBoom(bool delay = true) {
        if (delay) {
            WarningEffect();
            DOVirtual.DelayedCall(delayAttack, () => SpawnExplosion());
        }
        else {
            SpawnExplosion();
        }
    }
    private void SpawnExplosion() {
        var explosionClone = GameManager.Instance.GameLoader.SpawnExplosion(explosioner, transform.position);
        explosionClone.SetHitInfor(HitInfor.Damage.Value, null, HitInfor.Causer)
                   .SetRadius(radius)
                   .Explosioning();
        Destroy();
    }
    public void WarningEffect() {
        burnEffect.StartEffect(true);
    }
}
