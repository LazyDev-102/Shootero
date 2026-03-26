using Class_FSM;
using UnityEngine;

public class ShipAppearState : ShipState {
    #region Singleton
    public ShipAppearState() {

    }
    private static ShipAppearState instance = null;
    public static ShipAppearState Instance {
        get {
            if(instance == null) {
                instance = new ShipAppearState();
            }
            return instance;
        }
    }
    #endregion

    private Transition<ShipBase>[] transitions = { ShipAppearCompleteTransition.Instance };
    public override Color SceneGizmoColor => Color.yellow;
    protected override void DoEndActions(StateController<ShipBase> controller) {
        controller.ObjectBase.ShipMove.EndMoveAppear();
    }

    protected override void DoStartActions(StateController<ShipBase> controller) {
        controller.ObjectBase.ShipMove.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<ShipBase> controller) {

    }

    protected override Transition<ShipBase>[] GetTransitions() {
        return transitions;
    }
}
