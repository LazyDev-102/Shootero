using UnityEngine;

public class MB14Attack : MinibossAttack {

    #region References
    private MB14Base mb14Base;

    public MB14Base MB14Base {
        get {
            if (mb14Base == null) {
                mb14Base = MinibossBase as MB14Base;
            }
            return mb14Base;
        }
    }
    #endregion

    #region Special Attack
    [SerializeField, Range(0f, 1f)] private float percentAttackSpecial = 0.5f;

    private bool hasSpecital;

    public override void Initialize() {
        base.Initialize();
        hasSpecital = false;
    }

    public void CheckPhase() {
        if (hasSpecital) {
            return;
        }
        int currentHp = MB14Base.MB14Health.CurrentHp;
        int maxHp = MB14Base.MB14Stat.MaxHP.Value;
        float currentHpPercent = currentHp * 1.0f / maxHp;
        if (currentHpPercent <= percentAttackSpecial) {
            hasSpecital = true;
            MB14Base.IsSpecialState = true;
        }
    }


    //public override void Updating() {
    //    if (!MB14Base.IsSpecialState && MB14Base.MB14Health.GetPercentHPRemain() < percentAttackSpecial) {
    //        MB14Base.IsSpecialState = true;
    //        MB14Base.MB14Move.CanKnockBack = false;
    //        SetCurrentAttack(specialAttack);
    //    }
    //    //base.Updating();
    //    if (currentAttack) {
    //        currentAttack.Updating();
    //    }
    //}
    #endregion
}
