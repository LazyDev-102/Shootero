using DG.Tweening;
using UnityEngine;

public class MESpecialB08Move : EnemyMove {
    [SerializeField] private float moveSpeed;
    [SerializeField] protected AnimationCurve moveCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField] protected RangeFloatValue moveRandomPointMovePathValue;
    [SerializeField] private DotweenAnimation flashAnimation;

    private bool isPlayed;


    public void StartMoveTarget() {
        Vector2 point = EnemyBase.EnemyAttack.Target.position;
        targetMovePoint = point;
        direction = (point - (Vector2)transform.position).normalized;
        MyRigi.transform.localEulerAngles = new Vector3(0, 0, (Vector2.SignedAngle(Vector2.up, direction)));
        isEndMove = false;
        Vector2 midPoint = (point + (Vector2)transform.position) / 2;
        Vector2 n = Vector2.Perpendicular(direction);
        Vector2 midPathPoint = midPoint + n.normalized * moveRandomPointMovePathValue.GetRandomValue();
        Vector3[] pathPoints = new Vector3[3];
        pathPoints[0] = transform.position;
        pathPoints[1] = midPathPoint;
        pathPoints[2] = point;
        curMoveTween?.Kill();
        isPlayed = false;
        curMoveTween = transform.DOPath(pathPoints, moveSpeed, PathType.CatmullRom, PathMode.TopDown2D, 5).SetSpeedBased(true).SetLookAt(0.01f, Vector3.forward, Vector3.right).OnComplete(OnEndMove).SetEase(moveCurve);
        curMoveTween.OnUpdate(UpdateMove);
    }

    private void UpdateMove() {
        if (curMoveTween.ElapsedPercentage() > 0.65f) {
            if (!isPlayed) {
                isPlayed = true;
                if (flashAnimation) {
                    flashAnimation.Play();
                }
            }
        }
    }

    private void OnEndMove() {
        isEndMove = true;
    }
}
