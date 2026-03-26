

using Class_FSM;

public class B14IsDieTransition : B14Transition {
    #region Singleton
    public B14IsDieTransition() {

    }
    private static B14IsDieTransition instance = null;
    public static B14IsDieTransition Instance {
        get {
            if(instance == null) {
                instance = new B14IsDieTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<B14Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(B14DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<B14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B14Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<B14Base> controller) {
    }
}
