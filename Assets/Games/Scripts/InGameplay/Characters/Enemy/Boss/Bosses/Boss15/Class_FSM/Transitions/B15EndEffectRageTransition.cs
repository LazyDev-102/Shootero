

using Class_FSM;

public class B15EndEffectRageTransition : B15Transition {
    #region Singleton
    public B15EndEffectRageTransition() {

    }
    private static B15EndEffectRageTransition instance = null;
    public static B15EndEffectRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B15EndEffectRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B15Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B15RageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B15Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B15Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B15Base> controller) {
    }
}
