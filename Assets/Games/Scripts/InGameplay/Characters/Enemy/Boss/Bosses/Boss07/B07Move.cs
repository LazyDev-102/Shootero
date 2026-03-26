

using DG.Tweening;
using Gemmob;
using Helper;
using UnityEngine;

public class B07Move : BossMove {
    [Header("Rage")]
    [SerializeField] private float rageMoveSpeed;
    [SerializeField] private Area rageArea;
    [SerializeField] private RangeFloatValue rageRandomPointMovePathValue;
    [SerializeField] protected AnimationCurve rageCurve;
    [SerializeField] protected AnimationCurve rageCurve1;
    [SerializeField] protected ParticleSystem appearEffect;
    [SerializeField] protected ParticleSystem disappearEffect;

    public override void MoveDirect() {
        //transform.position = transform.position + (Vector3)(direction * currentMoveSpeed * Time.deltaTime);
    }

    private Vector2 startPos;
    private Vector2 endPos;

    public override void StartMoveAfterAttack() {

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
        //float timeMove = distanceMove / moveSpeed;
        float timeMove = appearEffect.main.duration - 0.1f;
        isEndMove = false;
        Vector2 midPoint = (point + (Vector2)transform.position) / 2;
        Vector2 n = Vector2.Perpendicular(direction);
        Vector2 midPathPoint = midPoint + n.normalized * moveRandomPointMovePathValue.GetRandomValue();
        Vector3[] pathPoints = new Vector3[3];
        pathPoints[0] = transform.position;
        pathPoints[1] = midPathPoint;
        pathPoints[2] = point;

        this.endPos = point;
        curMoveTween?.Kill();
        curMoveTween = transform.DOPath(pathPoints, timeMove, PathType.CatmullRom, PathMode.TopDown2D, 5).OnComplete(OnEndMoveAfterAttack).SetEase(moveCurve).OnKill(OnEndMoveBeKill);

        var effect = appearEffect.Spawn(CommonHUD.Instance.transform);
        if (effect != null) {
            effect.transform.position = transform.position;
            effect.Stop();
            effect.time = 0;
            effect.Play();
            DOVirtual.DelayedCall(effect.main.duration, () => {
                effect.Recycle();
            });
        }
        var effectClone = disappearEffect.Spawn(CommonHUD.Instance.transform);
        if (effectClone != null) {
            effectClone.transform.position = endPos;
            DOVirtual.DelayedCall(timeMove / 2, () => {
                effectClone.Stop();
                effectClone.time = 0;
                effectClone.Play();
                DOVirtual.DelayedCall(effectClone.main.duration, () => {
                    effectClone.Recycle();
                });
            });
        }
    }
    private void OnEndMoveBeKill() {
        if (!isEndMove)
            isEndMove = true;
    }
    protected override void OnEndMoveAfterAttack() {
        base.OnEndMoveAfterAttack();
        if (BossBase.IsDie()) {
            BossBase.Die();
        }
    }

    public virtual void StartMoveRage() {
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
        curMoveTween = transform.DOPath(pathPoints, timeMove, PathType.CatmullRom, PathMode.TopDown2D, 5).OnComplete(OnEndMoveRage).SetEase(rageCurve);
    }

    private void OnEndMoveRage() {
        isEndMove = true;
    }
}
