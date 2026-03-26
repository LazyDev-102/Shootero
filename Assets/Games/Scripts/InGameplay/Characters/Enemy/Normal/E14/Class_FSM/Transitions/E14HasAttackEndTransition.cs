

using Class_FSM;

public class E14HasAttackEndTransition : E14Transition {
    #region Singleton
    public E14HasAttackEndTransition() {

    }
    private static E14HasAttackEndTransition instance = null;
    public static E14HasAttackEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E14HasAttackEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E14Base> controller) {
        bool isTransition = !controller.ObjectBase.E14Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(E14MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E14Base> controller) {
    }
}
