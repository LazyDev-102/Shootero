

using Helper;
using System;
using UnityEngine;

public class BoomerangBullet : BulletBase {
    [SerializeField] protected Rigidbody2D myRigi;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float delayGoBack;

    private float speed;
    private float accelerationSpeed = 0f;


    private float currentSpeed;
    private Vector2 direction;
    private float deltaAttack;
    private Vector2 pointTarget;
    private bool isMoveBack;
    private Countdowner deltaCountdowner;
    private Transform transformTarget;
    private Action onEndBack;
    private Countdowner delayGoBackCountdowner;


    private void OnDestroy() {
        StopAllCoroutines();
    }
    private void OnDisable() {
        StopAllCoroutines();
    }

    public void Shoot(Vector2 pointTarget, float speed, float acceleration = 0f) {
        isMoveBack = false;
        this.speed = speed + SpeedStat.Value;
        currentSpeed = speed + SpeedStat.Value;
        this.pointTarget = pointTarget;
        direction = (pointTarget - (Vector2)transform.position).normalized;
        transform.up = direction;
        this.accelerationSpeed = acceleration;
    }

    public void SetTransformTarget(Transform target) {
        transformTarget = target;
    }

    public void SetInfo(float deltaAttack) {
        this.deltaAttack = deltaAttack;
        deltaCountdowner.StartCountdown(deltaAttack);
    }

    public void AddOnEndBack(Action onEndBack) {
        this.onEndBack = onEndBack;
    }

    private void FixedUpdate() {
        float deltaTime = Time.fixedDeltaTime * Time.timeScale;
        if (isMoveBack) {
            sprite.transform.Rotate(Vector3.back, -1 * rotateSpeed * deltaTime);
            if (delayGoBackCountdowner.IsTimeOut()) {
                Vector2 dir = ((Vector2)transformTarget.position - myRigi.position).normalized;
                myRigi.MovePosition(myRigi.position + dir * currentSpeed * deltaTime);
                currentSpeed += accelerationSpeed * deltaTime;
                if (Vector2.Distance(myRigi.position, transformTarget.position) < currentSpeed * deltaTime) {
                    EndMoveBack();
                }
            }
            else {
                delayGoBackCountdowner.Countdowning(deltaTime);
            }
        }
        else {
            sprite.transform.Rotate(Vector3.back, rotateSpeed * deltaTime);
            myRigi.MovePosition(myRigi.position + direction * currentSpeed * deltaTime);
            currentSpeed += accelerationSpeed * deltaTime;
            if (Vector2.Distance(myRigi.position, pointTarget) < currentSpeed * deltaTime) {
                StartMoveBack();
            }
        }
        deltaCountdowner.Countdowning(deltaTime);
        if (deltaCountdowner.IsTimeOut() && gameObject.activeInHierarchy) {
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

    private void StartMoveBack() {
        delayGoBackCountdowner.StartCountdown(delayGoBack);
        isMoveBack = true;
        currentSpeed = speed;
    }

    private void EndMoveBack() {
        onEndBack?.Invoke();
        onEndBack = null;
        Destroy();
    }
}
