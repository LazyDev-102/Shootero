

using Class_FSM;

public class CanReviveTransition : ShipTransition {
    #region Singleton
    private CanReviveTransition() {

    }
    private static CanReviveTransition instance = null;
    public static CanReviveTransition Instance {
        get {
            if (instance == null) {
                instance = new CanReviveTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<ShipBase> controller) {
        bool isTransition = controller.ObjectBase.IsReviving;
        if (isTransition) {
            controller.TransitionToState(ReviveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<ShipBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<ShipBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<ShipBase> controller) {
    }
}
