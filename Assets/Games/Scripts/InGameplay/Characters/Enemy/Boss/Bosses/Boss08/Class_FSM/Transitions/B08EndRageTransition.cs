

using Class_FSM;

public class B08EndRageTransition : B08Transition {

    #region Singleton
    public B08EndRageTransition() {

    }
    private static B08EndRageTransition instance = null;
    public static B08EndRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B08EndRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B08Base> controller) {
        bool isTransition = !controller.ObjectBase.B08Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(B08IdleState.Instance, this);
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
