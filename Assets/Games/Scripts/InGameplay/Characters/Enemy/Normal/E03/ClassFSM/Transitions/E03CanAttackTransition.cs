

using Class_FSM;

public class E03CanAttackTransition : E03Transition {
    #region Singleton
    public E03CanAttackTransition() {

    }
    private static E03CanAttackTransition instance = null;
    public static E03CanAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new E03CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E03Base> controller) {
        bool isTransition = controller.ObjectBase.E03Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(E03AttackState.Instance, this);
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
