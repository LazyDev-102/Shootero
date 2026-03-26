using Class_FSM;
using UnityEngine;

public class MB08AttackState : MB08State {

    #region Singleton
    public MB08AttackState() {

    }
    private static MB08AttackState instance = null;
    public static MB08AttackState Instance {
        get {
            if (instance == null) {
                instance = new MB08AttackState();
            }
            return instance;
        }
    }
    #endregion


    private MB08Transition[] transitions = { MB08EndAttackTransition.Instance };

    protected override void DoEndActions(StateController<MB08Base> controller) {
    }

    protected override void DoStartActions(StateController<MB08Base> controller) {
        MB08Attack attack = controller.ObjectBase.MB08Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MB08Base> controller) {
    }

    protected override Transition<MB08Base>[] GetTransitions() {
        return transitions;
    }
}
