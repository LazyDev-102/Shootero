
using Class_FSM;

public class B12EndPreRageTransition : B12Transition {
    #region Singleton
    public B12EndPreRageTransition() {

    }
    private static B12EndPreRageTransition instance = null;
    public static B12EndPreRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B12EndPreRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B12Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B12RageState.Instance, this);
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
