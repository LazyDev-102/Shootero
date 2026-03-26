using Class_FSM;

public class ShipAppearCompleteTransition : ShipTransition {
    #region Singleton
    public ShipAppearCompleteTransition() {

    }
    private static ShipAppearCompleteTransition instance = null;
    public static ShipAppearCompleteTransition Instance {
        get {
            if (instance == null) {
                instance = new ShipAppearCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<ShipBase> controller) {
        bool isTransition = controller.ObjectBase.ShipMove.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(IdleShipState.Instance, this);
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