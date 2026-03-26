

using Class_FSM;

public class E17HasAttackEndTransition : E17Transition {
    #region Singleton
    public E17HasAttackEndTransition() {

    }
    private static E17HasAttackEndTransition instance = null;
    public static E17HasAttackEndTransition Instance {
        get {
            if (instance == null) {
                instance = new E17HasAttackEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E17Base> controller) {
        bool isTransition = !controller.ObjectBase.E17Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(E17MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E17Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E17Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E17Base> controller) {
    }
}
