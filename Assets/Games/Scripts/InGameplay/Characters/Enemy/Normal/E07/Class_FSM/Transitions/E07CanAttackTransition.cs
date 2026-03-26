

using Class_FSM;

public class E07CanAttackTransition : E07Transition {
    #region Singleton
    public E07CanAttackTransition() {

    }
    private static E07CanAttackTransition instance = null;
    public static E07CanAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new E07CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E07Base> controller) {
        bool isTransition = controller.ObjectBase.E07Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(E07AttackMoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E07Base> controller) {

    }

    public override void DoBeforeTransitionActions(StateController<E07Base> controller) {

    }

    public override void DoWhileTransitionActions(StateController<E07Base> controller) {

    }
}
