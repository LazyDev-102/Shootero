

using Class_FSM;

public class HB01EndRageTransition : HB01Transition {

    #region Singleton
    public HB01EndRageTransition() {

    }
    private static HB01EndRageTransition instance = null;
    public static HB01EndRageTransition Instance {
        get {
            if (instance == null) {
                instance = new HB01EndRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<HB01Base> controller) {
        bool isTransition = !controller.ObjectBase.HB01Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(HB01StartState.Instance, this);
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
