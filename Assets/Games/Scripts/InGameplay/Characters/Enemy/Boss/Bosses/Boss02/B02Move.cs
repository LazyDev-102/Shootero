

using DG.Tweening;
using Helper;
using UnityEngine;

public class B02Move : BossMove {
    [Header("Rage")]
    [SerializeField] private float rageMoveSpeed;
    [SerializeField] private Area rageArea;
    [SerializeField] private RangeFloatValue rageRandomPointMovePathValue;
    [SerializeField] protected AnimationCurve rageCurve;

    public virtual void StartMoveRage() {
        Vector2 point = Vector2.zero;
        point = GetRandomInArea(rageArea);

        direction = (point - (Vector2)transform.position).normalized;
        isEndMove = false;
        Vector2 midPoint = (point + (Vector2)transform.position) / 2;
        Vector2 n = Vector2.Perpendicular(direction);
        Vector2 midPathPoint = midPoint + n.normalized * rageRandomPointMovePathValue.GetRandomValue();
        Vector3[] pathPoints = new Vector3[3];
        pathPoints[0] = transform.position;
        pathPoints[1] = midPathPoint;
        pathPoints[2] = point;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, rageMoveSpeed, PathType.CatmullRom, PathMode.TopDown2D, 5).SetSpeedBased(true).OnComplete(OnEndMoveRage).SetEase(rageCurve).OnKill(OnEndMoveBeKill);
    }

    private void OnEndMoveBeKill() {
        if (!isEndMove)
            isEndMove = true;
    }
    private void OnEndMoveRage() {
        isEndMove = true;
    }

}
