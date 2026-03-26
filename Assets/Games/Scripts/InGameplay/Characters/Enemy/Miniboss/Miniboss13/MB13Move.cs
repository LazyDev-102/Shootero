using DG.Tweening;
using UnityEngine;

public class MB13Move : MinibossMove {

    #region MB13 Attack Special
    [SerializeField] private float rageMoveSpeed;

    public override void MoveDirect() {
        //transform.position = transform.position + (Vector3)(direction * currentMoveSpeed * Time.deltaTime);
    }
    public virtual void StartMoveRage() {
        Vector2 point = GetRandomInArea(appearArea);

        targetMovePoint = point;
        direction = (point - (Vector2)transform.position).normalized;
        MyRigi.transform.localEulerAngles = new Vector3(0, 0, 180);
        distanceMove = Vector2.Distance(transform.position, point);
        isEndMove = false;
        Vector2 midPoint = (point + (Vector2)transform.position) / 2;
        Vector2 n = Vector2.Perpendicular(direction);
        Vector2 midPathPoint = midPoint + n.normalized * appearRandomPointMovePathValue.GetRandomValue();
        Vector3[] pathPoints = new Vector3[2];
        pathPoints[0] = transform.position;
        pathPoints[1] = point;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, rageMoveSpeed, PathType.CatmullRom, PathMode.TopDown2D, 5).SetSpeedBased(true).OnComplete(EndMoveRage).SetEase(Ease.Linear);
    }

    private void EndMoveRage() {
        isEndMove = true;
    }
    #endregion

    #region MB13 Attack 2
    [Header("MB13 Attack 2")]
    [SerializeField] protected float attackMoveSpeed = 5;
    [SerializeField] protected AnimationCurve attackMoveCurve;
    [SerializeField] protected RangeFloatValue attackMoveRandomPointMovePathValue;
    private Vector2 origin;
    public void SetTargetMoveAttack(Vector2 target, float moveSpeed) {
        currentMoveSpeed = moveSpeed;
        SetDirectionMove(target - myRigi.position);
        //LookTarget(target);
    }

    public void StartMoveAfterAttackMB13(Vector2 vector2) {
        var point = GetPointMoveMB13(vector2);

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
        curMoveTween = transform.DOPath(pathPoints, timeMove, PathType.CatmullRom, PathMode.TopDown2D, 5).OnComplete(OnEndMoveAfterAttackMB13).SetEase(attackMoveCurve).OnKill(OnEndMoveBeKill);
    }

    private void OnEndMoveBeKill() {
        if (!isEndMove)
            isEndMove = true;
    }
    private void OnEndMoveAfterAttackMB13() {
        isEndMove = true;
    }
    protected virtual Vector2 GetPointMoveMB13(Vector2 point) {
        return GetRandomInArea(point);
    }
    #endregion
}
