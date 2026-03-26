

using Class_FSM;

public class E17HasDieTransition : E17Transition {
    #region Singleton
    public E17HasDieTransition() {

    }
    private static E17HasDieTransition instance = null;
    public static E17HasDieTransition Instance {
        get {
            if (instance == null) {
                instance = new E17HasDieTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E17Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(E17DeadState.Instance, this);
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
