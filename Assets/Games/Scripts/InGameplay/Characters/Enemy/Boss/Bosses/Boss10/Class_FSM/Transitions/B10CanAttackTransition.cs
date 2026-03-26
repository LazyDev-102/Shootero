

using Class_FSM;

public class B10CanAttackTransition : B10Transition {
    #region Singleton
    public B10CanAttackTransition() {

    }
    private static B10CanAttackTransition instance = null;
    public static B10CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B10CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B10Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B10Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(B10AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B10Base> controller) {
    }
}
