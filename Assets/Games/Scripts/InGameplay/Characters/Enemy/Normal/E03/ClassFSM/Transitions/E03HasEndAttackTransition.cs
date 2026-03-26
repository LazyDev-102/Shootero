

using Class_FSM;

public class E03HasEndAttackTransition : E03Transition {
    #region Singleton
    public E03HasEndAttackTransition() {

    }
    private static E03HasEndAttackTransition instance = null;
    public static E03HasEndAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new E03HasEndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E03Base> controller) {
        bool isTransition = !controller.ObjectBase.E03Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(E03AimState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E03Base> controller) {
    }
}
