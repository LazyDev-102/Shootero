

using Class_FSM;

public class E10CanAppearTransition : E10Transition {
    #region Singleton
    public E10CanAppearTransition() {

    }
    private static E10CanAppearTransition instance = null;
    public static E10CanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E10CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E10Base> controller) {
        bool isTransition = controller.ObjectBase.E10Move.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(E10AppearState.Instance, this);
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
