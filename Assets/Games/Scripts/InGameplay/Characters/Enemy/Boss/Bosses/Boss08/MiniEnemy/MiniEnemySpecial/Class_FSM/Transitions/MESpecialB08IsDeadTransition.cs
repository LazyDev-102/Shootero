

using Class_FSM;

public class MESpecialB08IsDeadTransition : MESpecialB08Transition {
    #region Singleton
    public MESpecialB08IsDeadTransition() {

    }
    private static MESpecialB08IsDeadTransition instance = null;
    public static MESpecialB08IsDeadTransition Instance {
        get {
            if (instance == null) {
                instance = new MESpecialB08IsDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MESpecialB08Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(MESpecialB08DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MESpecialB08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MESpecialB08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MESpecialB08Base> controller) {
    }
}
