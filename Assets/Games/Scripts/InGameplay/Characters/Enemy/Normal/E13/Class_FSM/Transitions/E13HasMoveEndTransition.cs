

using Class_FSM;

public class E13HasMoveEndTransition : E13Transition {
    #region Singleton
    public E13HasMoveEndTransition() {

    }
    private static E13HasMoveEndTransition instance = null;
    public static E13HasMoveEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E13HasMoveEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E13Base> controller) {
        bool isTransition = controller.ObjectBase.E13Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E13AttackState.Instance, this);
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
