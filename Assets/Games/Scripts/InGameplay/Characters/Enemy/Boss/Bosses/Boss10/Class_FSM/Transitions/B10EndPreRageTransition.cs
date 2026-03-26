

using Class_FSM;

public class B10EndPreRageTransition : B10Transition {
    #region Singleton
    public B10EndPreRageTransition() {

    }
    private static B10EndPreRageTransition instance = null;
    public static B10EndPreRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B10EndPreRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B10Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B10StartRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B10Base> controller) {
    }
}
