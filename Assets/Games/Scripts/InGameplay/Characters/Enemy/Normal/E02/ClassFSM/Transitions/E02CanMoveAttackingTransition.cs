

using Class_FSM;

public class E02CanMoveAttackingTransition : E02Transition {
    #region Singleton
    public E02CanMoveAttackingTransition() {

    }
    private static E02CanMoveAttackingTransition instance = null;
    public static E02CanMoveAttackingTransition Instance {
        get {
            if(instance == null) {
                instance = new E02CanMoveAttackingTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E02Base> controller) {
        bool isTransition = controller.ObjectBase.E02Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(E02MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E02Base> controller) {
    }
}
