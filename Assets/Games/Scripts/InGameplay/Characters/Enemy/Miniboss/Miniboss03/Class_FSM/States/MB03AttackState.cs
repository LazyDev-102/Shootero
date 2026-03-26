using Class_FSM;
using UnityEngine;

public class MB03AttackState : MB03State {
    #region Singleton
    public MB03AttackState() {

    }
    private static MB03AttackState instance = null;
    public static MB03AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB03AttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB03Transition[] transitions = { MB03EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB03Base> controller) {
    }

    protected override void DoStartActions(StateController<MB03Base> controller) {
        MB03Attack attack = controller.ObjectBase.MB03Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB03Base> controller) {
    }

    protected override Transition<MB03Base>[] GetTransitions() {
        return transitions;
    }
}
