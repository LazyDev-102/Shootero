

using Class_FSM;

public class E14HasDieTransition : E14Transition {
    #region Singleton
    public E14HasDieTransition() {

    }
    private static E14HasDieTransition instance = null;
    public static E14HasDieTransition Instance {
        get {
            if (instance == null) {
                instance = new E14HasDieTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E14Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(E14DeadState.Instance, this);
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
