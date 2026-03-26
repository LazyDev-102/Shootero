

using Class_FSM;

public class E15HasDieTransition : E15Transition {
    #region Singleton
    public E15HasDieTransition() {

    }
    private static E15HasDieTransition instance = null;
    public static E15HasDieTransition Instance {
        get {
            if (instance == null) {
                instance = new E15HasDieTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E15Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(E15DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E15Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E15Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E15Base> controller) {
    }
}
