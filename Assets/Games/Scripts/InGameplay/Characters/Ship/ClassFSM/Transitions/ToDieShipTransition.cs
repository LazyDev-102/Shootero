

using Class_FSM;

public class ToDieShipTransition : ShipTransition {
    #region Singleton
    private ToDieShipTransition() {

    }
    private static ToDieShipTransition instance = null;
    public static ToDieShipTransition Instance {
        get {
            if (instance == null) {
                instance = new ToDieShipTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<ShipBase> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(DeadShipState.Instance, this);
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
