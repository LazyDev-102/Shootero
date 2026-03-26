
using Class_FSM;

public class B13EndPreRageTransition : B13Transition {
    #region Singleton
    public B13EndPreRageTransition() {

    }
    private static B13EndPreRageTransition instance = null;
    public static B13EndPreRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B13EndPreRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B13Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B13RageState.Instance, this);
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
