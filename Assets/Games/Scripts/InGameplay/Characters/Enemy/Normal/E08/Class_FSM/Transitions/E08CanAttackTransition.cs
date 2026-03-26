

using Class_FSM;

public class E08CanAttackTransition : E08Transition {
    #region Singleton
    public E08CanAttackTransition() {

    }
    private static E08CanAttackTransition instance = null;
    public static E08CanAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new E08CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E08Base> controller) {
        bool isTransition = controller.ObjectBase.E08Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(E08AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E08Base> controller) {
    }
}
