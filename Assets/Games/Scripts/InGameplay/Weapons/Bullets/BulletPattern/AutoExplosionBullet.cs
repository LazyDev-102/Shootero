

using UnityEngine;

public class AutoExplosionBullet : BulletBase {
    private float speed;
    private float accelerationSpeed = 0f;
    [SerializeField] protected Rigidbody2D myRigi;
    [SerializeField] protected bool isRotate;
    [SerializeField] protected float rotateSpeed;

    private Vector2 direction;
    private float minSpeed;
    private Countdowner timeLifeCountdowner = new Countdowner();
    private bool canUpdate = true;
    public override void Initalize() {
        base.Initalize();
    }

    public void Shoot(float speed, Vector2 direction, float acceleration = 0f, float minSpeed = float.MinValue, float timeLife = float.MaxValue) {
        this.speed = speed + SpeedStat.Value;
        transform.up = direction.normalized;
        this.direction = direction.normalized;
        this.accelerationSpeed = acceleration;
        this.minSpeed = minSpeed;
        timeLifeCountdowner.StartCountdown(timeLife);
    }

    private void FixedUpdate() {
        if (canUpdate) {
            float deltaTime = Time.fixedDeltaTime * Time.timeScale;
            myRigi.MovePosition(myRigi.position + direction * speed * deltaTime);
            speed += accelerationSpeed * deltaTime;
            speed = Mathf.Max(speed, minSpeed);
            if (isRotate && sprite) {
                sprite.transform.Rotate(Vector3.back, rotateSpeed * deltaTime);
            }

            if (!isHitted) {
                if (timeLifeCountdowner.IsTimeOut()) {
                    DestroyWithEffect();
                }
                timeLifeCountdowner.Countdowning(deltaTime);
            }
        }
    }


    protected override void Hit(Collider2D collision) {
        isHitted = true;
        GetComponent<Collider2D>().enabled = false;
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(hitInfor, transform.position);
        }
        DestroyWithEffect();
    }

    public AutoExplosionBullet SetTimeLife(float time) {
        timeLifeCountdowner.StartCountdown(time);
        return this;
    }
    
    public AutoExplosionBullet SetCanUpdate(bool state) {
        canUpdate = state;
        return this;
    }

}
