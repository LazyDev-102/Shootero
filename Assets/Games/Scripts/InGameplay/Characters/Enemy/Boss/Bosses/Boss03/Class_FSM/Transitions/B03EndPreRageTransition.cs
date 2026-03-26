

using Class_FSM;

public class B03EndPreRageTransition : B03Transition {
    #region Singleton
    public B03EndPreRageTransition() {

    }
    private static B03EndPreRageTransition instance = null;
    public static B03EndPreRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B03EndPreRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B03Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B03StartRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B03Base> controller) {
    }
}
