

using Class_FSM;

public class B07CanAttackTransition : B07Transition {
    #region Singleton
    public B07CanAttackTransition() {

    }
    private static B07CanAttackTransition instance = null;
    public static B07CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B07CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B07Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B07Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(B07AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B07Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B07Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B07Base> controller) {
    }
}
