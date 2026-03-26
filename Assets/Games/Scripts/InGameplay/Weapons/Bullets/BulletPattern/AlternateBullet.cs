using UnityEngine;

public class AlternateBullet : BulletBase {
    private float speed;
    private float accelerationSpeed = 0f;
    [SerializeField] protected Rigidbody2D myRigi;
    [SerializeField] protected bool canPiercing;
    [SerializeField] protected int maxPiercing = 4;
    [SerializeField] protected float reducePiercingPercent;

    protected int piercingTime;
    private Vector2 direction;
    private float minSpeed;

    public override void Initalize() {
        base.Initalize();
        if (canPiercing) {
            piercingTime = 0;
            SetAlpha(1);
        }
    }

    public void Shoot(float speed, Vector2 direction, float acceleration = 0f, float minSpeed = float.MinValue) {
        this.speed = speed + SpeedStat.Value;
        transform.up = direction.normalized;
        this.direction = direction.normalized;
        this.accelerationSpeed = acceleration;
        this.minSpeed = minSpeed;
    }

    public void Shoot(float speed, Quaternion rotation) {
        this.speed = speed;
        transform.rotation = rotation;
        this.direction = transform.up;
    }

    private void FixedUpdate() {
        myRigi.MovePosition(myRigi.position + direction * speed * Time.fixedDeltaTime);
        speed += accelerationSpeed * Time.fixedDeltaTime;
        speed = Mathf.Max(speed, minSpeed);
    }

    protected override bool IsBlockHit() {
        if (canPiercing) {
            if (maxPiercing < 0) {
                return false;
            }
            return piercingTime >= maxPiercing;
        }
        return isHitted;
    }

    protected override void Hit(Collider2D collision) {
        if (canPiercing) {
            piercingTime++;
            IHitbox victim = collision.GetComponent<IHitbox>();
            if (victim != null) {
                if (piercingTime > 1) {
                    if (reducePiercingPercent > 0) {
                        hitInfor.Damage.AddModifier(new StatModifier(reducePiercingPercent, StatModType.PercentAdd));
                    }
                }
                SetAlpha(1 - Mathf.Abs(reducePiercingPercent) * piercingTime);
                victim.TakeHit(hitInfor, transform.position);
            }
            if (piercingTime >= maxPiercing) {
                DestroyWithEffect();
            }
        }
        else {
            isHitted = true;
            GetComponent<Collider2D>().enabled = false;
            IHitbox victim = collision.GetComponent<IHitbox>();
            if (victim != null) {
                victim.TakeHit(hitInfor, transform.position);
            }
            DestroyWithEffect();
        }
    }
}