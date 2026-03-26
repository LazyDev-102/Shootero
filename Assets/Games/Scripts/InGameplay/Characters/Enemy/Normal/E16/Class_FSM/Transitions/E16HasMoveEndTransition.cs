

using Class_FSM;

public class E16HasMoveEndTransition : E16Transition {
    #region Singleton
    public E16HasMoveEndTransition() {

    }
    private static E16HasMoveEndTransition instance = null;
    public static E16HasMoveEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E16HasMoveEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E16Base> controller) {
        bool isTransition = controller.ObjectBase.E16Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E16AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E16Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E16Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E16Base> controller) {
    }
}
