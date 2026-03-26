

using Class_FSM;

public class E10EndMoveAppearTransition : E10Transition {
    #region Singleton
    public E10EndMoveAppearTransition() {

    }
    private static E10EndMoveAppearTransition instance = null;
    public static E10EndMoveAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E10EndMoveAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E10Base> controller) {
        bool isTransition = controller.ObjectBase.E10Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E10AimState.Instance, this);
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
