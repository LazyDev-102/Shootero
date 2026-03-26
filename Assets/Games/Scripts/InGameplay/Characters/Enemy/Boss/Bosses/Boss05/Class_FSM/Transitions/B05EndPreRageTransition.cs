
using Class_FSM;

public class B05EndPreRageTransition : B05Transition {
    #region Singleton
    public B05EndPreRageTransition() {

    }
    private static B05EndPreRageTransition instance = null;
    public static B05EndPreRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B05EndPreRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B05Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B05RageState.Instance, this);
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
