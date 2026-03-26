
using DG.Tweening;
using UnityEngine;

public class E01Move : EnemyMove {
    private E01Base e01Base;
    public E01Base E01Base {
        get {
            if (e01Base == null) {
                e01Base = EnemyBase as E01Base;
            }
            return e01Base;
        }
    }


    public override void StartMoveAppear() {
        Vector2 pointAppear = GetRandomInArea(appearArea);
        targetMovePoint = pointAppear;
        direction = (pointAppear - (Vector2)transform.position).normalized;
        isEndMove = false;
        LookTarget(pointAppear);
        curMoveTween?.Kill();
        curMoveTween = transform.DOMove(pointAppear, appearMoveSpeed).SetSpeedBased(true).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    public override void Knockback(Vector2 causer) {
        Vector2 curPos = transform.position;
        Vector2 direc = curPos - causer;
        if (curMoveTween != null && curMoveTween.IsPlaying()) {
            curMoveTween.Kill();
        }
        curMoveTween = transform.DOMove(curPos + direc.normalized * knockbackPower, knockbackDurantion).SetEase(knockbackCurveMove);
    }
    public void CheckCurrentSpeed() {
        if (currentMoveSpeed < 0)
            currentMoveSpeed = 1;
        if (idleMoveSpeed < 0)
            idleMoveSpeed = 1;
    }
}
