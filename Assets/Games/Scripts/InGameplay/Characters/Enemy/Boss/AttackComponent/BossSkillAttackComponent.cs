using UnityEngine;

public abstract class BossSkillAttackComponent : BossAttackComponent {
    protected int CurrentPhaseIndex {
        get {
            return GetBossAttack().BossBase.CurrentPhaseIndex;
        }
    }
}
