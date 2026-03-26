using UnityEngine;
using DG.Tweening;
using Helper;

public class MB09Move : MinibossMove {


    #region References
    private MB09Base mb09Base;
    public MB09Base MB09Base {
        get {
            if (mb09Base == null) {
                mb09Base = EnemyBase as MB09Base;
            }
            return mb09Base;
        }
    }
    #endregion


    [SerializeField, Range(0f, 5f)] protected float timeOneRoundRotation = 1f;
    [SerializeField] protected DG.Tweening.DOTweenAnimation anim;
    public override void Initialize() {
        base.Initialize();
        if (anim) {
            anim.duration = timeOneRoundRotation;
            anim.DOPlay();
        }
    }
    public override void Destroy() {
        anim.DOKill();
        base.Destroy();
    }

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
        while (!MB09Base.MyParent.CanMoveToPosition(pathPoints[2]));

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
}
