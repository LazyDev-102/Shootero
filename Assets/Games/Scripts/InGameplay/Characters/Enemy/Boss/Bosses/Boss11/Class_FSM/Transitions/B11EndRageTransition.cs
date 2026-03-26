

using Class_FSM;

public class B11EndRageTransition : B11Transition {

    #region Singleton
    public B11EndRageTransition() {

    }
    private static B11EndRageTransition instance = null;
    public static B11EndRageTransition Instance {
        get {
            if(instance == null) {
                instance = new B11EndRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B11Base> controller) {
        bool isTransition = !controller.ObjectBase.B11Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(B11IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B11Base> controller) {
    }
}
