
using Class_FSM;

public class B14EndPreRageTransition : B14Transition {
    #region Singleton
    public B14EndPreRageTransition() {

    }
    private static B14EndPreRageTransition instance = null;
    public static B14EndPreRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B14EndPreRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B14Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B14RageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B14Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B14Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B14Base> controller) {
    }
}
