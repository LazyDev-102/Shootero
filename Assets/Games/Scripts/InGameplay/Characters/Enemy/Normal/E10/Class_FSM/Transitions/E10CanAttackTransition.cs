

using Class_FSM;

public class E10CanAttackTransition : E10Transition {
    #region Singleton
    public E10CanAttackTransition() {

    }
    private static E10CanAttackTransition instance = null;
    public static E10CanAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new E10CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E10Base> controller) {
        bool isTransition = controller.ObjectBase.E10Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(E10AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E10Base> controller) {
    }
}
