

using Class_FSM;

public class ChestChip01MoveState : ChestChip01State {
    #region Singleton
    public ChestChip01MoveState() {

    }
    private static ChestChip01MoveState instance = null;
    public static ChestChip01MoveState Instance {
        get {
            if (instance == null) {
                instance = new ChestChip01MoveState();
            }
            return instance;
        }
    }
    #endregion

    private ChestChip01Transition[] transitons = { ChestChip01OutBoundTransition.Instance };
    protected override void DoEndActions(StateController<ChestChip01Base> controller) {
    }

    protected override void DoStartActions(StateController<ChestChip01Base> controller) {
        controller.ObjectBase.ChestChip01Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<ChestChip01Base> controller) {
        controller.ObjectBase.ChestChip01Move.MoveDirect();
    }

    protected override Transition<ChestChip01Base>[] GetTransitions() {
        return transitons;
    }
}
