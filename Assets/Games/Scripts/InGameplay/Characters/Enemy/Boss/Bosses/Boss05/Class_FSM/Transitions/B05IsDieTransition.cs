

using Class_FSM;

public class B05IsDieTransition : B05Transition {
    #region Singleton
    public B05IsDieTransition() {

    }
    private static B05IsDieTransition instance = null;
    public static B05IsDieTransition Instance {
        get {
            if(instance == null) {
                instance = new B05IsDieTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<B05Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(B05DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<B05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B05Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<B05Base> controller) {
    }
}
