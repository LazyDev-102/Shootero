

using Class_FSM;

public class E07EndMoveTransition : E07Transition {
    #region Singleton
    public E07EndMoveTransition() {

    }
    private static E07EndMoveTransition instance = null;
    public static E07EndMoveTransition Instance {
        get {
            if(instance == null) {
                instance = new E07EndMoveTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E07Base> controller) {
        bool isTransition = controller.ObjectBase.E07Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E07AimState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E07Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E07Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E07Base> controller) {
    }
}
