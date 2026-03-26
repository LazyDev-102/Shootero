

using Class_FSM;

public class E16HasDieTransition : E16Transition {
    #region Singleton
    public E16HasDieTransition() {

    }
    private static E16HasDieTransition instance = null;
    public static E16HasDieTransition Instance {
        get {
            if (instance == null) {
                instance = new E16HasDieTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E16Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(E16DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E16Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E16Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E16Base> controller) {
    }
}
