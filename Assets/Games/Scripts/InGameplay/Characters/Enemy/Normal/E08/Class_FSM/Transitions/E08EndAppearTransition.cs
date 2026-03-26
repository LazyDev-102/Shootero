

using Class_FSM;

public class E08EndAppearTransition : E08Transition {
    #region Singleton
    public E08EndAppearTransition() {

    }
    private static E08EndAppearTransition instance = null;
    public static E08EndAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new E08EndAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E08Base> controller) {
        bool isTransition = controller.ObjectBase.E08Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E08AimState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E08Base> controller) {
    }
}
