

using Class_FSM;

public class B02IsDeadTransition : B02Transition {
    #region Singleton
    public B02IsDeadTransition() {

    }
    private static B02IsDeadTransition instance = null;
    public static B02IsDeadTransition Instance {
        get {
            if(instance == null) {
                instance = new B02IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B02Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(B02DeadState.Instance, this);
        }

        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B02Base> controller) {
    }
}
