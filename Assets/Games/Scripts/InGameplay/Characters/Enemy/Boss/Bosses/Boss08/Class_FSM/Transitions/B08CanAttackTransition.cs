

using Class_FSM;

public class B08CanAttackTransition : B08Transition {
    #region Singleton
    public B08CanAttackTransition() {

    }
    private static B08CanAttackTransition instance = null;
    public static B08CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B08CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B08Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B08Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(B08AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B08Base> controller) {
    }
}
