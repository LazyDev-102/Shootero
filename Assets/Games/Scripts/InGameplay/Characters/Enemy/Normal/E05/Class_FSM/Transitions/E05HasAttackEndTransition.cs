

using Class_FSM;

public class E05HasAttackEndTransition : E05Transition {
    #region Singleton
    public E05HasAttackEndTransition() {

    }
    private static E05HasAttackEndTransition instance = null;
    public static E05HasAttackEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E05HasAttackEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E05Base> controller) {
        bool isTransition = !controller.ObjectBase.E05Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(E05MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E05Base> controller) {
    }
}
