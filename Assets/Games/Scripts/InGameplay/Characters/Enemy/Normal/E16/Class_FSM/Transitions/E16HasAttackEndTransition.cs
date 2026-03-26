

using Class_FSM;

public class E16HasAttackEndTransition : E16Transition {
    #region Singleton
    public E16HasAttackEndTransition() {

    }
    private static E16HasAttackEndTransition instance = null;
    public static E16HasAttackEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E16HasAttackEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E16Base> controller) {
        bool isTransition = !controller.ObjectBase.E16Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(E16MoveState.Instance, this);
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
