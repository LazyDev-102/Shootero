using Class_FSM;
using UnityEngine;

public class MB17AttackState : MB17State {
    #region Singleton
    public MB17AttackState() {

    }
    private static MB17AttackState instance = null;
    public static MB17AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB17AttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB17Transition[] transitions = { MB17EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB17Base> controller) {
    }

    protected override void DoStartActions(StateController<MB17Base> controller) {
        MB17Attack attack = controller.ObjectBase.MB17Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB17Base> controller) {
    }

    protected override Transition<MB17Base>[] GetTransitions() {
        return transitions;
    }
}
