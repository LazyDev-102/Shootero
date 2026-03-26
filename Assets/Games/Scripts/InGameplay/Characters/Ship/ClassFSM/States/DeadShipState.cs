

using Class_FSM;
using UnityEngine;

public class DeadShipState : ShipState {
    #region Singleton
    public DeadShipState() {

    }
    private static DeadShipState instance = null;
    public static DeadShipState Instance {
        get {
            if (instance == null) {
                instance = new DeadShipState();
            }
            return instance;
        }
    }
    #endregion
    public override Color SceneGizmoColor => Color.gray;
    private ShipTransition[] transitions = { CanReviveTransition.Instance };
    protected override void DoStartActions(StateController<ShipBase> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<ShipBase> controller) {
    }

    protected override void DoEndActions(StateController<ShipBase> controller) {
    }

    protected override Transition<ShipBase>[] GetTransitions() {
        return transitions;
    }
}
