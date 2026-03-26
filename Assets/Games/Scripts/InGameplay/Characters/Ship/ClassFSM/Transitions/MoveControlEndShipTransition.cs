

using Class_FSM;

public class MoveControlEndShipTransition : ShipTransition {
    #region Singleton
    private MoveControlEndShipTransition() {

    }
    private static MoveControlEndShipTransition instance = null;
    public static MoveControlEndShipTransition Instance {
        get {
            if (instance == null) {
                instance = new MoveControlEndShipTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<ShipBase> controller) {
        bool isTransition = controller.ObjectBase.ShipMove.HasMoveControlComplete();
        if (isTransition) {
            controller.TransitionToState(IdleShipState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<ShipBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<ShipBase> controller) {
    }

    public override void DoAfterTransitionActions(StateController<ShipBase> controller) {
    }
}
