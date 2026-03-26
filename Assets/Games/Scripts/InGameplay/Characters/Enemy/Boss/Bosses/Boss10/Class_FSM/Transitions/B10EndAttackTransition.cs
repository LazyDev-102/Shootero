

using Class_FSM;

public class B10EndAttackTransition : B10Transition {

    #region Singleton
    public B10EndAttackTransition() {

    }
    private static B10EndAttackTransition instance = null;
    public static B10EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B10EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B10Base> controller) {
        bool isTransition = !controller.ObjectBase.B10Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(B10MoveState.Instance, this);
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
