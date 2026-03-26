using Class_FSM;
using UnityEngine;

public class MB13AttackState : MB13State {
    #region Singleton
    public MB13AttackState() {

    }
    private static MB13AttackState instance = null;
    public static MB13AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB13AttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB13Transition[] transitions = { MB13EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB13Base> controller) {
    }

    protected override void DoStartActions(StateController<MB13Base> controller) {
        MB13Attack attack = controller.ObjectBase.MB13Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB13Base> controller) {
    }

    protected override Transition<MB13Base>[] GetTransitions() {
        return transitions;
    }
}
