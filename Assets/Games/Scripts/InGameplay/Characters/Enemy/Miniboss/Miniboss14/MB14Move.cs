using DG.Tweening;
using UnityEngine;

public class MB14Move : MinibossMove {
    private Vector2 origin;
    public bool CanKnockBack = true;
    public void SetTargetMoveAttack(Vector2 target, float moveSpeed) {
        currentMoveSpeed = moveSpeed;
        SetDirectionMove(target - myRigi.position);
        //LookTarget(target);
    }

    public virtual Vector2 GetPointMoveMB14(Vector2 point) {
        return GetRandomInArea(point);
    }
    public override void Knockback(Vector2 causer) {
        if (!CanKnockBack || MinibossBase.IsSpecialState)
            return;
        Vector2 curPos = transform.position;
        Vector2 direc = curPos - causer;
        TweenCallback callback = null;
        if (curMoveTween != null && curMoveTween.IsPlaying()) {
            callback = curMoveTween.onComplete;
            curMoveTween.Kill();
        }
        curMoveTween = transform.DOMove(curPos + direc.normalized * knockbackPower, knockbackDurantion).SetEase(knockbackCurveMove).OnComplete(callback);
    }
}
