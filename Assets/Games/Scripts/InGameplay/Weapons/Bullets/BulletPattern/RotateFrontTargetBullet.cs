using DG.Tweening;
using UnityEngine;

public class RotateFrontTargetBullet : BulletBase {
    [SerializeField] private DOTweenAnimation rotateAnim;

    private bool canAttack;
    private float moveTime;
    private float deltaAttack;
    private float rotateTimeSpeed;
    private float timeLife;

    private Vector2 targetPos;
    private Countdowner deltaCountdowner;
    private Tweener moveTweener;

    public override void Initalize() {
        base.Initalize();
        timeLife = 5;
        moveTime = 1;
        deltaAttack = 0.1f;
        canAttack = false;
        targetPos = transform.up;
    }

    public void Shoot(Vector2 targetPos, float moveTime, float deltaAttack, float rotateTimeSpeed, float timeLife) {
        this.deltaAttack = deltaAttack;
        this.moveTime = moveTime;
        this.targetPos = targetPos;
        this.rotateTimeSpeed = rotateTimeSpeed;
        this.timeLife = timeLife;

        canAttack = true;
        transform.up = targetPos.normalized;
        deltaCountdowner.StartCountdown(deltaAttack);
        SetTimeLife();
        SetRotateStatus();
        Move2Target();
    }

    private void SetTimeLife() {
        var autoDestroy = GetComponent<AutoDestroy>();
        if (autoDestroy != null) {
            autoDestroy.StartAutoDestroy(timeLife, AutoDestroy.HideType.Pool);
        } else {
            gameObject.AddComponent<AutoDestroy>().StartAutoDestroy(timeLife, AutoDestroy.HideType.Pool);
        }
    }

    private void SetRotateStatus() {
        if (rotateAnim != null) {
            rotateAnim.duration = rotateTimeSpeed;
        }
    }

    private void Move2Target() {
        if (moveTweener != null)
            moveTweener.Kill();

        moveTweener = transform.DOMove(targetPos, moveTime)
                               .SetEase(Ease.Linear)
                               .SetAutoKill(true);
    }

    private void Update() {
        if (canAttack) {
            deltaCountdowner.Countdowning(Time.deltaTime);
            if (deltaCountdowner.IsTimeOut()) {
                HitCollider.enabled = !HitCollider.enabled;
                deltaCountdowner.StartCountdown(deltaAttack);
            }
        }
    }
    protected override void Hit(Collider2D collision) {
        isHitted = true;
        HitCollider.enabled = false;
        IHitbox victim = collision.GetComponent<IHitbox>();
        if (victim != null) {
            victim.TakeHit(HitInfor, transform.position);
        }
    }
}
