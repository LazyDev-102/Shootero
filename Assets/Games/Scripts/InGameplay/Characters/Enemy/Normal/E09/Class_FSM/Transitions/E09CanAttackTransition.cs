

using Class_FSM;

public class E09CanAttackTransition : E09Transition {
    #region Singleton
    public E09CanAttackTransition() {

    }
    private static E09CanAttackTransition instance = null;
    public static E09CanAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new E09CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<E09Base> controller) {
        bool isTransition = controller.ObjectBase.E09Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(E09AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<E09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<E09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<E09Base> controller) {
    }
}
