

using Class_FSM;

public class E04HasMoveEndTransition : E04Transition {
    #region Singleton
    public E04HasMoveEndTransition() {

    }
    private static E04HasMoveEndTransition instance = null;
    public static E04HasMoveEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E04HasMoveEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E04Base> controller) {
        bool isTransition = controller.ObjectBase.E04Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E04IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E04Base> controller) {

    }

    public override void DoBeforeTransitionActions(StateController<E04Base> controller) {

    }

    public override void DoWhileTransitionActions(StateController<E04Base> controller) {

    }
}
