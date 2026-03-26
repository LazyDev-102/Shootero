using UnityEngine;
using DG.Tweening;
using Helper;

public class B14ChildMove : BossMove {


    #region References
    private B14ChildBase mb15ChildBase;
    public B14ChildBase B14ChildBase {
        get {
            if (mb15ChildBase == null) {
                mb15ChildBase = EnemyBase as B14ChildBase;
            }
            return mb15ChildBase;
        }
    }
    #endregion

    [SerializeField] protected float moveTime = 5;
    [SerializeField] protected AnimationCurve moveCurve1 = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField] protected RangeFloatValue moveRandomPointMovePathValue1;


    private float rotateSpeed;
    private Vector2 targetPosition;

    public void SetTargetPosition(Vector2 position) {
        targetPosition = position;
    }

    public void StartTargetPosition() {
        targetMovePoint = targetPosition;
        direction = (targetPosition - (Vector2)transform.position).normalized;
        MyRigi.transform.localEulerAngles = new Vector3(0, 0, (Vector2.SignedAngle(Vector2.up, direction)));
        isEndMove = false;
        Vector2 midPoint = (targetPosition + (Vector2)transform.position) / 2;
        Vector2 n = Vector2.Perpendicular(direction);
        Vector2 midPathPoint = midPoint + n.normalized * moveRandomPointMovePathValue1.GetRandomValue();
        Vector3[] pathPoints = new Vector3[3];
        pathPoints[0] = transform.position;
        pathPoints[1] = midPathPoint;
        pathPoints[2] = targetPosition;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, moveTime, PathType.CatmullRom, PathMode.TopDown2D, 5).SetLookAt(0.01f, Vector3.forward, Vector3.right).OnComplete(OnEndMove).SetEase(moveCurve1);
    }

    private void OnEndMove() {
        isEndMove = true;
    }

    public void SetRotateSpeed(float speed) {
        rotateSpeed = speed;
    }

    public void RotatingSefl() {
        transform.Rotate(Vector3.back, rotateSpeed * Time.deltaTime);
    }
}
