

using Class_FSM;

public class B09IsDieTransition : B09Transition {
    #region Singleton
    public B09IsDieTransition() {

    }
    private static B09IsDieTransition instance = null;
    public static B09IsDieTransition Instance {
        get {
            if(instance == null) {
                instance = new B09IsDieTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<B09Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(B09DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<B09Base> controller) {
    }
}
