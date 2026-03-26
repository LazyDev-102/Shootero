

using Class_FSM;

public class B13EndAttackTransition : B13Transition {
    #region Singleton
    public B13EndAttackTransition() {

    }
    private static B13EndAttackTransition instance = null;
    public static B13EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B13EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B13Base> controller) {
        bool isTransition = !controller.ObjectBase.B13Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(B13MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B13Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B13Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B13Base> controller) {
    }
}
