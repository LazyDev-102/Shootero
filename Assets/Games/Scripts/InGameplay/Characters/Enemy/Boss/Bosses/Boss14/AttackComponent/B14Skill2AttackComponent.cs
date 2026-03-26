using UnityEngine;

public class B14Skill2AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B14ShotController controller;
    [SerializeField] private B14Attack bossAttack;


    public override void PreloadIngame() {
    }


    public override void Attacking() {
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void StartAttack() {
        DG.Tweening.DOVirtual.DelayedCall(5, EndAttack);
    }

    public override void Updating() {
    }
}
