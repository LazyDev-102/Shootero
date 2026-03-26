using Gemmob;

using Helper;
using System.Collections;
using UnityEngine;

public class RotateHomingBullet : BulletBase {
    [SerializeField] private float turn;
    [SerializeField] private float delayHoming;
    [SerializeField] private float timeHoming;
    [SerializeField] private Rigidbody2D myRigi;
    [SerializeField] private float rotateSpeed;

    private float speed;
    private bool isHoming;
    private float countdownHoming;

    private Transform myTransform;
    private Transform target;
    private float deltaAttack;

    private Countdowner deltaCountdowner;

    private void Awake() {
        myTransform = transform;
    }

    public void SetInfo(float deltaAttack, float timeHoming) {
        this.deltaAttack = deltaAttack;
        deltaCountdowner.StartCountdown(deltaAttack);
        this.timeHoming = timeHoming;
    }

    public void Shoot(float speed, Transform target, Vector2 direction) {
        this.speed = speed + SpeedStat.Value;
        this.target = target;
        isHoming = false;
        myTransform.up = direction;
        myRigi.velocity = myTransform.up * speed;
        if (gameObject.activeInHierarchy)
            StartCoroutine(HoldHoming());
    }

    private IEnumerator HoldHoming() {
        yield return Yielder.Wait(delayHoming);
        countdownHoming = timeHoming;
        isHoming = true;
    }

    private void FixedUpdate() {
        float deltaTime = Time.fixedDeltaTime * Time.timeScale;
        if (isHoming && target != null && countdownHoming > 0) {
            myRigi.velocity = myTransform.up * speed;
            Vector3 targetVector = target.position - myTransform.position;
            float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;
            myRigi.angularVelocity = -1 * rotatingIndex * turn;
            countdownHoming -= Time.fixedDeltaTime;
        }
        else {
            myRigi.angularVelocity = 0;
            myTransform.up = myRigi.velocity;
        }

        sprite.transform.Rotate(Vector3.back, rotateSpeed * deltaTime);

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
}
