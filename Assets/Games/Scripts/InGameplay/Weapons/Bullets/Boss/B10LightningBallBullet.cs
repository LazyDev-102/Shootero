

using UnityEngine;

public class B10LightningBallBullet : BulletBase {


    [SerializeField] private SpriteRenderer outSprite;
    [SerializeField] private SpriteRenderer inSprite;
    [SerializeField] private float outRotateSpeed;
    [SerializeField] private float inRotateSpeed;



    private Countdowner lifeTimeCountdowner = new Countdowner();
    private Countdowner deltaAttackCountdonwer = new Countdowner();

    private float speed;
    private float accelerationSpeed = 0f;
    private Vector2 direction;
    private float deltaAttack;
    private Vector2 targetPosition;

    public void Shoot(Vector2 pointTarget, float speed, float acceleration = 0f) {
        this.speed = speed;
        this.targetPosition = pointTarget;
        direction = (pointTarget - (Vector2)transform.position).normalized;
        transform.up = direction;
        this.accelerationSpeed = acceleration;
    }


    public void SetInfo(float deltaAttack, float lifeTime) {
        this.deltaAttack = deltaAttack;
        deltaAttackCountdonwer.StartCountdown(deltaAttack);
        lifeTimeCountdowner.StartCountdown(lifeTime);
    }


    private void FixedUpdate() {
        float deltaTime = Time.fixedDeltaTime * Time.timeScale;
        outSprite.transform.Rotate(Vector3.back, outRotateSpeed * deltaTime);
        inSprite.transform.Rotate(Vector3.back, inRotateSpeed * deltaTime);

        MyRigi.MovePosition(MyRigi.position + direction * speed * deltaTime);
        speed += accelerationSpeed * deltaTime;
        if (Vector2.Distance(MyRigi.position, targetPosition) < speed * deltaTime) {
            speed = 0;
            accelerationSpeed = 0;
        }
        if (deltaAttackCountdonwer.IsTimeOut()) {
            HitCollider.enabled = true;
        }
        else {
            deltaAttackCountdonwer.Countdowning(deltaTime);
        }

        lifeTimeCountdowner.Countdowning(deltaTime);
        if (lifeTimeCountdowner.IsTimeOut()) {
            DestroyWithEffect();
        }
    }

    protected override bool IsBlockHit() {
        return deltaAttackCountdonwer.IsCountdowning();
    }

    protected override void Hit(Collider2D collision) {
        isHitted = true;
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(HitInfor, transform.position);
            HitCollider.enabled = false;
            deltaAttackCountdonwer.StartCountdown(deltaAttack);
        }
    }
}
