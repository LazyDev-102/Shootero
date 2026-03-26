using DG.Tweening;
using Helper;
using UnityEngine;

public class B14Move : BossMove {
    [Header("B14Move")]
    [SerializeField] protected float attackMoveSpeed = 5;
    [SerializeField] protected AnimationCurve attackMoveCurve;
    [SerializeField] protected RangeFloatValue attackMoveRandomPointMovePathValue;
    private Vector2 origin;
    public void SetTargetMoveAttack(Vector2 target, float moveSpeed) {
        currentMoveSpeed = moveSpeed;
        SetDirectionMove(target - myRigi.position);
        //LookTarget(target);
    }

    public void StartMoveAfterAttackB14(Vector2 vector2) {
        var point = GetPointMoveB14(vector2);

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
        curMoveTween = transform.DOPath(pathPoints, timeMove, PathType.CatmullRom, PathMode.TopDown2D, 5).OnComplete(OnEndMoveAfterAttackB14).SetEase(attackMoveCurve).OnKill(OnEndMoveBeKill);
    }

    private void OnEndMoveBeKill() {
        if (!isEndMove)
            isEndMove = true;
    }
    private void OnEndMoveAfterAttackB14() {
        isEndMove = true;
    }
    public virtual Vector2 GetPointMoveB14(Vector2 point) {
        return GetRandomInArea(point);
    }

    public void MoveRageAttack(Transform target, float duration, System.Action onCompleted) {
        Vector3[] pathPoints = new Vector3[4];
        pathPoints[0] = transform.position;
        pathPoints[1] = transform.position + transform.up.normalized * 1.5f;
        pathPoints[2] = target.position;
        pathPoints[3] = target.position.y > 0 ? target.position + target.up.normalized * 10 : target.position + target.up.normalized * -10;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, duration, PathType.CatmullRom, PathMode.TopDown2D, 5)
                                .SetLookAt(0.01f, Vector3.forward, Vector3.right)
                                .OnComplete(() => { onCompleted?.Invoke(); onCompleted = null; })
                                .OnKill(() => { onCompleted?.Invoke(); onCompleted = null; })
                                .SetEase(Ease.OutQuart);
    }
}