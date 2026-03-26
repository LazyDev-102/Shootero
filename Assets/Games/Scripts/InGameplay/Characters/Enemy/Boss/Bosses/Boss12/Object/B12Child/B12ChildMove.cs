using UnityEngine;
using Helper;
using DG.Tweening;
using Gemmob;

public class B12ChildMove : EnemyMove {
    private B12ChildBase e02Base;
    public B12ChildBase B12ChildBase {
        get {
            if (e02Base == null) {
                e02Base = EnemyBase as B12ChildBase;
            }
            return e02Base;
        }
    }


    [Header("B12ChildMove")]
    [SerializeField] private float attackMoveSpeed = 20;
    [SerializeField] private TrailRenderer moveTrail;


    private float sizeTrail = -1;
    private bool isKnockbackCompleted;

    public bool IsKnockbackCompleted { get => isKnockbackCompleted; set => isKnockbackCompleted = value; }

    public override void Initialize() {
        base.Initialize();
        IsKnockbackCompleted = false;
        canHasOutBorder = false;
        //ShowMoveTrail();
    }

    public override void Destroy() {
        //HideMoveTrail();
        base.Destroy();
    }

    public void SetTargetMoveAttack(Vector2 target) {
        //ShowMoveTrail();
        Vector2 curPosition = transform.position;
        direction = transform.up;
        distanceMove = 10;
        float timeMove = distanceMove / attackMoveSpeed;
        isEndMove = false;
        //LookTarget(direction);
        isKnockbackCompleted = false;
        curMoveTween?.Kill();
        curMoveTween = transform.DOMove(curPosition + direction * distanceMove, timeMove).SetLoops(int.MaxValue, LoopType.Incremental).SetEase(Ease.Linear);
    }

    public override void StartMoveAppear() {
        base.StartMoveAppear();
        //ShowMoveTrail();
    }

    public void EndTargetMoveAttack() {
        curMoveTween?.Kill();
    }

    public override void Knockback(Vector2 causer) {
        //Vector2 curPos = transform.position;
        //Vector2 direc = curPos - causer;
        //if (curMoveTween != null && curMoveTween.IsPlaying()) {
        //    curMoveTween.Kill();
        //}
        //curMoveTween = transform.DOMove(curPos + direc.normalized * knockbackPower, knockbackDurantion).SetEase(knockbackCurveMove).OnComplete(KnockbackComplete);

        Vector2 curPos = transform.position;
        Vector2 direc = curPos - causer;
        if (curMoveTween != null && curMoveTween.IsPlaying()) {
            curMoveTween.Kill();
        }
        canHasOutBorder = true;
        curMoveTween = transform.DOMove(curPos + direc.normalized * knockbackPower, knockbackDurantion).SetEase(knockbackCurveMove);
    }

    private void KnockbackComplete() {
        IsKnockbackCompleted = true;
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
    public void StartMove(Vector2 target, float timeMove, System.Action onComplete) {
        curMoveTween?.Kill();
        curMoveTween = transform.DOMove(target, timeMove).SetEase(Ease.Linear).OnComplete(() => onComplete?.Invoke());
    }

    public override bool CanMoveAppear() {
        return false;
    }
    private bool canHasOutBorder;
    public override void Updating() {
        base.Updating();
        if (canHasOutBorder && HasOutBorder()) {
            EnemyBase.Recycle();
        }
    }
}
