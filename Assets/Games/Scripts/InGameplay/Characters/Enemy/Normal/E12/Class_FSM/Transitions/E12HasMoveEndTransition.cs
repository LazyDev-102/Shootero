

using Class_FSM;

public class E12HasMoveEndTransition : E12Transition {
    #region Singleton
    public E12HasMoveEndTransition() {

    }
    private static E12HasMoveEndTransition instance = null;
    public static E12HasMoveEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E12HasMoveEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E12Base> controller) {
        bool isTransition = controller.ObjectBase.E12Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E12AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E12Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E12Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E12Base> controller) {
    }
}
