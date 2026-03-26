

using Class_FSM;
using UnityEngine;

public class MoveShipState : ShipState {
    #region Singleton
    private MoveShipState() {

    }
    private static MoveShipState instance = null;
    public static MoveShipState Instance {
        get {
            if(instance == null) {
                instance = new MoveShipState();
            }
            return instance;
        }
    }
    #endregion
    public override Color SceneGizmoColor => Color.cyan;

    private Transition<ShipBase>[] transitions = { MoveControlEndShipTransition.Instance };

    protected override void DoEndActions(StateController<ShipBase> controller) {
    }

    protected override void DoStartActions(StateController<ShipBase> controller) {

    }

    protected override void DoUpdateActions(StateController<ShipBase> controller) {
        //Attack
        controller.ObjectBase.ShipAttack.Attack();
        // move controll
        controller.ObjectBase.ShipMove.MoveControl();
    }

    protected override Transition<ShipBase>[] GetTransitions() {
        return transitions;
    }

}
