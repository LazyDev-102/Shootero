

using Class_FSM;

public class E12HasAttackEndTransition : E12Transition {
    #region Singleton
    public E12HasAttackEndTransition() {

    }
    private static E12HasAttackEndTransition instance = null;
    public static E12HasAttackEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E12HasAttackEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E12Base> controller) {
        bool isTransition = !controller.ObjectBase.E12Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(E12MoveState.Instance, this);
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
