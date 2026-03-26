

using Helper;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public abstract class EnemyMove : CharacterMove {
    [Header("EnemyMove")]
    [SerializeField] protected float currentMoveSpeed;
    [SerializeField] protected float speedRotateLook = 10f;
    [SerializeField] protected AnimationCurve defaultCurveMove = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [Header("Appear")]
    [SerializeField] protected float appearMoveSpeed = 5;
    [SerializeField] protected Area appearArea;
    [SerializeField] protected AnimationCurve appearCurveMove = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField] protected RangeFloatValue appearRandomPointMovePathValue;
    [Header("Knockback")]
    [SerializeField] protected float knockbackDurantion;
    [SerializeField] protected float knockbackPower;
    [SerializeField] protected AnimationCurve knockbackCurveMove = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [Header("MoveIdle")]
    [SerializeField] protected float idleMoveSpeed;
    [SerializeField] protected AnimationCurve idleMoveCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField] protected PointPath[] paths;
#if UNITY_EDITOR
    [SerializeField] protected DOTweenPath[] dotweenPaths;

    [ContextMenu("Update Path")]
    private void UpdatePath() {
        paths = new PointPath[dotweenPaths.Length];
        for (int i = 0; i < paths.Length; ++i) {
            paths[i] = new PointPath();
            paths[i].Points = dotweenPaths[i].wps.ToArray();
        }
    }
    [ContextMenu("Draw Path")]
    private void DrawPath() {
        for (int i = 0; i < paths.Length; ++i) {
            dotweenPaths[i].wps = paths[i].Points.ToList();
        }
    }
#endif
    protected Vector2 targetMovePoint;
    protected Vector2 direction;
    protected Vector2 viewPointInArea;
    protected AnimationCurve curCurveMove;
    protected float distanceMove;
    protected Tweener curMoveTween;
    protected bool isEndMove;
    protected bool isStopMoveIdle;


    private EnemyBase enemyBase;
    public EnemyBase EnemyBase {
        get {
            if (enemyBase == null) {
                enemyBase = CharacterBase as EnemyBase;
            }
            return enemyBase;
        }
    }

    public override void Destroy() {
        base.Destroy();
        if (curMoveTween != null)
            curMoveTween.Kill();
    }

    private void OnDisable() {
        if (curMoveTween != null)
            curMoveTween.Kill();
    }


    public virtual void Knockback(Vector2 causer) {
        Vector2 curPos = transform.position;
        Vector2 direc = curPos - causer;
        TweenCallback callback = null;
        if (curMoveTween != null && curMoveTween.IsPlaying()) {
            callback = curMoveTween.onComplete;
            curMoveTween.Kill();
        }
        curMoveTween = transform.DOMove(curPos + direc.normalized * knockbackPower, knockbackDurantion).SetEase(knockbackCurveMove).OnComplete(callback);
    }

    public virtual void StartMoveAppear() {
        Vector2 pointAppear = GetRandomInArea(appearArea);
        targetMovePoint = pointAppear;
        direction = (pointAppear - (Vector2)transform.position).normalized;
        MyRigi.transform.localEulerAngles = new Vector3(0, 0, (Vector2.SignedAngle(Vector2.up, direction)));
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

    public virtual void EndMove() {
        isEndMove = true;
        curMoveTween?.Kill();
    }

    protected virtual void EndMoveAppear() {
        isEndMove = true;
    }

    public virtual void StartMoveIdle() {
        if (isStopMoveIdle)
            return;
        if (paths.Length == 0) {
            return;
        }
        Vector3[] points = null;
        Vector3[] path = null;
        bool loop = false;
        int count = 0;
        do {
            count++;
            if (count > 20) {
                path = new Vector3[2];
                path[0] = transform.position;
                path[1] = GetRandomInArea(appearArea);
                break;
            }
            loop = false;
            points = RandomHelper.RandomInCollection(paths).Points;
            if (BorderHelper.IsOutBound(points[points.Length - 1])) {
                loop = true;
                continue;
            }
            path = new Vector3[points.Length];
            for (int i = 0; i < points.Length; ++i) {
                path[i] = transform.position + points[i];
            }
            if (BorderHelper.IsOutBound(path[path.Length - 1], -1)) {
                loop = true;
                continue;
            }
        } while (loop);
        distanceMove = GetDistancePath(path);
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(path, idleMoveSpeed, PathType.CatmullRom, PathMode.TopDown2D, 5).SetSpeedBased(true).SetEase(idleMoveCurve).OnComplete(StartMoveIdle);
    }

    public void EndMoveIdle() {
        if (curMoveTween != null) {
            curMoveTween.Kill();
        }
    }
    public void StopMoveIdle() {
        isStopMoveIdle = true;
    }
    public void RestartMoveIdle() {
        isStopMoveIdle = false;
    }
    protected float GetDistancePath(Vector3[] path) {
        float s = 0;
        for (int i = 1; i < path.Length; ++i) {
            s += Vector3.Distance(path[i - 1], path[i]);
        }
        return s;
    }

    protected virtual Vector2 GetRandomInArea(Area area) {
        viewPointInArea = BorderHelper.GetRandomViewPointInsideArea(area);
        return BorderHelper.GetWorldPointInsideArea(viewPointInArea);
    }
    protected virtual Vector2 GetRandomInArea(Vector2 vector2) {
        return BorderHelper.GetWorldPointInsideArea(vector2);
    }

    //public void CheckingPositionAppearPoint() {
    //    if (!CameraHelper.WorldPointInsideCameraView(targetMovePoint)) {
    //        targetMovePoint = BorderHelper.GetWorldPointInsideArea(viewPointInArea);
    //        direction = (targetMovePoint - (Vector2)transform.position).normalized;
    //        myRigi.MoveRotation(Vector2.SignedAngle(Vector2.up, direction));
    //    }
    //}

    public Tweener StartMoveFront(Vector2 position, float speed, AnimationCurve cuver, TweenCallback onComplete) {
        if (curMoveTween != null) {
            curMoveTween.Kill();
        }
        if (onComplete != null) {
            curMoveTween = transform.DOMove(position, speed).SetSpeedBased(true).SetEase(cuver).OnComplete(onComplete);
        }
        else {
            curMoveTween = transform.DOMove(position, speed).SetSpeedBased(true).SetEase(cuver);
        }
        return curMoveTween;
    }

    public virtual void MoveDirect() {
        //if (distanceMove != 0) {
        //    float t = 1 - Vector2.Distance(MyRigi.position, targetMovePoint) / distanceMove;
        //    float currentSpeedAfterCurve = currentMoveSpeed * curCurveMove.Evaluate(t);
        //    MyRigi.MovePosition(MyRigi.position + direction * currentSpeedAfterCurve * Time.deltaTime);
        //}
        //else {
        //    MyRigi.MovePosition(MyRigi.position + direction * currentMoveSpeed * Time.deltaTime);
        //}

    }

    public virtual void MovePush() {
        MyRigi.velocity = direction * currentMoveSpeed;
    }

    public virtual void MoveFront() {
        MyRigi.MovePosition(MyRigi.position + (Vector2)transform.up * currentMoveSpeed * Time.deltaTime);
    }

    public virtual void MoveBack() {
        MyRigi.MovePosition(MyRigi.position - (Vector2)transform.up * currentMoveSpeed * Time.deltaTime);
    }

    public override bool HasOutBorder() {
        Vector2 currentPosition = MyRigi.position;
        return BorderHelper.IsOutBound(currentPosition);
    }

    public void SetDirectionMove(Vector2 dir) {
        this.direction = dir.normalized;
    }

    public virtual void Rotate(float speed) {
        MyRigi.MoveRotation(MyRigi.rotation + speed * Time.deltaTime);
    }

    public virtual void LookAngle(float newAngle) {
        MyRigi.MoveRotation(Mathf.LerpAngle(MyRigi.rotation, newAngle, Time.deltaTime * speedRotateLook));
    }

    public virtual void LookAngle(float newAngle, float speedLook) {
        MyRigi.MoveRotation(Mathf.LerpAngle(MyRigi.rotation, newAngle, Time.deltaTime * speedLook));
    }

    public virtual void LookDirection(Vector2 direction) {
        MyRigi.MoveRotation(Mathf.LerpAngle(MyRigi.rotation, Vector2.SignedAngle(Vector2.up, direction), Time.deltaTime * speedRotateLook));
    }

    public virtual void LookDirection(Vector2 direction, float speedLook) {
        MyRigi.MoveRotation(Mathf.LerpAngle(MyRigi.rotation, Vector2.SignedAngle(Vector2.up, direction), Time.deltaTime * speedLook));
    }

    public virtual void LookTarget(Vector2 target) {
        LookDirection(target - (Vector2)transform.position);
    }

    public virtual void LookTarget(Vector2 target, float speedLook) {
        LookDirection(target - (Vector2)transform.position, speedLook);
    }

    public bool CompleteMoveToTarget() {
        return isEndMove;
    }

    public virtual bool CanMoveAppear() {
        return true;
    }


}

[System.Serializable]
public class PointPath {
    [SerializeField] private Vector3[] points;

    public Vector3[] Points { get => points; set => points = value; }
}