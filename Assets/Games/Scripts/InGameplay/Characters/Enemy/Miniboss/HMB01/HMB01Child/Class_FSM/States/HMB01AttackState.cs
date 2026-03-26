using Class_FSM;
using UnityEngine;

public class HMB01AttackState : HMB01State {
    #region Singleton
    public HMB01AttackState() {

    }
    private static HMB01AttackState instance = null;
    public static HMB01AttackState Instance {
        get {
            if (instance == null) {
                instance = new HMB01AttackState();
            }
            return instance;
        }
    }
    #endregion

    private HMB01Transition[] transitions = { HMB01EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<HMB01Base> controller) {
    }

    protected override void DoStartActions(StateController<HMB01Base> controller) {
        HMB01Attack attack = controller.ObjectBase.HMB01Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<HMB01Base> controller) {
    }

    protected override Transition<HMB01Base>[] GetTransitions() {
        return transitions;
    }
}
