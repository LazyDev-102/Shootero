

using Class_FSM;

public class B01IsDieTransition : B01Transition {
    #region Singleton
    public B01IsDieTransition() {

    }
    private static B01IsDieTransition instance = null;
    public static B01IsDieTransition Instance {
        get {
            if(instance == null) {
                instance = new B01IsDieTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<B01Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(B01DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<B01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B01Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<B01Base> controller) {
    }
}
