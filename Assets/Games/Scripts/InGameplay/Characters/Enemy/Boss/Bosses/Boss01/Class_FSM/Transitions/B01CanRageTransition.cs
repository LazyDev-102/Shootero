using Class_FSM;

public class B01CanRageTransition : B01Transition {

    #region Singleton
    public B01CanRageTransition() {

    }
    private static B01CanRageTransition instance = null;
    public static B01CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B01CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B01Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus && !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B01StartEffectRageState.Instance, this);
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
