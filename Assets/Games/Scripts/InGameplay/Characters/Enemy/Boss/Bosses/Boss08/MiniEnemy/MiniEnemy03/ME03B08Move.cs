using DG.Tweening;
using UnityEngine;

public class ME03B08Move : EnemyMove {
    [SerializeField] protected float moveTime = 5;
    [SerializeField] protected AnimationCurve moveCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField] protected RangeFloatValue moveRandomPointMovePathValue;
    [SerializeField] private float rotateSpeed;

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
        Vector2 midPathPoint = midPoint + n.normalized * moveRandomPointMovePathValue.GetRandomValue();
        Vector3[] pathPoints = new Vector3[3];
        pathPoints[0] = transform.position;
        pathPoints[1] = midPathPoint;
        pathPoints[2] = targetPosition;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, moveTime, PathType.CatmullRom, PathMode.TopDown2D, 5).SetLookAt(0.01f, Vector3.forward, Vector3.right).OnComplete(OnEndMove).SetEase(moveCurve);
    }

    private void OnEndMove() {
        isEndMove = true;
    }
}
