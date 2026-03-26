using DG.Tweening;
using Helper;
using UnityEngine;

public class BossMove : EnemyMove {
    [Header("BossMove")]
    [Header("MoveAfterAttack")]
    [SerializeField] protected float moveSpeed = 5;
    [SerializeField] protected AnimationCurve moveCurve;
    [SerializeField] protected Area moveArea;
    [SerializeField] protected RangeFloatValue moveRandomPointMovePathValue;
    [SerializeField] protected float minDistance;
    [Header("KnockOut")]
    [SerializeField] protected float knockOutLookSpeed = 15;
    [SerializeField] protected RangeFloatValue knockAngleRange;

    private float knockAngleLook;

    private BossBase bossBase;
    public BossBase BossBase {
        get {
            if (bossBase == null) {
                bossBase = EnemyBase as BossBase;
            }
            return bossBase;
        }
    }

    public virtual void StartMoveAfterAttack() {
        Vector2 curPoint = transform.position;
        Vector2 point = Vector2.zero;
        int count = 0;
        do {
            point = GetPointMove();
            count++;
        }
        while (Vector2.Distance(point, curPoint) < minDistance && count < 20);

        direction = (point - (Vector2)transform.position).normalized;
        distanceMove = Vector2.Distance(curPoint, point);
        float timeMove = distanceMove / moveSpeed;
        isEndMove = false;
        Vector2 midPoint = (point + (Vector2)transform.position) / 2;
        Vector2 n = Vector2.Perpendicular(direction);
        Vector2 midPathPoint = midPoint + n.normalized * moveRandomPointMovePathValue.GetRandomValue();
        Vector3[] pathPoints = new Vector3[3];
        pathPoints[0] = transform.position;
        pathPoints[1] = midPathPoint;
        pathPoints[2] = point;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, timeMove, PathType.CatmullRom, PathMode.TopDown2D, 5).OnComplete(OnEndMoveAfterAttack).SetEase(moveCurve).OnKill(OnEndMoveBeKill);
    }
    private void OnEndMoveBeKill() {
        if (!isEndMove)
            isEndMove = true;
    }

    protected virtual void OnEndMoveAfterAttack() {
        isEndMove = true;
    }

    protected virtual Vector2 GetPointMove() {
        return GetRandomInArea(moveArea);
    }

    public override void Knockback(Vector2 causer) {
    }

    public virtual void RageKnockback() {
        Vector2 curPos = transform.position;
        Vector2 direc = -transform.up;
        try {
            if (curMoveTween != null && curMoveTween.IsPlaying()) {
                curMoveTween.Kill();
            }
            curMoveTween = transform.DOMove(curPos + direc.normalized * knockbackPower, knockbackDurantion).SetEase(knockbackCurveMove).OnComplete(OnRageKnockbackComplete);
        }
        catch {
            curMoveTween = transform.DOMove(curPos + direc.normalized * knockbackPower, knockbackDurantion).SetEase(knockbackCurveMove).OnComplete(OnRageKnockbackComplete);
        }
        //if (RandomHelper.RandomWithProbability(50)) {
        //    knockAngleLook = MyRigi.rotation + knockAngleRange.GetRandomValue();
        //}
        //else {
        //    knockAngleLook = MyRigi.rotation - knockAngleRange.GetRandomValue();
        //}

    }

    public void KnockLooking() {
        //if (knockAngleLook != MyRigi.rotation) {
        //    LookAngle(knockAngleLook, knockOutLookSpeed);
        //}
    }

    private void OnRageKnockbackComplete() {

        BossBase.IsInEffectRage = false;
    }
#if UNITY_EDITOR
    [SerializeField] BossMove reference;
    [UnityEngine.ContextMenu("Convert")]
    protected void Convert() {
        myRigi = reference.myRigi;
        currentMoveSpeed = reference.currentMoveSpeed;
        defaultCurveMove = reference.defaultCurveMove;
        appearMoveSpeed = reference.appearMoveSpeed;
        appearArea = reference.appearArea;
        appearCurveMove = reference.appearCurveMove;
        appearRandomPointMovePathValue = reference.appearRandomPointMovePathValue;
        knockbackDurantion = reference.knockbackDurantion;
        knockbackPower = reference.knockbackPower;
        knockbackCurveMove = reference.knockbackCurveMove;
        idleMoveSpeed = reference.idleMoveSpeed;
        idleMoveCurve = reference.idleMoveCurve;
        paths = reference.paths;
        dotweenPaths = reference.dotweenPaths;
        moveSpeed = reference.moveSpeed;
        moveCurve = reference.moveCurve;
        moveArea = reference.moveArea;
        moveRandomPointMovePathValue = reference.moveRandomPointMovePathValue;
        minDistance = reference.minDistance;
        knockAngleLook = reference.knockAngleLook;
        knockAngleRange = reference.knockAngleRange;
    }
#endif
}
