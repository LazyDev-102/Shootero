

using Class_FSM;

public class B05EndRageTransition : B05Transition {

    #region Singleton
    public B05EndRageTransition() {

    }
    private static B05EndRageTransition instance = null;
    public static B05EndRageTransition Instance {
        get {
            if(instance == null) {
                instance = new B05EndRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B05Base> controller) {
        bool isTransition = !controller.ObjectBase.B05Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(B05IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B05Base> controller) {
    }
}
