using Class_FSM;

public class HB01CanRageTransition : HB01Transition {

    #region Singleton
    public HB01CanRageTransition() {

    }
    private static HB01CanRageTransition instance = null;
    public static HB01CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new HB01CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<HB01Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus && !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(HB01StartEffectRageState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<HB01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<HB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<HB01Base> controller) {
    }
}
