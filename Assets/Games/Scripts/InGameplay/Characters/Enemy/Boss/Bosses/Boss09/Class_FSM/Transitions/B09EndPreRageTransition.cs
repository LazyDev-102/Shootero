

using Class_FSM;

public class B09EndPreRageTransition : B09Transition {
    #region Singleton
    public B09EndPreRageTransition() {

    }
    private static B09EndPreRageTransition instance = null;
    public static B09EndPreRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B09EndPreRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B09Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B09RageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B09Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B09Base> controller) {
    }
}
