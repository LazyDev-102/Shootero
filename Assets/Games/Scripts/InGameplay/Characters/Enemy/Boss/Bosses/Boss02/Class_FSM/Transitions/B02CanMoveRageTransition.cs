

using Class_FSM;

public class B02CanMoveRageTransition : B02Transition {
    #region Singleton
    public B02CanMoveRageTransition() {

    }
    private static B02CanMoveRageTransition instance = null;
    public static B02CanMoveRageTransition Instance {
        get {
            if(instance == null) {
                instance = new B02CanMoveRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B02Base> controller) {
        bool isTransition = controller.ObjectBase.CanMoveRage();
        if(isTransition) {
            controller.TransitionToState(B02MoveRageState.Instance, this);
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
