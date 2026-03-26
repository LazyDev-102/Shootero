using UnityEngine;

public class FrontBullet : BulletBase {
    protected float speed;
    protected float accelerationSpeed = 0f;
    [SerializeField] protected bool isRotate;
    [SerializeField] protected float rotateSpeed;

    protected Vector2 direction;
    protected float minSpeed;

    public virtual void Shoot(float speed, Vector2 direction, float acceleration = 0f, float minSpeed = float.MinValue) {
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

    protected virtual void FixedUpdate() {
        float deltaTime = Time.fixedDeltaTime * Time.timeScale;
        MyRigi.MovePosition(MyRigi.position + direction * speed * deltaTime);
        speed += accelerationSpeed * deltaTime;
        speed = Mathf.Max(speed, minSpeed);
        if (isRotate && sprite) {
            sprite.transform.Rotate(Vector3.back, rotateSpeed * deltaTime);
        }
    }

    protected override bool IsBlockHit() {
        return isHitted;
    }

    protected override void Hit(Collider2D collision) {
        isHitted = true;
        HitCollider.enabled = false;
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(hitInfor, transform.position);
        }
        DestroyWithEffect();
    }
    public virtual void SetTimeFading(float time) {
    }
    public virtual void ChangeSpriteSize(float size) {
        transform.localScale *= size;
    }
}