

using Class_FSM;

public class E15HasMoveEndTransition : E15Transition {
    #region Singleton
    public E15HasMoveEndTransition() {

    }
    private static E15HasMoveEndTransition instance = null;
    public static E15HasMoveEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E15HasMoveEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E15Base> controller) {
        bool isTransition = controller.ObjectBase.E15Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(E15AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E15Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E15Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E15Base> controller) {
    }
}
