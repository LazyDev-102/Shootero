

using DG.Tweening;
using Helper;
using UnityEngine;

public class E04Move : EnemyMove {
    [Header("E04Move")]
    [SerializeField] protected AnimationCurve attackCurveMove = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField] private float attackMoveSpeed = 20;
    [SerializeField] private float rotateSpeedNormal = 1;
    [SerializeField] private float rotateSpeedAttack = 2;
    [SerializeField] private Transform objectRoatate;
    [SerializeField] private TrailRenderer moveTrail;


    private float sizeTrail = -1;
    private float currentRotateSpeed;

    public override void Initialize() {
        base.Initialize();
        ShowMoveTrail();
    }

    public override void Destroy() {
        HideMoveTrail();
        base.Destroy();
    }

    public void SetTargetMoveAttack(Vector2 target) {
        ShowMoveTrail();
        currentMoveSpeed = attackMoveSpeed;
        targetMovePoint = target;
        SetDirectionMove(target - myRigi.position);

        direction = (target - (Vector2)transform.position).normalized;
        distanceMove = Vector2.Distance(transform.position, target);
        float timeMove = distanceMove / appearMoveSpeed;
        isEndMove = false;
        Vector2 midPoint = (target + (Vector2)transform.position) / 2;
        Vector2 n = Vector2.Perpendicular(direction);
        Vector2 midPathPoint = midPoint + n.normalized * appearRandomPointMovePathValue.GetRandomValue();
        Vector3[] pathPoints = new Vector3[3];
        pathPoints[0] = transform.position;
        pathPoints[1] = midPathPoint;
        pathPoints[2] = target;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, timeMove, PathType.CatmullRom, PathMode.TopDown2D, 5).SetLookAt(0.01f, Vector3.forward, Vector3.right).OnComplete(EndMoveAttack).SetEase(attackCurveMove);
    }

    private void EndMoveAttack() {
        isEndMove = true;
    }

    public override void StartMoveAppear() {
        base.StartMoveAppear();
        ShowMoveTrail();
    }

    public void StartRotateNormal() {
        currentRotateSpeed = rotateSpeedNormal;
    }

    public void StartRotateAttack() {
        currentRotateSpeed = rotateSpeedAttack;
        // LookTarget(targetMovePoint);
    }

    public void RotateSelf() {
        objectRoatate.Rotate(Vector3.forward, currentRotateSpeed * Time.deltaTime);
    }
    public void HideMoveTrail() {
        if (moveTrail) {
            moveTrail.HideTrail();
        }
    }

    public void ShowMoveTrail() {
        if (moveTrail) {
            moveTrail.ShowTrail();
        }
    }

    public void SetSizeTrail(float sizePercent) {
        if (sizeTrail < 0) {
            sizeTrail = moveTrail.widthMultiplier;
        }
        moveTrail.widthMultiplier = sizeTrail * sizePercent;
    }
}
