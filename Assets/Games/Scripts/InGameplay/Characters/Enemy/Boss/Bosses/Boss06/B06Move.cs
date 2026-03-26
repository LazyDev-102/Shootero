using DG.Tweening;
using UnityEngine;

public class B06Move : BossMove {
    [Header("B06Move")]
    [SerializeField] protected float attackMoveSpeed = 5;
    [SerializeField] protected AnimationCurve attackMoveCurve;
    [SerializeField] protected RangeFloatValue attackMoveRandomPointMovePathValue;
    private Vector2 origin;
    private bool hasCompleteMoveAttack2;
    public void SetTargetMoveAttack(Vector2 target, float moveSpeed) {
        currentMoveSpeed = moveSpeed;
        SetDirectionMove(target - myRigi.position);
        //LookTarget(target);
    }

    public void StartMoveAfterAttackB06(Vector2 vector2) {
        var point = GetPointMoveB06(vector2);
        Vector2 curPoint = transform.position;
        direction = (point - (Vector2)transform.position).normalized;
        distanceMove = Vector2.Distance(curPoint, point);
        float timeMove = distanceMove / attackMoveSpeed;
        isEndMove = false;
        hasCompleteMoveAttack2 = false;
        Vector2 midPoint = (point + (Vector2)transform.position) / 2;
        Vector2 n = Vector2.Perpendicular(direction);
        Vector2 midPathPoint = midPoint + n.normalized * attackMoveRandomPointMovePathValue.GetRandomValue();
        Vector3[] pathPoints = new Vector3[3];
        pathPoints[0] = transform.position;
        pathPoints[1] = midPathPoint;
        pathPoints[2] = point;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, timeMove, PathType.CatmullRom, PathMode.TopDown2D, 5).OnComplete(OnEndMoveAfterAttackB06).SetEase(attackMoveCurve).OnKill(OnEndMoveBeKill);
        DOVirtual.DelayedCall(timeMove + 0.5f, () => {
            if (!isEndMove)
                isEndMove = true;
        });
    }

    private void OnEndMoveBeKill() {
        if (!isEndMove)
            isEndMove = true;
    }
    private void OnEndMoveAfterAttackB06() {
        isEndMove = true;
        hasCompleteMoveAttack2 = true;
    }


    public virtual Vector2 GetPointMoveB06(Vector2 point) {
        return GetRandomInArea(point);
    }


}