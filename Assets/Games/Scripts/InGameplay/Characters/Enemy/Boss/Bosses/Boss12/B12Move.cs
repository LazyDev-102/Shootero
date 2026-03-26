using DG.Tweening;
using Helper;
using UnityEngine;

public class B12Move : BossMove {
    [Header("B12Move")]
    [SerializeField] protected float attackMoveSpeed = 5;
    [SerializeField] protected AnimationCurve attackMoveCurve;
    [SerializeField] protected RangeFloatValue attackMoveRandomPointMovePathValue;
    private Vector2 origin;
    public void SetTargetMoveAttack(Vector2 target, float moveSpeed) {
        currentMoveSpeed = moveSpeed;
        SetDirectionMove(target - myRigi.position);
        //LookTarget(target);
    }

    public void StartMoveAfterAttackB12(Vector2 vector2) {
        var point = GetPointMoveB12(vector2);

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
        curMoveTween = transform.DOPath(pathPoints, timeMove, PathType.CatmullRom, PathMode.TopDown2D, 5).OnComplete(OnEndMoveAfterAttackB12).SetEase(attackMoveCurve).OnKill(OnEndMoveBeKill);
    }

    private void OnEndMoveBeKill() {
        if (!isEndMove)
            isEndMove = true;
    }
    private void OnEndMoveAfterAttackB12() {
        isEndMove = true;
    }
    public virtual Vector2 GetPointMoveB12(Vector2 point) {
        return GetRandomInArea(point);
    }

}