

using Class_FSM;

public class T03CanMoveTransition : T03Transition {
    #region Singleton
    public T03CanMoveTransition() {

    }
    private static T03CanMoveTransition instance = null;
    public static T03CanMoveTransition Instance {
        get {
            if (instance == null) {
                instance = new T03CanMoveTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<T03Base> controller) {
        bool isTransition = controller.ObjectBase.T03Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(T03MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<T03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<T03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<T03Base> controller) {
    }
}
