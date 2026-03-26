

using Class_FSM;

public class E10IsShotTransition : E10Transition {
    #region Singleton
    public E10IsShotTransition() {

    }
    private static E10IsShotTransition instance = null;
    public static E10IsShotTransition Instance {
        get {
            if(instance == null) {
                instance = new E10IsShotTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E10Base> controller) {
        bool isTransition = controller.ObjectBase.E10Attack.IsEndShot();
        if(isTransition) {
            controller.TransitionToState(E10AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E10Base> controller) {
    }
}
