

using Class_FSM;

public class E12HasDieTransition : E12Transition {
    #region Singleton
    public E12HasDieTransition() {

    }
    private static E12HasDieTransition instance = null;
    public static E12HasDieTransition Instance {
        get {
            if (instance == null) {
                instance = new E12HasDieTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E12Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(E12DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E12Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E12Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E12Base> controller) {
    }
}
