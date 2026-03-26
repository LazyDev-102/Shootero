

using Class_FSM;

public class B07EndPreRageTransition : B07Transition {
    #region Singleton
    public B07EndPreRageTransition() {

    }
    private static B07EndPreRageTransition instance = null;
    public static B07EndPreRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B07EndPreRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B07Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B07MoveRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B07Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B07Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B07Base> controller) {
    }
}
