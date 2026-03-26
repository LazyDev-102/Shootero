using UnityEngine;
using DG.Tweening;
using System.Collections;
using Gemmob;

public class MB01Move : MinibossMove {


    #region References
    private MB01Base mb01Base;
    public MB01Base MB01Base {
        get {
            if (mb01Base == null) {
                mb01Base = EnemyBase as MB01Base;
            }
            return mb01Base;
        }
    }
    #endregion


    [SerializeField, Range(0f, 5f)] protected float timeOneRoundRotation = 1f;
    [SerializeField] protected DG.Tweening.DOTweenAnimation anim;
    public override void Initialize() {
        base.Initialize();
        if (anim) {
            anim.duration = timeOneRoundRotation;
            anim.DOPlay();
        }
        hasOutBorder = false;
    }
    public override void Destroy() {
        anim.DOKill();
        base.Destroy();
    }

    public override void StartMoveAfterAttack() {
        Vector2 curPoint = transform.position;
        Vector2 point = Vector2.zero;

        Vector3[] pathPoints = new Vector3[3];
        var timeLoop = 0;
        do {
            do {
                point = GetPointMove();
            }
            while (Vector2.Distance(point, curPoint) < minDistance);
            timeLoop++;
            if (timeLoop > 100)
                break;

            direction = (point - (Vector2)transform.position).normalized;
            isEndMove = false;
            Vector2 midPoint = (point + (Vector2)transform.position) / 2;
            Vector2 n = Vector2.Perpendicular(direction);
            Vector2 midPathPoint = midPoint + n.normalized * moveRandomPointMovePathValue.GetRandomValue();
            pathPoints[0] = transform.position;
            pathPoints[1] = midPathPoint;
            pathPoints[2] = point;
        }
        while (!MB01Base.MyParent.CanMoveToPosition(pathPoints[2]));

        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, moveSpeed, PathType.CatmullRom, PathMode.TopDown2D, 5).SetSpeedBased(true).OnComplete(OnEndMoveAfterAttack).SetEase(moveCurve);
    }
    public override void StartMoveAppear() {
        Vector2 pointAppear = GetRandomInArea(appearArea);
        targetMovePoint = pointAppear;
        direction = (pointAppear - (Vector2)transform.position).normalized;
        MyRigi.transform.localEulerAngles = new Vector3(0, 0, (Vector2.SignedAngle(Vector2.up, direction)));
        distanceMove = Vector2.Distance(transform.position, pointAppear);
        float timeMove = distanceMove / appearMoveSpeed;
        isEndMove = false;
        Vector2 midPoint = (pointAppear + (Vector2)transform.position) / 2;
        Vector2 n = Vector2.Perpendicular(direction);
        Vector2 midPathPoint = midPoint + n.normalized * appearRandomPointMovePathValue.GetRandomValue();
        Vector3[] pathPoints = new Vector3[3];
        pathPoints[0] = transform.position;
        pathPoints[1] = midPathPoint;
        pathPoints[2] = pointAppear;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, appearMoveSpeed, PathType.CatmullRom, PathMode.TopDown2D, 5).SetSpeedBased(true).SetLookAt(0.01f, Vector3.forward, Vector3.right).OnComplete(EndMoveAppear).SetEase(appearCurveMove);
    }

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
    private bool hasOutBorder;
    public override void Updating() {
        base.Updating();
        if (HasOutBorder() && !hasOutBorder) {
            if (gameObject.activeInHierarchy)
                StartCoroutine(IEHasOutBorder(1f));
            StartMoveAppear();
        }
    }
    IEnumerator IEHasOutBorder(float time) {
        hasOutBorder = true;
        yield return Yielder.Wait(time);
        hasOutBorder = false;
    }
}
