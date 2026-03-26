


using Class_FSM;
using UnityEngine;

public class IdleShipState : ShipState {
    #region Singleton
    public IdleShipState() {

    }
    private static IdleShipState instance = null;
    public static IdleShipState Instance {
        get {
            if(instance == null) {
                instance = new IdleShipState();
            }
            return instance;
        }
    }
    #endregion
    private Transition<ShipBase>[] transitions = { ToControlMoveShipTransition.Instance };

    public override Color SceneGizmoColor => Color.green;
    protected override void DoStartActions(StateController<ShipBase> controller) {
    }

    protected override void DoUpdateActions(StateController<ShipBase> controller) {
        controller.ObjectBase.ShipAttack.Attack();
    }

    protected override void DoEndActions(StateController<ShipBase> controller) {

    }

    protected override Transition<ShipBase>[] GetTransitions() {
        return transitions;
    }

}
