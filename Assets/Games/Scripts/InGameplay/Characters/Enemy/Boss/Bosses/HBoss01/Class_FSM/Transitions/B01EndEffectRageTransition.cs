

using Class_FSM;

public class HB01EndEffectRageTransition : HB01Transition {
    #region Singleton
    public HB01EndEffectRageTransition() {

    }
    private static HB01EndEffectRageTransition instance = null;
    public static HB01EndEffectRageTransition Instance {
        get {
            if (instance == null) {
                instance = new HB01EndEffectRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<HB01Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(HB01RageState.Instance, this);
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
