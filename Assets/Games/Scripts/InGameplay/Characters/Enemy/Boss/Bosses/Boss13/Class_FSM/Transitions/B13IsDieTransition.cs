

using Class_FSM;

public class B13IsDieTransition : B13Transition {
    #region Singleton
    public B13IsDieTransition() {

    }
    private static B13IsDieTransition instance = null;
    public static B13IsDieTransition Instance {
        get {
            if (instance == null) {
                instance = new B13IsDieTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<B13Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(B13DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<B13Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B13Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<B13Base> controller) {
    }
}
