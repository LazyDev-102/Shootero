

using Class_FSM;

public class E05HasDieTransition : E05Transition {
    #region Singleton
    public E05HasDieTransition() {

    }
    private static E05HasDieTransition instance = null;
    public static E05HasDieTransition Instance {
        get {
            if(instance == null) {
                instance = new E05HasDieTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E05Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(E05DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E05Base> controller) {
    }
}
