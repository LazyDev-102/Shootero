

using Class_FSM;

public class B15EndAttackTransition : B15Transition {
    #region Singleton
    public B15EndAttackTransition() {

    }
    private static B15EndAttackTransition instance = null;
    public static B15EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B15EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B15Base> controller) {
        bool isTransition = !controller.ObjectBase.B15Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(B15IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B15Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B15Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B15Base> controller) {
    }
}
