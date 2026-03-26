using Class_FSM;

public class B15CanRageTransition : B15Transition {

    #region Singleton
    public B15CanRageTransition() {

    }
    private static B15CanRageTransition instance = null;
    public static B15CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B15CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B15Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus && !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B15StartEffectRageState.Instance, this);
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
