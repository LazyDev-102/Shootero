using Class_FSM;
using UnityEngine;

public class MB10AttackState : MB10State {
    #region Singleton
    public MB10AttackState() {

    }
    private static MB10AttackState instance = null;
    public static MB10AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB10AttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB10Transition[] transitions = { MB10EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB10Base> controller) {
    }

    protected override void DoStartActions(StateController<MB10Base> controller) {
        MB10Attack attack = controller.ObjectBase.MB10Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB10Base> controller) {
    }

    protected override Transition<MB10Base>[] GetTransitions() {
        return transitions;
    }
}
