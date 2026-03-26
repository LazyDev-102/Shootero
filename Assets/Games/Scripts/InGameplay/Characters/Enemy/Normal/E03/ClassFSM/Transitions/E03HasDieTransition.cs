

using Class_FSM;

public class E03HasDieTransition : E03Transition {
    #region Singleton
    public E03HasDieTransition() {

    }
    private static E03HasDieTransition instance = null;
    public static E03HasDieTransition Instance {
        get {
            if(instance == null) {
                instance = new E03HasDieTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<E03Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(E03DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<E03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E03Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<E03Base> controller) {
    }
}
