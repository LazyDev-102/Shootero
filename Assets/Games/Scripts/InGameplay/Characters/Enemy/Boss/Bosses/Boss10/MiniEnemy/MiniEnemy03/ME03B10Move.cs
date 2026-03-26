using DG.Tweening;
using UnityEngine;

public class ME03B10Move : EnemyMove {

    [SerializeField] protected float moveSpeed = 5;
    [SerializeField] protected AnimationCurve curveMove = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

    private Vector2 movePoint;

    public void SetMovePoint(Vector2 point) {
        movePoint = point;
    }

    public Vector2 GetMovePoint() {
        return movePoint;
    }

    public bool CanMovePoint() {
        return true;
    }

    public void StartMovePoint() {
        targetMovePoint = movePoint;
        isEndMove = false;
        Vector3[] pathPoints = new Vector3[2];
        pathPoints[0] = transform.position;
        pathPoints[1] = movePoint;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, moveSpeed, PathType.CatmullRom, PathMode.TopDown2D, 5).SetSpeedBased(true).SetLookAt(0.01f, Vector3.forward, Vector3.right).OnComplete(EndMovePoint).SetEase(curveMove);
    }

    private void EndMovePoint() {
        isEndMove = true;
    }
}
