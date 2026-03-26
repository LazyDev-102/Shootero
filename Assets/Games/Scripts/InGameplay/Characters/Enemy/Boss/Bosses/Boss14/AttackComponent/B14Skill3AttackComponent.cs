
using UnityEngine;

public class B14Skill3AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B14ShotController controller;
    [SerializeField] private B14Attack bossAttack;

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void StartAttack() {
        DG.Tweening.DOVirtual.DelayedCall(5, EndAttack);
    }


    public override void Updating() {
    }
    public override void Attacking() {
    }
}
