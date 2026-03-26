

using Class_FSM;

public class E06HasMoveEndTransition : E06Transition {
    #region Singleton
    public E06HasMoveEndTransition() {

    }
    private static E06HasMoveEndTransition instance = null;
    public static E06HasMoveEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E06HasMoveEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E06Base> controller) {
        bool isTransition = controller.ObjectBase.E06Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E06AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E06Base> controller) {
    }
}
