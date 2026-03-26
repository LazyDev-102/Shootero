

using Class_FSM;

public class E06HasAttackEndTransition : E06Transition {
    #region Singleton
    public E06HasAttackEndTransition() {

    }
    private static E06HasAttackEndTransition instance = null;
    public static E06HasAttackEndTransition Instance {
        get {
            if (instance == null) {
                instance = new E06HasAttackEndTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E06Base> controller) {
        bool isTransition = !controller.ObjectBase.E06Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(E06AimState.Instance, this);
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
