

using Class_FSM;

public class B02EndPreRageTransition : B02Transition {
    #region Singleton
    public B02EndPreRageTransition() {

    }
    private static B02EndPreRageTransition instance = null;
    public static B02EndPreRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B02EndPreRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B02Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B02StartRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B02Base> controller) {
    }
}
