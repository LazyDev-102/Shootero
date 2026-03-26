
using DG.Tweening;
using UnityEngine;

public class RocketBullet : BulletBase {
    [SerializeField] private LayerMask targetMask;

    private float speed;
    private bool canAttack;
    private bool isFoundTarget;
    private float offsetPoint = 1;
    private float timeAppear = 4;
    private Vector3 direction;
    private Vector3 prePos = Vector3.zero;
    private Vector3 nextPos = Vector3.zero;
    private Transform target;
    private Tweener curMoveTween;
    private Countdowner delayAttackCd = new Countdowner();

    public void Shoot(bool isLeft, float speed, float offset, float delayAttack) {
        this.speed = speed;
        offsetPoint = offset;
        delayAttackCd.StartCountdown(delayAttack);
        canAttack = true;
        isFoundTarget = false;
        sprite.transform.up = Vector3.zero;
        MoveAttack(timeAppear, isLeft);
    }

    private void GetTransformTarget() {
        if (!isFoundTarget) {
            isFoundTarget = true;
            curMoveTween?.Kill(false);
            var gameLoader = GameManager.Instance.GameLoader;
            var result = gameLoader.GetRandomEnemy();
            target = result != null ? result.transform : null;
        }
    }
    private void Update() {
        if (canAttack) {
            if (delayAttackCd.IsTimeOut()) {
                GetTransformTarget();
                //MoveAttack();
                MoveAttackTemp();
            }
            else {
                delayAttackCd.Countdowning(Time.deltaTime);
            }
        }
    }
    protected override void RemoveMe() {
        canAttack = false;
        base.RemoveMe();
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameTag.Respawn)) {
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
    private void MoveAttack(float duration, bool isLeft) {
        Vector3[] pathPoints = new Vector3[3];
        pathPoints[0] = transform.position;
        if (isLeft) {
            pathPoints[1] = transform.position + new Vector3(-offsetPoint, -offsetPoint, 0);
            pathPoints[2] = pathPoints[1] + new Vector3(-offsetPoint, offsetPoint * 20, 0);
        }
        else {
            pathPoints[1] = transform.position + new Vector3(offsetPoint, -offsetPoint, 0);
            pathPoints[2] = pathPoints[1] + new Vector3(offsetPoint, offsetPoint * 20, 0);
        }
        if (curMoveTween != null)
            curMoveTween.Kill();
        curMoveTween = transform.DOPath(pathPoints, duration, PathType.CatmullRom, PathMode.TopDown2D, 5).SetLookAt(0.01f, Vector3.forward, Vector3.right);
    }

    private void MoveAttack() {
        direction = (target == null || !target.gameObject.activeInHierarchy) ? transform.up.normalized : (target.position - transform.position).normalized;
        prePos = transform.position;
        nextPos = transform.position + direction * speed * Time.deltaTime;
        transform.position = nextPos;
        sprite.transform.up = (nextPos - prePos).normalized;
    }
    private void MoveAttackTemp() {
        var direction = (target == null || !target.gameObject.activeInHierarchy) ? (Vector2)transform.up.normalized : ((Vector2)target.position - MyRigi.position).normalized;
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        MyRigi.angularVelocity = -rotateAmount * speed * 10;
        MyRigi.velocity = transform.up * speed;
    }
    private bool launch;
    private void Attack01(bool isLeft) {
        var pos = isLeft ? transform.position + new Vector3(-offsetPoint, -offsetPoint, 0) : transform.position + new Vector3(offsetPoint, -offsetPoint, 0);
        if (curMoveTween != null)
            curMoveTween.Kill();
        curMoveTween = transform.DOMove(pos, 1f)
                                .SetEase(Ease.InOutSine)
                                .OnComplete(() => launch = true)
                                .OnKill(() => launch = true);
    }

}
