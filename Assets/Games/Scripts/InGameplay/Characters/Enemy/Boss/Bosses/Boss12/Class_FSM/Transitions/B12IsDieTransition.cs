

using Class_FSM;

public class B12IsDieTransition : B12Transition {
    #region Singleton
    public B12IsDieTransition() {

    }
    private static B12IsDieTransition instance = null;
    public static B12IsDieTransition Instance {
        get {
            if(instance == null) {
                instance = new B12IsDieTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<B12Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(B12DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<B12Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B12Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<B12Base> controller) {
    }
}
