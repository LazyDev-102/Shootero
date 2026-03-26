

using Class_FSM;

public class T03OutBoundTransition : T03Transition {
    #region Singleton
    public T03OutBoundTransition() {

    }
    private static T03OutBoundTransition instance = null;
    public static T03OutBoundTransition Instance {
        get {
            if (instance == null) {
                instance = new T03OutBoundTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<T03Base> controller) {
        bool isTransition = controller.ObjectBase.T03Move.HasOutBorder();
        if (isTransition) {
            controller.TransitionToState(T03DestroyState.Instance, this);
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
