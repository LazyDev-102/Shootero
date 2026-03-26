

using Class_FSM;

public class B04CanMoveRageTransition : B04Transition {
    #region Singleton
    public B04CanMoveRageTransition() {

    }
    private static B04CanMoveRageTransition instance = null;
    public static B04CanMoveRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B04CanMoveRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B04Base> controller) {
        bool isTransition = controller.ObjectBase.CanMoveRage();
        if (isTransition) {
            controller.TransitionToState(B04MoveRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B04Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B04Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B04Base> controller) {
    }
}
