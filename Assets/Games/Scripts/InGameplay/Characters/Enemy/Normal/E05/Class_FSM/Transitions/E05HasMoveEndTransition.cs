

using Class_FSM;

public class E05HasMoveEndTransition : E05Transition {
    #region Singleton
    public E05HasMoveEndTransition() {

    }
    private static E05HasMoveEndTransition instance = null;
    public static E05HasMoveEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E05HasMoveEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E05Base> controller) {
        bool isTransition = controller.ObjectBase.E05Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E05AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E05Base> controller) {
    }
}
