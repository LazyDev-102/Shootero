

using Class_FSM;

public class B15IsDieTransition : B15Transition {
    #region Singleton
    public B15IsDieTransition() {

    }
    private static B15IsDieTransition instance = null;
    public static B15IsDieTransition Instance {
        get {
            if(instance == null) {
                instance = new B15IsDieTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<B15Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(B15DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<B15Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B15Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<B15Base> controller) {
    }
}
