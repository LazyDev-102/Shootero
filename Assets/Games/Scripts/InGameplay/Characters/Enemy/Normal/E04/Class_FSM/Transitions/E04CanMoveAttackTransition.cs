

using Class_FSM;

public class E04CanMoveAttackTransition : E04Transition {
    #region Singleton
    public E04CanMoveAttackTransition() {

    }
    private static E04CanMoveAttackTransition instance = null;
    public static E04CanMoveAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new E04CanMoveAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E04Base> controller) {
        bool isTransition = controller.ObjectBase.E04Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(E04MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E04Base> controller) {

    }

    public override void DoBeforeTransitionActions(StateController<E04Base> controller) {

    }

    public override void DoWhileTransitionActions(StateController<E04Base> controller) {

    }
}
