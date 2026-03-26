using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : BulletBase {
    [SerializeField] protected Rigidbody2D myRigi;
    [SerializeField] protected int maxPiercing = 4;

    private int piercingTime;
    private float speed;
    private float accelerationSpeed = 0f;
    private float deltaHit;
    private float damagePercent;

    private Countdowner hitCountdowner;
    private Vector2 direction;
    private List<CharacterHitbox> characters = new List<CharacterHitbox>();
    public override void Initalize() {
        base.Initalize();
        piercingTime = 0;
        characters.Clear();

    }

    public void Shoot(float speed, Vector2 direction, float acceleration = 0f) {
        this.speed = speed;
        transform.up = direction.normalized;
        this.direction = direction.normalized;
        this.accelerationSpeed = acceleration;
    }

    public void Shoot(float speed, Quaternion rotation) {
        this.speed = speed;
        transform.rotation = rotation;
        this.direction = transform.up;
    }

    public void SetInfo(float damagePercent, float deltaHit) {
        this.deltaHit = deltaHit;
        this.damagePercent = damagePercent;
        HitInfor.Damage.AddModifier(new StatModifier(damagePercent, StatModType.PercentMult));
        hitCountdowner.StartCountdown(deltaHit);
        if (gameObject.activeInHierarchy)
            StartCoroutine(IStartHit());
    }

    private void FixedUpdate() {
        myRigi.MovePosition(myRigi.position + direction * speed * Time.fixedDeltaTime);
        speed += accelerationSpeed * Time.fixedDeltaTime;
        hitCountdowner.Countdowning(Time.fixedDeltaTime);
        if (hitCountdowner.IsTimeOut()) {
            hitCountdowner.StartCountdown(deltaHit);
            if (gameObject.activeInHierarchy)
                StartCoroutine(IStartHit());
        }
    }

    private IEnumerator IStartHit() {
        HitCollider.enabled = true;
        yield return Yielder.WaitForEndOfFrame;
        HitCollider.enabled = false;
    }

    protected override bool IsBlockHit() {
        return false;
    }

    protected override void Hit(Collider2D collision) {
        piercingTime++;
        if (piercingTime > maxPiercing) {
            DestroyWithEffect();
            return;
        }
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(hitInfor, transform.position);
        }
    }
}
