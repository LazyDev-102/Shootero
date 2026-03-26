using DG.Tweening;
using UnityEngine;

public class MB11Move : MinibossMove {

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
        curMoveTween = transform.DOPath(pathPoints, rageMoveSpeed, PathType.CatmullRom, PathMode.TopDown2D, 5).SetSpeedBased(true).OnComplete(EndMoveRage).SetEase(Ease.Linear).OnKill(OnEndMoveBeKill);
    }

    private void OnEndMoveBeKill() {
        if (!isEndMove)
            isEndMove = true;
    }
    private void EndMoveRage() {
        isEndMove = true;
    }
}
