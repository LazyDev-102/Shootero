

using Class_FSM;

public class E15HasAttackEndTransition : E15Transition {
    #region Singleton
    public E15HasAttackEndTransition() {

    }
    private static E15HasAttackEndTransition instance = null;
    public static E15HasAttackEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E15HasAttackEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E15Base> controller) {
        bool isTransition = !controller.ObjectBase.E15Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(E15MoveState.Instance, this);
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
