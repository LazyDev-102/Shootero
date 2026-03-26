
using Class_FSM;

public class B11EndPreRageTransition : B11Transition {
    #region Singleton
    public B11EndPreRageTransition() {

    }
    private static B11EndPreRageTransition instance = null;
    public static B11EndPreRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B11EndPreRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B11Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B11RageState.Instance, this);
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
