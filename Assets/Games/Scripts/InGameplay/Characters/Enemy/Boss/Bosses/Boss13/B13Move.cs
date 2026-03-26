using DG.Tweening;
using Helper;
using UnityEngine;

public class B13Move : BossMove {
    [Header("B13Move")]
    [SerializeField] protected float attackMoveSpeed = 5;
    [SerializeField] protected AnimationCurve attackMoveCurve;
    [SerializeField] protected RangeFloatValue attackMoveRandomPointMovePathValue;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation moveAnim;
    [SerializeField] private DOTweenAnimation[] anims;
    private Vector2 origin;
    public void SetTargetMoveAttack(Vector2 target, float moveSpeed) {
        currentMoveSpeed = moveSpeed;
        SetDirectionMove(target - myRigi.position);
        //LookTarget(target);
    }
    public void MoveInside(bool isMove) {
        if (moveAnim != null) {
            if (isMove)
                moveAnim.Play();
            else
                moveAnim.Stop();
        }
        if (anims != null) {
            foreach (var item in anims) {
                if (isMove)
                    item.DOPlay();
                else
                    item.DOPause();
            }
        }
    }
    public void StartMoveAfterAttackB13(Vector2 vector2) {
        var point = GetPointMoveB13(vector2);

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
        curMoveTween = transform.DOPath(pathPoints, timeMove, PathType.CatmullRom, PathMode.TopDown2D, 5).OnComplete(OnEndMoveAfterAttackB13).SetEase(attackMoveCurve).OnKill(OnEndMoveBeKill);
    }

    private void OnEndMoveBeKill() {
        if (!isEndMove)
            isEndMove = true;
    }
    private void OnEndMoveAfterAttackB13() {
        isEndMove = true;
    }
    public virtual Vector2 GetPointMoveB13(Vector2 point) {
        return GetRandomInArea(point);
    }
    public override void StartMoveAppear() {
        base.StartMoveAppear();
        MoveInside(true);
    }
}