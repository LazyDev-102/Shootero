

using Class_FSM;

public class ToControlMoveShipTransition : ShipTransition {
    #region Singleton
    private ToControlMoveShipTransition() {

    }
    private static ToControlMoveShipTransition instance = null;
    public static ToControlMoveShipTransition Instance {
        get {
            if (instance == null) {
                instance = new ToControlMoveShipTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<ShipBase> controller) {
        bool isTransition = controller.ObjectBase.ShipMove.CanMoveControl();
        if (isTransition) {
            controller.TransitionToState(MoveShipState.Instance, this);
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
