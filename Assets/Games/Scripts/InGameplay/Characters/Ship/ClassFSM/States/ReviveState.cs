

using Class_FSM;

public class ReviveState : ShipState {
    #region Singleton
    private ReviveState() {

    }
    private static ReviveState instance = null;
    public static ReviveState Instance {
        get {
            if (instance == null) {
                instance = new ReviveState();
            }
            return instance;
        }
    }
    #endregion

    private ShipTransition[] transitions = { EndReviveTransition.Instance };
    protected override void DoEndActions(StateController<ShipBase> controller) {
        controller.ObjectBase.EndRevive();
    }

    protected override void DoStartActions(StateController<ShipBase> controller) {
        controller.ObjectBase.ShipMove.StartMoveReivive();
    }

    protected override void DoUpdateActions(StateController<ShipBase> controller) {
    }

    protected override Transition<ShipBase>[] GetTransitions() {
        return transitions;
    }
}
