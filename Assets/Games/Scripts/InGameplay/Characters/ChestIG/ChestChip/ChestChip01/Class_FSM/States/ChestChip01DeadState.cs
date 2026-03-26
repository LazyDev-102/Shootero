

using Class_FSM;

public class ChestChip01DeadState : ChestChip01State {
    #region Singleton
    public ChestChip01DeadState() {

    }
    private static ChestChip01DeadState instance = null;
    public static ChestChip01DeadState Instance {
        get {
            if (instance == null) {
                instance = new ChestChip01DeadState();
            }
            return instance;
        }
    }
    #endregion

    protected override Transition<ChestChip01Base>[] GetTransitions() {
        return null;
    }

    protected override void DoStartActions(StateController<ChestChip01Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<ChestChip01Base> controller) {
    }

    protected override void DoEndActions(StateController<ChestChip01Base> controller) {
    }
}
