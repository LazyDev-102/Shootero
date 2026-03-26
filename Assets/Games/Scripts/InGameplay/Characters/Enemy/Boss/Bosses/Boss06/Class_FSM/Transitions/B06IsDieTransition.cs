

using Class_FSM;

public class B06IsDieTransition : B06Transition {
    #region Singleton
    public B06IsDieTransition() {

    }
    private static B06IsDieTransition instance = null;
    public static B06IsDieTransition Instance {
        get {
            if(instance == null) {
                instance = new B06IsDieTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<B06Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(B06DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<B06Base> controller) {
    }
}
