using DG.Tweening;
using Helper;
using UnityEngine;

public class MinibossMove : EnemyMove {
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

    private MinibossBase minibossBase;
    public MinibossBase MinibossBase {
        get {
            if (minibossBase == null) {
                minibossBase = EnemyBase as MinibossBase;
            }
            return minibossBase;
        }
    }

    public virtual void StartMoveAfterAttack() {
        Vector2 curPoint = transform.position;
        Vector2 point = Vector2.zero;
        int count = 0;
        do {
            count++;
            point = GetPointMove();
        }
        while (Vector2.Distance(point, curPoint) < minDistance && count < 20);

        direction = (point - (Vector2)transform.position).normalized;
        isEndMove = false;
        Vector2 midPoint = (point + (Vector2)transform.position) / 2;
        Vector2 n = Vector2.Perpendicular(direction);
        Vector2 midPathPoint = midPoint + n.normalized * moveRandomPointMovePathValue.GetRandomValue();
        Vector3[] pathPoints = new Vector3[3];
        pathPoints[0] = transform.position;
        pathPoints[1] = midPathPoint;
        pathPoints[2] = point;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, moveSpeed, PathType.CatmullRom, PathMode.TopDown2D, 5).SetSpeedBased(true).OnComplete(OnEndMoveAfterAttack).SetEase(moveCurve).OnKill(OnEndMoveBeKill);
    }
    protected virtual void OnEndMoveAfterAttack() {
        isEndMove = true;
    }
    private void OnEndMoveBeKill() {
        if (!isEndMove)
            isEndMove = true;
    }

    protected virtual Vector2 GetPointMove() {
        return GetRandomInArea(moveArea);
    }

#if UNITY_EDITOR
    [SerializeField] MinibossMove reference;
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
