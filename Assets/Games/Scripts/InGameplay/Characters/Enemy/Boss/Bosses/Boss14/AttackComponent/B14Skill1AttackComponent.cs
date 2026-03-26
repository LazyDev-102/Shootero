using UnityEngine;

public class B14Skill1AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B14ShotController controller;
    [SerializeField] private B14Attack bossAttack;


    public override void PreloadIngame() {
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }
    public override void StartAttack() {
        DG.Tweening.DOVirtual.DelayedCall(8, EndAttack);
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
    }
}
