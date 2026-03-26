

using Class_FSM;

public class B12EndRageTransition : B12Transition {

    #region Singleton
    public B12EndRageTransition() {

    }
    private static B12EndRageTransition instance = null;
    public static B12EndRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B12EndRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B12Base> controller) {
        bool isTransition = !controller.ObjectBase.B12Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(B12AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B12Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B12Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B12Base> controller) {
    }
}
