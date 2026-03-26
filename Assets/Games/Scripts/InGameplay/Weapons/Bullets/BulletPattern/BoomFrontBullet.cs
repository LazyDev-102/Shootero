using DG.Tweening;
using Helper;
using System;
using UnityEngine;

public class BoomFrontBullet : BulletBase {
    [SerializeField] protected Rigidbody2D myRigi;
    [SerializeField] private BurningEffect burnEffect;
    [SerializeField] private Explosioner explosioner;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float rotateAccelerSpeed;
    [SerializeField] private bool isNotTriggerBorder;

    private float speed;
    private float accelerationSpeed = 0;
    private Vector2 direction;
    private float delayAttack;
    private float minSpeed;
    private float radius;
    private Action onMoveComplete;
    private Vector3 target;
    private bool attacking;

    private Countdowner deltaCountdowner = new Countdowner();

    public override void Initalize() {
        base.Initalize();
        burnEffect.StopEffect(true);
        sprite.transform.localEulerAngles = Vector3.zero;
    }

    public BoomFrontBullet Shoot(Vector2 direction, float speed, float acceleration = 0f, float minSpeed = float.MinValue) {
        this.speed = speed + SpeedStat.Value;
        transform.up = direction.normalized;
        this.direction = direction.normalized;
        this.accelerationSpeed = acceleration;
        this.minSpeed = minSpeed;
        attacking = false;
        return this;
    }

    public BoomFrontBullet SetRotateSpeed(float rotateSpeed, float rotateAcceler) {
        this.rotateSpeed = rotateSpeed;
        this.rotateAccelerSpeed = rotateAcceler;
        return this;
    }

    public BoomFrontBullet SetTarget(Vector3 target) {
        this.target = target;
        return this;
    }
    public BoomFrontBullet SetBoomRadius(float radius) {
        this.radius = radius;
        return this;
    }
    public BoomFrontBullet SetMoveComplete(Action onMoveComplete, float delayAttack) {
        this.delayAttack = delayAttack;
        this.onMoveComplete = onMoveComplete;
        deltaCountdowner.StartCountdown(delayAttack);
        return this;
    }
    private void FixedUpdate() {
        if (!attacking) {
            float deltaTime = Time.fixedDeltaTime * Time.timeScale;
            myRigi.MovePosition(myRigi.position + direction * speed * deltaTime);
            speed += accelerationSpeed * deltaTime;
            speed = Mathf.Max(speed, minSpeed);

            if (sprite) {
                sprite.transform.Rotate(Vector3.back, rotateSpeed * deltaTime);
                rotateSpeed += rotateAccelerSpeed * deltaTime;
            }
            if (IsMoveComplete()) {
                attacking = true;
                AttackBoom();
            }
        }

    }

    private void AttackBoom(bool delay = true) {
        if (delay) {
            onMoveComplete?.Invoke();
            onMoveComplete = null;
            DOVirtual.DelayedCall(delayAttack, () => SpawnExplosion());
        }
        else {
            SpawnExplosion();
        }
    }
    private void SpawnExplosion() {
        if (explosioner != null && HitInfor != null) {
            var explosionClone = GameManager.Instance.GameLoader.SpawnExplosion(explosioner, transform.position);
            explosionClone.SetHitInfor(HitInfor.Damage.Value, null, HitInfor.Causer)
                       .SetRadius(radius)
                       .Explosioning();
        }
        Destroy();
    }
    private bool IsMoveComplete() {
        return Vector2.Distance(transform.position, target) < 1;
    }

    public void ResetSpeed(bool useSpeedBase) {
        speed = useSpeedBase ? SpeedStat.Value : 0;
    }
    public void WarningEffect() {
        burnEffect.StartEffect(true);
    }
    protected override bool IsBlockHit() {
        return false;
    }

    protected override void Hit(Collider2D collision) {
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(hitInfor, transform.position);
            AttackBoom(false);
        }
        Destroy();
    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (!isNotTriggerBorder && (collision.CompareTag(GameTag.Respawn))) {
            Destroy();
        }
        if (IsBlockHit()) {
            return;
        }
        foreach (var target in targetTypes) {
            if (collision.CompareTag(target.ToString())) {
                Hit(collision);
                return;
            }
        }
    }
    protected override void Destroy() {
        base.Destroy();
        onMoveComplete = null;
    }
}