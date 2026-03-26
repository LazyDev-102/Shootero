

using Class_FSM;

public class B01EndEffectRageTransition : B01Transition {
    #region Singleton
    public B01EndEffectRageTransition() {

    }
    private static B01EndEffectRageTransition instance = null;
    public static B01EndEffectRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B01EndEffectRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B01Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B01RageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B01Base> controller) {
    }
}
