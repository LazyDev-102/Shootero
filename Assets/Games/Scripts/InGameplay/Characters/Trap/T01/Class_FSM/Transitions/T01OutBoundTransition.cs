

using Class_FSM;

public class T01OutBoundTransition : T01Transition {
    #region Singleton
    public T01OutBoundTransition() {

    }
    private static T01OutBoundTransition instance = null;
    public static T01OutBoundTransition Instance {
        get {
            if (instance == null) {
                instance = new T01OutBoundTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<T01Base> controller) {
        bool isTransition = controller.ObjectBase.T01Move.HasOutBorder();
        if (isTransition) {
            controller.TransitionToState(T01DestroyState.Instance, this);
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
