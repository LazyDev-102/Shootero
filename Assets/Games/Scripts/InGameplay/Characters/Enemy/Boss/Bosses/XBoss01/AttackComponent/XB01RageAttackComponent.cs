using UnityEngine;

public class XB01RageAttackComponent : BossAttackComponent {
    [SerializeField] private XB01ShotController controller;
    [SerializeField] private XB01Attack bossAttack;

    private bool attacking;
    private bool attackOneShot;

    public override void PreloadIngame() {
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }
    public override void StartAttack() {
        //DG.Tweening.DOVirtual.DelayedCall(8, EndAttack);
        bossAttack.XB01Base.XB01Move.StartMoveAfterAttackXB01(new Vector2(0.5f, 0.5f));
        attacking = false;
        attackOneShot = false;
    }

    public override void Attacking() {
    }

    public override void EndAttack() {
        base.EndAttack();
    }

    public override void StopAttack() {
        base.StopAttack();
    }

    public override void Updating() {
        if (attackOneShot)
            return;
        if (!attacking) {
            attacking = bossAttack.XB01Base.XB01Move.CompleteMoveToTarget();
        }
        else {
            attackOneShot = true;
            controller.SetBossAttack(bossAttack);
            controller.SetData();
            controller.StartShotRoutine();
            controller.SetComplete(EndAttack);
        }
    }
}