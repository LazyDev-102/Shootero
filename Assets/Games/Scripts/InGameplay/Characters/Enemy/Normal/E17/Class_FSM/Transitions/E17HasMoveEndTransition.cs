

using Class_FSM;

public class E17HasMoveEndTransition : E17Transition {
    #region Singleton
    public E17HasMoveEndTransition() {

    }
    private static E17HasMoveEndTransition instance = null;
    public static E17HasMoveEndTransition Instance {
        get {
            if (instance == null) {
                instance = new E17HasMoveEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E17Base> controller) {
        bool isTransition = controller.ObjectBase.E17Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(E17AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E17Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E17Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E17Base> controller) {
    }
}
