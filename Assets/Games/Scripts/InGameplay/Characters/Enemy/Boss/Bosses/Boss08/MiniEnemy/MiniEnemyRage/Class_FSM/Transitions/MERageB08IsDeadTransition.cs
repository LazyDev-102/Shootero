

using Class_FSM;

public class MERageB08IsDeadTransition : MERageB08Transition {
    #region Singleton
    public MERageB08IsDeadTransition() {

    }
    private static MERageB08IsDeadTransition instance = null;
    public static MERageB08IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MERageB08IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MERageB08Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MERageB08DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MERageB08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MERageB08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MERageB08Base> controller) {
    }
}
