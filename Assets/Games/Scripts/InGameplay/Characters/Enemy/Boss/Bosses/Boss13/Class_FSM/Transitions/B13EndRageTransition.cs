

using Class_FSM;

public class B13EndRageTransition : B13Transition {

    #region Singleton
    public B13EndRageTransition() {

    }
    private static B13EndRageTransition instance = null;
    public static B13EndRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B13EndRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B13Base> controller) {
        bool isTransition = !controller.ObjectBase.B13Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(B13IdleState.Instance, this);
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
