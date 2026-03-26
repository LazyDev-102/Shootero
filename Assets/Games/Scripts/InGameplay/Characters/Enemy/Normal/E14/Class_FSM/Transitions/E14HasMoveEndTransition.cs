

using Class_FSM;

public class E14HasMoveEndTransition : E14Transition {
    #region Singleton
    public E14HasMoveEndTransition() {

    }
    private static E14HasMoveEndTransition instance = null;
    public static E14HasMoveEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E14HasMoveEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E14Base> controller) {
        bool isTransition = controller.ObjectBase.E14Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E14AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E14Base> controller) {
    }
}
