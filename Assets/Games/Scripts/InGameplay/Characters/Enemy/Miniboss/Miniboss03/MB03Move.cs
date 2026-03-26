using UnityEngine;
using DG.Tweening;
using Helper;

public class MB03Move : MinibossMove {
    [Header("E02Move")]
    [SerializeField] private float attackMoveSpeed = 20;
    [SerializeField] private TrailRenderer moveTrail;


    private float sizeTrail = -1;
    private bool isKnockbackCompleted;

    public bool IsKnockbackCompleted { get => isKnockbackCompleted; set => isKnockbackCompleted = value; }

    public override void Initialize() {
        base.Initialize();
        IsKnockbackCompleted = false;
        ShowMoveTrail();
    }

    public void SetTargetMoveAttack(Vector2 target) {
        ShowMoveTrail();
        Vector2 curPosition = transform.position;
        direction = transform.up;
        distanceMove = 10;
        float timeMove = distanceMove / attackMoveSpeed;
        isEndMove = false;
        //LookTarget(direction);
        isKnockbackCompleted = false;
        curMoveTween?.Kill();
        curMoveTween = transform.DOMove(curPosition + direction * distanceMove, timeMove).SetLoops(int.MaxValue, LoopType.Incremental).SetEase(Ease.Linear).OnComplete(() => isEndMove = true);
    }

    public override void StartMoveAppear() {
        base.StartMoveAppear();
        ShowMoveTrail();
    }

    public void EndTargetMoveAttack() {
        curMoveTween?.Kill();
    }

    public override void Knockback(Vector2 causer) {
        Vector2 curPos = transform.position;
        Vector2 direc = curPos - causer;
        if (curMoveTween != null && curMoveTween.IsPlaying()) {
            curMoveTween.Kill();
        }
        curMoveTween = transform.DOMove(curPos + direc.normalized * knockbackPower, knockbackDurantion).SetEase(knockbackCurveMove).OnComplete(KnockbackComplete);
    }

    private void KnockbackComplete() {
        IsKnockbackCompleted = true;
        isEndMove = true;
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
    public virtual Vector2 GetPointMoveMB03(Vector2 point) {
        return GetRandomInArea(point);
    }
}
