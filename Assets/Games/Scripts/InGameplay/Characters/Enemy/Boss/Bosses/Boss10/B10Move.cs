

using DG.Tweening;
using UnityEngine;

public class B10Move : BossMove {
    private B10Base b10Base;

    public B10Base B10Base {
        get {
            if (b10Base == null) {
                b10Base = CharacterBase as B10Base;
            }
            return b10Base;
        }
    }

    [SerializeField] private float rageMoveSpeed;
    [SerializeField] protected AnimationCurve moveRageCurve;

    public bool CanMoveRage() {
        return true;
    }

    public virtual void StartMoveRage() {
        Vector2 point = GetRandomInArea(new Vector2(0.5f, 0.5f));

        targetMovePoint = point;
        direction = (point - (Vector2)transform.position).normalized;
        isEndMove = false;
        Vector3[] pathPoints = new Vector3[2];
        pathPoints[0] = transform.position;
        pathPoints[1] = point;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, rageMoveSpeed, PathType.CatmullRom, PathMode.TopDown2D, 5).SetSpeedBased(true).OnComplete(EndMoveRage).SetEase(moveRageCurve).OnKill(OnEndMoveBeKill);
    }

    private void OnEndMoveBeKill() {
        if (!isEndMove)
            isEndMove = true;
    }
    private void EndMoveRage() {
        isEndMove = true;
    }

}
