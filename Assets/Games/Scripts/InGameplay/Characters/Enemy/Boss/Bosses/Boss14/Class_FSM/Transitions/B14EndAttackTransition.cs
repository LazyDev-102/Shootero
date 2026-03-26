

using Class_FSM;

public class B14EndAttackTransition : B14Transition {
    #region Singleton
    public B14EndAttackTransition() {

    }
    private static B14EndAttackTransition instance = null;
    public static B14EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B14EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B14Base> controller) {
        bool isTransition = !controller.ObjectBase.B14Attack.IsAttacking() && !controller.ObjectBase.IsInRageStatus;
        if (isTransition) {
            controller.TransitionToState(B14MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B14Base> controller) {
    }
}
