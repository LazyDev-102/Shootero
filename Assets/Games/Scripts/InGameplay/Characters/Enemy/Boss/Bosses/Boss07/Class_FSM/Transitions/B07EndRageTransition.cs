

using Class_FSM;

public class B07EndRageTransition : B07Transition {
    #region Singleton
    public B07EndRageTransition() {

    }
    private static B07EndRageTransition instance = null;
    public static B07EndRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B07EndRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B07Base> controller) {
        bool isTransition = !controller.ObjectBase.B07Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(B07MoveState.Instance, this);
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
