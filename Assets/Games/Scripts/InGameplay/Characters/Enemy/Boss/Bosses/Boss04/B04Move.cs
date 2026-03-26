using DG.Tweening;
using Helper;
using System.Linq;
using UnityEngine;

public class B04Move : BossMove {
    [SerializeField] private float lookMoveSpeed;
    [Header("Wings")]
    [SerializeField] private Transform wing1;
    [SerializeField] private Transform wing2;
    [SerializeField] private float openWingAngle;
    [SerializeField] private float closeWingAngle;
    [SerializeField] private float wingSpeed;
    [Header("Rage")]
    [SerializeField] private float rageMoveSpeed;
    [SerializeField] private Area rageArea;
    [SerializeField] private RangeFloatValue rageRandomPointMovePathValue;
    [SerializeField] protected AnimationCurve rageCurve;
    [Header("InRage")]
    [SerializeField] protected PointPath path;
    [SerializeField] protected float durationMoveInRage;

#if UNITY_EDITOR
    [SerializeField] protected DOTweenPath dotweenPath;

    [ContextMenu("Update Path In Rage")]
    private void UpdatePath() {
        path = new PointPath();
        path.Points = dotweenPath.wps.ToArray();
    }
    [ContextMenu("Draw Path In Rage")]
    private void DrawPath() {
        dotweenPath.wps = path.Points.ToList();
    }
#endif

    public override void Initialize() {
        base.Initialize();
    }
    public override void StartMoveAppear() {
        wing1.localEulerAngles = new Vector3(0, 0, openWingAngle);
        wing2.localEulerAngles = new Vector3(0, 0, -openWingAngle);
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
        curMoveTween = transform.DOPath(pathPoints, timeMove, PathType.CatmullRom, PathMode.TopDown2D, 5).SetLookAt(0.01f, Vector3.forward, Vector3.left).OnComplete(EndMoveAppear).SetEase(appearCurveMove);
        MyRigi.MoveRotation(Vector2.SignedAngle(Vector2.up, -direction));
    }
    protected override void EndMoveAppear() {
        base.EndMoveAppear();
        StartCloseWings(null);
    }
    public override void StartMoveAfterAttack() {
        isEndMove = false;
        StartOpenWings(() => {
            try {
                Vector2 curPoint = transform.position;
                Vector2 point = Vector2.zero;
                int count = 0;
                do {
                    point = GetPointMove();
                    count++;
                }
                while (Vector2.Distance(point, curPoint) < minDistance && count < 20);

                direction = (point - (Vector2)transform.position).normalized;
                distanceMove = Vector2.Distance(curPoint, point);
                float timeMove = distanceMove / moveSpeed;
                Vector2 midPoint = (point + (Vector2)transform.position) / 2;
                Vector2 n = Vector2.Perpendicular(direction);
                Vector2 midPathPoint = midPoint + n.normalized * moveRandomPointMovePathValue.GetRandomValue();
                Vector3[] pathPoints = new Vector3[3];
                pathPoints[0] = transform.position;
                pathPoints[1] = midPathPoint;
                pathPoints[2] = point;
                curMoveTween?.Kill();
                curMoveTween = transform.DOPath(pathPoints, timeMove, PathType.CatmullRom, PathMode.TopDown2D, 5)
                .SetLookAt(0.01f, Vector3.forward, Vector3.left).OnComplete(OnEndMoveAfterAttack).SetEase(moveCurve).OnKill(OnMoveAfterAttackBeKill);
            }
            catch {
                Debug.LogError("Boss Active Incorrect!");
                OnEndMoveAfterAttack();
            }
        });
    }
    protected override void OnEndMoveAfterAttack() {
        base.OnEndMoveAfterAttack();
        StartCloseWings(null);
    }
    protected void OnMoveAfterAttackBeKill() {
        if (!isEndMove) {
            OnEndMoveAfterAttack();
        }

    }
    private void StartOpenWings(TweenCallback onComplete) {
        wing1.DOLocalRotate(new Vector3(0, 0, openWingAngle), wingSpeed).SetEase(Ease.Linear).OnComplete(onComplete);
        wing2.DOLocalRotate(new Vector3(0, 0, -openWingAngle), wingSpeed).SetEase(Ease.Linear);
    }
    public void StartCloseWings(TweenCallback onComplete) {
        wing1.DOLocalRotate(new Vector3(0, 0, closeWingAngle), wingSpeed).SetEase(Ease.Linear).OnComplete(onComplete);
        wing2.DOLocalRotate(new Vector3(0, 0, -closeWingAngle), wingSpeed).SetEase(Ease.Linear);
    }
    public virtual void StartMoveRage() {
        try {
            Vector2 curPoint = transform.position;
            Vector2 point = Vector2.zero;
            point = GetRandomInArea(rageArea);

            direction = (point - (Vector2)transform.position).normalized;
            distanceMove = Vector2.Distance(curPoint, point);
            float timeMove = distanceMove / moveSpeed;
            isEndMove = false;
            Vector2 midPoint = (point + (Vector2)transform.position) / 2;
            Vector2 n = Vector2.Perpendicular(direction);
            Vector2 midPathPoint = midPoint + n.normalized * rageRandomPointMovePathValue.GetRandomValue();
            Vector3[] pathPoints = new Vector3[3];
            pathPoints[0] = transform.position;
            pathPoints[1] = midPathPoint;
            pathPoints[2] = point;
            curMoveTween?.Kill();
            curMoveTween = transform.DOPath(pathPoints, timeMove, PathType.CatmullRom, PathMode.TopDown2D, 5).OnComplete(OnEndMoveRage).SetEase(rageCurve).OnKill(OnMoveRageBeKill);
        }
        catch {
            OnEndMoveRage();
        }
    }
    public void StartMoveInRage() {
        Vector3[] pathPoint = new Vector3[path.Points.Length];
        for (int i = 0; i < pathPoint.Length; ++i) {
            pathPoint[i] = transform.position + path.Points[i];
        }
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoint, durationMoveInRage, PathType.CatmullRom, PathMode.TopDown2D, 20).SetOptions(true).SetEase(Ease.Linear).SetLoops(-1);
    }

    public void EndMoveInRage() {
        if (curMoveTween != null) {
            curMoveTween.Kill();
        }
    }
    protected void OnMoveRageBeKill() {
        if (!isEndMove) {
            OnEndMoveRage();
        }

    }
    private void OnEndMoveRage() {
        isEndMove = true;
    }
}
