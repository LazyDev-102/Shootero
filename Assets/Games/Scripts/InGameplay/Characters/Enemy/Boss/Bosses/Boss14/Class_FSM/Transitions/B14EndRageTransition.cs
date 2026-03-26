

using Class_FSM;

public class B14EndRageTransition : B14Transition {

    #region Singleton
    public B14EndRageTransition() {

    }
    private static B14EndRageTransition instance = null;
    public static B14EndRageTransition Instance {
        get {
            if(instance == null) {
                instance = new B14EndRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B14Base> controller) {
        bool isTransition = !controller.ObjectBase.B14Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(B14IdleState.Instance, this);
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
