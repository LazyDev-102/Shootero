using DG.Tweening;
using UnityEngine;

public class B14MiniShieldMove : MiniShieldMove {
    [SerializeField] private AnimationCurve moveAttackCurve;

    protected Tweener curMoveTween;
    public void MoveAttack(Transform target, float duration) {
        //if (target != null) {
        //    transform.DOMove(target.position, duration).SetEase(moveAttackCurve);
        //}
        Vector3[] pathPoints = new Vector3[4];
        pathPoints[0] = transform.position;
        pathPoints[1] = transform.position + transform.up.normalized * 1.5f;
        pathPoints[2] = target.position;
        pathPoints[3] = target.position.y > 0 ? target.position + target.up.normalized * 10 : target.position + target.up.normalized * -10;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, duration, PathType.CatmullRom, PathMode.TopDown2D, 5).SetLookAt(0.01f, Vector3.forward, Vector3.right).OnComplete(OnEndMove).SetEase(moveAttackCurve);
    }
    private void OnEndMove() {

    }
}
