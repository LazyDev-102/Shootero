

using Class_FSM;

public class E09EndAppearTransition : E09Transition {
    #region Singleton
    public E09EndAppearTransition() {

    }
    private static E09EndAppearTransition instance = null;
    public static E09EndAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E09EndAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E09Base> controller) {
        bool isTransition = controller.ObjectBase.E09Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E09AimState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E09Base> controller) {
    }
}
