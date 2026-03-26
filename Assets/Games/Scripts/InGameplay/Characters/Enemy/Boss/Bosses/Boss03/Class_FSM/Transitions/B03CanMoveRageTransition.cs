

using Class_FSM;

public class B03CanMoveRageTransition : B03Transition {
    #region Singleton
    public B03CanMoveRageTransition() {

    }
    private static B03CanMoveRageTransition instance = null;
    public static B03CanMoveRageTransition Instance {
        get {
            if(instance == null) {
                instance = new B03CanMoveRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B03Base> controller) {
        bool isTransition = controller.ObjectBase.CanMoveRage();
        if(isTransition) {
            controller.TransitionToState(B03MoveRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B03Base> controller) {
    }
}
