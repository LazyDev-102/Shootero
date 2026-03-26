

using Class_FSM;

public class T01CanMoveTransition : T01Transition {
    #region Singleton
    public T01CanMoveTransition() {

    }
    private static T01CanMoveTransition instance = null;
    public static T01CanMoveTransition Instance {
        get {
            if (instance == null) {
                instance = new T01CanMoveTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<T01Base> controller) {
        bool isTransition = controller.ObjectBase.T01Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(T01MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<T01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<T01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<T01Base> controller) {
    }
}
