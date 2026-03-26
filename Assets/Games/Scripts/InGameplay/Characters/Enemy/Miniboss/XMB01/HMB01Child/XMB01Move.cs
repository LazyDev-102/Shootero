using UnityEngine;
using DG.Tweening;
using Helper;

public class XMB01Move : MinibossMove {

    [SerializeField] protected float attackMoveSpeed = 5;
    [SerializeField] protected AnimationCurve attackMoveCurve;
    [SerializeField] protected RangeFloatValue attackMoveRandomPointMovePathValue;

    #region References
    private XMB01Base XMb01Base;
    public XMB01Base XMB01Base {
        get {
            if (XMb01Base == null) {
                XMb01Base = EnemyBase as XMB01Base;
            }
            return XMb01Base;
        }
    }
    #endregion

    public override void StartMoveAfterAttack() {
        Vector2 curPoint = transform.position;
        Vector2 point = Vector2.zero;

        Vector3[] pathPoints = new Vector3[3];
        var timeLoop = 0;
        do {
            do {
                point = GetPointMove();
            }
            while (Vector2.Distance(point, curPoint) < minDistance);
            timeLoop++;
            if (timeLoop > 100)
                break;

            direction = (point - (Vector2)transform.position).normalized;
            isEndMove = false;
            Vector2 midPoint = (point + (Vector2)transform.position) / 2;
            Vector2 n = Vector2.Perpendicular(direction);
            Vector2 midPathPoint = midPoint + n.normalized * moveRandomPointMovePathValue.GetRandomValue();
            pathPoints[0] = transform.position;
            pathPoints[1] = midPathPoint;
            pathPoints[2] = point;
        }
        while (!XMB01Base.MyParent.CanMoveToPosition(pathPoints[2]));

        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, moveSpeed, PathType.CatmullRom, PathMode.TopDown2D, 5).SetSpeedBased(true).OnComplete(OnEndMoveAfterAttack).SetEase(moveCurve).OnKill(OnEndMoveBeKill);
    }
    private void OnEndMoveBeKill() {
        if (!isEndMove)
            isEndMove = true;
    }

    public override void StartMoveAppear() {
        Vector2 pointAppear = GetRandomInArea(appearArea);
        targetMovePoint = pointAppear;
        direction = (pointAppear - (Vector2)transform.position).normalized;
        MyRigi.transform.localEulerAngles = new Vector3(0, 0, (Vector2.SignedAngle(Vector2.up, direction)));
        distanceMove = Vector2.Distance(transform.position, pointAppear);
        float timeMove = distanceMove / appearMoveSpeed;
        isEndMove = false;
        Vector2 midPoint = (pointAppear + (Vector2)transform.position) / 2;
        Vector2 n = Vector2.Perpendicular(direction);
        Vector2 midPathPoint = midPoint + n.normalized * appearRandomPointMovePathValue.GetRandomValue();
        Vector3[] pathPoints = new Vector3[3];
        pathPoints[0] = transform.position;
        pathPoints[1] = midPathPoint;
        pathPoints[2] = pointAppear;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, appearMoveSpeed, PathType.CatmullRom, PathMode.TopDown2D, 5).SetSpeedBased(true).SetLookAt(0.01f, Vector3.forward, Vector3.right).OnComplete(EndMoveAppear).SetEase(appearCurveMove).OnKill(OnEndMoveBeKill);
    }

    public void SetTargetMoveAttack(Vector2 target, float moveSpeed) {
        currentMoveSpeed = moveSpeed;
        SetDirectionMove(target - myRigi.position);
    }

    public void StartMoveAfterAttackXB01(Vector2 vector2) {
        var point = GetPointMoveXMB01(vector2);

        Vector2 curPoint = transform.position;
        direction = (point - (Vector2)transform.position).normalized;
        distanceMove = Vector2.Distance(curPoint, point);
        float timeMove = distanceMove / attackMoveSpeed;
        isEndMove = false;
        Vector2 midPoint = (point + (Vector2)transform.position) / 2;
        Vector2 n = Vector2.Perpendicular(direction);
        Vector2 midPathPoint = midPoint + n.normalized * attackMoveRandomPointMovePathValue.GetRandomValue();
        Vector3[] pathPoints = new Vector3[3];
        pathPoints[0] = transform.position;
        pathPoints[1] = midPathPoint;
        pathPoints[2] = point;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, timeMove, PathType.CatmullRom, PathMode.TopDown2D, 5).OnComplete(OnEndMoveAfterAttackXB01).SetEase(attackMoveCurve).OnKill(OnEndMoveBeKill);
    }

    private void OnEndMoveAfterAttackXB01() {
        isEndMove = true;
    }
    public virtual Vector2 GetPointMoveXMB01(Vector2 point) {
        return GetRandomInArea(point);
    }
}
