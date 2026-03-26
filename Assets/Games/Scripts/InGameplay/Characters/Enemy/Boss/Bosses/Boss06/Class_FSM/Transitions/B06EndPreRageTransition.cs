

using Class_FSM;

public class B06EndPreRageTransition : B06Transition {
    #region Singleton
    public B06EndPreRageTransition() {

    }
    private static B06EndPreRageTransition instance = null;
    public static B06EndPreRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B06EndPreRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B06Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B06RageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B06Base> controller) {
    }
}
