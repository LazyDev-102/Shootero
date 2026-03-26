

using Class_FSM;

public class E13HasAttackEndTransition : E13Transition {
    #region Singleton
    public E13HasAttackEndTransition() {

    }
    private static E13HasAttackEndTransition instance = null;
    public static E13HasAttackEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E13HasAttackEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E13Base> controller) {
        bool isTransition = !controller.ObjectBase.E13Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(E13MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E13Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E13Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E13Base> controller) {
    }
}
