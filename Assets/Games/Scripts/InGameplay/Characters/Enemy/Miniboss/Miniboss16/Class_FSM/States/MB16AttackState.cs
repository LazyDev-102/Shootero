using Class_FSM;
using UnityEngine;

public class MB16AttackState : MB16State {
    #region Singleton
    public MB16AttackState() {

    }
    private static MB16AttackState instance = null;
    public static MB16AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB16AttackState();
            }
            return instance;
        }
    }
    #endregion

    private MB16Transition[] transitions = { MB16EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB16Base> controller) {
    }

    protected override void DoStartActions(StateController<MB16Base> controller) {
        MB16Attack attack = controller.ObjectBase.MB16Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB16Base> controller) {
    }

    protected override Transition<MB16Base>[] GetTransitions() {
        return transitions;
    }
}
