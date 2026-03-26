

using Class_FSM;

public class E11HasAttackEndTransition : E11Transition {
    #region Singleton
    public E11HasAttackEndTransition() {

    }
    private static E11HasAttackEndTransition instance = null;
    public static E11HasAttackEndTransition Instance {
        get {
            if(instance == null) {
                instance = new E11HasAttackEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E11Base> controller) {
        bool isTransition = !controller.ObjectBase.E11Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(E11MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E11Base> controller) {
    }
}
