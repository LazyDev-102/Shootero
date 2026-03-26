

using Class_FSM;

public class B08EndPreRageTransition : B08Transition {
    #region Singleton
    public B08EndPreRageTransition() {

    }
    private static B08EndPreRageTransition instance = null;
    public static B08EndPreRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B08EndPreRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B08Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B08RageState.Instance, this);
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
