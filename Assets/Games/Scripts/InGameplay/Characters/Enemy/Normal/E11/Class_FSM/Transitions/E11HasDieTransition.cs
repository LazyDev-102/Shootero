

using Class_FSM;

public class E11HasDieTransition : E11Transition {
    #region Singleton
    public E11HasDieTransition() {

    }
    private static E11HasDieTransition instance = null;
    public static E11HasDieTransition Instance {
        get {
            if (instance == null) {
                instance = new E11HasDieTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E11Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(E11DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E11Base> controller) {
    }
}
