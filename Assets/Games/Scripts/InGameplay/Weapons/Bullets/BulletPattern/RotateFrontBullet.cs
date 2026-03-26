
using Helper;
using UnityEngine;



public class RotateFrontBullet : BulletBase {
    private float speed;
    private float accelerationSpeed = 0;
    [SerializeField] protected Rigidbody2D myRigi;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float rotateAccelerSpeed;
    [SerializeField] private bool isNotTriggerBorder;

    private Vector2 direction;
    private float deltaAttack;
    private float minSpeed;

    private Countdowner deltaCountdowner;

    public override void Initalize() {
        base.Initalize();
    }

    public void Shoot(Vector2 direction, float speed, float acceleration = 0f, float minSpeed = float.MinValue) {
        this.speed = speed + SpeedStat.Value;
        transform.up = direction.normalized;
        this.direction = direction.normalized;
        this.accelerationSpeed = acceleration;
        this.minSpeed = minSpeed;
    }

    public void SetRotateSpeed(float rotateSpeed, float rotateAcceler) {
        this.rotateSpeed = rotateSpeed;
        this.rotateAccelerSpeed = rotateAcceler;
    }

    public void SetInfo(float deltaAttack) {
        this.deltaAttack = deltaAttack;
        deltaCountdowner.StartCountdown(deltaAttack);
    }

    private void FixedUpdate() {
        float deltaTime = Time.fixedDeltaTime * Time.timeScale;
        myRigi.MovePosition(myRigi.position + direction * speed * deltaTime);
        speed += accelerationSpeed * deltaTime;
        speed = Mathf.Max(speed, minSpeed);

        if (sprite) {
            sprite.transform.Rotate(Vector3.back, rotateSpeed * deltaTime);
            rotateSpeed += rotateAccelerSpeed * deltaTime;
        }

        deltaCountdowner.Countdowning(deltaTime);
        if (deltaCountdowner.IsTimeOut()) {
            HitCollider.enabled = true;
            deltaCountdowner.StartCountdown(deltaAttack);
            this.DelayFrame(2, DisableCollider);
        }
    }

    private void DisableCollider() {
        HitCollider.enabled = false;
    }

    protected override bool IsBlockHit() {
        return false;
    }

    protected override void Hit(Collider2D collision) {
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(hitInfor, transform.position);
        }
    }

    public void ResetSpeed(bool useSpeedBase) {
        speed = useSpeedBase ? SpeedStat.Value : 0;
    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (!isNotTriggerBorder && (collision.CompareTag("Respawn") || collision.CompareTag("Finish"))) {
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
}