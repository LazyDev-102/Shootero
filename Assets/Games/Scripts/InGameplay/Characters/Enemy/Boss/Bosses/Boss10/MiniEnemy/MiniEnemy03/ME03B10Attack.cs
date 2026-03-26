using Gemmob;
using UnityEngine;

public class ME03B10Attack : EnemyAttack {

    private ME03B10Base me03B10base;

    public ME03B10Base ME03B10Base {
        get {
            if (me03B10base == null) {
                me03B10base = EnemyBase as ME03B10Base;
            }
            return me03B10base;
        }
    }


    [SerializeField] private LightningLine lightningLine;
    [SerializeField] private LineRenderer warningLine;
    [SerializeField] private float warningTime;

    private ME03B10Base myBrother;
    private Countdowner warningCountdowner;
    private bool isBigBrother;

    public override void Initialize() {
        base.Initialize();
        lightningLine.SetActive(false);
        warningLine.gameObject.SetActive(false);
    }

    public override bool CanAttack() {
        if (myBrother == null) {
            myBrother = ME03B10Base.GetBrother();
            if (myBrother == null) {
                return false;
            }
        }
        return !isAttacking && ME03B10Base.ME03B10Move.CompleteMoveToTarget() && !myBrother.IsDie() && myBrother.ME03B10Move.CompleteMoveToTarget();
    }

    public bool IsEndAttack() {
        if (myBrother == null) {
            myBrother = ME03B10Base.GetBrother();
        }
        return isAttacking && (myBrother == null || myBrother.IsDie());
    }

    protected override void Attacking() {
        isBigBrother = ME03B10Base.GetIsBigBrother();
        myBrother = ME03B10Base.GetBrother();
        if (isBigBrother) {
            warningCountdowner.StartCountdown(warningTime);
            warningLine.gameObject.SetActive(true);
            warningLine.SetPosition(0, transform.position);
            warningLine.SetPosition(1, myBrother.transform.position);
            lightningLine.SetInfor(ME03B10Base.ME03B10Stat.Atk.Value, ME03B10Base);
        }
        else if (!myBrother) {
            this.Recycle();
        }
    }

    public void UpdateLine() {
        if (isBigBrother) {
            if (warningCountdowner.IsCountdowning()) {
                warningCountdowner.Countdowning(Time.deltaTime);
                LookingBrother();
                myBrother.ME03B10Attack.LookingBrother();
                if (warningCountdowner.IsTimeOut()) {
                    lightningLine.SetActive(true);
                    lightningLine.UpdatePosition(transform.position, myBrother.transform.position);
                    warningLine.gameObject.SetActive(false);
                }
            }
            else {
                lightningLine.UpdatePosition(transform.position, myBrother.transform.position);
            }
        }
    }

    public void LookingBrother() {
        if (myBrother == null) {
            myBrother = ME03B10Base.GetBrother();
        }
        ME03B10Base.EnemyMove.LookTarget(myBrother.transform.position);
    }

    public override void EndAttack() {
        base.EndAttack();
        warningLine.gameObject.SetActive(false);
        lightningLine.SetActive(false);
        if (myBrother != null && !myBrother.IsDie()) {
            myBrother.ME03B10Health.ForceChangeCurrentHp(0);
        }
        myBrother = null;
        ME03B10Base.SetBrother(myBrother);
    }
}
