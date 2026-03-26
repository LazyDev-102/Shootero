

using Class_FSM;

public class T04CanMoveTransition : T04Transition {
    #region Singleton
    public T04CanMoveTransition() {

    }
    private static T04CanMoveTransition instance = null;
    public static T04CanMoveTransition Instance {
        get {
            if (instance == null) {
                instance = new T04CanMoveTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<T04Base> controller) {
        bool isTransition = controller.ObjectBase.T04Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(T04MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<T04Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<T04Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<T04Base> controller) {
    }
}
