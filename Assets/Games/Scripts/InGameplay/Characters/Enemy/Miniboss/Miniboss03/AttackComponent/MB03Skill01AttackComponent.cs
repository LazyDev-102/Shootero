using System.Collections;
using UnityEngine;
using Helper;
using DG.Tweening;


public class MB03Skill01AttackComponent : MinibossAttackComponent<MB03Attack> {

    [SerializeField] private float aimTime;
    [SerializeField] private TrailRenderer[] trails;
    private Countdowner aimCountdowner = new Countdowner();
    private Tweener curTween;

    public void StartAimTarget() {
        aimCountdowner.StartCountdown(aimTime);
    }

    public override void Attacking() {

        minibossAttack.MB03Base.MB03Move.SetTargetMoveAttack((Vector2)minibossAttack.Target.position);
    }


    public void AimTarget() {
        hasOut = false;
        minibossAttack.MB03Base.LookTarget();
    }

    public override void StartAttack() {
        StartAimTarget();
        minibossAttack.MB03Base.MB03Move.StartMoveIdle();

    }
    private bool hasOut = false;
    public override void Updating() {
        AimTarget();
        if (aimCountdowner.IsTimeOut()) {
            var isKnockBack = minibossAttack.MB03Base.MB03Move.IsKnockbackCompleted;
            var outBorder = minibossAttack.MB03Base.MB03Move.HasOutBorder();
            if (outBorder && !hasOut) {
                foreach (var t in trails) {
                    t.HideTrail();
                }
                hasOut = true;
                minibossAttack.MB03Base.MB03Move.EndMoveIdle();
                minibossAttack.MB03Base.MB03Move.StopMoveIdle();
                var ranVector2 = new Vector2(0.5f, 1.1f);
                minibossAttack.transform.position = minibossAttack.MB03Base.MB03Move.GetPointMoveMB03(ranVector2);
                minibossAttack.MB03Base.MB03Move.RestartMoveIdle();
                var posDefault = new Vector2(0.5f, 0.8f);
                curTween = minibossAttack.transform.DOMove(minibossAttack.MB03Base.MB03Move.GetPointMoveMB03(posDefault), 1f).OnComplete(() => EndAttack());
            }
            else
            if (isKnockBack) {
                EndAttack();
                return;
            }
        }
        else {
            aimCountdowner.Countdowning(Time.deltaTime);

        }
    }

    public override void EndAttack() {
        base.EndAttack();
        if (curTween != null) {
            curTween.Kill();
        }
        foreach (var t in trails) {
            t.ShowTrail();
        }
        minibossAttack.MB03Base.MB03Attack.ChooseAttack();
        minibossAttack.MB03Base.MB03Attack.Attack();

    }
}