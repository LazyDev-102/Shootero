

using Class_FSM;

public class B11IsDieTransition : B11Transition {
    #region Singleton
    public B11IsDieTransition() {

    }
    private static B11IsDieTransition instance = null;
    public static B11IsDieTransition Instance {
        get {
            if(instance == null) {
                instance = new B11IsDieTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<B11Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(B11DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<B11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B11Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<B11Base> controller) {
    }
}
