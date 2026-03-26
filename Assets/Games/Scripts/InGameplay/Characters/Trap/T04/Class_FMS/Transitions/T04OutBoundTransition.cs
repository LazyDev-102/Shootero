

using Class_FSM;

public class T04OutBoundTransition : T04Transition {
    #region Singleton
    public T04OutBoundTransition() {

    }
    private static T04OutBoundTransition instance = null;
    public static T04OutBoundTransition Instance {
        get {
            if (instance == null) {
                instance = new T04OutBoundTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<T04Base> controller) {
        bool isTransition = controller.ObjectBase.T04Move.HasOutBorder();
        if (isTransition) {
            controller.TransitionToState(T04DestroyState.Instance, this);
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
